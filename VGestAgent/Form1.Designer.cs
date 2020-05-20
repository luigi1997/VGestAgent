using Amazon.S3;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace VGestAgent
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            Hide();
            /*
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            */
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox_id = new System.Windows.Forms.TextBox();
            this.label_id = new System.Windows.Forms.Label();
            this.textBox_nome_versao = new System.Windows.Forms.TextBox();
            this.label_nome_versao = new System.Windows.Forms.Label();
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.label_url = new System.Windows.Forms.Label();
            this.label_url_obrigatorio = new System.Windows.Forms.Label();
            this.label_id_obrigatorio = new System.Windows.Forms.Label();
            this.button_get = new System.Windows.Forms.Button();
            this.textBox_update_text = new System.Windows.Forms.TextBox();
            this.panel_main = new System.Windows.Forms.Panel();
            this.button_mail = new System.Windows.Forms.Button();
            this.button_unzip = new System.Windows.Forms.Button();
            this.button_select = new System.Windows.Forms.Button();
            this.button_upload = new System.Windows.Forms.Button();
            this.button_download = new System.Windows.Forms.Button();
            this.checkBox_start = new System.Windows.Forms.CheckBox();
            this.button_logout = new System.Windows.Forms.Button();
            this.button_forcar_update = new System.Windows.Forms.Button();
            this.panel_login = new System.Windows.Forms.Panel();
            this.label_incorretos = new System.Windows.Forms.Label();
            this.button_login = new System.Windows.Forms.Button();
            this.label_password = new System.Windows.Forms.Label();
            this.label_username = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label_file_selected = new System.Windows.Forms.Label();
            this.progress_upload = new System.Windows.Forms.ProgressBar();
            this.panel_main.SuspendLayout();
            this.panel_login.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_id
            // 
            this.textBox_id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_id.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_id.Location = new System.Drawing.Point(12, 49);
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.Size = new System.Drawing.Size(692, 32);
            this.textBox_id.TabIndex = 11;
            // 
            // label_id
            // 
            this.label_id.AutoSize = true;
            this.label_id.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_id.Location = new System.Drawing.Point(12, 26);
            this.label_id.Name = "label_id";
            this.label_id.Size = new System.Drawing.Size(89, 19);
            this.label_id.TabIndex = 12;
            this.label_id.Text = "ID do Cliente";
            // 
            // textBox_nome_versao
            // 
            this.textBox_nome_versao.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_nome_versao.ForeColor = System.Drawing.Color.Black;
            this.textBox_nome_versao.Location = new System.Drawing.Point(12, 128);
            this.textBox_nome_versao.Name = "textBox_nome_versao";
            this.textBox_nome_versao.ReadOnly = true;
            this.textBox_nome_versao.Size = new System.Drawing.Size(776, 32);
            this.textBox_nome_versao.TabIndex = 13;
            // 
            // label_nome_versao
            // 
            this.label_nome_versao.AutoSize = true;
            this.label_nome_versao.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_nome_versao.Location = new System.Drawing.Point(12, 105);
            this.label_nome_versao.Name = "label_nome_versao";
            this.label_nome_versao.Size = new System.Drawing.Size(167, 19);
            this.label_nome_versao.TabIndex = 14;
            this.label_nome_versao.Text = "Nome e versão do Cliente";
            // 
            // textBox_url
            // 
            this.textBox_url.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_url.ForeColor = System.Drawing.Color.Black;
            this.textBox_url.Location = new System.Drawing.Point(12, 391);
            this.textBox_url.Name = "textBox_url";
            this.textBox_url.Size = new System.Drawing.Size(776, 32);
            this.textBox_url.TabIndex = 16;
            // 
            // label_url
            // 
            this.label_url.AutoSize = true;
            this.label_url.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_url.Location = new System.Drawing.Point(12, 368);
            this.label_url.Name = "label_url";
            this.label_url.Size = new System.Drawing.Size(71, 19);
            this.label_url.TabIndex = 17;
            this.label_url.Text = "Url da API";
            // 
            // label_url_obrigatorio
            // 
            this.label_url_obrigatorio.AutoSize = true;
            this.label_url_obrigatorio.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_url_obrigatorio.ForeColor = System.Drawing.Color.Red;
            this.label_url_obrigatorio.Location = new System.Drawing.Point(12, 426);
            this.label_url_obrigatorio.Name = "label_url_obrigatorio";
            this.label_url_obrigatorio.Size = new System.Drawing.Size(223, 19);
            this.label_url_obrigatorio.TabIndex = 18;
            this.label_url_obrigatorio.Text = "Url da API é um campo obrigatório";
            this.label_url_obrigatorio.Visible = false;
            // 
            // label_id_obrigatorio
            // 
            this.label_id_obrigatorio.AutoSize = true;
            this.label_id_obrigatorio.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_id_obrigatorio.ForeColor = System.Drawing.Color.Red;
            this.label_id_obrigatorio.Location = new System.Drawing.Point(12, 84);
            this.label_id_obrigatorio.Name = "label_id_obrigatorio";
            this.label_id_obrigatorio.Size = new System.Drawing.Size(241, 19);
            this.label_id_obrigatorio.TabIndex = 19;
            this.label_id_obrigatorio.Text = "ID do Cliente é um campo obrigatório";
            this.label_id_obrigatorio.Visible = false;
            // 
            // button_get
            // 
            this.button_get.BackColor = System.Drawing.Color.Lime;
            this.button_get.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_get.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_get.Image = global::VGestAgent.Properties.Resources.Ok_icon__2_;
            this.button_get.Location = new System.Drawing.Point(710, 46);
            this.button_get.Name = "button_get";
            this.button_get.Size = new System.Drawing.Size(78, 38);
            this.button_get.TabIndex = 20;
            this.button_get.UseVisualStyleBackColor = false;
            this.button_get.Click += new System.EventHandler(this.button_get_Click);
            // 
            // textBox_update_text
            // 
            this.textBox_update_text.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_update_text.ForeColor = System.Drawing.Color.Black;
            this.textBox_update_text.Location = new System.Drawing.Point(12, 166);
            this.textBox_update_text.Multiline = true;
            this.textBox_update_text.Name = "textBox_update_text";
            this.textBox_update_text.ReadOnly = true;
            this.textBox_update_text.Size = new System.Drawing.Size(776, 104);
            this.textBox_update_text.TabIndex = 21;
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.progress_upload);
            this.panel_main.Controls.Add(this.label_file_selected);
            this.panel_main.Controls.Add(this.button_mail);
            this.panel_main.Controls.Add(this.button_unzip);
            this.panel_main.Controls.Add(this.button_select);
            this.panel_main.Controls.Add(this.button_upload);
            this.panel_main.Controls.Add(this.button_download);
            this.panel_main.Controls.Add(this.checkBox_start);
            this.panel_main.Controls.Add(this.button_logout);
            this.panel_main.Controls.Add(this.button_forcar_update);
            this.panel_main.Controls.Add(this.textBox_update_text);
            this.panel_main.Controls.Add(this.button_get);
            this.panel_main.Controls.Add(this.label_url_obrigatorio);
            this.panel_main.Controls.Add(this.label_id_obrigatorio);
            this.panel_main.Controls.Add(this.label_url);
            this.panel_main.Controls.Add(this.textBox_url);
            this.panel_main.Controls.Add(this.label_nome_versao);
            this.panel_main.Controls.Add(this.textBox_nome_versao);
            this.panel_main.Controls.Add(this.label_id);
            this.panel_main.Controls.Add(this.textBox_id);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 0);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(800, 454);
            this.panel_main.TabIndex = 11;
            this.panel_main.Visible = false;
            // 
            // button_mail
            // 
            this.button_mail.Location = new System.Drawing.Point(713, 333);
            this.button_mail.Name = "button_mail";
            this.button_mail.Size = new System.Drawing.Size(75, 27);
            this.button_mail.TabIndex = 29;
            this.button_mail.Text = "mail";
            this.button_mail.UseVisualStyleBackColor = true;
            this.button_mail.Click += new System.EventHandler(this.button_mail_Click);
            // 
            // button_unzip
            // 
            this.button_unzip.Location = new System.Drawing.Point(568, 333);
            this.button_unzip.Name = "button_unzip";
            this.button_unzip.Size = new System.Drawing.Size(75, 27);
            this.button_unzip.TabIndex = 28;
            this.button_unzip.Text = "unzip";
            this.button_unzip.UseVisualStyleBackColor = true;
            this.button_unzip.Click += new System.EventHandler(this.button_unzip_Click);
            // 
            // button_select
            // 
            this.button_select.BackColor = System.Drawing.Color.Lime;
            this.button_select.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_select.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_select.Location = new System.Drawing.Point(362, 276);
            this.button_select.Name = "button_select";
            this.button_select.Size = new System.Drawing.Size(200, 39);
            this.button_select.TabIndex = 27;
            this.button_select.Text = "Selecionar Ficheiro";
            this.button_select.UseVisualStyleBackColor = false;
            this.button_select.Click += new System.EventHandler(this.button_select_Click);
            // 
            // button_upload
            // 
            this.button_upload.BackColor = System.Drawing.Color.DarkGray;
            this.button_upload.Enabled = false;
            this.button_upload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_upload.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_upload.Location = new System.Drawing.Point(362, 321);
            this.button_upload.Name = "button_upload";
            this.button_upload.Size = new System.Drawing.Size(200, 39);
            this.button_upload.TabIndex = 26;
            this.button_upload.Text = "Upload";
            this.button_upload.UseVisualStyleBackColor = false;
            this.button_upload.Click += new System.EventHandler(this.button_upload_Click);
            // 
            // button_download
            // 
            this.button_download.BackColor = System.Drawing.Color.DarkGray;
            this.button_download.Enabled = false;
            this.button_download.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_download.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_download.Location = new System.Drawing.Point(185, 276);
            this.button_download.Name = "button_download";
            this.button_download.Size = new System.Drawing.Size(171, 84);
            this.button_download.TabIndex = 25;
            this.button_download.Text = "Fazer Download da Versão";
            this.button_download.UseVisualStyleBackColor = false;
            this.button_download.Visible = false;
            this.button_download.Click += new System.EventHandler(this.button_download_Click);
            // 
            // checkBox_start
            // 
            this.checkBox_start.AutoSize = true;
            this.checkBox_start.Location = new System.Drawing.Point(362, 7);
            this.checkBox_start.Name = "checkBox_start";
            this.checkBox_start.Size = new System.Drawing.Size(322, 21);
            this.checkBox_start.TabIndex = 24;
            this.checkBox_start.Text = "Iniciar a aplicação quando inicia o computador";
            this.checkBox_start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_start.UseVisualStyleBackColor = true;
            this.checkBox_start.CheckedChanged += new System.EventHandler(this.checkBox_start_CheckedChanged);
            // 
            // button_logout
            // 
            this.button_logout.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.button_logout.Location = new System.Drawing.Point(689, 0);
            this.button_logout.Name = "button_logout";
            this.button_logout.Size = new System.Drawing.Size(110, 33);
            this.button_logout.TabIndex = 23;
            this.button_logout.Text = "Log Out";
            this.button_logout.UseVisualStyleBackColor = true;
            this.button_logout.Click += new System.EventHandler(this.button_logout_ClickAsync);
            // 
            // button_forcar_update
            // 
            this.button_forcar_update.BackColor = System.Drawing.Color.Lime;
            this.button_forcar_update.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_forcar_update.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_forcar_update.Location = new System.Drawing.Point(18, 276);
            this.button_forcar_update.Name = "button_forcar_update";
            this.button_forcar_update.Size = new System.Drawing.Size(161, 84);
            this.button_forcar_update.TabIndex = 22;
            this.button_forcar_update.Text = "Forçar Update Agora";
            this.button_forcar_update.UseVisualStyleBackColor = false;
            this.button_forcar_update.Visible = false;
            this.button_forcar_update.Click += new System.EventHandler(this.button_forcar_update_Click);
            // 
            // panel_login
            // 
            this.panel_login.BackColor = System.Drawing.SystemColors.Control;
            this.panel_login.Controls.Add(this.label_incorretos);
            this.panel_login.Controls.Add(this.button_login);
            this.panel_login.Controls.Add(this.label_password);
            this.panel_login.Controls.Add(this.label_username);
            this.panel_login.Controls.Add(this.textBox_password);
            this.panel_login.Controls.Add(this.textBox_username);
            this.panel_login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_login.Location = new System.Drawing.Point(0, 0);
            this.panel_login.Name = "panel_login";
            this.panel_login.Size = new System.Drawing.Size(800, 454);
            this.panel_login.TabIndex = 22;
            // 
            // label_incorretos
            // 
            this.label_incorretos.AutoSize = true;
            this.label_incorretos.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_incorretos.ForeColor = System.Drawing.Color.Red;
            this.label_incorretos.Location = new System.Drawing.Point(264, 242);
            this.label_incorretos.Name = "label_incorretos";
            this.label_incorretos.Size = new System.Drawing.Size(218, 19);
            this.label_incorretos.TabIndex = 5;
            this.label_incorretos.Text = "Username ou password incorretos";
            this.label_incorretos.Visible = false;
            // 
            // button_login
            // 
            this.button_login.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_login.Location = new System.Drawing.Point(348, 276);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(88, 38);
            this.button_login.TabIndex = 4;
            this.button_login.Text = "Login";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_password.Location = new System.Drawing.Point(263, 185);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(67, 19);
            this.label_password.TabIndex = 3;
            this.label_password.Text = "Password";
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_username.Location = new System.Drawing.Point(263, 122);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(71, 19);
            this.label_username.TabIndex = 2;
            this.label_username.Text = "Username";
            // 
            // textBox_password
            // 
            this.textBox_password.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_password.Location = new System.Drawing.Point(266, 206);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(267, 31);
            this.textBox_password.TabIndex = 1;
            this.textBox_password.Text = "admin";
            // 
            // textBox_username
            // 
            this.textBox_username.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_username.Location = new System.Drawing.Point(266, 142);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(267, 31);
            this.textBox_username.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipTitle = "VGestAgent";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "VGestAgent";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label_file_selected
            // 
            this.label_file_selected.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_file_selected.Location = new System.Drawing.Point(568, 276);
            this.label_file_selected.Name = "label_file_selected";
            this.label_file_selected.Size = new System.Drawing.Size(220, 52);
            this.label_file_selected.TabIndex = 30;
            this.label_file_selected.Text = "Nenhum Ficheiro Selecionado";
            // 
            // progress_upload
            // 
            this.progress_upload.Location = new System.Drawing.Point(568, 361);
            this.progress_upload.Name = "progress_upload";
            this.progress_upload.Size = new System.Drawing.Size(220, 26);
            this.progress_upload.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progress_upload.TabIndex = 31;
            this.progress_upload.UseWaitCursor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 454);
            this.Controls.Add(this.panel_main);
            this.Controls.Add(this.panel_login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VGestAgent";
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.panel_login.ResumeLayout(false);
            this.panel_login.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private HttpClient httpClient = new HttpClient();
        private Cliente cliente = new Cliente();
        private JObject jObject_da_versao_mais_recente;
        private AmazonS3Client client;

        private System.Windows.Forms.TextBox textBox_id;
        private System.Windows.Forms.Label label_id;
        private System.Windows.Forms.TextBox textBox_nome_versao;
        private System.Windows.Forms.Label label_nome_versao;
        private System.Windows.Forms.TextBox textBox_url;
        private System.Windows.Forms.Label label_url;
        private System.Windows.Forms.Label label_id_obrigatorio;
        private System.Windows.Forms.Label label_url_obrigatorio;
        private System.Windows.Forms.Button button_get;
        private System.Windows.Forms.TextBox textBox_update_text;
        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.Panel panel_login;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.Label label_username;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.Label label_incorretos;
        private System.Windows.Forms.Button button_forcar_update;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button button_logout;
        private System.Windows.Forms.CheckBox checkBox_start;
        private System.Windows.Forms.Button button_download;
        private System.Windows.Forms.Button button_upload;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button_select;
        private System.Windows.Forms.Button button_unzip;
        private System.Windows.Forms.Button button_mail;
        private System.Windows.Forms.Label label_file_selected;
        private System.Windows.Forms.ProgressBar progress_upload;
    }
}

