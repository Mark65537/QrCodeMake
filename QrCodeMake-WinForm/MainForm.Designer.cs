namespace QrCodeMake_WinForm
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pB_QrCode = new System.Windows.Forms.PictureBox();
            this.cMS_forPicBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMI_normal = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_stretchImage = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_centerImage = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_zoom = new System.Windows.Forms.ToolStripMenuItem();
            this.b_prev = new System.Windows.Forms.Button();
            this.b_next = new System.Windows.Forms.Button();
            this.tB_fileName = new System.Windows.Forms.TextBox();
            this.b_open = new System.Windows.Forms.Button();
            this.l_error = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nUD_border = new System.Windows.Forms.NumericUpDown();
            this.b_copyQr = new System.Windows.Forms.Button();
            this.cB_error = new System.Windows.Forms.ComboBox();
            this.oFD_file = new System.Windows.Forms.OpenFileDialog();
            this.b_sendEmail = new System.Windows.Forms.Button();
            this.b_about = new System.Windows.Forms.Button();
            this.chB_sendAll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pB_QrCode)).BeginInit();
            this.cMS_forPicBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_border)).BeginInit();
            this.SuspendLayout();
            // 
            // pB_QrCode
            // 
            this.pB_QrCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pB_QrCode.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pB_QrCode.ContextMenuStrip = this.cMS_forPicBox;
            this.pB_QrCode.Location = new System.Drawing.Point(67, 12);
            this.pB_QrCode.Name = "pB_QrCode";
            this.pB_QrCode.Size = new System.Drawing.Size(266, 215);
            this.pB_QrCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pB_QrCode.TabIndex = 0;
            this.pB_QrCode.TabStop = false;
            this.pB_QrCode.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pB_QrCode_MouseClick);
            // 
            // cMS_forPicBox
            // 
            this.cMS_forPicBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_normal,
            this.TSMI_stretchImage,
            this.TSMI_centerImage,
            this.TSMI_zoom});
            this.cMS_forPicBox.Name = "cMS_forPicBox";
            this.cMS_forPicBox.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cMS_forPicBox.Size = new System.Drawing.Size(148, 92);
            this.cMS_forPicBox.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cMS_forPicBox_ItemClicked);
            // 
            // TSMI_normal
            // 
            this.TSMI_normal.Name = "TSMI_normal";
            this.TSMI_normal.Size = new System.Drawing.Size(147, 22);
            this.TSMI_normal.Text = "Normal";
            // 
            // TSMI_stretchImage
            // 
            this.TSMI_stretchImage.Name = "TSMI_stretchImage";
            this.TSMI_stretchImage.Size = new System.Drawing.Size(147, 22);
            this.TSMI_stretchImage.Text = "Stretch Image";
            // 
            // TSMI_centerImage
            // 
            this.TSMI_centerImage.Name = "TSMI_centerImage";
            this.TSMI_centerImage.Size = new System.Drawing.Size(147, 22);
            this.TSMI_centerImage.Text = "Center Image";
            // 
            // TSMI_zoom
            // 
            this.TSMI_zoom.Name = "TSMI_zoom";
            this.TSMI_zoom.Size = new System.Drawing.Size(147, 22);
            this.TSMI_zoom.Text = "Zoom";
            // 
            // b_prev
            // 
            this.b_prev.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.b_prev.Enabled = false;
            this.b_prev.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.b_prev.Location = new System.Drawing.Point(67, 234);
            this.b_prev.Name = "b_prev";
            this.b_prev.Size = new System.Drawing.Size(75, 23);
            this.b_prev.TabIndex = 1;
            this.b_prev.Text = "←";
            this.b_prev.UseVisualStyleBackColor = true;
            this.b_prev.Click += new System.EventHandler(this.b_prev_Click);
            // 
            // b_next
            // 
            this.b_next.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.b_next.AutoSize = true;
            this.b_next.Enabled = false;
            this.b_next.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.b_next.Location = new System.Drawing.Point(257, 232);
            this.b_next.Name = "b_next";
            this.b_next.Size = new System.Drawing.Size(75, 25);
            this.b_next.TabIndex = 2;
            this.b_next.Text = "→";
            this.b_next.UseVisualStyleBackColor = true;
            this.b_next.Click += new System.EventHandler(this.b_next_Click);
            // 
            // tB_fileName
            // 
            this.tB_fileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_fileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tB_fileName.Location = new System.Drawing.Point(13, 264);
            this.tB_fileName.Name = "tB_fileName";
            this.tB_fileName.Size = new System.Drawing.Size(285, 21);
            this.tB_fileName.TabIndex = 3;
            // 
            // b_open
            // 
            this.b_open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_open.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.b_open.Location = new System.Drawing.Point(314, 263);
            this.b_open.Name = "b_open";
            this.b_open.Size = new System.Drawing.Size(75, 23);
            this.b_open.TabIndex = 4;
            this.b_open.Text = "обзор";
            this.b_open.UseVisualStyleBackColor = true;
            this.b_open.Click += new System.EventHandler(this.b_open_Click);
            // 
            // l_error
            // 
            this.l_error.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.l_error.AutoSize = true;
            this.l_error.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.l_error.Location = new System.Drawing.Point(14, 295);
            this.l_error.Name = "l_error";
            this.l_error.Size = new System.Drawing.Size(119, 15);
            this.l_error.TabIndex = 5;
            this.l_error.Text = "Ошибка коррекции:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(269, 295);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Границы";
            this.label1.Visible = false;
            // 
            // nUD_border
            // 
            this.nUD_border.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nUD_border.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nUD_border.Location = new System.Drawing.Point(329, 293);
            this.nUD_border.Name = "nUD_border";
            this.nUD_border.Size = new System.Drawing.Size(61, 21);
            this.nUD_border.TabIndex = 7;
            this.nUD_border.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nUD_border.Visible = false;
            // 
            // b_copyQr
            // 
            this.b_copyQr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.b_copyQr.Enabled = false;
            this.b_copyQr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.b_copyQr.Location = new System.Drawing.Point(13, 323);
            this.b_copyQr.Name = "b_copyQr";
            this.b_copyQr.Size = new System.Drawing.Size(138, 23);
            this.b_copyQr.TabIndex = 8;
            this.b_copyQr.Text = "Копировать QR Код";
            this.b_copyQr.UseVisualStyleBackColor = true;
            this.b_copyQr.Click += new System.EventHandler(this.b_copyQr_Click);
            // 
            // cB_error
            // 
            this.cB_error.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cB_error.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_error.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cB_error.FormattingEnabled = true;
            this.cB_error.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "Quartile",
            "High"});
            this.cB_error.Location = new System.Drawing.Point(131, 291);
            this.cB_error.Name = "cB_error";
            this.cB_error.Size = new System.Drawing.Size(132, 23);
            this.cB_error.TabIndex = 9;
            // 
            // oFD_file
            // 
            this.oFD_file.FileName = "openFileDialog1";
            // 
            // b_sendEmail
            // 
            this.b_sendEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_sendEmail.Enabled = false;
            this.b_sendEmail.Location = new System.Drawing.Point(162, 323);
            this.b_sendEmail.Name = "b_sendEmail";
            this.b_sendEmail.Size = new System.Drawing.Size(123, 23);
            this.b_sendEmail.TabIndex = 10;
            this.b_sendEmail.Text = "Отправить по email";
            this.b_sendEmail.UseVisualStyleBackColor = true;
            this.b_sendEmail.Click += new System.EventHandler(this.b_sendEmail_Click);
            // 
            // b_about
            // 
            this.b_about.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.b_about.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.b_about.Location = new System.Drawing.Point(3, 361);
            this.b_about.Name = "b_about";
            this.b_about.Size = new System.Drawing.Size(83, 19);
            this.b_about.TabIndex = 11;
            this.b_about.Text = "о программе";
            this.b_about.UseVisualStyleBackColor = true;
            // 
            // chB_sendAll
            // 
            this.chB_sendAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chB_sendAll.AutoSize = true;
            this.chB_sendAll.Checked = true;
            this.chB_sendAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chB_sendAll.Location = new System.Drawing.Point(293, 326);
            this.chB_sendAll.Name = "chB_sendAll";
            this.chB_sendAll.Size = new System.Drawing.Size(109, 17);
            this.chB_sendAll.TabIndex = 12;
            this.chB_sendAll.Text = "Отправить всем";
            this.chB_sendAll.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 383);
            this.Controls.Add(this.chB_sendAll);
            this.Controls.Add(this.b_about);
            this.Controls.Add(this.b_sendEmail);
            this.Controls.Add(this.cB_error);
            this.Controls.Add(this.b_copyQr);
            this.Controls.Add(this.nUD_border);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.l_error);
            this.Controls.Add(this.b_open);
            this.Controls.Add(this.tB_fileName);
            this.Controls.Add(this.b_next);
            this.Controls.Add(this.b_prev);
            this.Controls.Add(this.pB_QrCode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "QrCodeMake ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pB_QrCode)).EndInit();
            this.cMS_forPicBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nUD_border)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pB_QrCode;
        private System.Windows.Forms.Button b_prev;
        private System.Windows.Forms.Button b_next;
        private System.Windows.Forms.TextBox tB_fileName;
        private System.Windows.Forms.Button b_open;
        private System.Windows.Forms.Label l_error;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nUD_border;
        private System.Windows.Forms.Button b_copyQr;
        private System.Windows.Forms.ComboBox cB_error;
        private System.Windows.Forms.OpenFileDialog oFD_file;
        private System.Windows.Forms.ContextMenuStrip cMS_forPicBox;
        private System.Windows.Forms.ToolStripMenuItem TSMI_normal;
        private System.Windows.Forms.ToolStripMenuItem TSMI_stretchImage;
        private System.Windows.Forms.ToolStripMenuItem TSMI_centerImage;
        private System.Windows.Forms.ToolStripMenuItem TSMI_zoom;
        private System.Windows.Forms.Button b_sendEmail;
        private System.Windows.Forms.Button b_about;
        private System.Windows.Forms.CheckBox chB_sendAll;
    }
}

