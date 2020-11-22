namespace WorkingWithData
{
    partial class UserAuthorization
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserAuthorization));
            this.Aut_ConfirmButon = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Aut_LoginTextBox = new System.Windows.Forms.TextBox();
            this.Aut_PasswordTextBox = new System.Windows.Forms.TextBox();
            this.Aut_LoginErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.Aut_CancelButton = new System.Windows.Forms.Button();
            this.Aut_PasswordErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Aut_LoginErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Aut_PasswordErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // Aut_ConfirmButon
            // 
            this.Aut_ConfirmButon.Location = new System.Drawing.Point(35, 95);
            this.Aut_ConfirmButon.Name = "Aut_ConfirmButon";
            this.Aut_ConfirmButon.Size = new System.Drawing.Size(100, 23);
            this.Aut_ConfirmButon.TabIndex = 2;
            this.Aut_ConfirmButon.Text = "Авторизуватись";
            this.Aut_ConfirmButon.UseVisualStyleBackColor = true;
            this.Aut_ConfirmButon.Click += new System.EventHandler(this.Aut_ConfirmButon_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Логін";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Пароль";
            // 
            // Aut_LoginTextBox
            // 
            this.Aut_LoginTextBox.Location = new System.Drawing.Point(82, 20);
            this.Aut_LoginTextBox.Name = "Aut_LoginTextBox";
            this.Aut_LoginTextBox.Size = new System.Drawing.Size(155, 20);
            this.Aut_LoginTextBox.TabIndex = 0;
            this.Aut_LoginTextBox.Enter += new System.EventHandler(this.Aut_LoginTextBox_Enter);
            this.Aut_LoginTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Aut_LoginTextBox_KeyPress);
            this.Aut_LoginTextBox.Leave += new System.EventHandler(this.Aut_LoginTextBox_Leave);
            this.Aut_LoginTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.Aut_LoginTextBox_Validating);
            // 
            // Aut_PasswordTextBox
            // 
            this.Aut_PasswordTextBox.Location = new System.Drawing.Point(82, 58);
            this.Aut_PasswordTextBox.Name = "Aut_PasswordTextBox";
            this.Aut_PasswordTextBox.PasswordChar = '*';
            this.Aut_PasswordTextBox.Size = new System.Drawing.Size(155, 20);
            this.Aut_PasswordTextBox.TabIndex = 1;
            this.Aut_PasswordTextBox.UseSystemPasswordChar = true;
            this.Aut_PasswordTextBox.Enter += new System.EventHandler(this.Aut_PasswordTextBox_Enter);
            this.Aut_PasswordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Aut_PasswordTextBox_KeyPress);
            this.Aut_PasswordTextBox.Leave += new System.EventHandler(this.Aut_PasswordTextBox_Leave);
            this.Aut_PasswordTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.Aut_PasswordTextBox_Validating);
            // 
            // Aut_LoginErrorProvider
            // 
            this.Aut_LoginErrorProvider.ContainerControl = this;
            // 
            // Aut_CancelButton
            // 
            this.Aut_CancelButton.Location = new System.Drawing.Point(162, 95);
            this.Aut_CancelButton.Name = "Aut_CancelButton";
            this.Aut_CancelButton.Size = new System.Drawing.Size(75, 23);
            this.Aut_CancelButton.TabIndex = 3;
            this.Aut_CancelButton.Text = "Відміна";
            this.Aut_CancelButton.UseVisualStyleBackColor = true;
            this.Aut_CancelButton.Click += new System.EventHandler(this.Aut_CancelButton_Click);
            // 
            // Aut_PasswordErrorProvider
            // 
            this.Aut_PasswordErrorProvider.ContainerControl = this;
            // 
            // UserAuthorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 129);
            this.Controls.Add(this.Aut_CancelButton);
            this.Controls.Add(this.Aut_PasswordTextBox);
            this.Controls.Add(this.Aut_LoginTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Aut_ConfirmButon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserAuthorization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизація у системі";
            this.Load += new System.EventHandler(this.UserAuthorization_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Aut_LoginErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Aut_PasswordErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Aut_ConfirmButon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Aut_LoginTextBox;
        private System.Windows.Forms.TextBox Aut_PasswordTextBox;
        private System.Windows.Forms.ErrorProvider Aut_LoginErrorProvider;
        private System.Windows.Forms.Button Aut_CancelButton;
        private System.Windows.Forms.ErrorProvider Aut_PasswordErrorProvider;
    }
}