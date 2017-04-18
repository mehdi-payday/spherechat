namespace SphereClient {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing ) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.pictureBox20 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel8 = new SphereClient.Components.GroupListPanel();
            this.panel7 = new SphereClient.Components.DiscussionListPanel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.sendMessage = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.pictureBox19 = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.panel4 = new SphereClient.Components.MessageListPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.pictureBox16 = new System.Windows.Forms.PictureBox();
            this.pictureBox17 = new System.Windows.Forms.PictureBox();
            this.pictureBox18 = new System.Windows.Forms.PictureBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox19)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(153)))), ((int)(((byte)(231)))));
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label40);
            this.panel1.Controls.Add(this.pictureBox20);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 117);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Log Out";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(201)))), ((int)(((byte)(205)))));
            this.label40.Location = new System.Drawing.Point(60, 68);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(110, 20);
            this.label40.TabIndex = 5;
            this.label40.Text = "Sphere Chat";
            // 
            // pictureBox20
            // 
            this.pictureBox20.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox20.BackgroundImage = global::SphereClient.Properties.Resources.path4149;
            this.pictureBox20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox20.Location = new System.Drawing.Point(93, 16);
            this.pictureBox20.Name = "pictureBox20";
            this.pictureBox20.Size = new System.Drawing.Size(44, 49);
            this.pictureBox20.TabIndex = 4;
            this.pictureBox20.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::SphereClient.Properties.Resources.Capture;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.panel7);
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(128)))), ((int)(((byte)(159)))));
            this.panel2.Location = new System.Drawing.Point(0, 117);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(235, 583);
            this.panel2.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(1)))), ((int)(((byte)(56)))));
            this.panel8.Location = new System.Drawing.Point(0, 275);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(235, 308);
            this.panel8.TabIndex = 5;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(1)))), ((int)(((byte)(56)))));
            this.panel7.Location = new System.Drawing.Point(0, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(235, 276);
            this.panel7.TabIndex = 4;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.White;
            this.panel9.Controls.Add(this.sendMessage);
            this.panel9.Controls.Add(this.button3);
            this.panel9.Controls.Add(this.richTextBox1);
            this.panel9.Location = new System.Drawing.Point(235, 644);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(665, 56);
            this.panel9.TabIndex = 0;
            // 
            // sendMessage
            // 
            this.sendMessage.BackColor = System.Drawing.Color.White;
            this.sendMessage.BackgroundImage = global::SphereClient.Properties.Resources.paper_plane_document_send_sent_mail_512;
            this.sendMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sendMessage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sendMessage.FlatAppearance.BorderSize = 0;
            this.sendMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendMessage.Location = new System.Drawing.Point(581, 12);
            this.sendMessage.Name = "sendMessage";
            this.sendMessage.Size = new System.Drawing.Size(23, 34);
            this.sendMessage.TabIndex = 1;
            this.sendMessage.UseVisualStyleBackColor = false;
            this.sendMessage.Click += new System.EventHandler(this.Send_Message_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.BackgroundImage = global::SphereClient.Properties.Resources.g4167;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(624, 11);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(20, 35);
            this.button3.TabIndex = 3;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(6, 9);
            this.richTextBox1.MaxLength = 512;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(563, 40);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.linkLabel3);
            this.panel5.Controls.Add(this.pictureBox19);
            this.panel5.Location = new System.Drawing.Point(900, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 114);
            this.panel5.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 56);
            this.label1.TabIndex = 4;
            this.label1.Text = "Your Name Here";
            // 
            // linkLabel3
            // 
            this.linkLabel3.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(1)))), ((int)(((byte)(56)))));
            this.linkLabel3.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(1)))), ((int)(((byte)(56)))));
            this.linkLabel3.Location = new System.Drawing.Point(5, 91);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(56, 13);
            this.linkLabel3.TabIndex = 3;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Edit profile";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.editProfile_LinkClicked);
            // 
            // pictureBox19
            // 
            this.pictureBox19.BackColor = System.Drawing.Color.White;
            this.pictureBox19.BackgroundImage = global::SphereClient.Properties.Resources.default_user_image;
            this.pictureBox19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox19.Location = new System.Drawing.Point(90, 6);
            this.pictureBox19.Name = "pictureBox19";
            this.pictureBox19.Size = new System.Drawing.Size(102, 100);
            this.pictureBox19.TabIndex = 0;
            this.pictureBox19.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel6.Controls.Add(this.tabControl1);
            this.panel6.Location = new System.Drawing.Point(900, 117);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 583);
            this.panel6.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::SphereClient.Properties.Resources.Capture1;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label42);
            this.panel3.Location = new System.Drawing.Point(234, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(666, 117);
            this.panel3.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(56)))), ((int)(((byte)(202)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(582, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 38;
            this.button1.Text = "Manage";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(187)))), ((int)(((byte)(249)))));
            this.label2.Location = new System.Drawing.Point(7, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 37;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(187)))), ((int)(((byte)(249)))));
            this.label42.Location = new System.Drawing.Point(6, 16);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(188, 25);
            this.label42.TabIndex = 36;
            this.label42.Text = "Select a channel";
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Location = new System.Drawing.Point(235, 115);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(666, 531);
            this.panel4.TabIndex = 2;
            this.panel4.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.panel4_ControlAdded);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label36);
            this.tabPage2.Controls.Add(this.label37);
            this.tabPage2.Controls.Add(this.label34);
            this.tabPage2.Controls.Add(this.label35);
            this.tabPage2.Controls.Add(this.label32);
            this.tabPage2.Controls.Add(this.label33);
            this.tabPage2.Controls.Add(this.label31);
            this.tabPage2.Controls.Add(this.label25);
            this.tabPage2.Controls.Add(this.pictureBox18);
            this.tabPage2.Controls.Add(this.pictureBox17);
            this.tabPage2.Controls.Add(this.pictureBox16);
            this.tabPage2.Controls.Add(this.pictureBox15);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 556);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Files";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pictureBox15
            // 
            this.pictureBox15.BackColor = System.Drawing.Color.White;
            this.pictureBox15.BackgroundImage = global::SphereClient.Properties.Resources.text_file_3_xxl;
            this.pictureBox15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox15.Location = new System.Drawing.Point(7, 7);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(40, 40);
            this.pictureBox15.TabIndex = 0;
            this.pictureBox15.TabStop = false;
            // 
            // pictureBox16
            // 
            this.pictureBox16.BackColor = System.Drawing.Color.White;
            this.pictureBox16.BackgroundImage = global::SphereClient.Properties.Resources.text_file_3_xxl;
            this.pictureBox16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox16.Location = new System.Drawing.Point(7, 53);
            this.pictureBox16.Name = "pictureBox16";
            this.pictureBox16.Size = new System.Drawing.Size(40, 40);
            this.pictureBox16.TabIndex = 3;
            this.pictureBox16.TabStop = false;
            // 
            // pictureBox17
            // 
            this.pictureBox17.BackColor = System.Drawing.Color.White;
            this.pictureBox17.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox17.BackgroundImage")));
            this.pictureBox17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox17.Location = new System.Drawing.Point(7, 99);
            this.pictureBox17.Name = "pictureBox17";
            this.pictureBox17.Size = new System.Drawing.Size(40, 40);
            this.pictureBox17.TabIndex = 6;
            this.pictureBox17.TabStop = false;
            // 
            // pictureBox18
            // 
            this.pictureBox18.BackColor = System.Drawing.Color.White;
            this.pictureBox18.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox18.BackgroundImage")));
            this.pictureBox18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox18.Location = new System.Drawing.Point(7, 145);
            this.pictureBox18.Name = "pictureBox18";
            this.pictureBox18.Size = new System.Drawing.Size(40, 40);
            this.pictureBox18.TabIndex = 9;
            this.pictureBox18.TabStop = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(51, 10);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(123, 16);
            this.label25.TabIndex = 1;
            this.label25.Text = "Rapport de sta...";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label31.Location = new System.Drawing.Point(51, 26);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(103, 15);
            this.label31.TabIndex = 2;
            this.label31.Text = "Click to download";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(51, 53);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(77, 16);
            this.label33.TabIndex = 4;
            this.label33.Text = "Dipshit.py";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label32.Location = new System.Drawing.Point(51, 70);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(103, 15);
            this.label32.TabIndex = 5;
            this.label32.Text = "Click to download";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(51, 101);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(73, 16);
            this.label35.TabIndex = 7;
            this.label35.Text = ".gitignore";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label34.Location = new System.Drawing.Point(53, 119);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(103, 15);
            this.label34.TabIndex = 8;
            this.label34.Text = "Click to download";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(52, 146);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(97, 16);
            this.label37.TabIndex = 10;
            this.label37.Text = "README.md";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label36.Location = new System.Drawing.Point(53, 163);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(103, 15);
            this.label36.TabIndex = 11;
            this.label36.Text = "Click to download";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(1, -4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(200, 585);
            this.tabControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SphereClient.Properties.Resources.chatting_bg;
            this.ClientSize = new System.Drawing.Size(1101, 700);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Sphere chat";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox19)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private SphereClient.Components.MessageListPanel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private SphereClient.Components.DiscussionListPanel panel7;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button sendMessage;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.PictureBox pictureBox19;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.PictureBox pictureBox20;
        private System.Windows.Forms.Label label42;
        private Components.GroupListPanel panel8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.PictureBox pictureBox18;
        private System.Windows.Forms.PictureBox pictureBox17;
        private System.Windows.Forms.PictureBox pictureBox16;
        private System.Windows.Forms.PictureBox pictureBox15;
    }
}

