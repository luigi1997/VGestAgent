using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//xis
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
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
            textBox1.Text = "d0fadd8d-3b8b-4331-9dac-884953462b41";
            textBox3.Text = "localhost:44370";
            textBox5.Text = "admin";
            textBox6.Text = "admin";
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

        private async void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            this.button2.BackColor = System.Drawing.Color.DarkGray;
            textBox4.Text = "";


            if (textBox1.Text.Length == 0)
            {
                label4.Visible = true;
            }
            else
            {
                label4.Visible = false;
            }

            if (textBox3.Text.Length == 0)
            {
                label5.Visible = true;
            }
            else
            {
                label5.Visible = false;
            }

            if (textBox1.Text.Length == 0 || textBox3.Text.Length == 0)
            {
                button2.Enabled = true;
                this.button2.BackColor = System.Drawing.Color.Lime;
                return;
            }

            try
            {
                await getVersoesDoCliente(httpClient);
                button2.Enabled = true;
                this.button2.BackColor = System.Drawing.Color.Lime;
            }
            catch (Exception)
            {
                Console.WriteLine("Erro");
                button2.Enabled = true;
                this.button2.BackColor = System.Drawing.Color.Lime;
            }

        }

        private async Task getCliente(HttpClient httpClient)
        {
            string url = "https://" + textBox3.Text + "/api/Clientes/GetCliente?id=" + textBox1.Text;

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

                textBox2.ForeColor = System.Drawing.Color.Black;
                textBox2.Text = cliente.Nome + ", versão atual é " + cliente.Versao_atual;
                //textBox4.Text = cliente.ToString();
            }
            else
            {
                textBox2.ForeColor = System.Drawing.Color.DarkGray;
                textBox2.Text = "Erro cliente com esse id não existe.";
            }
        }

        private async Task getVersoesDoCliente(HttpClient httpClient)
        {
            button4.Visible = false;
            button7.Visible = false;

            await getCliente(httpClient);

            if (cliente.Versao_atual == null)
            {
                return;
            }

            textBox4.Text = "";

            string url = "https://" + textBox3.Text + "/api/VersoesClientes/GetVersaoMaisRecenteDoCliente?id_c=" + textBox1.Text;

            var httpResponse = await httpClient.GetAsync(url);

            var jsonString = await httpResponse.Content.ReadAsStringAsync();

            jObject_da_versao_mais_recente = JObject.Parse(jsonString);

            if (cliente.Versao_atual.Equals(jObject_da_versao_mais_recente.GetValue("id")))
            {
                textBox4.Text = "Já possui a versão mais recente.";
                return;
            }

            url = "https://" + textBox3.Text + "/api/VersoesClientes/GetVersoesDoCliente?id_c=" + textBox1.Text;

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
                        button4.Visible = true;

                        textBox4.Text += "A versão " + jObject_da_versao_mais_recente.GetValue("tag_name");
                        textBox4.Text += " (" + jObject.GetValue("versao_ID") + ") será atualizada a ";
                        textBox4.Text += jObject.GetValue("data_distribuicao").ToString().Split(' ')[0] + " às ";
                        textBox4.Text += jObject.GetValue("data_distribuicao").ToString().Split(' ')[1];
                    }
                    else
                    {
                        textBox4.Text += "Já possui a versão mais recente " + jObject_da_versao_mais_recente.GetValue("tag_name");
                        textBox4.Text += " (" + jObject.GetValue("versao_ID") + ").";
                    }
                    button7.Visible = true;
                    button7.Text = "Fazer Download da Versão " + versao_disponivel;
                }
            }

            url = "https://" + textBox3.Text + "/api/Versoes/GetVersao?id=" + cliente.Versao_atual;

            httpResponse = await httpClient.GetAsync(url);

            jsonString = await httpResponse.Content.ReadAsStringAsync();

            versao_disponivel = JObject.Parse(jsonString);

            textBox2.Text = cliente.Nome + ", versão atual é " + versao_disponivel.GetValue("tag_name") + " (" + cliente.Versao_atual + ")";
            button7.Text = "Fazer Download da Versão " + versao_disponivel.GetValue("tag_name");

            if (checkObject())
            {
                button7.Enabled = true;
                this.button7.BackColor = System.Drawing.Color.Lime;
            }
            else
            {
                button7.Enabled = false;
                this.button7.BackColor = System.Drawing.Color.DarkGray;
            }
        }

        /**
         * Botao Login
         */
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "" || textBox6.Text == "")
            {
                label8.Text = "Username e password são campos obrigatórios.";
                label8.Visible = true;
                return;
            }

            if (textBox5.Text == "admin" && textBox6.Text == "admin")
            {
                label8.Visible = false;
                panel2.Visible = false;
                panel1.Visible = true;
            }
            else
            {
                label8.Text = "Username e password incorretos.";
                label8.Visible = true;
            }
        }

        /**
         * Botao Forçar Update
         */
        private async void button4_Click(object sender, EventArgs e)
        {
            Cliente cliente_atualizado = (Cliente)cliente.Clone();
            cliente_atualizado.Versao_atual = jObject_da_versao_mais_recente.GetValue("id").ToString();

            string url = "https://" + textBox3.Text + "/api/Clientes/PutCliente?id=" + cliente.Id + "&hora=";

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
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
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
        private void button5_ClickAsync(object senderr, EventArgs e)
        {
            label8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = true;
        }

        /**
         * Botao de Download
         */
        private async void button7_Click(object sender, EventArgs e)
        {
            await DownloadObjectAsync();
        }

        private async void button6_Click(object sender, EventArgs e)
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
                var pathAndFileName = $"C:\\OlifelVersoes\\{keyName}";
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
                    await fileTransferUtility.UploadAsync(filePath, "olifel", key);
                }
            }
            catch (Exception)
            {
                throw;
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

        private void button8_Click(object sender, EventArgs e)
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
                Console.WriteLine(filePath);
            }
            if (filePath.Length > 0)
            {
                button6.Enabled = true;
                this.button6.BackColor = System.Drawing.Color.Lime;
            }
            else
            {
                button6.Enabled = false;
                this.button6.BackColor = System.Drawing.Color.DarkGray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            unzip("C:\\OlifelVersoes\\vgnet-versions\\V2020.05.06.zip", "C:\\OlifelVersoes\\vgnet-versions");
        }

        private void unzip(string zipFilePath, string folderPath)
        {
            ZipFile.ExtractToDirectory(zipFilePath, folderPath);
        }
    }
}
