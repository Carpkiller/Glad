namespace Glad
{
    partial class HlavneOkno
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Login = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBoxLokacia = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxKlikajZalar = new System.Windows.Forms.CheckBox();
            this.checkBoxKlikajArenu = new System.Windows.Forms.CheckBox();
            this.checkBoxKlikajProvinciu = new System.Windows.Forms.CheckBox();
            this.checkBoxJeModZarabaci = new System.Windows.Forms.CheckBox();
            this.checkBoxJeModPrieskum = new System.Windows.Forms.CheckBox();
            this.checkBoxTurmaProv = new System.Windows.Forms.CheckBox();
            this.checkBoxKlikajTurmu = new System.Windows.Forms.CheckBox();
            this.checkBoxKlikajExpedicie = new System.Windows.Forms.CheckBox();
            this.checkBoxJeKostym = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(2, 58);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(1079, 542);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("https://s4-sk.gladiatus.gameforge.com/game/", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(257, 30);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(89, 20);
            this.txtPassword.TabIndex = 10;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(73, 30);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(113, 20);
            this.txtUsername.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Username";
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(361, 28);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(75, 23);
            this.Login.TabIndex = 6;
            this.Login.Text = "Login";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1405, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(490, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "label3";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(1087, 454);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(306, 146);
            this.textBox1.TabIndex = 19;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(1262, 425);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(131, 23);
            this.button4.TabIndex = 18;
            this.button4.Text = "Stop";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(1087, 425);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "Start";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBoxLokacia
            // 
            this.comboBoxLokacia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLokacia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLokacia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxLokacia.FormattingEnabled = true;
            this.comboBoxLokacia.Items.AddRange(new object[] {
            "Amulety",
            "Prstene"});
            this.comboBoxLokacia.Location = new System.Drawing.Point(6, 60);
            this.comboBoxLokacia.Name = "comboBoxLokacia";
            this.comboBoxLokacia.Size = new System.Drawing.Size(291, 23);
            this.comboBoxLokacia.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Lokacia";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 603);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1405, 22);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(1090, 338);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(107, 17);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "Zalohuj na aukcii";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(447, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Zlato :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(578, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Exp. body :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(644, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "label7";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(690, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Zivot :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(733, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "label9";
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(1087, 396);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(131, 23);
            this.button5.TabIndex = 30;
            this.button5.Text = "Arena";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(1262, 396);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(131, 23);
            this.button6.TabIndex = 31;
            this.button6.Text = "Turma";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(928, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Arena";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(971, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "label11";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(803, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 35;
            this.label12.Text = "Expedicia";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(862, 33);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 34;
            this.label13.Text = "label13";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxKlikajExpedicie);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxLokacia);
            this.groupBox1.Location = new System.Drawing.Point(1090, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 95);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Expedicie";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxJeKostym);
            this.groupBox2.Controls.Add(this.checkBoxJeModZarabaci);
            this.groupBox2.Controls.Add(this.checkBoxJeModPrieskum);
            this.groupBox2.Controls.Add(this.checkBoxTurmaProv);
            this.groupBox2.Controls.Add(this.checkBoxKlikajTurmu);
            this.groupBox2.Controls.Add(this.checkBoxKlikajProvinciu);
            this.groupBox2.Controls.Add(this.checkBoxKlikajArenu);
            this.groupBox2.Location = new System.Drawing.Point(1090, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 136);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Areny";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxKlikajZalar);
            this.groupBox3.Location = new System.Drawing.Point(1090, 135);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 46);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Zalare";
            // 
            // checkBoxKlikajZalar
            // 
            this.checkBoxKlikajZalar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxKlikajZalar.AutoSize = true;
            this.checkBoxKlikajZalar.Location = new System.Drawing.Point(6, 19);
            this.checkBoxKlikajZalar.Name = "checkBoxKlikajZalar";
            this.checkBoxKlikajZalar.Size = new System.Drawing.Size(76, 17);
            this.checkBoxKlikajZalar.TabIndex = 39;
            this.checkBoxKlikajZalar.Text = "Klikaj zalar";
            this.checkBoxKlikajZalar.UseVisualStyleBackColor = true;
            // 
            // checkBoxKlikajArenu
            // 
            this.checkBoxKlikajArenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxKlikajArenu.AutoSize = true;
            this.checkBoxKlikajArenu.Checked = true;
            this.checkBoxKlikajArenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKlikajArenu.Location = new System.Drawing.Point(6, 30);
            this.checkBoxKlikajArenu.Name = "checkBoxKlikajArenu";
            this.checkBoxKlikajArenu.Size = new System.Drawing.Size(81, 17);
            this.checkBoxKlikajArenu.TabIndex = 39;
            this.checkBoxKlikajArenu.Text = "Klikaj arenu";
            this.checkBoxKlikajArenu.UseVisualStyleBackColor = true;
            // 
            // checkBoxKlikajProvinciu
            // 
            this.checkBoxKlikajProvinciu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxKlikajProvinciu.AutoSize = true;
            this.checkBoxKlikajProvinciu.Checked = true;
            this.checkBoxKlikajProvinciu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKlikajProvinciu.Location = new System.Drawing.Point(6, 53);
            this.checkBoxKlikajProvinciu.Name = "checkBoxKlikajProvinciu";
            this.checkBoxKlikajProvinciu.Size = new System.Drawing.Size(97, 17);
            this.checkBoxKlikajProvinciu.TabIndex = 40;
            this.checkBoxKlikajProvinciu.Text = "Klikaj provinciu";
            this.checkBoxKlikajProvinciu.UseVisualStyleBackColor = true;
            // 
            // checkBoxJeModZarabaci
            // 
            this.checkBoxJeModZarabaci.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxJeModZarabaci.AutoSize = true;
            this.checkBoxJeModZarabaci.Location = new System.Drawing.Point(172, 53);
            this.checkBoxJeModZarabaci.Name = "checkBoxJeModZarabaci";
            this.checkBoxJeModZarabaci.Size = new System.Drawing.Size(91, 17);
            this.checkBoxJeModZarabaci.TabIndex = 42;
            this.checkBoxJeModZarabaci.Text = "Zarabaci mod";
            this.checkBoxJeModZarabaci.UseVisualStyleBackColor = true;
            // 
            // checkBoxJeModPrieskum
            // 
            this.checkBoxJeModPrieskum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxJeModPrieskum.AutoSize = true;
            this.checkBoxJeModPrieskum.Checked = true;
            this.checkBoxJeModPrieskum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxJeModPrieskum.Location = new System.Drawing.Point(172, 30);
            this.checkBoxJeModPrieskum.Name = "checkBoxJeModPrieskum";
            this.checkBoxJeModPrieskum.Size = new System.Drawing.Size(103, 17);
            this.checkBoxJeModPrieskum.TabIndex = 41;
            this.checkBoxJeModPrieskum.Text = "Prieskumny mod";
            this.checkBoxJeModPrieskum.UseVisualStyleBackColor = true;
            // 
            // checkBoxTurmaProv
            // 
            this.checkBoxTurmaProv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxTurmaProv.AutoSize = true;
            this.checkBoxTurmaProv.Location = new System.Drawing.Point(6, 103);
            this.checkBoxTurmaProv.Name = "checkBoxTurmaProv";
            this.checkBoxTurmaProv.Size = new System.Drawing.Size(126, 17);
            this.checkBoxTurmaProv.TabIndex = 40;
            this.checkBoxTurmaProv.Text = "Klikaj turmu provinciu";
            this.checkBoxTurmaProv.UseVisualStyleBackColor = true;
            // 
            // checkBoxKlikajTurmu
            // 
            this.checkBoxKlikajTurmu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxKlikajTurmu.AutoSize = true;
            this.checkBoxKlikajTurmu.Location = new System.Drawing.Point(7, 80);
            this.checkBoxKlikajTurmu.Name = "checkBoxKlikajTurmu";
            this.checkBoxKlikajTurmu.Size = new System.Drawing.Size(80, 17);
            this.checkBoxKlikajTurmu.TabIndex = 39;
            this.checkBoxKlikajTurmu.Text = "Klikaj turmu";
            this.checkBoxKlikajTurmu.UseVisualStyleBackColor = true;
            // 
            // checkBoxKlikajExpedicie
            // 
            this.checkBoxKlikajExpedicie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxKlikajExpedicie.AutoSize = true;
            this.checkBoxKlikajExpedicie.Checked = true;
            this.checkBoxKlikajExpedicie.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKlikajExpedicie.Location = new System.Drawing.Point(8, 24);
            this.checkBoxKlikajExpedicie.Name = "checkBoxKlikajExpedicie";
            this.checkBoxKlikajExpedicie.Size = new System.Drawing.Size(99, 17);
            this.checkBoxKlikajExpedicie.TabIndex = 40;
            this.checkBoxKlikajExpedicie.Text = "Klikaj expedicie";
            this.checkBoxKlikajExpedicie.UseVisualStyleBackColor = true;
            // 
            // checkBoxJeKostym
            // 
            this.checkBoxJeKostym.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxJeKostym.AutoSize = true;
            this.checkBoxJeKostym.Location = new System.Drawing.Point(172, 103);
            this.checkBoxJeKostym.Name = "checkBoxJeKostym";
            this.checkBoxJeKostym.Size = new System.Drawing.Size(60, 17);
            this.checkBoxJeKostym.TabIndex = 43;
            this.checkBoxJeKostym.Text = "Kostym";
            this.checkBoxJeKostym.UseVisualStyleBackColor = true;
            // 
            // HlavneOkno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1405, 625);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "HlavneOkno";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        public System.Windows.Forms.TextBox txtPassword;
        public System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBoxLokacia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxKlikajZalar;
        private System.Windows.Forms.CheckBox checkBoxKlikajArenu;
        private System.Windows.Forms.CheckBox checkBoxJeModZarabaci;
        private System.Windows.Forms.CheckBox checkBoxJeModPrieskum;
        private System.Windows.Forms.CheckBox checkBoxKlikajProvinciu;
        private System.Windows.Forms.CheckBox checkBoxTurmaProv;
        private System.Windows.Forms.CheckBox checkBoxKlikajTurmu;
        private System.Windows.Forms.CheckBox checkBoxKlikajExpedicie;
        private System.Windows.Forms.CheckBox checkBoxJeKostym;
    }
}

