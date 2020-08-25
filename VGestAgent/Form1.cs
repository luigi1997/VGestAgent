using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Dapper;
using Ionic.Zip;
using Microsoft.Win32.TaskScheduler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VGNetGlobais;
using Task = System.Threading.Tasks.Task;

namespace VGestAgent
{
    public partial class Form1 : Form
    {
        private HttpClient httpClient = new HttpClient();
        private AmazonS3Client clientAmazon;

        private JObject cliente, versao_mais_recente, versao_disponivel;
        private string filePath, localhost, cliente_id, data_atualizacao;

        public Form1()
        {
            InitializeComponent();

            try
            {
                bool tarefaExiste = false;

                foreach (var item in TaskService.Instance.RootFolder.AllTasks)
                {
                    if (item.ToString().Equals("VGestAgentTask"))
                    {
                        tarefaExiste = true;
                        break;
                    }
                }
                if (!tarefaExiste)
                {
                    using (TaskService ts = new TaskService())
                    {
                        TaskDefinition td = ts.NewTaskFromFile("VGestAgentTask.xml");
                        ts.RootFolder.RegisterTaskDefinition("", td);
                        Application.Exit();
                        Environment.Exit(1);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Erro na criacao da tarefa.");
            }

            if (Properties.Settings.Default.update)
                checkBox_updates.Checked = true;
            else
                checkBox_updates.Checked = false;

            textBox_id.Text = Properties.Settings.Default.id;
            textBox_url.Text = Properties.Settings.Default.url;

            notifyIcon1.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon1.ContextMenuStrip.Items.Add("Sair", null, Menu_Sair_Click);

            AmazonS3Config config = new AmazonS3Config
            {
                ServiceURL = "https://fra1.digitaloceanspaces.com/"
            };

            BasicAWSCredentials credentials = new BasicAWSCredentials("YJ4M7GEE6N2BNPAXCTOR", "HSBr4I2eNWe4dGwRqgo+u+5wB84m7nSAK6ppO4mZ+5c");

            clientAmazon = new AmazonS3Client(credentials, config);

            button_get_Click(null, null);

            panel_login.Visible = true;
            panel_main.Visible = false;

            checkVersao();
        }

        private async void button_get_Click(object sender, EventArgs e)
        {
            button_get.Enabled = false;
            button_forcar_update.Visible = false;

            button_get.BackColor = System.Drawing.Color.DarkGray;
            label_id_obrigatorio.Visible = false;
            label_url_obrigatorio.Visible = false;
            textBox_nome_versao.Text = "";
            textBox_update_text.Text = "";

            //salvar id e url
            Properties.Settings.Default.id = textBox_id.Text;
            Properties.Settings.Default.url = textBox_url.Text;
            Properties.Settings.Default.Save();

            if (textBox_id.Text.Length == 0)
                label_id_obrigatorio.Visible = true;

            if (textBox_url.Text.Length == 0)
                label_url_obrigatorio.Visible = true;

            if (textBox_id.Text.Length != 0 && textBox_url.Text.Length != 0)
            {
                try
                {
                    cliente_id = textBox_id.Text;
                    localhost = textBox_url.Text;
                    if (sender != null)
                        await getVersoesDoCliente();
                }
                catch (Exception)
                {
                    textBox_nome_versao.Text = "Erro - Pedido não foi concluido com sucesso";
                    Console.WriteLine("Erro");
                }
            }

            button_get.Enabled = true;
            button_get.BackColor = System.Drawing.Color.Lime;
        }

        /**
         * Faz o pedido a api para procurar/encontrar/obter o cliente com o id inserido
         */
        private async Task<bool> getClienteAsync()
        {
            string url = "https://" + localhost + "/api/Clientes/GetCliente?id=" + cliente_id;
            try
            {
                var httpResponse = await httpClient.GetAsync(url);

                if (httpResponse.IsSuccessStatusCode)
                {
                    string jsonString = await httpResponse.Content.ReadAsStringAsync();
                    cliente = JObject.Parse(jsonString);
                    return true;
                }
                else
                {
                    textBox_nome_versao.Text = "Erro cliente com esse id não existe.";
                }
            }
            catch (Exception)
            {
                textBox_update_text.Text = "Erro na ligação à API.";
                Console.WriteLine("Erro na ligacao a API.");
            }
            return false;
        }

        private async Task getVersoesDoCliente()
        {
            button_forcar_update.Visible = false;

            if (cliente == null || cliente.Property("id").Value.ToString() != cliente_id)
                if (!await getClienteAsync())
                {
                    Console.WriteLine("Erro - Nao foi possivel obter as informaçoes do cliente.");
                    return;
                }

            textBox_update_text.Text = "";

            try
            {
                string url = "https://" + localhost + "/api/VersoesClientes/GetVersaoMaisRecenteDoCliente?id_c=" + cliente_id;
                var httpResponse = await httpClient.GetAsync(url);

                if (httpResponse.IsSuccessStatusCode && httpResponse.StatusCode == HttpStatusCode.OK)
                    versao_mais_recente = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());
                else
                {
                    versao_mais_recente = null;
                    Console.WriteLine("Erro - Nao conseguiu obter a versao mais recente.");
                }
            }
            catch (Exception)
            {
                textBox_update_text.Text = "Erro na ligação à API.";
                Console.WriteLine("Erro na ligacao a API.");
                return;
            }

            try
            {
                var url = "https://" + localhost + "/api/Versoes/GetVersao?id=" + cliente.Property("versao_atual").Value.ToString();

                var httpResponse = await httpClient.GetAsync(url);

                if (httpResponse.IsSuccessStatusCode)
                    versao_disponivel = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());
                else
                {
                    versao_disponivel = null;
                    Console.WriteLine("Erro - Nao conseguiu obter a versao disponivel.");
                }
            }
            catch (Exception)
            {
                textBox_update_text.Text = "Erro na ligação à API.";
                Console.WriteLine("Erro na ligacao a API.");
                return;
            }

            if (versao_disponivel != null)
            {
                textBox_nome_versao.Text = cliente.Property("nome").Value.ToString() + " tem a versão " + versao_disponivel.Property("tag_name").Value.ToString() + " (" + cliente.Property("versao_atual").Value.ToString() + ")";

                if (checkObject(versao_disponivel.Property("tag_name").Value.ToString()))
                {
                    button_download.Text = "Fazer Download da Versão " + versao_disponivel.Property("tag_name").Value.ToString();
                    button_download.Enabled = true;
                    button_download.Visible = true;
                    button_download.BackColor = System.Drawing.Color.Lime;
                }
            }
            else
            {
                textBox_nome_versao.Text = cliente.Property("nome").Value.ToString() + " (Não possui uma versão do software)";
                Console.WriteLine("Erro - Nao conseguiu obter informacao da versao.");
            }

            if (versao_mais_recente == null)
            {
                textBox_update_text.Text = "Cliente não tem novas versões disponiveis.";
                Console.WriteLine("Erro - Nao conseguiu obter informacao da versao.");
            }
            else
            {
                if (versao_disponivel != null)
                {
                    if (versao_mais_recente.Property("id").Value.ToString() == versao_disponivel.Property("id").Value.ToString())
                    {
                        textBox_update_text.Text = "Já possui a versao mais recente.";
                        return;
                    }
                }
                /*
                 * Caso nao tenha a versao mais recente, vai buscar a data e hora de distribuicao da versao
                 */
                try
                {
                    var url = "https://" + localhost + "/api/VersoesClientes/GetVersaoCliente?id_v=" + versao_mais_recente.Property("id").Value.ToString() + "&id_c=" + cliente.Property("id").Value.ToString();

                    var httpResponse = await httpClient.GetAsync(url);

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        data_atualizacao = JObject.Parse(await httpResponse.Content.ReadAsStringAsync()).Property("data_distribuicao").Value.ToString();

                        if (checkObject(versao_mais_recente.Property("tag_name").Value.ToString()))
                        {
                            button_forcar_update.Enabled = true;
                            button_forcar_update.Visible = true;
                            button_forcar_update.BackColor = System.Drawing.Color.Lime;
                        }

                        textBox_update_text.Text = "A versão " + versao_mais_recente.Property("tag_name").Value.ToString() + " (" +
                            versao_mais_recente.Property("id").Value.ToString() + ") vai ser atualizada no dia " + data_atualizacao.Split(' ')[0] + " às " + data_atualizacao.Split(' ')[1];

                        verificaSeJaEHoraDeAtualizar();
                    }
                    else
                    {
                        Console.WriteLine("Erro - Nao foi possivel obter informação da data de distribuiçao da versao.");
                    }
                }
                catch (Exception)
                {
                    textBox_update_text.Text = "Erro na ligação à API.";
                    Console.WriteLine("Erro na ligacao a API.");
                    return;
                }

            }
        }

        /**
         * Compara a hora prevista com a hora atual e se ja tiver passado da data é feito o update pra a nova versao
         */
        private void verificaSeJaEHoraDeAtualizar()
        {
            if (checkBox_updates.Checked)
            {
                var data = Convert.ToDateTime(data_atualizacao);
                if (data <= DateTime.Now)
                    if (checkObject(versao_mais_recente.Property("tag_name").Value.ToString()))
                        button_forcar_update_Click(null, null);
            }
        }

        /**
         * Recebe o nome da versao e verifica se existe na clound um fichero com esse nome
         */
        private async Task checkVersao()
        {
            getVersoesDoCliente();
            int segundos = DateTime.Now.Second;
            //espera um minuto
            if (segundos >= 0 && segundos < 30)
                await Task.Delay(60000 - segundos * 1000);
            else
                await Task.Delay(60000 + (60000 - segundos * 1000));
            checkVersao();
            return;
        }

        /**
         * Botao Login
         */
        private void button_login_Click(object sender, EventArgs e)
        {
            if (textBox_username.Text == "" || textBox_password.Text == "")
            {
                label_incorretos.Text = "Username e password são campos obrigatórios.";
                label_incorretos.Visible = true;
                return;
            }

            if (textBox_username.Text == "admin" && textBox_password.Text == "admin")
            {
                label_incorretos.Visible = false;
                panel_login.Visible = false;
                panel_main.Visible = true;
                textBox_username.Text = "";
                textBox_password.Text = "";
            }
            else
            {
                label_incorretos.Text = "Username e password incorretos.";
                label_incorretos.Visible = true;
            }
        }

        /**
         * Botao Forçar Update
         */
        private async void button_forcar_update_Click(object sender, EventArgs e)
        {
            JObject cliente_atualizado = (JObject)cliente.DeepClone();
            cliente_atualizado.Property("versao_atual").Value = versao_mais_recente.Property("id").Value.ToString();

            string url = "https://" + localhost + "/api/Clientes/PutCliente?id=" + cliente.Property("id").Value.ToString();

            await PutCliente(cliente_atualizado, url);

            if (!await getClienteAsync())
                return;

            await getVersoesDoCliente();

            if (checkObject(versao_disponivel.Property("tag_name").Value.ToString()))
                await DownloadObjectAsync(null);
        }

        /**
         * Atualiza a versao do cliente
         */
        private async Task<HttpResponseMessage> PutCliente<T>(T postedBody, string actionUrl)
        {
            string stringData = JsonConvert.SerializeObject(postedBody);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            return await client.PutAsync(actionUrl, contentData); ;
        }

        /**
         * Abre o forms
         */
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;

            Visible = true;
            ShowInTaskbar = true;
        }

        /**
         * Fecha a aplicacao
         */
        private void Menu_Sair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /**
         * Log Out Button
         */
        private void button_logout_ClickAsync(object senderr, EventArgs e)
        {
            panel_main.Visible = false;
            panel_login.Visible = true;
        }

        /**
         * Botao de Download
         */
        private async void button_download_Click(object sender, EventArgs e)
        {
            await DownloadObjectAsync(sender);
        }

        private bool checkObject(string versao_tag_name)
        {
            try
            {
                ListObjectsRequest request = new ListObjectsRequest();
                request.BucketName = "olifel";
                ListObjectsResponse response = clientAmazon.ListObjects(request);

                foreach (S3Object item in response.S3Objects)
                    if (item.Key.ToString().Contains(versao_tag_name))
                        return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Versao nao existe na base de dados.");
                throw;
            }
            return false;
        }

        /**
         * Download do ficheiro, faz override do ficheiro
         */
        private async Task DownloadObjectAsync(object sender)
        {
            string bucketName = "olifel";
            string keyName = "vgnet-versions/" + versao_disponivel.Property("tag_name").Value.ToString() + ".zip";

            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                string responseBody = String.Empty;
                var pathAndFileName = getPath() + "\\" + versao_disponivel.Property("tag_name").Value.ToString() + ".zip";

                progress_download.Visible = true;
                button_download.Visible = false;

                using (var response = await clientAmazon.GetObjectAsync(request))
                    await response.WriteResponseStreamToFileAsync(pathAndFileName, false, CancellationToken.None);
                unzip(pathAndFileName, getPath());

                deleteFile(pathAndFileName);

                if (sender == null)
                    sendEmail(cliente.Property("nome").Value.ToString(), versao_disponivel.Property("tag_name").Value.ToString());

                if (sender != null)
                    MessageBox.Show("Download e extração do ficheiro efetuado com sucesso", "Download", MessageBoxButtons.OK);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Erro a fazer download");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            progress_download.Visible = false;
            button_download.Visible = true;
        }

        class file : IComparable<file>
        {
            public string fileName;
            public DateTime lastWriteTime;

            public int CompareTo(file file)
            {
                if (this.lastWriteTime < file.lastWriteTime)
                {
                    return 1;
                }

                if (this.lastWriteTime > file.lastWriteTime)
                {
                    return -1;
                }

                return 0;

            }
        }

        private void deleteFile(string pathAndFileName)
        {
            string folderPath = getPath();
            List<file> files = new List<file>();

            string[] fileEntries = Directory.GetFiles(folderPath); //get all files on the save directory
            foreach (string fileName in fileEntries)
            {
                if (fileName.EndsWith(".zip"))
                {
                    file file = new file
                    {
                        fileName = fileName,
                        lastWriteTime = File.GetLastWriteTime(fileName)
                    };
                    files.Add(file);
                }
            }

            files.Sort();
            while (files.Count > 2)
            {
                File.Delete(files.Last().fileName);
                files.Remove(files.Last());
            }
        }

        /**
         * Metodo que extrai o conteudo do ficheiro zip pra uma pasta
         * 
         * zipFilePath - full path do ficheiro zip
         * folderPath - caminho da pasta onde irá ser guardado o conteudo do ficheiro zip
         */
        private void unzip(string zipFilePath, string folderPath)
        {
            using (ZipFile zip = ZipFile.Read(zipFilePath))
            {
                foreach (ZipEntry entry in zip)
                    entry.Extract(folderPath, ExtractExistingFileAction.OverwriteSilently);
            }
        }

        /**
         * Metodo que vai buscar o caminho onde vao ser guardados os dados da nova versao do erp
         */
        private string getPath()
        {
            using (IDbConnection vgtabCnn = new SqlConnection(VGGlobais.VGTabCnnString()))
            {
                string path = vgtabCnn.Query<string>("select * from _SrcPath").FirstOrDefault();
                return path;
            }
        }

        private void checkBox_updates_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_updates.Checked)
                Properties.Settings.Default.update = true;
            else
                Properties.Settings.Default.update = false;
            Properties.Settings.Default.Save();
        }

        private void login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_login_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        #region Delete From Bucket

        private void DeleteObjectAsync()
        {
            string bucketName = "olifel";
            string keyName = "vgnet-versions\\V2020.05.04.zip";
            try
            {
                DeleteObjectRequest request = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                clientAmazon.DeleteObject(request);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Erro a fazer download");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region Upload

        /**
         * Botao de Upload
         */
        private async void button_upload_Click(object sender, EventArgs e)
        {
            await UploadObjectAsync();
        }

        /**
         * Select file to upload
         */
        private void button_select_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                DefaultExt = "zip",
                Filter = "zip files (*.zip)|*.zip",
                CheckFileExists = true,
                CheckPathExists = true,

                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                label_file_selected.Text = filePath;
            }

            if (filePath.Length > 0 && filePath.EndsWith(".zip"))
            {
                button_upload.Enabled = true;
                button_upload.BackColor = System.Drawing.Color.Lime;
            }
            else
            {
                button_upload.Enabled = false;
                button_upload.BackColor = System.Drawing.Color.DarkGray;
            }
        }

        /**
        * Upload do ficheiro selecionado, faz override do ficheiro na base de dados se tiver o mesmo nome
        */
        private async Task UploadObjectAsync()
        {
            try
            {
                if (filePath.Length > 0)
                {
                    string key = "vgnet-versions/" + filePath.Split('\\')[filePath.Split('\\').Length - 1];
                    var fileTransferUtility = new TransferUtility(clientAmazon);

                    var uploadRequest =
                        new TransferUtilityUploadRequest
                        {
                            BucketName = "olifel",
                            FilePath = filePath,
                            Key = key
                        };
                    progress_upload.Visible = true;
                    button_upload.Visible = false;
                    await fileTransferUtility.UploadAsync(uploadRequest);

                    MessageBox.Show("Upload do ficheiro efetuado com sucesso", "Upload", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {
                var result = MessageBox.Show("Erro - ficheiro não existe", "Upload", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                    await UploadObjectAsync();
            }
            progress_upload.Visible = false;
            button_upload.Visible = true;
            filePath = String.Empty;
            label_file_selected.Text = "Nenhum ficheiro selecionado";
            button_upload.Enabled = false;
            button_upload.BackColor = System.Drawing.Color.DarkGray;
        }

        #endregion

        #region Email

        private void sendEmail(string nomeCliente, string versao)
        {
            /*
            msg = new MailMessage { From = new MailAddress("versionupdateolifel2020@gmail.com", "Titulo", Encoding.UTF8) };
            msg.To.Add(new MailAddress("versionupdateolifel2020@gmail.com"));
            msg.Subject = "Assunto";
            msg.Body = "Corpo do email";
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            */
            //VGGlobais.SendEmail("Assunto", "versionupdateolifel2020@gmail.com", "", "", "Corpo", "Assinatura", null, null, null);
            
            NetworkCredential login = new NetworkCredential("versionupdateolifel2020", "xakleyhirwabydsl");
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = login
            };

            MailMessage msg = new MailMessage
            {
                From = new MailAddress("versionupdateolifel2020@gmail.com", "Atualização " + nomeCliente, Encoding.UTF8),
                Subject = "Update de Versao",
                Body = nomeCliente + " atualizou para a versão: " + versao,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true
            };
            msg.To.Add(new MailAddress("versionupdateolifel2020@gmail.com"));

            smtpClient.Send(msg);
        }
        #endregion
    }
}
