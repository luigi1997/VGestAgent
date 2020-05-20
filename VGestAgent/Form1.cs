using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Dapper;
using Ionic.Zip;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VGNetGlobais;

namespace VGestAgent
{
    public partial class Form1 : Form
    {
        private class Cliente
        {
            public string Id { get; set; }
            public string Nome { get; set; }
            public int Grupo { get; set; }
            public string Versao_atual { get; set; }
            public DateTime Data_limite_atualizacoes { get; set; }
            public DateTime Data_limite_funcionamento { get; set; }
            public bool Contrato_assistencia { get; set; }
            public DateTime Data_criacao { get; set; }
            public DateTime Data_ultima_alteracao { get; set; }
            public string Classe { get; set; }
            public string Terceiro { get; set; }
            public int Numero_postos { get; set; }

            public Cliente Clone()
            {
                return new Cliente { Id = this.Id, Nome = this.Nome, Grupo = this.Grupo, Versao_atual = this.Versao_atual, Data_limite_atualizacoes = this.Data_limite_atualizacoes, Data_limite_funcionamento = this.Data_limite_funcionamento, Contrato_assistencia = this.Contrato_assistencia, Data_criacao = this.Data_criacao, Data_ultima_alteracao = this.Data_ultima_alteracao, Classe = this.Classe, Terceiro = this.Terceiro, Numero_postos = this.Numero_postos };
            }

            public override string ToString()
            {
                return String.Format("Id: {0}\nNome: {1}\nGrupo: {2}\nVersao_atual: {3}\nData_limite_atualizacoes: {4}" +
                    "\nData_limite_funcionamento: {5}\nContrato_assistencia: {6}\nData_criacao: {7}\nData_ultima_alteracao: {8}" +
                    "\nClasse: {9}\nTerceiro: {10}\nNumero_postos: {11}",
                    Id, Nome, Grupo, Versao_atual, Data_limite_atualizacoes, Data_limite_funcionamento, Contrato_assistencia,
                    Data_criacao, Data_ultima_alteracao, Classe, Terceiro, Numero_postos);
            }
        }

        private RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        private string filePath = String.Empty;
        private JObject versao_disponivel;
        public Form1()
        {
            InitializeComponent();
            if (registryKey.GetValue("VGestAgent") != null)
            {
                checkBox_start.Checked = true;
            }
            else
            {
                checkBox_start.Checked = false;
            }
            textBox_id.Text = "d0fadd8d-3b8b-4331-9dac-884953462b41";
            textBox_url.Text = "localhost:44370";
            textBox_username.Text = "admin";
            textBox_password.Text = "admin";
            notifyIcon1.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon1.ContextMenuStrip.Items.Add("Sair", null, this.Menu_Sair_Click);

            //amazon
            AmazonS3Config config = new AmazonS3Config
            {
                ServiceURL = "https://fra1.digitaloceanspaces.com/"
            };

            var credentials = new BasicAWSCredentials("YJ4M7GEE6N2BNPAXCTOR", "HSBr4I2eNWe4dGwRqgo+u+5wB84m7nSAK6ppO4mZ+5c");

            ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object serder, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

            client = new AmazonS3Client(credentials, config);
        }

        private async void button_get_Click(object sender, EventArgs e)
        {
            button_get.Enabled = false;
            this.button_get.BackColor = System.Drawing.Color.DarkGray;
            textBox_update_text.Text = "";


            if (textBox_id.Text.Length == 0)
            {
                label_id_obrigatorio.Visible = true;
            }
            else
            {
                label_id_obrigatorio.Visible = false;
            }

            if (textBox_url.Text.Length == 0)
            {
                label_url_obrigatorio.Visible = true;
            }
            else
            {
                label_url_obrigatorio.Visible = false;
            }

            if (textBox_id.Text.Length == 0 || textBox_url.Text.Length == 0)
            {
                button_get.Enabled = true;
                this.button_get.BackColor = System.Drawing.Color.Lime;
                return;
            }

            try
            {
                await getVersoesDoCliente(httpClient);
                button_get.Enabled = true;
                this.button_get.BackColor = System.Drawing.Color.Lime;
            }
            catch (Exception)
            {
                Console.WriteLine("Erro");
                button_get.Enabled = true;
                this.button_get.BackColor = System.Drawing.Color.Lime;
            }

        }

        private async Task getCliente(HttpClient httpClient)
        {
            string url = "https://" + textBox_url.Text + "/api/Clientes/GetCliente?id=" + textBox_id.Text;

            var httpResponse = await httpClient.GetAsync(url);

            var jsonString = await httpResponse.Content.ReadAsStringAsync();

            JObject objeto = JObject.Parse(jsonString.ToString());
            if (objeto.GetValue("nome") != null)
            {
                cliente = new Cliente();
                cliente.Id = objeto.GetValue("id").ToString();
                cliente.Nome = objeto.GetValue("nome").ToString();
                cliente.Grupo = Int32.Parse(objeto.GetValue("grupo").ToString());
                cliente.Versao_atual = objeto.GetValue("versao_atual").ToString();
                cliente.Data_limite_atualizacoes = DateTime.Parse(objeto.GetValue("data_limite_atualizacoes").ToString());
                cliente.Data_limite_funcionamento = DateTime.Parse(objeto.GetValue("data_limite_funcionamento").ToString());
                cliente.Contrato_assistencia = bool.Parse(objeto.GetValue("contrato_assistencia").ToString());
                cliente.Data_criacao = DateTime.Parse(objeto.GetValue("data_criacao").ToString());
                cliente.Data_ultima_alteracao = DateTime.Parse(objeto.GetValue("data_ultima_alteracao").ToString());
                cliente.Classe = objeto.GetValue("classe").ToString();
                cliente.Terceiro = objeto.GetValue("terceiro").ToString();
                cliente.Numero_postos = Int32.Parse(objeto.GetValue("numero_postos").ToString());

                textBox_nome_versao.ForeColor = System.Drawing.Color.Black;
                textBox_nome_versao.Text = cliente.Nome + ", versão atual é " + cliente.Versao_atual;
                //textBox4.Text = cliente.ToString();
            }
            else
            {
                textBox_nome_versao.ForeColor = System.Drawing.Color.DarkGray;
                textBox_nome_versao.Text = "Erro cliente com esse id não existe.";
            }
        }

        private async Task getVersoesDoCliente(HttpClient httpClient)
        {
            button_forcar_update.Visible = false;
            button_download.Visible = false;

            await getCliente(httpClient);

            if (cliente.Versao_atual == null)
            {
                return;
            }

            textBox_update_text.Text = "";

            string url = "https://" + textBox_url.Text + "/api/VersoesClientes/GetVersaoMaisRecenteDoCliente?id_c=" + textBox_id.Text;

            var httpResponse = await httpClient.GetAsync(url);

            var jsonString = await httpResponse.Content.ReadAsStringAsync();

            jObject_da_versao_mais_recente = JObject.Parse(jsonString);

            if (cliente.Versao_atual.Equals(jObject_da_versao_mais_recente.GetValue("id")))
            {
                textBox_update_text.Text = "Já possui a versão mais recente.";
                return;
            }

            url = "https://" + textBox_url.Text + "/api/VersoesClientes/GetVersoesDoCliente?id_c=" + textBox_id.Text;

            httpResponse = await httpClient.GetAsync(url);

            jsonString = await httpResponse.Content.ReadAsStringAsync();

            JArray jArray_versoes_distribuidas = JArray.Parse(jsonString.ToString());

            foreach (var objeto in jArray_versoes_distribuidas)
            {
                JObject jObject = JObject.Parse(objeto.ToString());
                if (jObject.GetValue("versao_ID").Equals(jObject_da_versao_mais_recente.GetValue("id")))
                {
                    if (cliente.Versao_atual != jObject_da_versao_mais_recente.GetValue("id").ToString())
                    {
                        button_forcar_update.Visible = true;

                        textBox_update_text.Text += "A versão " + jObject_da_versao_mais_recente.GetValue("tag_name");
                        textBox_update_text.Text += " (" + jObject.GetValue("versao_ID") + ") será atualizada a ";
                        textBox_update_text.Text += jObject.GetValue("data_distribuicao").ToString().Split(' ')[0] + " às ";
                        textBox_update_text.Text += jObject.GetValue("data_distribuicao").ToString().Split(' ')[1];
                    }
                    else
                    {
                        textBox_update_text.Text += "Já possui a versão mais recente " + jObject_da_versao_mais_recente.GetValue("tag_name");
                        textBox_update_text.Text += " (" + jObject.GetValue("versao_ID") + ").";
                    }
                    button_download.Visible = true;
                    button_download.Text = "Fazer Download da Versão " + versao_disponivel;
                }
            }

            url = "https://" + textBox_url.Text + "/api/Versoes/GetVersao?id=" + cliente.Versao_atual;

            httpResponse = await httpClient.GetAsync(url);

            jsonString = await httpResponse.Content.ReadAsStringAsync();

            versao_disponivel = JObject.Parse(jsonString);

            textBox_nome_versao.Text = cliente.Nome + ", versão atual é " + versao_disponivel.GetValue("tag_name") + " (" + cliente.Versao_atual + ")";
            button_download.Text = "Fazer Download da Versão " + versao_disponivel.GetValue("tag_name");

            if (checkObject())
            {
                button_download.Enabled = true;
                this.button_download.BackColor = System.Drawing.Color.Lime;
            }
            else
            {
                button_download.Enabled = false;
                this.button_download.BackColor = System.Drawing.Color.DarkGray;
            }
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
            Cliente cliente_atualizado = (Cliente)cliente.Clone();
            cliente_atualizado.Versao_atual = jObject_da_versao_mais_recente.GetValue("id").ToString();

            string url = "https://" + textBox_url.Text + "/api/Clientes/PutCliente?id=" + cliente.Id + "&hora=";

            await PutCliente<Cliente>(cliente_atualizado, url);

            await getVersoesDoCliente(httpClient);
        }

        /**
         * Atualiza a versao do cliente
         */
        public async Task<HttpResponseMessage> PutCliente<T>(T postedBody, string actionUrl)
        {
            string stringData = JsonConvert.SerializeObject(postedBody);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            return await client.PutAsync(actionUrl, contentData); ;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Menu_Sair_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void checkBox_start_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_start.Checked)
            {
                registryKey.SetValue("VGestAgent", Application.ExecutablePath);
            }
            else
            {
                registryKey.DeleteValue("VGestAgent");
            }
        }

        /**
         * Log Out Button
         */
        private void button_logout_ClickAsync(object senderr, EventArgs e)
        {
            label_incorretos.Visible = false;
            panel_main.Visible = false;
            panel_login.Visible = true;
        }

        /**
         * Botao de Download
         */
        private async void button_download_Click(object sender, EventArgs e)
        {
            await DownloadObjectAsync();
        }

        private async void button_upload_Click(object sender, EventArgs e)
        {
            await UploadObjectAsync();
        }

        private async Task ListBucketsAsync()
        {
            try
            {
                ListBucketsResponse response = await client.ListBucketsAsync();

                Console.Write("Buckets: ");

                foreach (S3Bucket item in response.Buckets)
                {
                    Console.Write(item.BucketName + "; ");
                }
                Console.WriteLine();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool checkObject()
        {
            try
            {
                ListObjectsRequest request = new ListObjectsRequest();
                request.BucketName = "olifel";
                ListObjectsResponse response = client.ListObjects(request);

                foreach (S3Object item in response.S3Objects)
                {
                    if (item.Key.ToString().Contains(versao_disponivel.GetValue("tag_name").ToString()))
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        private void ListObjects()
        {
            try
            {
                ListObjectsRequest request = new ListObjectsRequest();
                request.BucketName = "olifel";
                ListObjectsResponse response = client.ListObjects(request);

                Console.WriteLine("Objects: ");

                foreach (S3Object item in response.S3Objects)
                {
                    Console.WriteLine("Key: " + item.Key + "; ");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task DownloadObjectAsync()
        {
            string bucketName = "olifel";
            string keyName = "vgnet-versions/" + versao_disponivel.GetValue("tag_name").ToString() + ".zip";

            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                string responseBody = String.Empty;
                var pathAndFileName = getPath() + "\\" + versao_disponivel.GetValue("tag_name").ToString() + ".zip";
                using (var response = await client.GetObjectAsync(request))
                    await response.WriteResponseStreamToFileAsync(pathAndFileName, true, CancellationToken.None);
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

        private async Task UploadObjectAsync()
        {
            try
            {
                if (filePath.Length > 0)
                {
                    string key = "vgnet-versions/" + filePath.Split('\\')[filePath.Split('\\').Length - 1];
                    var fileTransferUtility = new TransferUtility(client);

                    var uploadRequest =
                        new TransferUtilityUploadRequest
                        {
                            BucketName = "olifel",
                            FilePath = filePath,
                            Key = key
                        };
                    Console.WriteLine("Upload em progresso");
                    fileTransferUtility.Upload(uploadRequest);
                    Console.WriteLine("Fim de upload");
                    MessageBox.Show("Ficheiro carregado com sucesso", "Upload", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {
                var result = MessageBox.Show("Erro ficheiro não existe", "Upload", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                    await UploadObjectAsync();
            }
        }

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

                client.DeleteObject(request);
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
            if (filePath.Length > 0)
            {
                button_upload.Enabled = true;
                this.button_upload.BackColor = System.Drawing.Color.Lime;
            }
            else
            {
                button_upload.Enabled = false;
                this.button_upload.BackColor = System.Drawing.Color.DarkGray;
            }
        }

        private void button_unzip_Click(object sender, EventArgs e)
        {
            string path = getPath();
            unzip(path + "\\V2020.05.06.zip", path);
        }

        private void button_mail_Click(object sender, EventArgs e)
        {
            sendEmail();
        }
        private void unzip(string zipFilePath, string folderPath)
        {
            using (ZipFile zip = ZipFile.Read(zipFilePath))
            {
                foreach (ZipEntry entry in zip)
                {
                    entry.Extract(folderPath, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

        private string getPath()
        {
            using (IDbConnection vgtabCnn = new SqlConnection(VGGlobais.VGTabCnnString()))
            {
                string path = vgtabCnn.Query<string>("select * from _SrcPath").FirstOrDefault();
                return path;
            }
        }

        private void sendEmail()
        {
            NetworkCredential login;
            SmtpClient client;
            MailMessage msg;

            login = new NetworkCredential("versionupdateolifel2020", "fkwqfzfzkqcspwkr");
            client = new SmtpClient("smtp.gmail.com");
            client.Port = Convert.ToInt32("587");
            client.EnableSsl = true;
            client.Credentials = login;

            msg = new MailMessage { From = new MailAddress("versionupdateolifel2020@gmail.com", "Titulo", Encoding.UTF8) };
            msg.To.Add(new MailAddress("8160182@estg.ipp.pt"));
            msg.Subject = "Assunto";
            msg.Body = "Corpo do email";
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;

            client.Send(msg);
        }

    }
}
