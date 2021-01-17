namespace AES_encryption
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_problem = new System.Windows.Forms.Label();
            this.label_infoFile = new System.Windows.Forms.Label();
            this.label_infoFile_tag = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.button_openFile = new System.Windows.Forms.Button();
            this.button_Encryption = new System.Windows.Forms.Button();
            this.button_NonEncryption = new System.Windows.Forms.Button();
            this.label_password = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_problem);
            this.panel1.Controls.Add(this.label_infoFile);
            this.panel1.Controls.Add(this.label_infoFile_tag);
            this.panel1.Location = new System.Drawing.Point(26, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(339, 133);
            this.panel1.TabIndex = 0;
            // 
            // label_problem
            // 
            this.label_problem.AutoSize = true;
            this.label_problem.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_problem.Location = new System.Drawing.Point(16, 87);
            this.label_problem.MaximumSize = new System.Drawing.Size(300, 0);
            this.label_problem.MinimumSize = new System.Drawing.Size(100, 0);
            this.label_problem.Name = "label_problem";
            this.label_problem.Size = new System.Drawing.Size(100, 20);
            this.label_problem.TabIndex = 2;
            // 
            // label_infoFile
            // 
            this.label_infoFile.AutoSize = true;
            this.label_infoFile.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_infoFile.Location = new System.Drawing.Point(16, 52);
            this.label_infoFile.Name = "label_infoFile";
            this.label_infoFile.Size = new System.Drawing.Size(179, 20);
            this.label_infoFile.TabIndex = 1;
            this.label_infoFile.Text = "Сначала выберете файл";
            // 
            // label_infoFile_tag
            // 
            this.label_infoFile_tag.AutoSize = true;
            this.label_infoFile_tag.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_infoFile_tag.Location = new System.Drawing.Point(16, 16);
            this.label_infoFile_tag.Name = "label_infoFile_tag";
            this.label_infoFile_tag.Size = new System.Drawing.Size(201, 23);
            this.label_infoFile_tag.TabIndex = 0;
            this.label_infoFile_tag.Text = "Информация о файле:";
            // 
            // textBox_password
            // 
            this.textBox_password.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_password.Location = new System.Drawing.Point(479, 35);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(217, 24);
            this.textBox_password.TabIndex = 1;
            this.textBox_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_openFile
            // 
            this.button_openFile.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_openFile.Location = new System.Drawing.Point(26, 151);
            this.button_openFile.Name = "button_openFile";
            this.button_openFile.Size = new System.Drawing.Size(158, 33);
            this.button_openFile.TabIndex = 2;
            this.button_openFile.Text = "Выбрать файл";
            this.button_openFile.UseVisualStyleBackColor = true;
            this.button_openFile.Click += new System.EventHandler(this.button_openFile_Click);
            // 
            // button_Encryption
            // 
            this.button_Encryption.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Encryption.Location = new System.Drawing.Point(395, 65);
            this.button_Encryption.Name = "button_Encryption";
            this.button_Encryption.Size = new System.Drawing.Size(158, 33);
            this.button_Encryption.TabIndex = 3;
            this.button_Encryption.Text = "Зашифровать";
            this.button_Encryption.UseVisualStyleBackColor = true;
            this.button_Encryption.Click += new System.EventHandler(this.button_Encryption_Click);
            // 
            // button_NonEncryption
            // 
            this.button_NonEncryption.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_NonEncryption.Location = new System.Drawing.Point(569, 65);
            this.button_NonEncryption.Name = "button_NonEncryption";
            this.button_NonEncryption.Size = new System.Drawing.Size(158, 33);
            this.button_NonEncryption.TabIndex = 4;
            this.button_NonEncryption.Text = "Дешифровать";
            this.button_NonEncryption.UseVisualStyleBackColor = true;
            this.button_NonEncryption.Click += new System.EventHandler(this.button_NonEncryption_Click);
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_password.Location = new System.Drawing.Point(475, 12);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(62, 20);
            this.label_password.TabIndex = 3;
            this.label_password.Text = "Пароль";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 210);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.button_NonEncryption);
            this.Controls.Add(this.button_Encryption);
            this.Controls.Add(this.button_openFile);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyAES Encryption";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_infoFile;
        private System.Windows.Forms.Label label_infoFile_tag;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Button button_openFile;
        private System.Windows.Forms.Button button_Encryption;
        private System.Windows.Forms.Button button_NonEncryption;
        private System.Windows.Forms.Label label_problem;
        private System.Windows.Forms.Label label_password;
    }
}

