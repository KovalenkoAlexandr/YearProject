using MySql.Data.MySqlClient;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WorkingWithData
{
    public partial class DataDeletion : Form
    {
        string targetdirectory = @"\\ALEX\Common\";
        string iconpath = ProjectData.GetProjectFolder() + @"\EER icons\";
        int id_role = -1;
        string DelDiscpStandartText = "Введіть назву дисципліни для пошуку";
        string DelUserStandartText = "Введіть прізвище користувача для пошуку";
        Users _user;
        public DataDeletion(Users user)
        {
            _user = user;
            InitializeComponent();
        }

        private void DataDeletion_Load(object sender, EventArgs e)
        {
            DeleteUserPageInitialSize();
        }

        public String ReplaceCharInString(String str, int index, Char newSymb)
        {
            return str.Remove(index, 1).Insert(index, newSymb.ToString());
        }

        private void RemoveDisciline()
        {
            string discipline = null;
            int number = -1;
            string error = null;
            int id_discip = -1;
            DialogResult affirmative = DialogResult.None;
            if (DelDiscipList.SelectedIndex == -1 && String.IsNullOrEmpty(DelDiscpTextBox.Text.Trim()))
            {
                MessageBox.Show("Спочатку оберіть дисципліну");
            }
            else
            {
                discipline = DelDiscipList.SelectedItem.ToString();
                number = GetNumberOfDocuments(discipline);
                if (number <= 0)
                {
                    affirmative = MyMessages.QuestionMessage("Дисципліна " + discipline + " буде видалена з бази даних. " +
                    "Ви впевнені, що хочете продовжити?");
                }
                else
                {
                    affirmative = MyMessages.QuestionMessage("Дисципліна " + discipline + " буде видалена з бази даних. " +
                    "За цією дисципліною закріплено " + number + " навчальних матеріалів вони також будуть видалені(з бази даних)." +
                    "Ви впевнені, що хочете продовжити?");
                }
                if (affirmative == DialogResult.Yes)
                {
                    try
                    {
                        using (MySqlConnection con = DBUtils.GetDBConnection())
                        {
                            con.Open();
                            MySqlCommand cmd = new MySqlCommand("SelectDisciplineID", con)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmd.Parameters.AddWithValue("_name", discipline);
                            using (MySqlDataReader r = cmd.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    id_discip = int.Parse(r["id"].ToString());
                                }
                            }
                            cmd = new MySqlCommand("DeleteDiscipline", con)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmd.Parameters.AddWithValue("_id", id_discip);
                            int rows = cmd.ExecuteNonQuery();
                            if (rows >= 0)
                            {
                                MessageBox.Show("Навчальна дисципліна " + discipline + " була видалена з бази даних");
                                if (Directory.Exists(targetdirectory + discipline))
                                {
                                    Directory.Delete(targetdirectory + discipline, true);
                                }
                            }
                            else
                            {
                                using (MySqlDataReader r = cmd.ExecuteReader())
                                {
                                    while (r.Read())
                                    {
                                        error = r["@full_error"].ToString();
                                    }
                                }
                                MessageBox.Show(error);
                            }
                        }
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.Message);
                    }
                }
            }
            DelDisciplineListBoxFill(DelDiscpTextBox.Text.Trim());
        }

        private int GetNumberOfDocuments(string discipline)
        {
            int number = -1;
            string num;
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("GetNumberOfDocuments", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_name", discipline);
                    num = System.Convert.ToString(cmd.ExecuteScalar());
                    number = Int32.Parse(num);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return number;
        }

        private void DelDisciplineListBoxFill(string text)
        {
            if (text == DelDiscpStandartText)
            {
                text = String.Empty;
            }
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    DelDiscipList.Items.Clear();
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SelectDisciplineLikeEntered", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("_name", text);
                    MySqlDataReader drd = cmd.ExecuteReader();
                    while (drd.Read())
                    {
                        DelDiscipList.Items.Add(drd["name"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Text)
            {
                case "Видалити користувача":
                    {
                        DeleteUserPageInitialSize();
                        break;
                    }
                case "Видалити дисципліну":
                    {
                        DeleteDisciplineInitialSize();
                        break;
                    }
            }
        }

        private void DeleteUserPageInitialSize()
        {
            UsersListBoxFill(DelUserSearchTextBox.Text.Trim());
            DelUserSearchTextBox.Text = DelUserStandartText;
            DelUserSearchTextBox.ForeColor = SystemColors.GrayText;
            if (DelUserSearchTextBox.BackColor == Color.Red)
            {
                DelUserSearchTextBox.BackColor = Color.White;
            }
            DelUsersList.SelectedIndex = 0;
            FormResize(800, 539);
        }

        private void DeleteDisciplineInitialSize()
        {
            DelDisciplineListBoxFill(DelDiscpTextBox.Text.Trim());
            DelDiscpTextBox.Text = DelDiscpStandartText;
            DelDiscpTextBox.ForeColor = SystemColors.GrayText;
            if (DelDiscpTextBox.BackColor == Color.Red)
            {
                DelDiscpTextBox.BackColor = Color.White;
            }
            DelDiscipList.SelectedIndex = 0;
            FormResize(900, 400);
        }

        private void DelDiscipSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            DelDisciplineListBoxFill(DelDiscpTextBox.Text.Trim());
        }

        private int GetNumberOfDisciplines(string user)
        {
            int number = -1;
            int id = -1;
            string _role = DelUserroleLabel.Text.Trim();
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("GetRoleID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_role", _role);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            id_role = int.Parse(r["Role_id"].ToString());
                        }
                    }
                    cmd = new MySqlCommand("GetUserID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_username", user);
                    cmd.Parameters.AddWithValue("_role", id_role);
                    id = int.Parse(cmd.ExecuteScalar().ToString());
                    cmd = new MySqlCommand("GetNumberOfDisciplines", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_id", id);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            number = int.Parse(r["disciplines_count"].ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return number;
        }
        private void UsersListBoxFill(string text)
        {
            if (text == DelUserStandartText)
            {
                text = String.Empty;
            }
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    DelUsersList.Items.Clear();
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SelectOtherUsers", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("_name", text);
                    cmd.Parameters.AddWithValue("_id", _user.Id);
                    MySqlDataReader drd = cmd.ExecuteReader();
                    while (drd.Read())
                    {
                        DelUsersList.Items.Add(drd["sirname"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void DelUsersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string user = DelUsersList.SelectedItem.ToString();
            GetUserInfo(user);
            DeleteUserPageResize();
        }

        private void DeleteUserPageResize()
        {
            DelUserInfoPanel.Visible = true;
            /*FormResize(746, 390);
            UserDisciplinesGroupBox.Height = 188;
            UserDisciplinesGroupBox.Width = 325;
            MaterialCount.Height = 188;
            MaterialCount.Width = 175;*/
        }

        private void GetUserInfo(string user)
        {
            //string path = ProjectData.GetProjectFolder() + @"\EER icons\";
            int userid = -1;
            int num_discp = 0;
            int num_doc = 0;
            List<int> types = new List<int>();
            List<int> type_elements = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
            ImageList imageList = new ImageList
            {
                ImageSize = new Size(32, 32)
            };
            MaterialsListView.SmallImageList = imageList;

            try
            {
                DelUserDisciplines.Items.Clear();
                MaterialsListView.Items.Clear();
                if (imageList.Images.Count != 0)
                {
                    imageList.Images.Clear();
                }
                imageList.Images.Add(new Bitmap(iconpath+"icons8-audio-file-48.png"));
                imageList.Images.Add(new Bitmap(iconpath + "icons8-video-file-48.png"));
                imageList.Images.Add(new Bitmap(iconpath + "icons8-microsoft-powerpoint-48.png"));
                imageList.Images.Add(new Bitmap(iconpath + "icons8-pdf-48.png"));
                imageList.Images.Add(new Bitmap(iconpath + "icons8-microsoft-word-48.png"));
                imageList.Images.Add(new Bitmap(iconpath+ "icons8-image-file-48.png"));
                imageList.Images.Add(new Bitmap(iconpath + "icons8-microsoft-excel-48.png"));
                imageList.Images.Add(new Bitmap(iconpath + "icons8-archive-48.png"));
                //imageList.Images.Add(new Bitmap(@"E:\Student\C# tests\Диплом\Иконки\icons8-file-48.png"));

                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("GetUserInfo", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_name", user);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            DelUsernameLabel.Text = r["User"].ToString();
                            DelUserroleLabel.Text = r["Role"].ToString();
                            userid = (int)r["user_id"];
                        }
                    }
                    if (DelUserroleLabel.Text.Trim() == "Викладач")
                    {
                        UserDisciplinesGroupBox.Visible = true;
                        UserRolePicture.Load(iconpath+"Teacher.png");
                        cmd = new MySqlCommand("GetNumberOfDisciplines", con)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("_id", userid);
                        using (MySqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                num_discp = int.Parse(r["disciplines_count"].ToString());
                            }

                        }
                        if (num_discp == 0)
                        {
                            DelUserDisciplines.Items.Add("Викладач не веде жодної дисципліни");
                            DisciplinesNumberLabel.Text = "Відсутні";
                            DisciplinesNumberLabel.ForeColor = Color.Red;
                            UserDisciplinesGroupBox.Visible = false;

                        }
                        else
                        {
                            UserDisciplinesGroupBox.Visible = true;
                            DisciplinesNumberLabel.Text = num_discp.ToString();
                            DisciplinesNumberLabel.ForeColor = Color.Black;
                            cmd = new MySqlCommand("DisciplinesOfUser", con)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmd.Parameters.AddWithValue("_user_ID", userid);
                            using (MySqlDataReader r = cmd.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    DelUserDisciplines.Items.Add(r["Discipline"].ToString());
                                }
                            }
                        }
                        cmd = new MySqlCommand("GetNumberOfDocumentsOfUser", con)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("_id", userid);
                        using (MySqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                num_doc = int.Parse(r["NumberOfDocuments"].ToString());
                            }
                        }
                        if (num_doc == 0)
                        {
                            DocumentsNumberLabel.Text = "Відсутні";
                            DocumentsNumberLabel.ForeColor = Color.Red;
                            MaterialCount.Visible = false;
                        }
                        else
                        {
                            MaterialCount.Visible = true;
                            DocumentsNumberLabel.Text = num_doc.ToString();
                            DocumentsNumberLabel.ForeColor = Color.Black;
                            cmd = new MySqlCommand("ShowDocumentsOfUser", con)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmd.Parameters.AddWithValue("_id", userid);
                            using (MySqlDataReader r = cmd.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    types.Add((int)r["type"]);
                                }

                                foreach (int i in types)
                                {
                                    switch (i)
                                    {
                                        case 1:
                                            type_elements[0] = type_elements[0] + 1;
                                            break;
                                        case 2:
                                            type_elements[1] = type_elements[1] + 1;
                                            break;
                                        case 3:
                                            type_elements[2] = type_elements[2] + 1;
                                            break;
                                        case 4:
                                            type_elements[3] = type_elements[3] + 1;
                                            break;
                                        case 5:
                                            type_elements[4] = type_elements[4] + 1;
                                            break;
                                        case 6:
                                            type_elements[5] = type_elements[5] + 1;
                                            break;
                                        case 7:
                                            type_elements[6] = type_elements[6] + 1;
                                            break;
                                        case 8:
                                            type_elements[7] = type_elements[7] + 1;
                                            break;
                                    }
                                }

                                for (int j = 0; j < type_elements.Count; j++)
                                {
                                    if (type_elements[j] != 0)
                                    {
                                        ListViewItem listViewItem = new ListViewItem(new string[] { "", type_elements[j].ToString() })
                                        {
                                            ImageIndex = j
                                        };
                                        MaterialsListView.Items.Add(listViewItem);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        UserRolePicture.Load(iconpath+"Administrator.png");
                        DisciplinesNumberLabel.Text = "--------";
                        DocumentsNumberLabel.Text = "--------";
                        DisciplinesNumberLabel.ForeColor = Color.Black;
                        DocumentsNumberLabel.ForeColor = Color.Black;
                        UserDisciplinesGroupBox.Visible = false;
                        MaterialCount.Visible = false;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void FormResize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        private void DelUserDisciplines_Resize(object sender, EventArgs e)
        {
            UserDisciplinesGroupBox.Width = this.Width;
            // UserDisciplinesGroupBox.Height = this.Height;
        }

        private void DelUsersList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //select the item under the mouse pointer
                DelUsersList.SelectedIndex = DelUsersList.IndexFromPoint(e.Location);
                if (DelUsersList.SelectedIndex != -1)
                {
                    DelUserContextMenu.Show();
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string user = null;
            int userid = -1;
            int number = -1;
            string error = null;
            DialogResult affirmative = DialogResult.None;
            Studyload.Discipline.Clear();
            if (DelUsersList.SelectedIndex == -1)
            {
                MessageBox.Show("Спочатку оберіть користувача");
            }
            else
            {
                user = DelUsersList.SelectedItem.ToString();
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("GetUserInfo", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_name", user);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            userid = (int)r["user_id"];
                        }
                    }
                }
                number = GetNumberOfDisciplines(user);
                if (number <= 0)
                {
                    affirmative = MyMessages.QuestionMessage("Користувач " + user + " буде видалений з бази даних. " +
                    "Ви впевнені, що хочете продовжити?");
                }
                else
                {
                    affirmative = MyMessages.QuestionMessage("Викладач " + user + " буде видалений з бази даних. " +
                    "За цим викладачем закріплено " + number + " навчальних дисциплін." +
                    "Ви впевнені, що хочете продовжити?");
                }
                if (affirmative == DialogResult.Yes)
                {
                    if (id_role == 2 && number != 0)
                    {
                        affirmative = MyMessages.QuestionMessage("Бажаєте назначити дисципліни, які веде викладач " + user +
                                            " іншому користувачу?");
                        if (affirmative == DialogResult.No)
                        {
                            RemoveUser(user, id_role);
                        }
                        else
                        {
                            Studyload.Olduser = user;
                            foreach (string i in DelUserDisciplines.Items)
                            {
                                Studyload.Discipline.Add(i);
                            }
                            StudyloadRebuild form = new StudyloadRebuild()
                            {
                                Owner = this
                            };
                            form.ShowDialog();
                        }
                    }
                    else
                    {
                        RemoveUser(user, id_role);
                    }
                }
            }
            DeleteUserPageInitialSize();
        }

        internal static void RemoveUser(string user, int role)
        {
            string error = null;
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("DeleteUser", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_user", user);
                    cmd.Parameters.AddWithValue("_role", role);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows >= 0)
                    {
                        MessageBox.Show("Користувач " + user + " був успішно видалений");
                    }
                    else
                    {
                        using (MySqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                error = r["@full_error"].ToString();
                            }
                        }
                        MessageBox.Show(error);
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void DelUserSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string user = DelUserSearchTextBox.Text.Trim();
            UsersListBoxFill(user);

        }

        private void DelUserSearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string c = e.KeyChar.ToString();
            if (!(Regex.Match(c, @"[А-Яа-яіІїЇєЄҐґ'`' '-]").Success | e.KeyChar == 8) || Regex.Match(c, @"[ЫыъЪёЁэЭ]").Success)
            {
                //DelUserSearchTextBox.BackColor = Color.Red;
                DelUserSirnameErrorProvider.SetError(DelUserSearchTextBox, "Виявлено некоректні дані у полі даних для пошуку користувача." +
                    " Дозволяються лише символи українського алфавіту");
                if (!String.IsNullOrEmpty(DelUserSirnameErrorProvider.GetError(DelUserSearchTextBox)))
                {
                    MyMessages.WarningMessage(DelUserSirnameErrorProvider.GetError(DelUserSearchTextBox));
                }
                e.Handled = true;
            }
            else
            {
                DelUserSirnameErrorProvider.SetError(DelUserSearchTextBox, String.Empty);
                //DelUserSearchTextBox.BackColor = Color.White;
            }
        }

        private void DelDiscpTextBox_Enter(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
            if (DelDiscpTextBox.Text.Trim() == DelDiscpStandartText)
            {
                DelDiscpTextBox.Text = "";
                DelDiscpTextBox.ForeColor = SystemColors.WindowText;
            }
        }

        private void DelDiscipList_Leave(object sender, EventArgs e)
        {

        }

        private void DelDiscpTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string c = e.KeyChar.ToString();
            if (!(Regex.Match(c, @"[A-Za-zА-Яа-яіІїЇєЄҐґ'`' '-]").Success | e.KeyChar == 8) || Regex.Match(c, @"[ЫыъЪёЁэЭ]").Success)
            {
                DelDiscpTextBox.BackColor = Color.Red;
                e.Handled = true;
            }
            else
            {
                DelDiscpTextBox.BackColor = Color.White;
            }
        }

        private void DelDiscpTextBox_BackColorChanged(object sender, EventArgs e)
        {
            if (DelDiscpTextBox.BackColor == Color.Red)
            {
                toolTip1.SetToolTip(DelDiscpTextBox, "Дозволяються лише символи українського алфавіту");
            }
            else if (DelDiscpTextBox.BackColor == Color.White)
            {
                toolTip1.Dispose();
            }
        }

        private void DelDiscipList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string discipline = DelDiscipList.SelectedItem.ToString();
            DisciplineInfo(discipline);
            DelDisciplineResize();
        }

        private void DelDisciplineResize()
        {
            DisciplineTeachers.Load(iconpath+"Teachers.png");
            DisciplineDocuments.Load(iconpath+"File.png");
            DiscipNameLabel.Text = DelDiscipList.SelectedItem.ToString();
            DisciplTeachersPanel.Visible = false;
            MaterialsCountPanel.Visible = false;
            DisciplineInfoPanel.Visible = true;
        }

        private void DelDiscipList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //select the item under the mouse pointer
                DelDiscipList.SelectedIndex = DelDiscipList.IndexFromPoint(e.Location);
                if (DelDiscipList.SelectedIndex != -1)
                {
                    DelDisciplineContextMenu.Show();
                }
            }
        }

        private void DelUserMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void FormDelete_SizeChanged(object sender, EventArgs e)
        {
            if (Width < 330)
            {
                FormResize(330, Height);
            }
            if (Height < 250)
            {
                FormResize(Width, 250);
            }
        }

        private void DisciplineInfo(string discipline)
        {
            int discip_id = -1;
            int discip_count = 0;
            int material_count = 0;
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SelectDisciplineID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_name", discipline);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            discip_id = (int)r["id"];
                        }
                    }
                    cmd = new MySqlCommand("GetTeacherCountForDisciplines", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_idDiscipline", discip_id);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            discip_count = int.Parse(r["Teachers"].ToString());
                        }
                        if (discip_count == 0)
                        {
                            DiscipTeachersCount.ForeColor = Color.Red;
                        }
                        else
                        {
                            DiscipTeachersCount.ForeColor = Color.Black;
                        }
                        DiscipTeachersCount.Text = discip_count.ToString();
                    }
                    cmd = new MySqlCommand("GetDisciplineMaterialsCount", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_idDiscipline", discip_id);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            material_count = int.Parse(r["MaterialsCount"].ToString());
                        }
                        if (material_count == 0)
                        {
                            MaterialsCount.ForeColor = Color.Red;
                        }
                        else
                        {
                            MaterialsCount.ForeColor = Color.Black;
                        }
                        MaterialsCount.Text = material_count.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void DisciplineTeachers_Click(object sender, EventArgs e)
        {
            bool flag = true;
            string count = DiscipTeachersCount.Text.Trim();
            if (flag)
            {
                if (int.Parse(count) != 0)
                {
                    DisciplTeachersPanel.Visible = true;
                    flag = false;
                }
                else
                {
                    DisciplTeachersPanel.Visible = false;
                }
            }
        }

        private void TeachersList_VisibleChanged(object sender, EventArgs e)
        {
            string discipline = DelDiscipList.SelectedItem.ToString();
            int discip_id = -1;
            try
            {
                TeachersList.Items.Clear();
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SelectDisciplineID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_name", discipline);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            discip_id = (int)r["id"];
                        }
                    }
                    cmd = new MySqlCommand("GetDisciplineTeachers", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_idDiscipline", discip_id);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            TeachersList.Items.Add(r["Users"].ToString());
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void DisciplineDocuments_Click(object sender, EventArgs e)
        {
            bool flag = true;
            string count = MaterialsCount.Text.Trim();
            if (flag)
            {
                if (int.Parse(count) != 0)
                {
                    MaterialsCountPanel.Visible = true;
                    flag = false;
                }
                else
                {
                    MaterialsCountPanel.Visible = false;
                }
            }
        }

        private void MaterialsList_VisibleChanged(object sender, EventArgs e)
        {
            string discipline = DelDiscipList.SelectedItem.ToString();
            string material;
            int discip_id = -1;
            try
            {
                //TeachersList.Items.Clear();
                MaterialsList.Items.Clear();
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SelectDisciplineID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_name", discipline);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            discip_id = (int)r["id"];
                        }
                    }
                    cmd = new MySqlCommand("GetDisciplineMaterials", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_idDiscipline", discip_id);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            material = Path.GetFileName(r["Materials"].ToString());
                            MaterialsList.Items.Add(material);
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void MaterialsList_SelectedIndexChanged(object sender, EventArgs e)
        {
			toolTip1.SetToolTip(MaterialsList, MaterialsList.SelectedItem.ToString());
        }

        private void DelUserSearchTextBox_Leave(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
            if (DelUserSearchTextBox.Text.Trim().Length == 0)
            {
                DelUserSearchTextBox.Text = DelUserStandartText;
                DelUserSearchTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void DelUserSearchTextBox_Enter(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
            if (DelUserSearchTextBox.Text.Trim() == DelUserStandartText)
            {
                DelUserSearchTextBox.Text = "";
                DelUserSearchTextBox.ForeColor = SystemColors.WindowText;
            }
        }

        private void GoBackToolStripButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DelDisciplineStripButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void DelUserStripButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void DelDiscpTextBox_Leave(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
            if (DelDiscpTextBox.Text.Trim().Length == 0)
            {
                DelDiscpTextBox.Text = DelDiscpStandartText;
                DelDiscpTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void DelDiscpTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            string c = e.KeyChar.ToString();
            string error = DelDisciplineErrorProvider.GetError(DelDiscpTextBox);
            if (!(Regex.Match(c, @"[A-Za-zА-Яа-яіІїЇєЄҐґ'`' '-]").Success | e.KeyChar == 8) || Regex.Match(c, @"[ЫыъЪёЁэЭ]").Success)
            {
                DelDisciplineErrorProvider.SetError(DelDiscpTextBox, "Виявлено некоректні дані у полі даних для пошуку навчальної дисципліни." +
                    " Дозволяються лише символи українського або латинського алфавіту");
                if (!String.IsNullOrEmpty(error))
                {
                    MyMessages.WarningMessage(error);
                }
                e.Handled = true;
            }
            else
            {
                DelDisciplineErrorProvider.SetError(DelDiscpTextBox, String.Empty);
            }
        }

        private void DelDisciplineContextMenu_Click(object sender, EventArgs e)
        {
            RemoveDisciline();
        }

        private void DataDeletion_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult affirmative = MyMessages.FormClosing();
            if (affirmative == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

		private void TeachersList_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
