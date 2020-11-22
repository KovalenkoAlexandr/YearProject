namespace WorkingWithData
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panel2 = new System.Windows.Forms.Panel();
			this.SearchField = new System.Windows.Forms.TextBox();
			this.SearchButton = new System.Windows.Forms.Button();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.downloadStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redactStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deletStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.statusStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(517, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.TabStop = true;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "notifyIcon1";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.Click += new System.EventHandler(this.NotifyIcon1_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
			this.statusStrip1.Location = new System.Drawing.Point(0, 180);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.statusStrip1.Size = new System.Drawing.Size(517, 29);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(151, 24);
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(155, 24);
			this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.treeView1);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 24);
			this.panel1.Margin = new System.Windows.Forms.Padding(4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(517, 156);
			this.panel1.TabIndex = 2;
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.treeView1.ImageIndex = 0;
			this.treeView1.ImageList = this.imageList1;
			this.treeView1.ItemHeight = 32;
			this.treeView1.Location = new System.Drawing.Point(0, 28);
			this.treeView1.Margin = new System.Windows.Forms.Padding(4);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = 1;
			this.treeView1.Size = new System.Drawing.Size(517, 128);
			this.treeView1.TabIndex = 1;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "icons8-folder-48.jpg");
			this.imageList1.Images.SetKeyName(1, "icons8-opened-folder-48.jpg");
			this.imageList1.Images.SetKeyName(2, "icons8-microsoft-word-48.png");
			this.imageList1.Images.SetKeyName(3, "icons8-microsoft-powerpoint-48.png");
			this.imageList1.Images.SetKeyName(4, "icons8-microsoft-excel-48.png");
			this.imageList1.Images.SetKeyName(5, "icons8-pdf-48.png");
			this.imageList1.Images.SetKeyName(6, "icons8-archive-48.png");
			this.imageList1.Images.SetKeyName(7, "icons8-audio-file-48.png");
			this.imageList1.Images.SetKeyName(8, "icons8-image-file-48.png");
			this.imageList1.Images.SetKeyName(9, "icons8-video-file-48.png");
			this.imageList1.Images.SetKeyName(10, "icons8-file-48.png");
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.SearchField);
			this.panel2.Controls.Add(this.SearchButton);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Margin = new System.Windows.Forms.Padding(4);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(517, 28);
			this.panel2.TabIndex = 0;
			// 
			// SearchField
			// 
			this.SearchField.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SearchField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.SearchField.Location = new System.Drawing.Point(0, 0);
			this.SearchField.Margin = new System.Windows.Forms.Padding(4);
			this.SearchField.Name = "SearchField";
			this.SearchField.Size = new System.Drawing.Size(472, 30);
			this.SearchField.TabIndex = 1;
			this.SearchField.Enter += new System.EventHandler(this.SearchField_Enter);
			this.SearchField.Leave += new System.EventHandler(this.SearchField_Leave);
			// 
			// SearchButton
			// 
			this.SearchButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SearchButton.Image = ((System.Drawing.Image)(resources.GetObject("SearchButton.Image")));
			this.SearchButton.Location = new System.Drawing.Point(472, 0);
			this.SearchButton.Margin = new System.Windows.Forms.Padding(4);
			this.SearchButton.Name = "SearchButton";
			this.SearchButton.Size = new System.Drawing.Size(45, 28);
			this.SearchButton.TabIndex = 0;
			this.SearchButton.UseVisualStyleBackColor = true;
			this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openStripMenuItem,
            this.downloadStripMenuItem,
            this.redactStripMenuItem,
            this.deletStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(155, 100);
			// 
			// openStripMenuItem
			// 
			this.openStripMenuItem.Name = "openStripMenuItem";
			this.openStripMenuItem.Size = new System.Drawing.Size(154, 24);
			this.openStripMenuItem.Text = "Відкрити";
			this.openStripMenuItem.Click += new System.EventHandler(this.openStripMenuItem_Click);
			// 
			// downloadStripMenuItem
			// 
			this.downloadStripMenuItem.Name = "downloadStripMenuItem";
			this.downloadStripMenuItem.Size = new System.Drawing.Size(154, 24);
			this.downloadStripMenuItem.Text = "Зберегти";
			this.downloadStripMenuItem.Click += new System.EventHandler(this.downloadStripMenuItem_Click);
			// 
			// redactStripMenuItem
			// 
			this.redactStripMenuItem.Name = "redactStripMenuItem";
			this.redactStripMenuItem.Size = new System.Drawing.Size(154, 24);
			this.redactStripMenuItem.Text = "Редагувати";
			this.redactStripMenuItem.Click += new System.EventHandler(this.redactStripMenuItem_Click);
			// 
			// deletStripMenuItem
			// 
			this.deletStripMenuItem.Name = "deletStripMenuItem";
			this.deletStripMenuItem.Size = new System.Drawing.Size(154, 24);
			this.deletStripMenuItem.Text = "Видалити";
			this.deletStripMenuItem.Click += new System.EventHandler(this.deletStripMenuItem_Click);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(517, 209);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Електронний освітній ресурс";
			this.Activated += new System.EventHandler(this.Form1_Activated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox SearchField;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redactStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timer1;
    }
}

