namespace TreeNodeInMainMenu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redactStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(12, 51);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 1;
            this.treeView1.Size = new System.Drawing.Size(293, 387);
            this.treeView1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-folder-48(2).png");
            this.imageList1.Images.SetKeyName(1, "icons8-opened-folder-48(2).png");
            this.imageList1.Images.SetKeyName(2, "icons8-microsoft-word-48.png");
            this.imageList1.Images.SetKeyName(3, "icons8-microsoft-powerpoint-48.png");
            this.imageList1.Images.SetKeyName(4, "icons8-microsoft-excel-48.png");
            this.imageList1.Images.SetKeyName(5, "icons8-pdf-48.png");
            this.imageList1.Images.SetKeyName(6, "icons8-archive-folder-48.png");
            this.imageList1.Images.SetKeyName(7, "icons8-audio-file-48.png");
            this.imageList1.Images.SetKeyName(8, "icons8-image-file-48.png");
            this.imageList1.Images.SetKeyName(9, "icons8-video-file-48.png");
            this.imageList1.Images.SetKeyName(10, "icons8-file-48.png");
            // 
            // treeView2
            // 
            this.treeView2.Location = new System.Drawing.Point(12, 51);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(293, 387);
            this.treeView2.TabIndex = 6;
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 92);
            // 
            // openStripMenuItem
            // 
            this.openStripMenuItem.Name = "openStripMenuItem";
            this.openStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.openStripMenuItem.Text = "Відкрити";
            this.openStripMenuItem.Click += new System.EventHandler(this.OpenStripMenuItem_Click);
            // 
            // downloadStripMenuItem
            // 
            this.downloadStripMenuItem.Name = "downloadStripMenuItem";
            this.downloadStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.downloadStripMenuItem.Text = "Зберегти";
            this.downloadStripMenuItem.Click += new System.EventHandler(this.DownloadStripMenuItem_Click);
            // 
            // redactStripMenuItem
            // 
            this.redactStripMenuItem.Name = "redactStripMenuItem";
            this.redactStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.redactStripMenuItem.Text = "Редагувати";
            this.redactStripMenuItem.Click += new System.EventHandler(this.RedactStripMenuItem_Click);
            // 
            // deletStripMenuItem
            // 
            this.deletStripMenuItem.Name = "deletStripMenuItem";
            this.deletStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.deletStripMenuItem.Text = "Видалити";
            this.deletStripMenuItem.Click += new System.EventHandler(this.DeletStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(311, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(477, 20);
            this.textBox1.TabIndex = 8;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 25);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(259, 20);
            this.textBox2.TabIndex = 9;
            this.textBox2.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(277, 25);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(28, 20);
            this.button6.TabIndex = 10;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 450);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.treeView2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.TreeView treeView2;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem openStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem downloadStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redactStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deletStripMenuItem;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Timer timer1;
	}
}

