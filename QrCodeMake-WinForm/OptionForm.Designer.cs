namespace QrCodeMake_WinForm
{
    partial class OptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionForm));
            this.tC_conf = new System.Windows.Forms.TabControl();
            this.tP_conf = new System.Windows.Forms.TabPage();
            this.dGV_changeable = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dGV_unChangeable = new System.Windows.Forms.DataGridView();
            this.b_save = new System.Windows.Forms.Button();
            this.b_cancel = new System.Windows.Forms.Button();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tC_conf.SuspendLayout();
            this.tP_conf.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_changeable)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_unChangeable)).BeginInit();
            this.SuspendLayout();
            // 
            // tC_conf
            // 
            this.tC_conf.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tC_conf.Controls.Add(this.tP_conf);
            this.tC_conf.Controls.Add(this.tabPage2);
            this.tC_conf.Location = new System.Drawing.Point(0, 0);
            this.tC_conf.Name = "tC_conf";
            this.tC_conf.SelectedIndex = 0;
            this.tC_conf.Size = new System.Drawing.Size(629, 562);
            this.tC_conf.TabIndex = 0;
            // 
            // tP_conf
            // 
            this.tP_conf.Controls.Add(this.dGV_changeable);
            this.tP_conf.Location = new System.Drawing.Point(4, 22);
            this.tP_conf.Name = "tP_conf";
            this.tP_conf.Padding = new System.Windows.Forms.Padding(3);
            this.tP_conf.Size = new System.Drawing.Size(621, 536);
            this.tP_conf.TabIndex = 0;
            this.tP_conf.Text = "Изменяемые";
            this.tP_conf.UseVisualStyleBackColor = true;
            // 
            // dGV_changeable
            // 
            this.dGV_changeable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGV_changeable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGV_changeable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_changeable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dGV_changeable.Location = new System.Drawing.Point(0, 0);
            this.dGV_changeable.Name = "dGV_changeable";
            this.dGV_changeable.Size = new System.Drawing.Size(618, 533);
            this.dGV_changeable.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Имя";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Значение";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Описание";
            this.Column3.Name = "Column3";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dGV_unChangeable);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(621, 536);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Неизменяемые";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dGV_unChangeable
            // 
            this.dGV_unChangeable.AllowUserToAddRows = false;
            this.dGV_unChangeable.AllowUserToDeleteRows = false;
            this.dGV_unChangeable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGV_unChangeable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGV_unChangeable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3});
            this.dGV_unChangeable.Location = new System.Drawing.Point(0, 0);
            this.dGV_unChangeable.Name = "dGV_unChangeable";
            this.dGV_unChangeable.ReadOnly = true;
            this.dGV_unChangeable.Size = new System.Drawing.Size(618, 533);
            this.dGV_unChangeable.TabIndex = 1;
            // 
            // b_save
            // 
            this.b_save.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_save.Location = new System.Drawing.Point(525, 572);
            this.b_save.Name = "b_save";
            this.b_save.Size = new System.Drawing.Size(92, 30);
            this.b_save.TabIndex = 1;
            this.b_save.Text = "Сохранить";
            this.b_save.UseVisualStyleBackColor = true;
            this.b_save.Click += new System.EventHandler(this.b_save_Click);
            // 
            // b_cancel
            // 
            this.b_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_cancel.Location = new System.Drawing.Point(427, 572);
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.Size = new System.Drawing.Size(92, 30);
            this.b_cancel.TabIndex = 2;
            this.b_cancel.Text = "Отмена";
            this.b_cancel.UseVisualStyleBackColor = true;
            this.b_cancel.Click += new System.EventHandler(this.b_cancel_Click);
            // 
            // Column4
            // 
            this.Column4.FillWeight = 15.22843F;
            this.Column4.HeaderText = "ID";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 142.3858F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Имя";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 142.3858F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Описание";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 614);
            this.Controls.Add(this.b_cancel);
            this.Controls.Add(this.b_save);
            this.Controls.Add(this.tC_conf);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionForm";
            this.Text = "Настройка заменителей";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.tC_conf.ResumeLayout(false);
            this.tP_conf.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_changeable)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_unChangeable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tC_conf;
        private System.Windows.Forms.TabPage tP_conf;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dGV_changeable;
        private System.Windows.Forms.Button b_save;
        private System.Windows.Forms.Button b_cancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridView dGV_unChangeable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}