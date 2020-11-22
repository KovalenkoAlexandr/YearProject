using MySql.Data.MySqlClient;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingWithData
{
    public partial class StudyloadRebuild : Form
    {
        string targetdirectory = @"\\ALEX\Common\";
        public StudyloadRebuild()
        {
            InitializeComponent();
        }

        private void StudyloadRebuild_Load(object sender, EventArgs e)
        {
            StudyloadRebuildInitialsize();
        }

        private void StudyloadRebuildInitialsize()
        {
            DisciplinesList.Items.Clear();
            panel2.Enabled = false;
            OldTeacherLabel.Text = Studyload.Olduser;
            TeachersComboBox.Items.Clear();
            foreach (string i in Studyload.Discipline)
            {
                DisciplinesList.Items.Add(i);
            }
            //CancelRebuildButton.Enabled = false;
            ConfirmRebuildButton.Enabled = false;
            TransferButton.Enabled = false;
        }

        private void ConfirmRebuildButton_Click(object sender, EventArgs e)
        {
            //Studyload.Discipline.Clear();
            string discipline = DisciplinesList.SelectedItem.ToString();
            //List<string> discipline = new List<string>()
            string user = OldTeacherLabel.Text.Trim();
            int userid = -1;
            string error = null;
            Studyload.Studyloadid = -1;
            int Reb_s_l = -1;
            int disciplines = -1;
            try
            {
                for (int i = 0; i<Studyload.Discipline.Count; i++)
                {
                    disciplines++;
                    using (MySqlConnection con = DBUtils.GetDBConnection())
                    {
                        con.Open();
                        Studyload.Studyloadid = GetStudyloadId(user, i, con);
                        MySqlCommand cmd = new MySqlCommand("GetUserId", con)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("_username", Studyload.Newuser);
                        cmd.Parameters.AddWithValue("_role", 2);
                        using (MySqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                userid = int.Parse(r["ID"].ToString());
                            }
                        }
                        Reb_s_l = GetStudyloadId(Studyload.Newuser, i, con);
                        if (Reb_s_l == -1)
                        {
                            cmd = new MySqlCommand("RebuildStudyload", con)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmd.Parameters.AddWithValue("_iduser", userid);
                            cmd.Parameters.AddWithValue("_Id_s_l", Studyload.Studyloadid);
                            int rows = cmd.ExecuteNonQuery();
                            if (rows >= 0)
                            {
                                if (Directory.Exists(targetdirectory + Studyload.Discipline[i] + user))
                                {
                                    Directory.Delete(targetdirectory + Studyload.Discipline[i] + user, true);
                                }
                                Studyload.Studyloadid = -1;

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
                                MyMessages.ErrorMessage(error);
                            }
                            Studyload.Studyloadid = GetStudyloadId(user,i,con);
                            if (Studyload.Studyloadid != -1)
                            {
                                MyMessages.WarningMessage("Ви перенаправили не всі дисципліни");
                            }
                        }
                    }
                }
                if (disciplines!=-1)
                {
                    MessageBox.Show("Навчальні дисципліни було перенаправлено");
                }
                DataDeletion.RemoveUser(user, 2);
                Studyload.Discipline.Clear();
               //Close();
            }
            catch (Exception e1)
            {
                MyMessages.ErrorMessage(e1.Message);
            }
            Close();
        }

        private static int GetStudyloadId(string user, int i, MySqlConnection con)
        {
            int id = -1;
            MySqlCommand cmd = new MySqlCommand("GetStudyloadID", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("_name", Studyload.Discipline[i]);
            cmd.Parameters.AddWithValue("_userfio", user);
            cmd.Parameters.AddWithValue("_userrole", 2);
            using (MySqlDataReader r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                   id = int.Parse(r["studyload_id"].ToString());
                }
            }
            return id;
        }

        private void DisciplinesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int userid = -1;
            string user = OldTeacherLabel.Text.Trim();
            TeachersComboBox.Items.Clear();
            try
            {
                string discipline = DisciplinesList.SelectedItem.ToString();
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("GetUserID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_username", user);
                    cmd.Parameters.AddWithValue("_role",2);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            userid = int.Parse(r["ID"].ToString());
                        }
                    }
                    cmd = new MySqlCommand("SelectNewTeachers", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_idoldteacher", userid);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            TeachersComboBox.Items.Add(r["Teachers"].ToString());
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                if (e1.Message.Contains("Ссылка на объект не указывает на экземпляр объекта"))
                {
                    MyMessages.ErrorMessage("Ви не обрали дисципліну");
                }
                else
                {
                    MyMessages.ErrorMessage(e1.Message);
                }
            }
            TeachersComboBox.Enabled = true;
        }


        private void TeachersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TransferButton.Enabled = true;
        }

        private void TransferButton_Click(object sender, EventArgs e)
        {
            string discipline = DisciplinesList.SelectedItem.ToString();
            Studyload.Newuser = TeachersComboBox.Text.Trim();
            DialogResult affirmative = DialogResult.None;
            List<string> list = new List<string>();
            int index = 0;
            if (RebuildedStudyload.Items.Count < DisciplinesList.Items.Count)
            {
                RebuildedStudyload.Items.Add(discipline + "->" + Studyload.Newuser);
            }
            else
            {
                foreach (string i in RebuildedStudyload.Items)
                {
                    list.Add(i);
                }
                for (int i = 0; i<list.Count;i++)
                {
                    if (list[i].Contains(discipline))
                    {
                        index = i;
                        affirmative = MyMessages.QuestionMessage("Ви вже переназначили цю дисципліну." +
                            "Ви бажаєте переназначити її знову?");
                        if (affirmative == DialogResult.Yes)
                        {
                            RebuildedStudyload.Items.RemoveAt(index);
                            RebuildedStudyload.Items.Insert(index, discipline + "->" + Studyload.Newuser);
                        }
                    }
                }
            }
            if(RebuildedStudyload.Items.Count == DisciplinesList.Items.Count)
            {
                ConfirmRebuildButton.Enabled = true;
                foreach (string i in DisciplinesList.Items)
                {
                    Studyload.Discipline.Add(discipline);
                }
            }
            else
            {
                ConfirmRebuildButton.Enabled = false;
            }
            CancelRebuildButton.Enabled = true;
            TeachersComboBox.Text = String.Empty;
        }

        private void CancelRebuildButton_Click(object sender, EventArgs e)
        {
            Studyload.Discipline.Clear();
            Close();
        }

        private void StudyloadRebuild_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
