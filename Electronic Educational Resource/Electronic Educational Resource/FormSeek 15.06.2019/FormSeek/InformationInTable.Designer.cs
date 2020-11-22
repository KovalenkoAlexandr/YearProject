namespace FormSeek
{
	partial class InformationInTable
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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem2_1 = new System.Windows.Forms.ToolStripMenuItem();
			this.pieToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.doughnotToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.pointToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.columnToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.barToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.типДіаграмиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pieToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.doughnotToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.pointToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.columnToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.barToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem1,
            this.ToolStripMenuItem2,
            this.ToolStripMenuItem3});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(1483, 28);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// ToolStripMenuItem1
			// 
			this.ToolStripMenuItem1.Image = global::FormSeek.Properties.Resources.icons8_import_32;
			this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
			this.ToolStripMenuItem1.Size = new System.Drawing.Size(132, 24);
			this.ToolStripMenuItem1.Text = "Повернутися";
			this.ToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
			// 
			// ToolStripMenuItem2
			// 
			this.ToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem2_1});
			this.ToolStripMenuItem2.Image = global::FormSeek.Properties.Resources.icons8_bar_chart_32;
			this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
			this.ToolStripMenuItem2.Size = new System.Drawing.Size(343, 24);
			this.ToolStripMenuItem2.Text = "Відобразити кількість дисциплін на графіку";
			this.ToolStripMenuItem2.Click += new System.EventHandler(this.ToolStripMenuItem2_Click);
			// 
			// ToolStripMenuItem2_1
			// 
			this.ToolStripMenuItem2_1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pieToolStripMenuItem1,
            this.doughnotToolStripMenuItem1,
            this.pointToolStripMenuItem1,
            this.columnToolStripMenuItem1,
            this.barToolStripMenuItem1});
			this.ToolStripMenuItem2_1.Name = "ToolStripMenuItem2_1";
			this.ToolStripMenuItem2_1.Size = new System.Drawing.Size(216, 26);
			this.ToolStripMenuItem2_1.Text = "Тип діграми";
			// 
			// pieToolStripMenuItem1
			// 
			this.pieToolStripMenuItem1.Image = global::FormSeek.Properties.Resources.pie;
			this.pieToolStripMenuItem1.Name = "pieToolStripMenuItem1";
			this.pieToolStripMenuItem1.Size = new System.Drawing.Size(150, 26);
			this.pieToolStripMenuItem1.Text = "Pie";
			this.pieToolStripMenuItem1.ToolTipText = "Графік у вигляді кола";
			this.pieToolStripMenuItem1.Click += new System.EventHandler(this.pieToolStripMenuItem1_Click);
			// 
			// doughnotToolStripMenuItem1
			// 
			this.doughnotToolStripMenuItem1.Image = global::FormSeek.Properties.Resources.doughnut;
			this.doughnotToolStripMenuItem1.Name = "doughnotToolStripMenuItem1";
			this.doughnotToolStripMenuItem1.Size = new System.Drawing.Size(150, 26);
			this.doughnotToolStripMenuItem1.Text = "Doughnut";
			this.doughnotToolStripMenuItem1.ToolTipText = "Графік у вигляді кола з отвором";
			this.doughnotToolStripMenuItem1.Click += new System.EventHandler(this.doughnutToolStripMenuItem1_Click);
			// 
			// pointToolStripMenuItem1
			// 
			this.pointToolStripMenuItem1.Image = global::FormSeek.Properties.Resources.point;
			this.pointToolStripMenuItem1.Name = "pointToolStripMenuItem1";
			this.pointToolStripMenuItem1.Size = new System.Drawing.Size(150, 26);
			this.pointToolStripMenuItem1.Text = "Point";
			this.pointToolStripMenuItem1.ToolTipText = "Графік у вигляді крапок";
			this.pointToolStripMenuItem1.Click += new System.EventHandler(this.pointToolStripMenuItem1_Click);
			// 
			// columnToolStripMenuItem1
			// 
			this.columnToolStripMenuItem1.Image = global::FormSeek.Properties.Resources.column;
			this.columnToolStripMenuItem1.Name = "columnToolStripMenuItem1";
			this.columnToolStripMenuItem1.Size = new System.Drawing.Size(150, 26);
			this.columnToolStripMenuItem1.Text = "Column";
			this.columnToolStripMenuItem1.ToolTipText = "Графік у вигляді стовпців";
			this.columnToolStripMenuItem1.Click += new System.EventHandler(this.columnToolStripMenuItem1_Click);
			// 
			// barToolStripMenuItem1
			// 
			this.barToolStripMenuItem1.Image = global::FormSeek.Properties.Resources.bar;
			this.barToolStripMenuItem1.Name = "barToolStripMenuItem1";
			this.barToolStripMenuItem1.Size = new System.Drawing.Size(150, 26);
			this.barToolStripMenuItem1.Text = "Bar";
			this.barToolStripMenuItem1.ToolTipText = "Графік у вигляді рядків";
			this.barToolStripMenuItem1.Click += new System.EventHandler(this.barToolStripMenuItem1_Click);
			// 
			// ToolStripMenuItem3
			// 
			this.ToolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.типДіаграмиToolStripMenuItem});
			this.ToolStripMenuItem3.Image = global::FormSeek.Properties.Resources.icons8_bar_chart_32;
			this.ToolStripMenuItem3.Name = "ToolStripMenuItem3";
			this.ToolStripMenuItem3.Size = new System.Drawing.Size(348, 24);
			this.ToolStripMenuItem3.Text = "Відобразити кількість документів на графіку";
			this.ToolStripMenuItem3.Click += new System.EventHandler(this.ToolStripMenuItem3_Click);
			// 
			// типДіаграмиToolStripMenuItem
			// 
			this.типДіаграмиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pieToolStripMenuItem2,
            this.doughnotToolStripMenuItem2,
            this.pointToolStripMenuItem2,
            this.columnToolStripMenuItem2,
            this.barToolStripMenuItem2});
			this.типДіаграмиToolStripMenuItem.Name = "типДіаграмиToolStripMenuItem";
			this.типДіаграмиToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
			this.типДіаграмиToolStripMenuItem.Text = "Тип діаграми";
			// 
			// pieToolStripMenuItem2
			// 
			this.pieToolStripMenuItem2.Image = global::FormSeek.Properties.Resources.pie;
			this.pieToolStripMenuItem2.Name = "pieToolStripMenuItem2";
			this.pieToolStripMenuItem2.Size = new System.Drawing.Size(150, 26);
			this.pieToolStripMenuItem2.Text = "Pie";
			this.pieToolStripMenuItem2.ToolTipText = "Графік у вигляді кола";
			this.pieToolStripMenuItem2.Click += new System.EventHandler(this.pieToolStripMenuItem2_Click);
			// 
			// doughnotToolStripMenuItem2
			// 
			this.doughnotToolStripMenuItem2.Image = global::FormSeek.Properties.Resources.doughnut;
			this.doughnotToolStripMenuItem2.Name = "doughnotToolStripMenuItem2";
			this.doughnotToolStripMenuItem2.Size = new System.Drawing.Size(150, 26);
			this.doughnotToolStripMenuItem2.Text = "Doughnut";
			this.doughnotToolStripMenuItem2.ToolTipText = "Графік у вигляді кола з отвором";
			this.doughnotToolStripMenuItem2.Click += new System.EventHandler(this.doughnutToolStripMenuItem2_Click);
			// 
			// pointToolStripMenuItem2
			// 
			this.pointToolStripMenuItem2.Image = global::FormSeek.Properties.Resources.point;
			this.pointToolStripMenuItem2.Name = "pointToolStripMenuItem2";
			this.pointToolStripMenuItem2.Size = new System.Drawing.Size(150, 26);
			this.pointToolStripMenuItem2.Text = "Point";
			this.pointToolStripMenuItem2.ToolTipText = "Графік у вигляді крапок ";
			this.pointToolStripMenuItem2.Click += new System.EventHandler(this.pointToolStripMenuItem2_Click);
			// 
			// columnToolStripMenuItem2
			// 
			this.columnToolStripMenuItem2.Image = global::FormSeek.Properties.Resources.column;
			this.columnToolStripMenuItem2.Name = "columnToolStripMenuItem2";
			this.columnToolStripMenuItem2.Size = new System.Drawing.Size(150, 26);
			this.columnToolStripMenuItem2.Text = "Column";
			this.columnToolStripMenuItem2.ToolTipText = "Графік у вигляді стовпців ";
			this.columnToolStripMenuItem2.Click += new System.EventHandler(this.columnToolStripMenuItem2_Click);
			// 
			// barToolStripMenuItem2
			// 
			this.barToolStripMenuItem2.Image = global::FormSeek.Properties.Resources.bar;
			this.barToolStripMenuItem2.Name = "barToolStripMenuItem2";
			this.barToolStripMenuItem2.Size = new System.Drawing.Size(150, 26);
			this.barToolStripMenuItem2.Text = "Bar";
			this.barToolStripMenuItem2.ToolTipText = "Графік у вигляді рядків";
			this.barToolStripMenuItem2.Click += new System.EventHandler(this.barToolStripMenuItem2_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeColumns = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
			this.dataGridView1.Location = new System.Drawing.Point(16, 33);
			this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridView1.Size = new System.Drawing.Size(747, 506);
			this.dataGridView1.TabIndex = 4;
			// 
			// Column1
			// 
			this.Column1.HeaderText = "Викладач";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Column1.Width = 150;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "Кількість дисциплін ";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Column2.Width = 150;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "Кількість викладених документів";
			this.Column3.Name = "Column3";
			this.Column3.ReadOnly = true;
			this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Column3.Width = 200;
			// 
			// chart1
			// 
			this.chart1.BorderlineColor = System.Drawing.Color.Black;
			this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
			chartArea1.Name = "ChartArea1";
			this.chart1.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.chart1.Legends.Add(legend1);
			this.chart1.Location = new System.Drawing.Point(771, 33);
			this.chart1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.chart1.Name = "chart1";
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
			series1.Legend = "Legend1";
			series1.Name = "Дисципліни";
			this.chart1.Series.Add(series1);
			this.chart1.Size = new System.Drawing.Size(696, 506);
			this.chart1.TabIndex = 5;
			this.chart1.Text = "chart1";
			title1.Name = "Title1";
			title1.Text = "Дисципліни";
			this.chart1.Titles.Add(title1);
			// 
			// chart2
			// 
			this.chart2.BorderlineColor = System.Drawing.Color.Black;
			this.chart2.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
			chartArea2.Name = "ChartArea1";
			this.chart2.ChartAreas.Add(chartArea2);
			legend2.Name = "Legend1";
			this.chart2.Legends.Add(legend2);
			this.chart2.Location = new System.Drawing.Point(771, 33);
			this.chart2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.chart2.Name = "chart2";
			series2.ChartArea = "ChartArea1";
			series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
			series2.Legend = "Legend1";
			series2.Name = "Документи";
			this.chart2.Series.Add(series2);
			this.chart2.Size = new System.Drawing.Size(696, 506);
			this.chart2.TabIndex = 6;
			this.chart2.Text = "chart2";
			title2.Name = "Title1";
			title2.Text = "Документи";
			this.chart2.Titles.Add(title2);
			// 
			// InformationInTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1483, 554);
			this.Controls.Add(this.chart2);
			this.Controls.Add(this.chart1);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "InformationInTable";
			this.Text = "Графічне представлення даних";
			this.Load += new System.EventHandler(this.InformationInTable_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem2_1;
		private System.Windows.Forms.ToolStripMenuItem pieToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem doughnotToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem pointToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem columnToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem barToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem типДіаграмиToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pieToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem doughnotToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem pointToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem columnToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem barToolStripMenuItem2;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
		private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}