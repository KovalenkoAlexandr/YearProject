using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using Shared;

namespace TreeNodeInMainMenu
{
	public partial class Form1 : Form
	{
		public static int[] masPositionNode = new int[4];
		public static string[] strWay = new string[7];

		ClassOptionsSeek obTypeSeek = new ClassOptionsSeek();
		ClassMaterials[] arrayMaterials = new ClassMaterials[50];
        Users ob = new Users();
        

		int countArrayMaterials = 0;
		int countNodes = 0;

		bool timerFlag = false;

		int[] masPositionElement = new int[4];

		public Form1(/*Users user*/ string fio, int id, int role)
		{

            ob.GetUser(fio, id, role);
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			loadDefoltTree();
            deleteNodes();
            getTypeForDocument();
		}

        private void OpenStripMenuItem_Click(object sender, EventArgs e)
		{
			seekFocusNode(Convert.ToString(treeView1.SelectedNode), 1);
		}

		private void DownloadStripMenuItem_Click(object sender, EventArgs e)
		{
			seekFocusNode(Convert.ToString(treeView1.SelectedNode), 2);
		}

		private void RedactStripMenuItem_Click(object sender, EventArgs e)
		{
			seekFocusNode(Convert.ToString(treeView1.SelectedNode), 3);
			timerFlag = true;
		}

		private void DeletStripMenuItem_Click(object sender, EventArgs e)
		{
			seekFocusNode(Convert.ToString(treeView1.SelectedNode), 4);
		}
		private void Button6_Click(object sender, EventArgs e)
		{
			bool tmpForSeek2 = false;
			countNodes = treeView1.GetNodeCount(tmpForSeek2);
			for (int i = 0; i < countNodes; i++) treeView1.Nodes[0].Remove();

			loadDefoltTree();
			getTypeForDocument();

			seekTreeNode(textBox2.Text);
			deleteCloseNode(textBox2.Text);
		}

		private void TextBox2_TextChanged(object sender, EventArgs e)
		{

		}

		// ----------------------------------------------------------------------------------------------

		void loadDefoltTree()
		{
			countArrayMaterials = 0;

            //string connStr = "server=localhost;user=root;database=diploma;password=s1233212;charset=cp1251";
            //MySqlConnection conn = new MySqlConnection(connectString());
            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

			string sql;
			if (obTypeSeek.sortFlag == false) sql = "SELECT * FROM disciplines;";
			else sql = "SELECT * FROM disciplines order by disciplines.discipline_name;";

			MySqlCommand command = new MySqlCommand(sql, conn);
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				treeView1.BeginUpdate();

				treeView1.Nodes.Add(reader[1].ToString());
				countNodes++;

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
									}

									bool secondTmp = false;
									int t = Convert.ToInt32(treeView1.Nodes[i].Nodes[j].GetNodeCount(secondTmp));

									for (int oneElement = 0; oneElement < Convert.ToInt32(treeView1.Nodes[i].Nodes[j].GetNodeCount(secondTmp)); oneElement++) // проход по под-подверткам, чтобы записать в нужное место еденицу информации, так как в одной таблице находится информация про папку и элементы в папке
									{

										if (treeView1.Nodes[i].Nodes[j].Nodes[oneElement].Text == reader[3].ToString())
										{
											treeView1.Nodes[i].Nodes[j].Nodes[oneElement].Nodes.Add(reader[4].ToString() + Path.GetExtension(reader[5].ToString())); // нахождение нужной позиции и создание под-под-подветки (последний уровень веток)

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

								if (ob.Id_role == 3)
								{
									redactStripMenuItem.Visible = false;
									deletStripMenuItem.Visible = false;
								}
								else
								{
									redactStripMenuItem.Visible = true;
									deletStripMenuItem.Visible = true;
								}
								continue;
							}
							else
							{
								if (tmpString.IndexOf("pdf") != -1)
								{
									treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-pdf-48.png";
									treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-pdf-48.png";
									treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

									if (ob.Id_role == 3)
									{
										redactStripMenuItem.Visible = false;
										deletStripMenuItem.Visible = false;
									}
									else
									{
										redactStripMenuItem.Visible = true;
										deletStripMenuItem.Visible = true;
									}
									continue;
								}
								else
								{
									if (tmpString.IndexOf("xls") != -1 || tmpString.IndexOf("xlsx") != -1 || tmpString.IndexOf("xlsm") != -1 || tmpString.IndexOf("xltx") != -1 || tmpString.IndexOf("xltm") != -1 || tmpString.IndexOf("xlsb") != -1 || tmpString.IndexOf("xlam") != -1 || tmpString.IndexOf("xla") != -1)
									{
										treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-microsoft-excel-48.png";
										treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-microsoft-excel-48.png";
										treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

										if (ob.Id_role == 3)
										{
											redactStripMenuItem.Visible = false;
											deletStripMenuItem.Visible = false;
										}
										else
										{
											redactStripMenuItem.Visible = true;
											deletStripMenuItem.Visible = true;
										}
										continue;
									}
									else
									{
										if (tmpString.IndexOf("pptx") != -1 || tmpString.IndexOf("ppt") != -1 || tmpString.IndexOf("pptm") != -1 || tmpString.IndexOf("ppsx") != -1 || tmpString.IndexOf("xltm") != -1 || tmpString.IndexOf("pps") != -1 || tmpString.IndexOf("ppsm") != -1 || tmpString.IndexOf("potx") != -1 || tmpString.IndexOf("pot") != -1 || tmpString.IndexOf("potm") != -1 || tmpString.IndexOf("ppam") != -1 || tmpString.IndexOf("ppa") != -1)
										{
											treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-microsoft-powerpoint-48.png";
											treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-microsoft-powerpoint-48.png";
											treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

											if (ob.Id_role == 3)
											{
												redactStripMenuItem.Visible = false;
												deletStripMenuItem.Visible = false;
											}
											else
											{
												redactStripMenuItem.Visible = true;
												deletStripMenuItem.Visible = true;
											}
											continue;
										}
										else
										{
											if (tmpString.IndexOf("zip") != -1 || tmpString.IndexOf("7z") != -1 || tmpString.IndexOf("rar") != -1 || tmpString.IndexOf("jap") != -1 || tmpString.IndexOf("gz") != -1)
											{
												treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-archive-folder-48.png";
												treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-archive-folder-48.png";
												treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

												if (ob.Id_role == 3)
												{
													redactStripMenuItem.Visible = false;
													deletStripMenuItem.Visible = false;
												}
												else
												{
													redactStripMenuItem.Visible = true;
													deletStripMenuItem.Visible = true;
												}
												continue;
											}
											else
											{
												if (tmpString.IndexOf("bmp") != -1 || tmpString.IndexOf("jpg") != -1 || tmpString.IndexOf("gif") != -1 || tmpString.IndexOf("png") != -1 || tmpString.IndexOf("tiff") != -1)
												{
													treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-image-file-48.png";
													treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-image-file-48.png";
													treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

													if (ob.Id_role == 3)
													{
														redactStripMenuItem.Visible = false;
														deletStripMenuItem.Visible = false;
													}
													else
													{
														redactStripMenuItem.Visible = true;
														deletStripMenuItem.Visible = true;
													}
													continue;
												}
												else
												{
													if (tmpString.IndexOf("mpg") != -1 || tmpString.IndexOf("mov") != -1 || tmpString.IndexOf("wmv") != -1 || tmpString.IndexOf("avi") != -1 || tmpString.IndexOf("mp4") != -1)
													{
														treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-video-file-48.png";
														treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-video-file-48.png";
														treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

														if (ob.Id_role == 3)
														{
															redactStripMenuItem.Visible = false;
															deletStripMenuItem.Visible = false;
														}
														else
														{
															redactStripMenuItem.Visible = true;
															deletStripMenuItem.Visible = true;
														}
														continue;
													}
													else
													{
														if (tmpString.IndexOf("wav") != -1 || tmpString.IndexOf("aif") != -1 || tmpString.IndexOf("mp3") != -1 || tmpString.IndexOf("mid") != -1)
														{
															treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-audio-file-48.png";
															treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-audio-file-48.png";
															treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

															if (ob.Id_role == 3)
															{
																redactStripMenuItem.Visible = false;
																deletStripMenuItem.Visible = false;
															}
															else
															{
																redactStripMenuItem.Visible = true;
																deletStripMenuItem.Visible = true;
															}
															continue;
														}
														else
														{
															treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].SelectedImageKey = "icons8-file-48.png";
															treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ImageKey = "icons8-file-48.png";
															treeView1.Nodes[tmpFirst].Nodes[tmpSecond].Nodes[tmpThird].Nodes[tmpFourth].ContextMenuStrip = contextMenuStrip1;

															if (ob.Id_role == 3)
															{
																redactStripMenuItem.Visible = false;
																deletStripMenuItem.Visible = false;
															}
															else
															{
																redactStripMenuItem.Visible = true;
																deletStripMenuItem.Visible = true;
															}
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

		void deleteNodes()
		{
			treeView1.Visible = false;

			bool tmpForSeek2 = false;
			countNodes = treeView1.GetNodeCount(tmpForSeek2);
			for (int i = 0; i < countNodes; i++) treeView1.Nodes[0].Remove();

			countNodes = 0;
			loadDefoltTree();
			getTypeForDocument();
			
				if (ob.Id_role == 2)
				{
					bool tmpForSeek = false;
					bool flagPosition = false;
					int position = 0;

					int first = treeView1.GetNodeCount(tmpForSeek);

					for (int i = 0; i < first; i++)
					{
						flagPosition = false;
						for (int j = 0; j < treeView1.Nodes[position].GetNodeCount(tmpForSeek); j++)
						{
							if (treeView1.Nodes[position].Nodes[j].Text == ob.Fio)
							{
								position++;
								flagPosition = true;
								break;
							}
						}
						if (flagPosition == false) treeView1.Nodes[position].Remove();
					}
				}

			treeView1.Visible = true;
		}

		void seekFocusNode(string tmp, int type)
		{
			string tmpSecond = tmp.Substring(10);

			tmpSecond = deleteTypeDocument(tmpSecond);

            //string connStr = "server=localhost;user=root;database=diploma;password=s1233212;charset=cp1251";
            //MySqlConnection conn = new MySqlConnection(connectString());
            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
			//string sql = "SELECT users.fio, subject.name_subject,nameofdocument.name_type_document, materials.name_doc_element, type.name_type, teg.teg_text FROM materials, nameofdocument, oneuser_onesubject, users, subject, oneteg, teg, type WHERE materials.id_name_document = nameofdocument.id_n_d and oneuser_onesubject.id_s_l = materials.id_study_load and users.id_u = oneuser_onesubject.id_user and subject.id_s = oneuser_onesubject.id_subject and materials.id_m = oneteg.id_materials and oneteg.id_teg = teg.id_teg and materials.id_type = type.id_t and materials.name_doc_element LIKE('%" + tmpSecond + "%');";
			string sql = "SELECT users.fio, disciplines.discipline_name, document_specification.name_specification, material.name_doc_element, type.name_type, material.ID_m FROM material, document_specification, studyload, users, disciplines, type WHERE material.id_specification = document_specification.id_d_s and studyload.id_s_l = material.id_study_load and users.id_u = studyload.id_user and disciplines.id_d = studyload.id_discipline and material.id_type = type.id_t and material.name_doc_element LIKE('%" + tmpSecond + "%');";
			
			MySqlCommand command = new MySqlCommand(sql, conn);
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				bool tmpFlag = false;

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
													if (type == 1) openFileInWay(reader[1].ToString(), reader[0].ToString(), reader[2].ToString(), reader[3].ToString());
													if (type == 2) downloadFileInWay(reader[1].ToString(), reader[0].ToString(), reader[2].ToString(), reader[3].ToString());
													if (type == 3)
													{
														//textBox1.Text = reader[1].ToString() + "\\" + reader[0].ToString() + "\\" + reader[2].ToString() + "\\" + reader[3].ToString() + " - редактирование";
														
															strWay[0] = reader[0].ToString();
															strWay[1] = reader[1].ToString();
															strWay[2] = reader[2].ToString();
															strWay[3] = reader[3].ToString();
															strWay[4] = reader[4].ToString();
															strWay[5] = reader[5].ToString();
															strWay[6] = Convert.ToString(ob.Id_role);
														
														FormRedaction formReddaction = new FormRedaction();
														formReddaction.StartPosition = FormStartPosition.CenterScreen;

														formReddaction.ShowDialog();

														masPositionElement[0] = i;
														masPositionElement[1] = j;
														masPositionElement[2] = m;
														masPositionElement[3] = n;


														tmpFlag = true;
													}
													if (type == 4) deleteDocument(i, j, m, n, reader[5].ToString());
												}
											}

											if (tmpFlag == true) break;
										}
									}

									if (tmpFlag == true) break;
								}
							}

							if (tmpFlag == true) break;
						}
					}

					if (tmpFlag == true) break;
				}

				if (tmpFlag == true) break;
			}
			reader.Close();
			conn.Close();
		}


		string deleteTypeDocument(string tmp)
		{
			string resTmp = "";
			int position = 0;

			for(int i = tmp.Length-1; i >= 0; i--)
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

		void openFileInWay(string way1, string way2, string way3, string way4)
		{
			//string connStr = "server=localhost;user=root;database=diploma;password=s1233212;charset=cp1251";
			//MySqlConnection conn = new MySqlConnection(connectString());
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
						Process.Start(reader[4].ToString());
					}
					catch
					{
						MessageBox.Show("Документ не знайдено!");
					}
					break;
				}
			}
			reader.Close();
			conn.Close();
		}

		void downloadFileInWay(string way1, string way2, string way3, string way4)
		{
			//string connStr = "server=localhost;user=root;database=diploma;password=s1233212;charset=cp1251";
			//MySqlConnection conn = new MySqlConnection(connectString());
            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

			string sql = "SELECT users.fio, disciplines.discipline_name, document_specification.name_specification, material.name_doc_element, material.way FROM material, document_specification, studyload, users, disciplines WHERE material.id_specification = document_specification.id_d_s and studyload.id_s_l = material.id_study_load and users.id_u = studyload.id_user and disciplines.id_d = studyload.id_discipline;";

			MySqlCommand command = new MySqlCommand(sql, conn);
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				if (way1 == reader[1].ToString() && way2 == reader[0].ToString() && way3 == reader[2].ToString() && way4 == reader[3].ToString())
				{
					MessageBox.Show(reader[4].ToString());
					//try
					//{
					//	Process.Start(reader[4].ToString());
					//}
					//catch
					//{
					//	MessageBox.Show("Документ не знайдено!");
					//}
					//break;
				}
			}
			reader.Close();
			conn.Close();
		}

		void seekTreeNode(string tmp)
		{
			bool tmpForSeek = false;

			for (int i = 0; i < treeView1.GetNodeCount(tmpForSeek); i++)
			{
				if(relatedWordsFor_materials(treeView1.Nodes[i].Text, tmp) == true)
				{
					if (treeView1.Nodes[i].IsExpanded == false) treeView1.Nodes[i].Toggle();
				}

				for (int j = 0; j < treeView1.Nodes[i].GetNodeCount(tmpForSeek); j++)
				{
					if (relatedWordsFor_materials(treeView1.Nodes[i].Nodes[j].Text, tmp) == true)
					{
						if (treeView1.Nodes[i].IsExpanded == true)
						{
							if (treeView1.Nodes[i].Nodes[j].IsExpanded == false) treeView1.Nodes[i].Nodes[j].Toggle();
						}
						else
						{
							treeView1.Nodes[i].Toggle();
							treeView1.Nodes[i].Nodes[j].Toggle();
						}
					}

					for (int m = 0; m < treeView1.Nodes[i].Nodes[j].GetNodeCount(tmpForSeek); m++)
					{
						if (relatedWordsFor_materials(treeView1.Nodes[i].Nodes[j].Nodes[m].Text, tmp) == true)
						{
							if (treeView1.Nodes[i].IsExpanded == true)
							{
								if (treeView1.Nodes[i].Nodes[j].IsExpanded == true)
								{
									if (treeView1.Nodes[i].Nodes[j].Nodes[m].IsExpanded == false) treeView1.Nodes[i].Nodes[j].Nodes[m].Toggle();
								}
								else
								{
									treeView1.Nodes[i].Toggle();
									treeView1.Nodes[i].Nodes[j].Toggle();
									treeView1.Nodes[i].Nodes[j].Nodes[m].Toggle();
								}
							}
							else treeView1.Nodes[i].Toggle();
						}

						for (int n = 0; n < treeView1.Nodes[i].Nodes[j].Nodes[m].GetNodeCount(tmpForSeek); n++)
						{
							if (relatedWordsFor_materials(treeView1.Nodes[i].Nodes[j].Nodes[m].Nodes[n].Text, tmp) == true)
							{
								if (treeView1.Nodes[i].IsExpanded == true)
								{
									if (treeView1.Nodes[i].Nodes[j].IsExpanded == true)
									{
										if (treeView1.Nodes[i].Nodes[j].Nodes[m].IsExpanded == true) continue;
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
		}

		void deleteCloseNode(string tmp)
		{
			bool tmpForSeek = false;
			int position = 0;
			

			int countFirst = treeView1.GetNodeCount(tmpForSeek);

			for (int i = 0; i < countFirst; i++)
			{
				if (treeView1.Nodes[position].IsExpanded == true || relatedWordsFor_materials(treeView1.Nodes[position].Text, tmp) == true) position++;
				else treeView1.Nodes[position].Remove();
			}
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

		void deleteDocument(int i, int j, int m, int n, string tmp)
		{
			DialogResult resDialog = MessageBox.Show("Ви дійсно бажаєте видалити документ - " + treeView1.Nodes[i].Nodes[j].Nodes[m].Nodes[n].Text + " ?", "Видалення документу", MessageBoxButtons.YesNo);
			if(resDialog == DialogResult.Yes)
			{
				treeView1.Nodes[i].Nodes[j].Nodes[m].Nodes[n].Remove();

				//string connStr = "server=localhost;user=root;database=diploma;password=s1233212;charset=cp1251";
				//MySqlConnection conn = new MySqlConnection(connectString());
                MySqlConnection conn = DBUtils.GetDBConnection();
                conn.Open();

				string sql = "DELETE FROM material WHERE ID_m = " + tmp + ";";
				MySqlCommand command = new MySqlCommand(sql, conn);
				command.ExecuteNonQuery();

				conn.Close();

				conn.Open();

				sql = "DELETE FROM material_tag WHERE ID_material = " + tmp + ";";
				command = new MySqlCommand(sql, conn);
				command.ExecuteNonQuery();

				conn.Close();

				MessageBox.Show("Документ видалено!");
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (timerFlag == true)
			{
				treeView1.Visible = false;

				deleteNodes();
				timerFlag = false;

				treeView1.Nodes[masPositionElement[0]].Toggle();
				treeView1.Nodes[masPositionElement[0]].Nodes[masPositionElement[1]].Toggle();
				//try
				//{
				//	treeView1.Nodes[masPositionElement[0]].Nodes[masPositionElement[1]].Nodes[masPositionElement[2]].Toggle();
				//}
				//catch
				//{
					
				//}

				treeView1.Visible = true;
			}
		}

		//string connectString()
		//{
		//	//string connStr = "server=localhost;user=root;database=diploma;password=audiomachine;charset=utf8";
  //          //string connStr = DBUtils.GetDBConnection();
  //          //return connStr;
		//}
	}
}
