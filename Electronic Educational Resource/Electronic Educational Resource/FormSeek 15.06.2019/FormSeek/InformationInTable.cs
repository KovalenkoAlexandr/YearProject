using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using Shared;

namespace FormSeek
{
	public partial class InformationInTable : Form
	{
		ClassInfoInTable[] masOb = new ClassInfoInTable[50];
		int countMasOb = 0;

		int []masTopCountSubject = new int[5] { 0, 0, 0, 0, 0 };
		int countSubject = 0;

		int[] masTopCountMaterials = new int[5] { 0, 0, 0, 0, 0 };
		int countMaterials = 0;

		int chartChoice;

		public InformationInTable()
		{
			InitializeComponent();
		}

		private void InformationInTable_Load(object sender, EventArgs e)
		{
			createMasOb();
			inputDataGridView();

			createChart1();
			createChart2();

			chart1.Visible = true;
			chart2.Visible = false;

			chartChoice = 1;
			chart1.Series["Дисципліни"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
		}

		public void createMasOb()
		{
			//string connStr = "server=localhost;user=root;database=diploma;password=s1233212;";
			MySqlConnection conn = DBUtils.GetDBConnection();
			conn.Open();

			string sql = "SELECT users.id_u, users.fio FROM users, roles WHERE roles.id_r = users.id_role and roles.id_r = 2;";

			MySqlCommand command = new MySqlCommand(sql, conn);
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				masOb[countMasOb] = new ClassInfoInTable();
				masOb[countMasOb].id_fio = Convert.ToInt32(reader[0]);
				masOb[countMasOb].fio = reader[1].ToString();
				countMasOb++;
			}
			reader.Close();

			sql = "SELECT users.id_u, users.fio, count(disciplines.discipline_name) FROM users, studyload, disciplines WHERE users.id_u = studyload.id_user and disciplines.id_d = studyload.id_discipline GROUP BY users.fio;";

			command = new MySqlCommand(sql, conn);
			reader = command.ExecuteReader();

			while (reader.Read())
			{
				for (int i = 0; i < countMasOb; i++)
				{
					if (Convert.ToInt32(reader[2]) > 0)
					{
						if (masOb[i].id_fio == Convert.ToInt32(reader[0]))
						{
							masOb[i].countSubject = Convert.ToInt32(reader[2]);
							inputMasTopSubject(Convert.ToInt32(reader[2]));
						}
					}
					else break;
				}
			}
			reader.Close();

			sql = "SELECT users.id_u, users.fio, count(material.name_doc_element) FROM users, studyload, disciplines, material WHERE users.id_u = studyload.id_user and disciplines.id_d = studyload.id_discipline and  studyload.id_s_l = material.id_study_load GROUP BY users.fio;";

			command = new MySqlCommand(sql, conn);
			reader = command.ExecuteReader();

			while (reader.Read())
			{
				for (int i = 0; i < countMasOb; i++)
				{
					if (Convert.ToInt32(reader[2]) > 0)
					{
						if (masOb[i].id_fio == Convert.ToInt32(reader[0]))
						{
							masOb[i].countMaterials = Convert.ToInt32(reader[2]);
							inputMasTopMaterials(Convert.ToInt32(reader[2]));
						}
					}
					else break;
				}
			}
			reader.Close();
			conn.Close();
		}

		public void inputDataGridView()
		{
			for (int i = 0; i < countMasOb; i++)
			{
				dataGridView1.Rows.Add(masOb[i].fio, masOb[i].countSubject, masOb[i].countMaterials);
			}
		}

		public void createChart1()
		{
			countSubject = 0;
			for (int i = 0; i < countMasOb; i++)
			{
				if (masOb[i].countSubject > masTopCountSubject[4] && countSubject < 10)
				{
					chart1.Series["Дисципліни"].Points.AddXY(masOb[i].fio, masOb[i].countSubject);
					countSubject++;
				}
				else
				{
					if (countSubject > 11) break;
				}
			}
		}

		public void createChart2()
		{
			countMaterials = 0;
			for (int i = 0; i < countMasOb; i++)
			{
				if (masOb[i].countMaterials > masTopCountMaterials[4] && countMaterials < 10)
				{
					chart2.Series["Документи"].Points.AddXY(masOb[i].fio, masOb[i].countMaterials);
					countMaterials++;
				}
				else
				{
					if (countMaterials > 11) break;
				}
			}
		}

		private void chart1_Click(object sender, EventArgs e)
		{

		}

		private void ToolStripMenuItem2_Click(object sender, EventArgs e)
		{

		}

		private void ToolStripMenuItem3_Click(object sender, EventArgs e)
		{

		}

		public void changeChart()
		{
			if (chartChoice == 1)
			{
				chart1.Visible = true;
				chart2.Visible = false;
			}
			else
			{
				chart2.Visible = true;
				chart1.Visible = false;
			}
		}

		private void pieToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			chartChoice = 1;
			chart1.Series["Дисципліни"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

			changeChart();
		}

		private void doughnutToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			chartChoice = 1;
			chart1.Series["Дисципліни"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;

			changeChart();
		}

		private void pointToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			chartChoice = 1;
			chart1.Series["Дисципліни"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

			changeChart();
		}

		private void columnToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			chartChoice = 1;
			chart1.Series["Дисципліни"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

			changeChart();
		}

		private void barToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			chartChoice = 1;
			chart1.Series["Дисципліни"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
			changeChart();
		}

		private void pieToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			chartChoice = 2;
			chart2.Series["Документи"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

			changeChart();
		}

		private void doughnutToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			chartChoice = 2;
			chart2.Series["Документи"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;

			changeChart();
		}

		private void columnToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			chartChoice = 2;
			chart2.Series["Документи"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

			changeChart();
		}

		private void pointToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			chartChoice = 2;
			chart2.Series["Документи"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

			changeChart();
		}

		private void barToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			chartChoice = 2;
			chart2.Series["Документи"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;

			changeChart();
		}

		private void ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Hide();
		}

		void inputMasTopMaterials(int tmp)
		{
			if(countMaterials < 5)
			{
				masTopCountMaterials[countMaterials] = tmp;
				countMaterials++;
			}
			else
			{
				sortMasTopMaterials();
				if (tmp > masTopCountMaterials[0])
				{
					masTopCountMaterials[4] = masTopCountMaterials[3];
					masTopCountMaterials[3] = masTopCountMaterials[2];
					masTopCountMaterials[2] = masTopCountMaterials[1];
					masTopCountMaterials[1] = masTopCountMaterials[0];
					masTopCountMaterials[0] = tmp;
				}
			}
		}

		void sortMasTopMaterials()
		{
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					if(masTopCountMaterials[j] < masTopCountMaterials[j+1])
					{
						int tmp = masTopCountMaterials[j];
						masTopCountMaterials[j] = masTopCountMaterials[j+1];
						masTopCountMaterials[j] = tmp;
					}
				}
			}
		}

		void inputMasTopSubject(int tmp)
		{
			if (countSubject < 5)
			{
				masTopCountSubject[countSubject] = tmp;
				countSubject++;
			}
			else
			{
				sortMasTopSubject();
				if (tmp > masTopCountSubject[0])
				{
					masTopCountSubject[4] = masTopCountSubject[3];
					masTopCountSubject[3] = masTopCountSubject[2];
					masTopCountSubject[2] = masTopCountSubject[1];
					masTopCountSubject[1] = masTopCountSubject[0];
					masTopCountSubject[0] = tmp;
				}

			}
		}

		void sortMasTopSubject()
		{
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					if (masTopCountSubject[j] < masTopCountSubject[j+1])
					{
						int tmp = masTopCountSubject[j];
						masTopCountSubject[j] = masTopCountSubject[j+1];
						masTopCountSubject[j+1] = tmp;
					}
				}
			}
		}

		string connectString()
		{
			string connStr = "server=localhost;user=root;database=diploma;password=audiomachine;charset=utf8";
			return connStr;
		}
	}
}