namespace WorkingWithData
{
    partial class StudyloadRebuild
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudyloadRebuild));
            this.panel2 = new System.Windows.Forms.Panel();
            this.RebuildedStudyload = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TransferButton = new System.Windows.Forms.Button();
            this.TeachersComboBox = new System.Windows.Forms.ComboBox();
            this.CancelRebuildButton = new System.Windows.Forms.Button();
            this.ConfirmRebuildButton = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.DisciplinesList = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.OldTeacherLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.RebuildedStudyload);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(354, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(341, 379);
            this.panel2.TabIndex = 1;
            // 
            // RebuildedStudyload
            // 
            this.RebuildedStudyload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RebuildedStudyload.Enabled = false;
            this.RebuildedStudyload.FormattingEnabled = true;
            this.RebuildedStudyload.Location = new System.Drawing.Point(0, 0);
            this.RebuildedStudyload.Name = "RebuildedStudyload";
            this.RebuildedStudyload.Size = new System.Drawing.Size(341, 379);
            this.RebuildedStudyload.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.CancelRebuildButton);
            this.panel1.Controls.Add(this.ConfirmRebuildButton);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(354, 379);
            this.panel1.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Location = new System.Drawing.Point(3, 229);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(341, 84);
            this.panel5.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TransferButton);
            this.groupBox1.Controls.Add(this.TeachersComboBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Оберіть нового викладача";
            // 
            // TransferButton
            // 
            this.TransferButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TransferButton.Location = new System.Drawing.Point(3, 58);
            this.TransferButton.Name = "TransferButton";
            this.TransferButton.Size = new System.Drawing.Size(335, 23);
            this.TransferButton.TabIndex = 2;
            this.TransferButton.Text = "Передати";
            this.TransferButton.UseVisualStyleBackColor = true;
            this.TransferButton.Click += new System.EventHandler(this.TransferButton_Click);
            // 
            // TeachersComboBox
            // 
            this.TeachersComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TeachersComboBox.Enabled = false;
            this.TeachersComboBox.FormattingEnabled = true;
            this.TeachersComboBox.Location = new System.Drawing.Point(3, 16);
            this.TeachersComboBox.Name = "TeachersComboBox";
            this.TeachersComboBox.Size = new System.Drawing.Size(335, 21);
            this.TeachersComboBox.TabIndex = 1;
            this.TeachersComboBox.SelectedIndexChanged += new System.EventHandler(this.TeachersComboBox_SelectedIndexChanged);
            // 
            // CancelRebuildButton
            // 
            this.CancelRebuildButton.Location = new System.Drawing.Point(190, 346);
            this.CancelRebuildButton.Name = "CancelRebuildButton";
            this.CancelRebuildButton.Size = new System.Drawing.Size(75, 23);
            this.CancelRebuildButton.TabIndex = 4;
            this.CancelRebuildButton.Text = "Відміна";
            this.CancelRebuildButton.UseVisualStyleBackColor = true;
            this.CancelRebuildButton.Click += new System.EventHandler(this.CancelRebuildButton_Click);
            // 
            // ConfirmRebuildButton
            // 
            this.ConfirmRebuildButton.Location = new System.Drawing.Point(79, 346);
            this.ConfirmRebuildButton.Name = "ConfirmRebuildButton";
            this.ConfirmRebuildButton.Size = new System.Drawing.Size(78, 23);
            this.ConfirmRebuildButton.TabIndex = 3;
            this.ConfirmRebuildButton.Text = "Підтвердити";
            this.ConfirmRebuildButton.UseVisualStyleBackColor = true;
            this.ConfirmRebuildButton.Click += new System.EventHandler(this.ConfirmRebuildButton_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.DisciplinesList);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 43);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(354, 180);
            this.panel4.TabIndex = 1;
            // 
            // DisciplinesList
            // 
            this.DisciplinesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisciplinesList.FormattingEnabled = true;
            this.DisciplinesList.Location = new System.Drawing.Point(0, 0);
            this.DisciplinesList.Name = "DisciplinesList";
            this.DisciplinesList.Size = new System.Drawing.Size(354, 180);
            this.DisciplinesList.TabIndex = 0;
            this.DisciplinesList.SelectedIndexChanged += new System.EventHandler(this.DisciplinesList_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.OldTeacherLabel);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(354, 43);
            this.panel3.TabIndex = 0;
            // 
            // OldTeacherLabel
            // 
            this.OldTeacherLabel.AutoSize = true;
            this.OldTeacherLabel.Location = new System.Drawing.Point(76, 16);
            this.OldTeacherLabel.Name = "OldTeacherLabel";
            this.OldTeacherLabel.Size = new System.Drawing.Size(0, 13);
            this.OldTeacherLabel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Викладач:";
            // 
            // StudyloadRebuild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 379);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StudyloadRebuild";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Перенаправити дисципліну іншому викладачу";
            this.Load += new System.EventHandler(this.StudyloadRebuild_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label OldTeacherLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox RebuildedStudyload;
        private System.Windows.Forms.Button ConfirmRebuildButton;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox TeachersComboBox;
        private System.Windows.Forms.ListBox DisciplinesList;
        private System.Windows.Forms.Button TransferButton;
        private System.Windows.Forms.Button CancelRebuildButton;
    }
}