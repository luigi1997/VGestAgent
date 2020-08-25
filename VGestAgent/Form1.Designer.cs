using System;

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

            panel_main.Visible = false;
            panel_login.Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Properties.Settings.Default.id != "" && Properties.Settings.Default.url != "")
            {
                Visible = false; // Hide form window.
                ShowInTaskbar = false; // Remove from taskbar.
            }

            base.OnLoad(e);
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
            this.checkBox_updates = new System.Windows.Forms.CheckBox();
            this.button_licenca = new System.Windows.Forms.Button();
            this.progress_download = new System.Windows.Forms.ProgressBar();
            this.progress_upload = new System.Windows.Forms.ProgressBar();
            this.button_upload = new System.Windows.Forms.Button();
            this.label_file_selected = new System.Windows.Forms.Label();
            this.button_select = new System.Windows.Forms.Button();
            this.button_download = new System.Windows.Forms.Button();
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
            this.panel_main.SuspendLayout();
            this.panel_login.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_id
            // 
            this.textBox_id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_id.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_id.Location = new System.Drawing.Point(9, 40);
            this.textBox_id.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.Size = new System.Drawing.Size(520, 27);
            this.textBox_id.TabIndex = 11;
            // 
            // label_id
            // 
            this.label_id.AutoSize = true;
            this.label_id.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_id.Location = new System.Drawing.Point(9, 21);
            this.label_id.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_id.Name = "label_id";
            this.label_id.Size = new System.Drawing.Size(74, 13);
            this.label_id.TabIndex = 12;
            this.label_id.Text = "ID do Cliente";
            // 
            // textBox_nome_versao
            // 
            this.textBox_nome_versao.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_nome_versao.ForeColor = System.Drawing.Color.Black;
            this.textBox_nome_versao.Location = new System.Drawing.Point(9, 104);
            this.textBox_nome_versao.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_nome_versao.Name = "textBox_nome_versao";
            this.textBox_nome_versao.ReadOnly = true;
            this.textBox_nome_versao.Size = new System.Drawing.Size(583, 27);
            this.textBox_nome_versao.TabIndex = 13;
            // 
            // label_nome_versao
            // 
            this.label_nome_versao.AutoSize = true;
            this.label_nome_versao.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_nome_versao.Location = new System.Drawing.Point(9, 85);
            this.label_nome_versao.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_nome_versao.Name = "label_nome_versao";
            this.label_nome_versao.Size = new System.Drawing.Size(138, 13);
            this.label_nome_versao.TabIndex = 14;
            this.label_nome_versao.Text = "Nome e versão do Cliente";
            // 
            // textBox_url
            // 
            this.textBox_url.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_url.ForeColor = System.Drawing.Color.Black;
            this.textBox_url.Location = new System.Drawing.Point(9, 318);
            this.textBox_url.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_url.Name = "textBox_url";
            this.textBox_url.Size = new System.Drawing.Size(583, 27);
            this.textBox_url.TabIndex = 16;
            // 
            // label_url
            // 
            this.label_url.AutoSize = true;
            this.label_url.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_url.Location = new System.Drawing.Point(9, 299);
            this.label_url.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_url.Name = "label_url";
            this.label_url.Size = new System.Drawing.Size(57, 13);
            this.label_url.TabIndex = 17;
            this.label_url.Text = "Url da API";
            // 
            // label_url_obrigatorio
            // 
            this.label_url_obrigatorio.AutoSize = true;
            this.label_url_obrigatorio.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_url_obrigatorio.ForeColor = System.Drawing.Color.Red;
            this.label_url_obrigatorio.Location = new System.Drawing.Point(9, 346);
            this.label_url_obrigatorio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_url_obrigatorio.Name = "label_url_obrigatorio";
            this.label_url_obrigatorio.Size = new System.Drawing.Size(184, 13);
            this.label_url_obrigatorio.TabIndex = 18;
            this.label_url_obrigatorio.Text = "Url da API é um campo obrigatório";
            this.label_url_obrigatorio.Visible = false;
            // 
            // label_id_obrigatorio
            // 
            this.label_id_obrigatorio.AutoSize = true;
            this.label_id_obrigatorio.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_id_obrigatorio.ForeColor = System.Drawing.Color.Red;
            this.label_id_obrigatorio.Location = new System.Drawing.Point(9, 68);
            this.label_id_obrigatorio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_id_obrigatorio.Name = "label_id_obrigatorio";
            this.label_id_obrigatorio.Size = new System.Drawing.Size(201, 13);
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
            this.button_get.Location = new System.Drawing.Point(532, 37);
            this.button_get.Margin = new System.Windows.Forms.Padding(2);
            this.button_get.Name = "button_get";
            this.button_get.Size = new System.Drawing.Size(58, 31);
            this.button_get.TabIndex = 20;
            this.button_get.UseVisualStyleBackColor = false;
            this.button_get.Click += new System.EventHandler(this.button_get_Click);
            // 
            // textBox_update_text
            // 
            this.textBox_update_text.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_update_text.ForeColor = System.Drawing.Color.Black;
            this.textBox_update_text.Location = new System.Drawing.Point(9, 135);
            this.textBox_update_text.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_update_text.Multiline = true;
            this.textBox_update_text.Name = "textBox_update_text";
            this.textBox_update_text.ReadOnly = true;
            this.textBox_update_text.Size = new System.Drawing.Size(583, 85);
            this.textBox_update_text.TabIndex = 21;
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.checkBox_updates);
            this.panel_main.Controls.Add(this.button_licenca);
            this.panel_main.Controls.Add(this.progress_download);
            this.panel_main.Controls.Add(this.progress_upload);
            this.panel_main.Controls.Add(this.button_upload);
            this.panel_main.Controls.Add(this.label_file_selected);
            this.panel_main.Controls.Add(this.button_select);
            this.panel_main.Controls.Add(this.button_download);
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
            this.panel_main.Margin = new System.Windows.Forms.Padding(2);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(600, 369);
            this.panel_main.TabIndex = 11;
            this.panel_main.Visible = false;
            // 
            // checkBox_updates
            // 
            this.checkBox_updates.AutoSize = true;
            this.checkBox_updates.Location = new System.Drawing.Point(386, 7);
            this.checkBox_updates.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_updates.Name = "checkBox_updates";
            this.checkBox_updates.Size = new System.Drawing.Size(127, 17);
            this.checkBox_updates.TabIndex = 34;
            this.checkBox_updates.Text = "Updates Automáticos";
            this.checkBox_updates.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_updates.UseVisualStyleBackColor = true;
            this.checkBox_updates.CheckedChanged += new System.EventHandler(this.checkBox_updates_CheckedChanged);
            // 
            // button_licenca
            // 
            this.button_licenca.Location = new System.Drawing.Point(430, 76);
            this.button_licenca.Margin = new System.Windows.Forms.Padding(2);
            this.button_licenca.Name = "button_licenca";
            this.button_licenca.Size = new System.Drawing.Size(161, 24);
            this.button_licenca.TabIndex = 33;
            this.button_licenca.Text = "check licença";
            this.button_licenca.UseVisualStyleBackColor = true;
            this.button_licenca.Visible = false;
            // 
            // progress_download
            // 
            this.progress_download.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progress_download.Location = new System.Drawing.Point(139, 270);
            this.progress_download.Margin = new System.Windows.Forms.Padding(2);
            this.progress_download.MarqueeAnimationSpeed = 20;
            this.progress_download.Name = "progress_download";
            this.progress_download.Size = new System.Drawing.Size(128, 23);
            this.progress_download.Step = 0;
            this.progress_download.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progress_download.TabIndex = 32;
            this.progress_download.Visible = false;
            // 
            // progress_upload
            // 
            this.progress_upload.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progress_upload.Location = new System.Drawing.Point(272, 271);
            this.progress_upload.Margin = new System.Windows.Forms.Padding(2);
            this.progress_upload.MarqueeAnimationSpeed = 20;
            this.progress_upload.Name = "progress_upload";
            this.progress_upload.Size = new System.Drawing.Size(150, 23);
            this.progress_upload.Step = 0;
            this.progress_upload.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progress_upload.TabIndex = 31;
            this.progress_upload.Visible = false;
            // 
            // button_upload
            // 
            this.button_upload.BackColor = System.Drawing.Color.DarkGray;
            this.button_upload.Enabled = false;
            this.button_upload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_upload.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_upload.Location = new System.Drawing.Point(272, 261);
            this.button_upload.Margin = new System.Windows.Forms.Padding(2);
            this.button_upload.Name = "button_upload";
            this.button_upload.Size = new System.Drawing.Size(150, 32);
            this.button_upload.TabIndex = 26;
            this.button_upload.Text = "Upload";
            this.button_upload.UseVisualStyleBackColor = false;
            this.button_upload.Visible = false;
            this.button_upload.Click += new System.EventHandler(this.button_upload_Click);
            // 
            // label_file_selected
            // 
            this.label_file_selected.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_file_selected.Location = new System.Drawing.Point(426, 224);
            this.label_file_selected.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_file_selected.Name = "label_file_selected";
            this.label_file_selected.Size = new System.Drawing.Size(165, 68);
            this.label_file_selected.TabIndex = 30;
            this.label_file_selected.Text = "Nenhum Ficheiro Selecionado";
            this.label_file_selected.Visible = false;
            // 
            // button_select
            // 
            this.button_select.BackColor = System.Drawing.Color.Lime;
            this.button_select.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_select.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_select.Location = new System.Drawing.Point(272, 224);
            this.button_select.Margin = new System.Windows.Forms.Padding(2);
            this.button_select.Name = "button_select";
            this.button_select.Size = new System.Drawing.Size(150, 32);
            this.button_select.TabIndex = 27;
            this.button_select.Text = "Selecionar Ficheiro";
            this.button_select.UseVisualStyleBackColor = false;
            this.button_select.Visible = false;
            this.button_select.Click += new System.EventHandler(this.button_select_Click);
            // 
            // button_download
            // 
            this.button_download.BackColor = System.Drawing.Color.DarkGray;
            this.button_download.Enabled = false;
            this.button_download.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_download.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_download.Location = new System.Drawing.Point(139, 224);
            this.button_download.Margin = new System.Windows.Forms.Padding(2);
            this.button_download.Name = "button_download";
            this.button_download.Size = new System.Drawing.Size(128, 68);
            this.button_download.TabIndex = 25;
            this.button_download.Text = "Fazer Download da Versão";
            this.button_download.UseVisualStyleBackColor = false;
            this.button_download.Visible = false;
            this.button_download.Click += new System.EventHandler(this.button_download_Click);
            // 
            // button_logout
            // 
            this.button_logout.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.button_logout.Location = new System.Drawing.Point(517, 0);
            this.button_logout.Margin = new System.Windows.Forms.Padding(2);
            this.button_logout.Name = "button_logout";
            this.button_logout.Size = new System.Drawing.Size(82, 27);
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
            this.button_forcar_update.Location = new System.Drawing.Point(14, 224);
            this.button_forcar_update.Margin = new System.Windows.Forms.Padding(2);
            this.button_forcar_update.Name = "button_forcar_update";
            this.button_forcar_update.Size = new System.Drawing.Size(121, 68);
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
            this.panel_login.Margin = new System.Windows.Forms.Padding(2);
            this.panel_login.Name = "panel_login";
            this.panel_login.Size = new System.Drawing.Size(600, 369);
            this.panel_login.TabIndex = 22;
            // 
            // label_incorretos
            // 
            this.label_incorretos.AutoSize = true;
            this.label_incorretos.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_incorretos.ForeColor = System.Drawing.Color.Red;
            this.label_incorretos.Location = new System.Drawing.Point(198, 197);
            this.label_incorretos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_incorretos.Name = "label_incorretos";
            this.label_incorretos.Size = new System.Drawing.Size(183, 13);
            this.label_incorretos.TabIndex = 5;
            this.label_incorretos.Text = "Username ou password incorretos";
            this.label_incorretos.Visible = false;
            // 
            // button_login
            // 
            this.button_login.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_login.Location = new System.Drawing.Point(261, 224);
            this.button_login.Margin = new System.Windows.Forms.Padding(2);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(66, 31);
            this.button_login.TabIndex = 4;
            this.button_login.Text = "Login";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_password.Location = new System.Drawing.Point(197, 150);
            this.label_password.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(56, 13);
            this.label_password.TabIndex = 3;
            this.label_password.Text = "Password";
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.label_username.Location = new System.Drawing.Point(197, 99);
            this.label_username.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(58, 13);
            this.label_username.TabIndex = 2;
            this.label_username.Text = "Username";
            // 
            // textBox_password
            // 
            this.textBox_password.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_password.Location = new System.Drawing.Point(200, 167);
            this.textBox_password.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(201, 27);
            this.textBox_password.TabIndex = 1;
            this.textBox_password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.login_KeyDown);
            // 
            // textBox_username
            // 
            this.textBox_username.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_username.Location = new System.Drawing.Point(200, 115);
            this.textBox_username.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(201, 27);
            this.textBox_username.TabIndex = 0;
            this.textBox_username.KeyDown += new System.Windows.Forms.KeyEventHandler(this.login_KeyDown);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(600, 369);
            this.Controls.Add(this.panel_main);
            this.Controls.Add(this.panel_login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.Button button_download;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar progress_download;
        private System.Windows.Forms.CheckBox checkBox_updates;
        private System.Windows.Forms.Button button_licenca;
        private System.Windows.Forms.ProgressBar progress_upload;
        private System.Windows.Forms.Button button_upload;
        private System.Windows.Forms.Label label_file_selected;
        private System.Windows.Forms.Button button_select;
    }
}