using System;
using System.Diagnostics;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using System.IO;
using Shared;

namespace FormSeek
{
    public partial class Form1 : Form
	{
		ClassOptionsSeek obTypeSeek = new ClassOptionsSeek();
		ClassMaterials[] arrayMaterials = new ClassMaterials[50];
		ClassCountElementsInTreeView countElements = new ClassCountElementsInTreeView();

        string forlderpath = ProjectData.GetProjectFolder();

        int countArrayMaterials = 0;

		int focusType = 0;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			textBox2.Visible = false;

			checkBox1.Checked = true;
			checkBox2.Checked = true;
			checkBox3.Checked = true;
			checkBox4.Checked = true;
			checkBox5.Checked = true;

			defaultOptions();

			loadDefoltTree();
			getTypeForDocument();

			infomationAboutElement();
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			obTypeSeek.radioButton = 1;

			groupBox3.Enabled = true;

			if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false) button1.Enabled = false;
			else button1.Enabled = true;
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			obTypeSeek.radioButton = 2;

			button1.Enabled = true;
			groupBox3.Enabled = false;
		}

		private void radioButton3_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButton3.Checked == true) obTypeSeek.radioButtonSecond = 1;
			else obTypeSeek.radioButtonSecond = 2;

			bool tmpForSeek = false;
			int countNodes = treeView1.GetNodeCount(tmpForSeek);
			for (int i = 0; i < countNodes; i++) treeView1.Nodes[0].Remove();

			loadDefoltTree();
			getTypeForDocument();

			if (obTypeSeek.radioButtonSecond == 1)
			{
				if (textBox1.Text.Length != 0)
				{
					if (checkBox1.Checked == true || checkBox2.Checked == true || checkBox3.Checked == true || checkBox4.Checked == true || checkBox5.Checked == true) buttonClickFunction();
				}
			}
		}

		private void radioButton4_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButton4.Checked == true) obTypeSeek.radioButtonSecond = 2;
			else obTypeSeek.radioButtonSecond = 1;

			if (obTypeSeek.radioButtonSecond == 2)
			{
				if (textBox1.Text.Length != 0)
				{
					if (checkBox1.Checked == true || checkBox2.Checked == true || checkBox3.Checked == true || checkBox4.Checked == true || checkBox5.Checked == true) buttonClickFunction();
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (textBox1.Text.Length != 0)
			{
				treeView1.Visible = false;
				buttonClickFunction();
				treeView1.Visible = true;
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			bool tmp = checkBox5.Checked;
			if (checkBox1.Checked == false) obTypeSeek.inputCheckBox(false, 0);
			else obTypeSeek.inputCheckBox(true, 0);

			if (checkBox1.Checked == false && checkBox5.Checked == true) checkOtherCheckBox();

			if (checkBox1.Checked == true && checkBox2.Checked == true && checkBox3.Checked == true && checkBox4.Checked == true && checkBox5.Checked == false) checkBox5.Checked = true;
			if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false) button1.Enabled = false;
			else button1.Enabled = true;
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox2.Checked == false) obTypeSeek.inputCheckBox(false, 1);
			else obTypeSeek.inputCheckBox(true, 1);

			if (checkBox2.Checked == false && checkBox5.Checked == true) checkOtherCheckBox();

			if (checkBox1.Checked == true && checkBox2.Checked == true && checkBox3.Checked == true && checkBox4.Checked == true && checkBox5.Checked == false) checkBox5.Checked = true;
			if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false) button1.Enabled = false;
			else button1.Enabled = true;
		}

		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox3.Checked == false) obTypeSeek.inputCheckBox(false, 2);
			else obTypeSeek.inputCheckBox(true, 2);

			if (checkBox3.Checked == false && checkBox5.Checked == true) checkOtherCheckBox();

			if (checkBox1.Checked == true && checkBox2.Checked == true && checkBox3.Checked == true && checkBox4.Checked == true && checkBox5.Checked == false) checkBox5.Checked = true;
			if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false) button1.Enabled = false;
			else button1.Enabled = true;
		}

		private void checkBox4_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox4.Checked == false) obTypeSeek.inputCheckBox(false, 3);
			else obTypeSeek.inputCheckBox(true, 3);

			if (checkBox4.Checked == false && checkBox5.Checked == true) checkOtherCheckBox();

			if (checkBox1.Checked == true && checkBox2.Checked == true && checkBox3.Checked == true && checkBox4.Checked == true && checkBox5.Checked == false) checkBox5.Checked = true;
			if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false) button1.Enabled = false;
			else button1.Enabled = true;
		}

		private void checkBox5_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox5.Checked == false)
			{
				if (checkBox1.Checked == true && checkBox2.Checked == true && checkBox3.Checked == true && checkBox4.Checked == true)
				{
					button1.Enabled = false;

					obTypeSeek.inputCheckBox(false, false, false, false, false);

					checkBox1.Checked = false;
					checkBox2.Checked = false;
					checkBox3.Checked = false;
					checkBox4.Checked = false;
				}
			}
			else
			{
				obTypeSeek.inputCheckBox(true, true, true, true, true);

				checkBox1.Checked = true;
				checkBox2.Checked = true;
				checkBox3.Checked = true;
				checkBox4.Checked = true;
			}
		}

		private void checkBox6_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox6.Checked == true) obTypeSeek.masMessage[0] = true;
			else obTypeSeek.masMessage[0] = false;
		}

		private void checkBox7_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox7.Checked == true) obTypeSeek.masMessage[1] = true;
			else obTypeSeek.masMessage[1] = false;
		}

		private void openMenuItem_Click(object sender, EventArgs e)
		{
			focusType = 1;
			if (treeView1.SelectedNode != null) seekFocusNode(Convert.ToString(treeView1.SelectedNode));
		}

		private void downloadMenuItem_Click(object sender, EventArgs e)
		{
			focusType = 2;
			if (treeView1.SelectedNode != null) seekFocusNode(Convert.ToString(treeView1.SelectedNode));
		}

		private void button2_Click(object sender, EventArgs e)
		{
			treeView1.CollapseAll();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			bool tmpForSeek = false;
			int countNodes = treeView1.GetNodeCount(tmpForSeek);
			for (int i = 0; i < countNodes; i++) treeView1.Nodes[0].Remove();

			loadDefoltTree();
			getTypeForDocument();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (obTypeSeek.sortFlag == false) obTypeSeek.sortFlag = true;
			else obTypeSeek.sortFlag = false;

			bool tmpForSeek = false;
			int countNodes = treeView1.GetNodeCount(tmpForSeek);
			for (int i = 0; i < countNodes; i++) treeView1.Nodes[0].Remove();

			loadDefoltTree();
			getTypeForDocument();

		}

		private void ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			InformationInTable form2 = new InformationInTable();

			Hide();

			form2.StartPosition = FormStartPosition.CenterScreen;
			form2.ShowDialog();
			Show();
		}

		private void ToolStripMenuItem3_Click(object sender, EventArgs e)
		{
			InformationAboutProgram form3 = new InformationAboutProgram();

			Hide();
			form3.StartPosition = FormStartPosition.CenterScreen;
			form3.ShowDialog();
			Show();
			//try
			//{
			//	Process.Start("C:\\Users\\AlGord\\Desktop\\Пояснювальна записка, розділ з охорони праці.docx");
			//}
			//catch
			//{
			//	MessageBox.Show("Документ не знайдено!");
			//}
		}

		void defaultOptions()
		{
			obTypeSeek.radioButton = 1;
			obTypeSeek.radioButtonSecond = 1;
		}

		void buttonClickFunction()
		{
			if (obTypeSeek.radioButtonSecond == 2)
			{

				bool tmpForSeek = false;
				int countNodes = treeView1.GetNodeCount(tmpForSeek);
				for (int i = 0; i < countNodes; i++) treeView1.Nodes[0].Remove();

				loadDefoltTree();
				getTypeForDocument();
			}

			treeView1.CollapseAll(); // СВАРАЧИВАЕТ ВСЕ УЗЛЫ

			if (obTypeSeek.masCheckBox[4] == true || obTypeSeek.radioButton == 2)
			{
				//treeView1.ExpandAll(); РАЗВОРАЧИВАЕТ ВСЕ УЗЛЫ

				treeView1.Visible = false;
				if (obTypeSeek.radioButton == 1)
				{
					if (seekByName(textBox1.Text) == false)
					{
						if (checkOptionsForMessageNotFound() == true) MessageBox.Show("По даному запиту - \"" + textBox1.Text + "\" нічного не знайдено!");
					}
					else
					{
						showFoundNodes(true, textBox1.Text);
					}
				}
				else
				{
					int resSeekByKeywords = seekByKeywords(textBox1.Text);
					if (resSeekByKeywords == 0)
					{
						if (checkOptionsForMessageNotFound() == true) MessageBox.Show("По даному запиту  - \"" + textBox1.Text + "\" нічного не знайдено!");
					}
					else
					{
						if (resSeekByKeywords > 1)
						{
							if (checkOptionsForMessageFound() == true) MessageBox.Show("По даному запиту  - \"" + textBox1.Text + "\" знайдено декілька матеріалів(" + resSeekByKeywords + ")!");
						}
						showFoundNodes(false, textBox1.Text);
					}
				}

				treeView1.Visible = true;
			}
			else
			{
				int[] masResSeekWithParameters = new int[4];

				if (obTypeSeek.masCheckBox[0] == true)
				{
					masResSeekWithParameters[0] = Convert.ToInt32(seekByName_subject(textBox1.Text));
				}

				if (obTypeSeek.masCheckBox[1] == true)
				{
					masResSeekWithParameters[1] = Convert.ToInt32(seekByName_name(textBox1.Text));
				}

				if (obTypeSeek.masCheckBox[2] == true)
				{
					masResSeekWithParameters[2] = Convert.ToInt32(seekByName_nameofdocument(textBox1.Text));
				}

				if (obTypeSeek.masCheckBox[3] == true)
				{
					masResSeekWithParameters[3] = Convert.ToInt32(seekByName_materials(textBox1.Text));
				}

				int[] masFlagForSeek = new int[4];
				int countMasFlagForSeek = 0;

				for (int i = 0; i < obTypeSeek.masCheckBox.Length; i++)
				{
					if (obTypeSeek.masCheckBox[i] == true)
					{
						masFlagForSeek[countMasFlagForSeek++] = masResSeekWithParameters[i];
					}
				}

				int resSeek = 0;

				for (int i = 0; i < countMasFlagForSeek; i++)
				{
					if (masFlagForSeek[i] > 0) resSeek += masFlagForSeek[i];
				}

				if (resSeek == 0)
				{
					if (checkOptionsForMessageNotFound() == true)  MessageBox.Show("По даному запиту  - \"" + textBox1.Text + "\" нічного не знайдено!");
				}
				else
				{
					if (resSeek > 1)
					{
						if (checkOptionsForMessageFound() == true) MessageBox.Show("По даному запиту  - \"" + textBox1.Text + "\" знайдено декілька матеріалів(" + resSeek + ")!"); ;
					}

					showFoundNodes(obTypeSeek.masCheckBox[0], textBox1.Text);
				}
			}
		}

		void checkOtherCheckBox()
		{
			checkBox5.Checked = false;
			obTypeSeek.inputCheckBox(false, 4);
		}

		void infomationAboutElement()
		{
			countElements.countAllElements = countElements.countSubject + countElements.countName + countElements.countTypeDocuments + countElements.countDocuments;
			label1_1.Text = Convert.ToString(countElements.countAllElements);
			label2_1.Text = Convert.ToString(countElements.countSubject);
			label3_1.Text = Convert.ToString(countElements.countName);
			label4_1.Text = Convert.ToString(countElements.countTypeDocuments);
			label5_1.Text = Convert.ToString(countElements.countDocuments);
		}

		// ---------------------------------------------------------------- Построение дерева
		void loadDefoltTree()
		{
			countArrayMaterials = 0; // --------------------------------------------------------------------------------------------------------------------

			// ----------------------------------------------------------- Отображение Предметов
			//string connStr = "server=localhost;user=root;database=diploma;password=s1233212;charset=cp1251";
			MySqlConnection conn = DBUtils.GetDBConnection();
			conn.Open();

			string sql;
			if(obTypeSeek.sortFlag == false) sql = "SELECT * FROM disciplines;";
			else sql = "SELECT * FROM disciplines order by disciplines.discipline_name;";

			MySqlCommand command = new MySqlCommand(sql, conn);
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				treeView1.BeginUpdate();

				treeView1.Nodes.Add(reader[1].ToString());

				countElements.countSubject++;
				treeView1.EndUpdate();

			}
			reader.Close();
			conn.Close();
			// ----------------------------------------------------------------------------------------------------------------------

			// ----------------------------------------------------------- Отображение Преподавателей
			conn.Open();
			//sql = "SELECT users.fio, oneuser_onesubject.id_user, oneuser_onesubject.id_subject, subject.name_subject FROM users, oneuser_onesubject, subject WHERE users.id_u = oneuser_onesubject.id_user and oneuser_onesubject.id_subject = subject.id_s;";

			sql = "SELECT users.fio, studyload.id_user, studyload.id_discipline, disciplines.discipline_name FROM users, studyload, disciplines WHERE users.id_u = studyload.id_user and studyload.id_discipline = disciplines.id_d;";

			command = new MySqlCommand(sql, conn);
			reader = command.ExecuteReader();

			while (reader.Read())
			{
				bool countFirstNodesFlag = false;
				int countFirstNodes = Convert.ToInt32(treeView1.GetNodeCount(countFirstNodesFlag));
				int positionNodes = 0;

				treeView1.BeginUpdate();

				for (int i = 0; i < countFirstNodes; i++)
				{
					if (treeView1.Nodes[i].Text == reader[3].ToString())
					{
						positionNodes = i;
						break;
					}
				}

				treeView1.Nodes[positionNodes].Nodes.Add(reader[0].ToString());

				arrayMaterials[countArrayMaterials] = new ClassMaterials();
				arrayMaterials[countArrayMaterials].name = reader[0].ToString();
				arrayMaterials[countArrayMaterials++].discipline = reader[3].ToString();

				countElements.countName++;

				treeView1.EndUpdate();
			}
			reader.Close();
			conn.Close();
			// ----------------------------------------------------------------------------------------------------------------------

			// ----------------------------------------------------------- Отображение Материалов
			conn.Open();

			sql = "SELECT users.fio,studyload.id_s_l,disciplines.discipline_name, document_specification.name_specification, material.name_doc_element, material.way FROM material, document_specification, studyload, users, disciplines, type WHERE material.id_specification = document_specification.id_d_s and studyload.id_s_l = material.id_study_load and users.id_u = studyload.id_user and disciplines.id_d = studyload.id_discipline and material.id_type = type.id_t;";
			command = new MySqlCommand(sql, conn);
			reader = command.ExecuteReader();

			while (reader.Read())
			{
				bool writeNode = false;

				bool countFirstNodesFlag = false;
				int countFirstNodes = Convert.ToInt32(treeView1.GetNodeCount(countFirstNodesFlag));
				bool tmp = false;
				int tmpCount = 0; // количесво веток 

				treeView1.BeginUpdate();

				for (int i = 0; i < countFirstNodes; i++) // проход по всем основным папкам (дисциплины) count - подсчет всех дисциплин
				{
					tmpCount = Convert.ToInt32(treeView1.Nodes[i].GetNodeCount(tmp)); // подсчет количесва веток
					for (int j = 0; j < tmpCount; j++) // проход по подветкам (группам)
					{
						if (treeView1.Nodes[i].Text == reader[2].ToString() && treeView1.Nodes[i].Nodes[j].Text == reader[0].ToString()) // проверка на нужную подветку (на нужную группу)
						{
							for (int countArray = 0; countArray < countArrayMaterials; countArray++)
							{
								if (arrayMaterials[countArray].name == reader[0].ToString() && arrayMaterials[countArray].discipline == reader[2].ToString())
								{
									if (arrayMaterials[countArray].checkPrint(reader[3].ToString()) == false)
									{
										arrayMaterials[countArray].printToMaterials(reader[3].ToString());
										treeView1.Nodes[i].Nodes[j].Nodes.Add(reader[3].ToString());

										countElements.countTypeDocuments++;
									}

									bool secondTmp = false;
									int t = Convert.ToInt32(treeView1.Nodes[i].Nodes[j].GetNodeCount(secondTmp));

									for (int oneElement = 0; oneElement < Convert.ToInt32(treeView1.Nodes[i].Nodes[j].GetNodeCount(secondTmp)); oneElement++) // проход по под-подверткам, чтобы записать в нужное место еденицу информации, так как в одной таблице находится информация про папку и элементы в папке
									{

										if (treeView1.Nodes[i].Nodes[j].Nodes[oneElement].Text == reader[3].ToString())
										{
											treeView1.Nodes[i].Nodes[j].Nodes[oneElement].Nodes.Add(reader[4].ToString() + Path.GetExtension(reader[5].ToString())); // нахождение нужной позиции и создание под-под-подветки (последний уровень веток)

											countElements.countDocuments++;

											writeNode = true;
											break;
										}
									}
								}
							}
						}
					}

					if (writeNode == true) break;
				}
				treeView1.EndUpdate();
			}

			reader.Close();
			conn.Close();
			// ----------------------------------------------------------------------------------------------------------------------
		}

		void getTypeForDocument()
		{
			bool countFirstNodesFlag = false;
			int countFirstNodes = Convert.ToInt32(treeView1.GetNodeCount(countFirstNodesFlag));

			treeView1.BeginUpdate();

			for (int tmpFirst = 0; tmpFirst < countFirstNodes; tmpFirst++)
			{
				bool countSecondNodesFlag = false;
				int countSecondNodes = Convert.ToInt32(treeView1.Nodes[tmpFirst].GetNodeCount(countSecondNodesFlag));

				for (int tmpSecond = 0; tmpSecond < countSecondNodes; tmpSecond++)
				{
					bool countThirdNodesFlag = false;
					int countThirdNodes = Convert.ToInt32(treeView1.Nodes[tmpFirst].Nodes[tmpSecond].GetNodeCount(countThirdNodesFlag));
					for (int tmpThird = 0; tmpThird < countThirdNodes; tmpThird++)
					{
						bool countFourthNodesFlag = false;
						int countFourthNodes = Convert.ToInt32(treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].GetNodeCount(countFourthNodesFlag));
						for (int tmpFourth = 0; tmpFourth < countFourthNodes; tmpFourth++)
						{
							string tmpString = treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].Text;

							if (tmpString.IndexOf("doc") != -1 || tmpString.IndexOf("docx") != -1 || tmpString.IndexOf("docm") != -1 || tmpString.IndexOf("dotm") != -1 || tmpString.IndexOf("dot") != -1 || tmpString.IndexOf("dotx") != -1)
							{
								treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-microsoft-word-48.png";
								treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-microsoft-word-48.png";
								treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

								continue;
							}
							else
							{
								if (tmpString.IndexOf("pdf") != -1)
								{
									treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-pdf-48.png";
									treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-pdf-48.png";
									treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

									continue;
								}
								else
								{
									if (tmpString.IndexOf("xls") != -1 || tmpString.IndexOf("xlsx") != -1 || tmpString.IndexOf("xlsm") != -1 || tmpString.IndexOf("xltx") != -1 || tmpString.IndexOf("xltm") != -1 || tmpString.IndexOf("xlsb") != -1 || tmpString.IndexOf("xlam") != -1 || tmpString.IndexOf("xla") != -1)
									{
										treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-microsoft-excel-48.png";
										treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-microsoft-excel-48.png";
										treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

										continue;
									}
									else
									{
										if (tmpString.IndexOf("pptx") != -1 || tmpString.IndexOf("ppt") != -1 || tmpString.IndexOf("pptm") != -1 || tmpString.IndexOf("ppsx") != -1 || tmpString.IndexOf("xltm") != -1 || tmpString.IndexOf("pps") != -1 || tmpString.IndexOf("ppsm") != -1 || tmpString.IndexOf("potx") != -1 || tmpString.IndexOf("pot") != -1 || tmpString.IndexOf("potm") != -1 || tmpString.IndexOf("ppam") != -1 || tmpString.IndexOf("ppa") != -1)
										{
											treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-microsoft-powerpoint-48.png";
											treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-microsoft-powerpoint-48.png";
											treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

											continue;
										}
										else
										{
											if (tmpString.IndexOf("zip") != -1 || tmpString.IndexOf("7z") != -1 || tmpString.IndexOf("rar") != -1 || tmpString.IndexOf("jap") != -1 || tmpString.IndexOf("gz") != -1)
											{
												treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-archive-folder-48.png";
												treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-archive-folder-48.png";
												treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

												continue;
											}
											else
											{
												if (tmpString.IndexOf("bmp") != -1 || tmpString.IndexOf("jpg") != -1 || tmpString.IndexOf("gif") != -1 || tmpString.IndexOf("png") != -1 || tmpString.IndexOf("tiff") != -1)
												{
													treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-image-file-48.png";
													treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-image-file-48.png";
													treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

													continue;
												}
												else
												{
													if (tmpString.IndexOf("mpg") != -1 || tmpString.IndexOf("mov") != -1 || tmpString.IndexOf("wmv") != -1 || tmpString.IndexOf("avi") != -1 || tmpString.IndexOf("mp4") != -1)
													{
														treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-video-file-48.png";
														treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-video-file-48.png";
														treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

														continue;
													}
													else
													{
														if (tmpString.IndexOf("wav") != -1 || tmpString.IndexOf("aif") != -1 || tmpString.IndexOf("mp3") != -1 || tmpString.IndexOf("mid") != -1)
														{
															treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-audio-file-48.png";
															treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-audio-file-48.png";
															treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;
															
															continue;
														}
														else
														{
															treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-file-48.png";
															treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-file-48.png";
															treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

															continue;
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			treeView1.EndUpdate();
		}
		// ---------------------------------------------------------------- Поиск по имени
		bool seekByName(string tmp)
		{
			int resAllElement = 0;
			resAllElement += seekByName_subject(tmp);
			resAllElement += seekByName_name(tmp);
			resAllElement += seekByName_nameofdocument(tmp);
			resAllElement += seekByName_materials(tmp);

			if(resAllElement != 0)
			{
				if (resAllElement > 1)
				{
					if (checkOptionsForMessageFound() == true) MessageBox.Show("По даному запиту - \"" + textBox1.Text + "\" знайдено декілька матеріалів(" + resAllElement + ")!");
					return true;
				}
				else return true;
			}
			return false;

			//if (resSeekByName_subject != 0)
			//{
			//	if (resSeekByName_subject > 1)
			//	{
					
			//	}
			//	return true;
			//}
			//else
			//{
			//	int resSeekByName_name = seekByName_name(tmp);
			//	if (resSeekByName_name != 0)
			//	{
			//		if (resSeekByName_name > 1)
			//		{
			//			if (checkOptionsForMessageFound() == true) MessageBox.Show("По даному запиту - \"" + textBox1.Text + "\" знайдено декілька матеріалів(" + resSeekByName_name + ")!");
			//		}
			//		return true;
			//	}
			//	else
			//	{
			//		int resSeekByName_nameofdocument = seekByName_nameofdocument(tmp);
			//		if (resSeekByName_nameofdocument != 0)
			//		{
			//			if (resSeekByName_nameofdocument > 1)
			//			{
			//				if (checkOptionsForMessageFound() == true) MessageBox.Show("По даному запиту - \"" + textBox1.Text + "\" знайдено декілька матеріалів(" + resSeekByName_nameofdocument + ")!");
			//			}
			//			return true;
			//		}
			//		else
			//		{
			//			int resSeekByName_materials = seekByName_materials(tmp);
			//			if (resSeekByName_materials != 0)
			//			{
			//				if (resSeekByName_materials > 1)
			//				{
			//					if (checkOptionsForMessageFound() == true) MessageBox.Show("По даному запиту  - \"" + textBox1.Text + "\" знайдено декілька матеріалів(" + resSeekByName_materials + ")!");
			//				}
			//				return true;
			//			}
			//			else return false;
			//		}
			//	}
			//}
		}

		int seekByName_subject(string tmp)
		{
			bool tmpForSeek = false;
			int FlagFindElement = 0;

			for (int i = 0; i < treeView1.GetNodeCount(tmpForSeek); i++)
			{
				if (relatedWordsFor_materials(treeView1.Nodes[i].Text, tmp) == true)
				{
					treeView1.Nodes[i].Toggle();
					FlagFindElement++;
				}
			}

			return FlagFindElement;
		}

		int seekByName_name(string tmp)
		{
			int FlagFindElement = 0;
			bool tmpForSeek = false;

			for (int i = 0; i < treeView1.GetNodeCount(tmpForSeek); i++)
			{
				for (int j = 0; j < treeView1.Nodes[i].GetNodeCount(tmpForSeek); j++)
				{	
					if (relatedWordsFor_materials(treeView1.Nodes[i].Nodes[j].Text, tmp) == true)
					{
						FlagFindElement++;

						if (treeView1.Nodes[i].IsExpanded == true) treeView1.Nodes[i].Nodes[j].Toggle();
						else
						{
							treeView1.Nodes[i].Toggle();
							treeView1.Nodes[i].Nodes[j].Toggle();
						}
					}
				}
			}

			return FlagFindElement;
		}

		int seekByName_nameofdocument(string tmp)
		{
			int FlagFindElement = 0;
			
			bool tmpForSeek = false;
			for (int i = 0; i < treeView1.GetNodeCount(tmpForSeek); i++)
			{
				for (int j = 0; j < treeView1.Nodes[i].GetNodeCount(tmpForSeek); j++)
				{
					for (int m = 0; m < treeView1.Nodes[i].Nodes[j].GetNodeCount(tmpForSeek); m++)
					{
						if (relatedWordsFor_materials(treeView1.Nodes[i].Nodes[j].Nodes[m].Text, tmp) == true)
						{
							FlagFindElement++;

							if (treeView1.Nodes[i].IsExpanded == true)
							{
								if (treeView1.Nodes[i].Nodes[j].IsExpanded == true) treeView1.Nodes[i].Nodes[j].Nodes[m].Toggle();
								else
								{
									treeView1.Nodes[i].Nodes[j].Toggle();
									treeView1.Nodes[i].Nodes[j].Nodes[m].Toggle();
								}
							}
							else
							{
								treeView1.Nodes[i].Toggle();
								treeView1.Nodes[i].Nodes[j].Toggle();
								treeView1.Nodes[i].Nodes[j].Nodes[m].Toggle();
							}
						}
					}
				}
			}

			return FlagFindElement;
		}

		int seekByName_materials(string tmp)
		{
			int FlagFindElement = 0;

			//int []masNumberLastElement = new int[3] { 0, 0, 0 };

			bool tmpForSeek = false;
			for (int i = 0; i < treeView1.GetNodeCount(tmpForSeek); i++)
			{
				for (int j = 0; j < treeView1.Nodes[i].GetNodeCount(tmpForSeek); j++)
				{
					for (int m = 0; m < treeView1.Nodes[i].Nodes[j].GetNodeCount(tmpForSeek); m++)
					{
						for (int n = 0; n < treeView1.Nodes[i].Nodes[j].Nodes[m].GetNodeCount(tmpForSeek); n++)
						{
							if (relatedWordsFor_materials(treeView1.Nodes[i].Nodes[j].Nodes[m].Nodes[n].Text, tmp) == true)
							{
								FlagFindElement++;

								if (treeView1.Nodes[i].IsExpanded == true)
								{
									if (treeView1.Nodes[i].Nodes[j].IsExpanded == true)
									{
										if(treeView1.Nodes[i].Nodes[j].Nodes[m].IsExpanded == true) treeView1.Nodes[i].Nodes[j].Nodes[m].Nodes[n].Toggle();
										else
										{
											treeView1.Nodes[i].Nodes[j].Nodes[m].Toggle();
											treeView1.Nodes[i].Nodes[j].Nodes[m].Nodes[n].Toggle();
										}
									}
									else
									{
										treeView1.Nodes[i].Nodes[j].Toggle();
										treeView1.Nodes[i].Nodes[j].Nodes[m].Toggle();
										treeView1.Nodes[i].Nodes[j].Nodes[m].Nodes[n].Toggle();
									}
								}
								else
								{
									treeView1.Nodes[i].Toggle();
									treeView1.Nodes[i].Nodes[j].Toggle();
									treeView1.Nodes[i].Nodes[j].Nodes[m].Toggle();
									treeView1.Nodes[i].Nodes[j].Nodes[m].Nodes[n].Toggle();
								}
							}
						}	
					}
				}
			}

			return FlagFindElement;
		}

		string relatedWords(string tmp)
		{
			string[] tmpStr_1_3 = tmp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			string tmpStr_1_3_2 = "";
			foreach (string i in tmpStr_1_3)
			{
				tmpStr_1_3_2 += i;
			}

			return tmpStr_1_3_2;
		}

		bool relatedWordsFor_materials(string firstStr, string secondStr)
		{
			string tmpStr_1_1 = firstStr.ToLower();
			string tmpStr_2_1 = secondStr.ToLower();

			string[] tmpStr_1_3 = tmpStr_1_1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			string tmpStr_1_3_2 = "";
			foreach (string i in tmpStr_1_3)
			{
				tmpStr_1_3_2 += i;
			}
			string[] tmpStr_2_3 = tmpStr_2_1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			string tmpStr_2_3_2 = "";
			foreach (string i in tmpStr_2_3)
			{
				tmpStr_2_3_2 += i;
			}

			if (firstStr == secondStr) return true;
			if (tmpStr_1_1 == tmpStr_2_1) return true;
			if (tmpStr_1_3_2 == tmpStr_2_3_2) return true;
			if (tmpStr_1_1.IndexOf(tmpStr_2_1) != -1) return true;
			if (tmpStr_1_3_2.IndexOf(tmpStr_2_3_2) != -1) return true;

			return false;
		}

		int checkTypeDocument(string tmp)
		{
			int indexOfSubstring = -1;

			//string connStr = "server=localhost;user=root;database=diploma;password=s1233212;charset=cp1251";
			MySqlConnection conn = DBUtils.GetDBConnection();
			conn.Open();

			string sql = "SELECT * FROM type;";

			MySqlCommand command = new MySqlCommand(sql, conn);
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				indexOfSubstring = tmp.IndexOf(reader[1].ToString());

				if (indexOfSubstring != -1) break;


			}
			reader.Close();
			conn.Close();

			return indexOfSubstring;
		}

		string createTypeForDocument(string tmp)
		{
			int position = 0;
			for (int i = tmp.Length - 1; i > 0; i--)
			{
				if (tmp[i] == '.')
				{
					position = i;
					break;
				}
			}
			tmp = tmp.Substring(0, position);
			return tmp;
		}
		// ---------------------------------------------------------------- Поиск по ключевым словам
		int seekByKeywords(string tmp)
		{
			int FlagFindElement = 0;

			//string connStr = "server=localhost;user=root;database=diploma;password=s1233212;charset=cp1251";
			MySqlConnection conn = DBUtils.GetDBConnection();
			conn.Open();
			//sql = "SELECT users.fio, subject.name_subject,nameofdocument.name_type_document, materials.name_doc_element, type.name_type, teg.teg_text FROM materials, nameofdocument, oneuser_onesubject, users, subject, oneteg, teg, type WHERE materials.id_name_document = nameofdocument.id_n_d and oneuser_onesubject.id_s_l = materials.id_study_load and users.id_u = oneuser_onesubject.id_user and subject.id_s = oneuser_onesubject.id_subject and materials.id_m = oneteg.id_materials and oneteg.id_teg = teg.id_teg and materials.id_type = type.id_t and teg.teg_text LIKE('%" + textBox1.Text + "%'); ";
			string sql = "SELECT disciplines.discipline_name, users.fio, document_specification.name_specification, material.Name_doc_element, material_tag.ID_material FROM users, disciplines, studyload, material, document_specification, material_tag, tags WHERE document_specification.ID_d_s = material.ID_specification and users.ID_u = studyload.ID_user and disciplines.ID_d = studyload.ID_discipline and studyload.ID_s_l = material.ID_study_load and material.ID_m = material_tag.ID_material and material_tag.ID_tag = tags.ID_tag and tags.Tag_name LIKE('%" + relatedWords(textBox1.Text) + "%');";

			MySqlCommand command = new MySqlCommand(sql, conn);
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				bool tmpForSeek = false;
				for (int i = 0; i < treeView1.GetNodeCount(tmpForSeek); i++)
				{
					if (relatedWordsFor_materials(treeView1.Nodes[i].Text, reader[0].ToString()) == true)
					{
						for (int j = 0; j < treeView1.Nodes[i].GetNodeCount(tmpForSeek); j++)
						{
							if (relatedWordsFor_materials(treeView1.Nodes[i].Nodes[j].Text, reader[1].ToString()) == true)
							{
								for (int m = 0; m < treeView1.Nodes[i].Nodes[j].GetNodeCount(tmpForSeek); m++)
								{
									if (treeView1.Nodes[i].Nodes[j].Nodes[m].Text == reader[2].ToString())
									{
										for (int n = 0; n < treeView1.Nodes[i].Nodes[j].Nodes[m].GetNodeCount(tmpForSeek); n++)
										{
											string tmpForReader = reader[3].ToString() + "." + Path.GetExtension(reader[4].ToString());

											if (relatedWordsFor_materials(treeView1.Nodes[i].Nodes[j].Nodes[m].Nodes[n].Text, tmpForReader) == true)
											{
												FlagFindElement++;
												
												if (treeView1.Nodes[i].Nodes[j].IsExpanded == true)
												{
													if (treeView1.Nodes[i].Nodes[j].Nodes[m].IsExpanded == true) treeView1.Nodes[i].Nodes[j].Nodes[m].Nodes[n].Toggle();
													else
													{
														treeView1.Nodes[i].Nodes[j].Nodes[m].Toggle();
														treeView1.Nodes[i].Nodes[j].Nodes[m].Nodes[n].Toggle();
													}
												}
												else
												{
													treeView1.Nodes[i].Toggle();
													treeView1.Nodes[i].Nodes[j].Toggle();
													treeView1.Nodes[i].Nodes[j].Nodes[m].Toggle();
													treeView1.Nodes[i].Nodes[j].Nodes[m].Nodes[n].Toggle();
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			reader.Close();
			conn.Close();

			return FlagFindElement;
		}
		// ---------------------------------------------------------------- Удаление не найденых узлов дерева
		void showFoundNodes(bool flagFirstLevel, string tmp)
		{
			bool tmpForSeek = false;
			int countNodes = treeView1.GetNodeCount(tmpForSeek);

			if (obTypeSeek.radioButtonSecond == 2)
			{
				tmpForSeek = false;
				countNodes = treeView1.GetNodeCount(tmpForSeek);

				int positionRemove = 0;

				for (int i = 0; i < countNodes; i++)
				{
					if (treeView1.Nodes[positionRemove].IsExpanded == true || (flagFirstLevel == true && relatedWordsFor_materials(treeView1.Nodes[positionRemove].Text, tmp))) positionRemove++;
					else treeView1.Nodes[positionRemove].Remove();

				}
			}
		}
		
		bool checkOptionsForMessageFound()
		{
			if (checkBox6.Checked == true) return true;
			else return false;
		}

		bool checkOptionsForMessageNotFound()
		{
			if (checkBox7.Checked == true) return true;
			else return false;
		}

		void seekFocusNode(string tmp)
		{
			string tmpSecond = tmp.Substring(10);

			tmpSecond = deleteTypeDocument(tmpSecond);
			
			//string connStr = "server=localhost;user=root;database=diploma;password=s1233212;charset=cp1251";
			MySqlConnection conn = DBUtils.GetDBConnection();
			conn.Open();
			//string sql = "SELECT users.fio, subject.name_subject,nameofdocument.name_type_document, materials.name_doc_element, type.name_type, teg.teg_text FROM materials, nameofdocument, oneuser_onesubject, users, subject, oneteg, teg, type WHERE materials.id_name_document = nameofdocument.id_n_d and oneuser_onesubject.id_s_l = materials.id_study_load and users.id_u = oneuser_onesubject.id_user and subject.id_s = oneuser_onesubject.id_subject and materials.id_m = oneteg.id_materials and oneteg.id_teg = teg.id_teg and materials.id_type = type.id_t and materials.name_doc_element LIKE('%" + tmpSecond + "%');";
			string sql = "SELECT users.fio, disciplines.discipline_name, document_specification.name_specification, material.name_doc_element, type.name_type, material.way FROM material, document_specification, studyload, users, disciplines, type WHERE material.id_specification = document_specification.id_d_s and studyload.id_s_l = material.id_study_load and users.id_u = studyload.id_user and disciplines.id_d = studyload.id_discipline and material.id_type = type.id_t and material.name_doc_element LIKE('%" + tmpSecond + "%');";

			MySqlCommand command = new MySqlCommand(sql, conn);
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				bool tmpForSeek = false;
				for (int i = 0; i < treeView1.GetNodeCount(tmpForSeek); i++)
				{
					if (treeView1.Nodes[i].Text == reader[1].ToString())
					{
						for (int j = 0; j < treeView1.Nodes[i].GetNodeCount(tmpForSeek); j++)
						{
							if (treeView1.Nodes[i].Nodes[j].Text == reader[0].ToString())
							{
								for (int m = 0; m < treeView1.Nodes[i].Nodes[j].GetNodeCount(tmpForSeek); m++)
								{
									if (treeView1.Nodes[i].Nodes[j].Nodes[m].Text == reader[2].ToString())
									{
										for (int n = 0; n < treeView1.Nodes[i].Nodes[j].Nodes[m].GetNodeCount(tmpForSeek); n++)
										{
											if (tmpSecond == reader[3].ToString())
											{
												if (treeView1.Nodes[i].Nodes[j].Nodes[m].Nodes[n].IsSelected == true)
												{
													//textBox2.Text = reader[1].ToString() + "\\" + reader[0].ToString() + "\\" + reader[2].ToString() + "\\" + reader[3].ToString();
													if (focusType == 1) openFileInWay(reader[1].ToString(), reader[0].ToString(), reader[2].ToString(), reader[3].ToString(), Path.GetExtension(reader[5].ToString()));
													if (focusType == 2) downloadFileInWay(reader[1].ToString(), reader[0].ToString(), reader[2].ToString(), reader[3].ToString(), Path.GetExtension(reader[5].ToString()));
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			reader.Close();
			conn.Close();
		}

		string deleteTypeDocument(string tmp)
		{
			string resTmp = "";
			int position = 0;

			for (int i = tmp.Length - 1; i >= 0; i--)
			{
				if (tmp[i] == '.')
				{
					position = i;
					break;
				}
			}

			resTmp = tmp.Substring(0, position);

			return resTmp;
		}

		void openFileInWay(string way1, string way2, string way3, string way4, string way5)
		{
			//string connStr = "server=localhost;user=root;database=diploma;password=123;charset=cp1251";
			MySqlConnection conn = DBUtils.GetDBConnection();
			conn.Open();

			string sql = "SELECT users.fio, disciplines.discipline_name, document_specification.name_specification, material.name_doc_element, material.way FROM material, document_specification, studyload, users, disciplines WHERE material.id_specification = document_specification.id_d_s and studyload.id_s_l = material.id_study_load and users.id_u = studyload.id_user and disciplines.id_d = studyload.id_discipline;";

			MySqlCommand command = new MySqlCommand(sql, conn);
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				if (way1 == reader[1].ToString() && way2 == reader[0].ToString() && way3 == reader[2].ToString() && way4 == reader[3].ToString())
				{
					try
					{
						FileInfo file = new FileInfo(reader[4].ToString());
						file.CopyTo(forlderpath + file.Name, true);

						FileInfo fileCope = new FileInfo(forlderpath + file.Name);

						Process tmp = Process.Start(fileCope.FullName);

						while (!tmp.HasExited)
						{
							tmp.Refresh();
						}

						fileCope.Delete();
					}
					catch (Exception tmp)
					{
						//MessageBox.Show(tmp.Message);
					}
					break;
				}
			}
			reader.Close();
			conn.Close();
		}

		void downloadFileInWay(string way1, string way2, string way3, string way4, string way5)
		{
            long FileSize = 0;
			//string connStr = "server=localhost;user=root;database=diploma;password=123;charset=cp1251";
			MySqlConnection conn = DBUtils.GetDBConnection();
			conn.Open();

			string sql = "SELECT users.fio, disciplines.discipline_name, document_specification.name_specification, material.name_doc_element, material.way FROM material, document_specification, studyload, users, disciplines WHERE material.id_specification = document_specification.id_d_s and studyload.id_s_l = material.id_study_load and users.id_u = studyload.id_user and disciplines.id_d = studyload.id_discipline;";

			MySqlCommand command = new MySqlCommand(sql, conn);
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				if (way1 == reader[1].ToString() && way2 == reader[0].ToString() && way3 == reader[2].ToString() && way4 == reader[3].ToString())
				{
					Stream myStream;
					SaveFileDialog saveFileDialog1 = new SaveFileDialog();

					saveFileDialog1.FileName = reader[3].ToString();
					saveFileDialog1.Filter = "files (*" + way5 + ")|*" + way5;

					saveFileDialog1.RestoreDirectory = true;

					if (saveFileDialog1.ShowDialog() == DialogResult.OK)
					{
						//MessageBox.Show(saveFileDialog1.FileName);

						FileInfo file = new FileInfo(reader[4].ToString());
                        FileSize = file.Length;
                        file.CopyTo(saveFileDialog1.FileName, true);
                        FileInfo newfile = new FileInfo(saveFileDialog1.FileName);
                        if (newfile.Exists && newfile.Length == FileSize)
                        {
                            MessageBox.Show("Файл було успішно завантажено." +
                                "Розміщення файлу: " + newfile.FullName);
                        }
                        else
                        {
                            MyMessages.WarningMessage("Файл було завантажено з помилками. " +
                                "Будь ласка повторіть завантаження");
                        }
                    }


				}
			}
			reader.Close();
			conn.Close();
		}

		private void ToolStripMenuItem4_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(forlderpath + @"\Довідник до програми (Електронний освітній ресурс).chm");
			}
			catch
			{
				MessageBox.Show(" Документ не знайдено!");
			}
		}
        /*
		string connectString()
		{
			string connStr = "server=localhost;user=root;database=diploma;password=audiomachine;charset=utf8";
			return connStr;
		}
        */
		private void ToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			Close();
		}

	}

}
