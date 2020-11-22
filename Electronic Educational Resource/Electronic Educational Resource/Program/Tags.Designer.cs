namespace WorkingWithData
{
    partial class Tags
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tags));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TagsCancelButton = new System.Windows.Forms.Button();
            this.TagsListBox = new System.Windows.Forms.ListBox();
            this.ChooseTagButton = new System.Windows.Forms.Button();
            this.TagsTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ChoosedTagDeleteButton = new System.Windows.Forms.Button();
            this.TagsConfirmButton = new System.Windows.Forms.Button();
            this.ChoosedTagsListBox = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TagsUserStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TagsDisciplineStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TagsDocumentStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TagsTextErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.TagsExitButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TagsTextErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TagsCancelButton);
            this.groupBox1.Controls.Add(this.TagsListBox);
            this.groupBox1.Controls.Add(this.ChooseTagButton);
            this.groupBox1.Controls.Add(this.TagsTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 262);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Вкажіть ключове слово/фразу";
            // 
            // TagsCancelButton
            // 
            this.TagsCancelButton.Location = new System.Drawing.Point(167, 239);
            this.TagsCancelButton.Name = "TagsCancelButton";
            this.TagsCancelButton.Size = new System.Drawing.Size(75, 23);
            this.TagsCancelButton.TabIndex = 2;
            this.TagsCancelButton.Text = "Відміна";
            this.TagsCancelButton.UseVisualStyleBackColor = true;
            this.TagsCancelButton.Click += new System.EventHandler(this.TagsCancelButton_Click);
            // 
            // TagsListBox
            // 
            this.TagsListBox.FormattingEnabled = true;
            this.TagsListBox.Location = new System.Drawing.Point(6, 45);
            this.TagsListBox.Name = "TagsListBox";
            this.TagsListBox.Size = new System.Drawing.Size(288, 186);
            this.TagsListBox.TabIndex = 4;
            this.TagsListBox.SelectedIndexChanged += new System.EventHandler(this.TagsListBox_SelectedIndexChanged);
            // 
            // ChooseTagButton
            // 
            this.ChooseTagButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChooseTagButton.Location = new System.Drawing.Point(36, 239);
            this.ChooseTagButton.Name = "ChooseTagButton";
            this.ChooseTagButton.Size = new System.Drawing.Size(75, 23);
            this.ChooseTagButton.TabIndex = 1;
            this.ChooseTagButton.Text = "Обрати";
            this.ChooseTagButton.UseVisualStyleBackColor = true;
            this.ChooseTagButton.Click += new System.EventHandler(this.ChooseTagButton_Click);
            // 
            // TagsTextBox
            // 
            this.TagsTextBox.Location = new System.Drawing.Point(6, 19);
            this.TagsTextBox.Name = "TagsTextBox";
            this.TagsTextBox.Size = new System.Drawing.Size(261, 20);
            this.TagsTextBox.TabIndex = 0;
            this.TagsTextBox.TextChanged += new System.EventHandler(this.TagsTextBox_TextChanged);
            this.TagsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TagsTextBox_KeyPress);
            this.TagsTextBox.Leave += new System.EventHandler(this.TagsTextBox_Leave);
            this.TagsTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.TagsTextBox_Validating);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ChoosedTagDeleteButton);
            this.groupBox2.Controls.Add(this.TagsConfirmButton);
            this.groupBox2.Controls.Add(this.ChoosedTagsListBox);
            this.groupBox2.Location = new System.Drawing.Point(336, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 262);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ключові слова, які будуть закріплені за документом";
            // 
            // ChoosedTagDeleteButton
            // 
            this.ChoosedTagDeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChoosedTagDeleteButton.Location = new System.Drawing.Point(194, 239);
            this.ChoosedTagDeleteButton.Name = "ChoosedTagDeleteButton";
            this.ChoosedTagDeleteButton.Size = new System.Drawing.Size(88, 23);
            this.ChoosedTagDeleteButton.TabIndex = 4;
            this.ChoosedTagDeleteButton.Text = "Видалити всі";
            this.ChoosedTagDeleteButton.UseVisualStyleBackColor = true;
            this.ChoosedTagDeleteButton.Click += new System.EventHandler(this.ChoosedTagDeleteButton_Click);
            this.ChoosedTagDeleteButton.MouseHover += new System.EventHandler(this.ChoosedTagDeleteButton_MouseHover);
            // 
            // TagsConfirmButton
            // 
            this.TagsConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TagsConfirmButton.Location = new System.Drawing.Point(68, 239);
            this.TagsConfirmButton.Name = "TagsConfirmButton";
            this.TagsConfirmButton.Size = new System.Drawing.Size(83, 23);
            this.TagsConfirmButton.TabIndex = 3;
            this.TagsConfirmButton.Text = "Підтвердити";
            this.TagsConfirmButton.UseVisualStyleBackColor = true;
            this.TagsConfirmButton.Click += new System.EventHandler(this.TagsConfirmButton_Click);
            // 
            // ChoosedTagsListBox
            // 
            this.ChoosedTagsListBox.FormattingEnabled = true;
            this.ChoosedTagsListBox.Location = new System.Drawing.Point(6, 19);
            this.ChoosedTagsListBox.Name = "ChoosedTagsListBox";
            this.ChoosedTagsListBox.Size = new System.Drawing.Size(276, 212);
            this.ChoosedTagsListBox.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TagsUserStatusLabel,
            this.TagsDisciplineStatusLabel,
            this.TagsDocumentStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 300);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(630, 24);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TagsUserStatusLabel
            // 
            this.TagsUserStatusLabel.Name = "TagsUserStatusLabel";
            this.TagsUserStatusLabel.Size = new System.Drawing.Size(109, 19);
            this.TagsUserStatusLabel.Text = "Логін користувача";
            this.TagsUserStatusLabel.ToolTipText = "Логін користувача";
            // 
            // TagsDisciplineStatusLabel
            // 
            this.TagsDisciplineStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.TagsDisciplineStatusLabel.Name = "TagsDisciplineStatusLabel";
            this.TagsDisciplineStatusLabel.Size = new System.Drawing.Size(137, 19);
            this.TagsDisciplineStatusLabel.Text = "Навчальна дисципліна";
            this.TagsDisciplineStatusLabel.ToolTipText = "Навчальна дисципліна";
            // 
            // TagsDocumentStatusLabel
            // 
            this.TagsDocumentStatusLabel.Name = "TagsDocumentStatusLabel";
            this.TagsDocumentStatusLabel.Size = new System.Drawing.Size(100, 19);
            this.TagsDocumentStatusLabel.Text = "Назва документа";
            this.TagsDocumentStatusLabel.ToolTipText = "Назва документа";
            // 
            // TagsTextErrorProvider
            // 
            this.TagsTextErrorProvider.ContainerControl = this;
            // 
            // TagsExitButton
            // 
            this.TagsExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TagsExitButton.Location = new System.Drawing.Point(558, 277);
            this.TagsExitButton.Name = "TagsExitButton";
            this.TagsExitButton.Size = new System.Drawing.Size(72, 22);
            this.TagsExitButton.TabIndex = 5;
            this.TagsExitButton.Text = "Завершити";
            this.TagsExitButton.UseVisualStyleBackColor = true;
            this.TagsExitButton.Click += new System.EventHandler(this.TagsExitButton_Click);
            // 
            // Tags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 324);
            this.Controls.Add(this.TagsExitButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Tags";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вибір ключових слів";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tags_FormClosing);
            this.Load += new System.EventHandler(this.Tags_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TagsTextErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox TagsListBox;
        private System.Windows.Forms.Button ChooseTagButton;
        private System.Windows.Forms.TextBox TagsTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button TagsConfirmButton;
        private System.Windows.Forms.ListBox ChoosedTagsListBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ErrorProvider TagsTextErrorProvider;
        private System.Windows.Forms.Button TagsCancelButton;
        private System.Windows.Forms.Button TagsExitButton;
        private System.Windows.Forms.ToolStripStatusLabel TagsUserStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel TagsDisciplineStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel TagsDocumentStatusLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button ChoosedTagDeleteButton;
    }
}