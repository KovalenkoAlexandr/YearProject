using MySql.Data.MySqlClient;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingWithData
{
    public partial class DataView : Form
    {
        string DisciplineHintText = "Введіть назву навчальної дисципліни для пошуку";
        string UserHintText = "Введіть прізвище користувача для пошуку";
        string MaterialsHintText = "Введіть назву документа для пошуку";
        string StudyloadHintText = "Введіть прізвище викладача або назву дисципліни для пошуку";
        public DataView(int role)
        {
            InitializeComponent();
        }

        private void DataView_Load(object sender, EventArgs e)
        {
            DisciplinesSearchText.Text = DisciplineHintText;
            DisciplinesSearchText.ForeColor = SystemColors.GrayText;
            string discipl_in = DisciplinesSearchText.Text.Trim();
            SelectDisciplines(discipl_in);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DataViewTabControl.SelectedIndex)
            {
                case 0:
                    DisciplinesSearchText.Text = DisciplineHintText;
                    DisciplinesSearchText.ForeColor = SystemColors.GrayText;
                    string discipl_in = DisciplinesSearchText.Text.Trim();
                    SelectDisciplines(discipl_in);
                    break;
                case 1:
                    UsersSearchText.Text = UserHintText;
                    UsersSearchText.ForeColor = SystemColors.GrayText;
                    string text = UsersSearchText.Text.Trim();
                    SelectUsers(text);
                    break;
                case 2:
                    MaterialsSearchText.Text = MaterialsHintText;
                    MaterialsSearchText.ForeColor = SystemColors.GrayText;
                    string material_in = MaterialsSearchText.Text.Trim();
                    SelectMaterials(material_in);
                    break;
                case 3:
                    StudyloadSearchText.Text = StudyloadHintText;
                    StudyloadSearchText.ForeColor = SystemColors.GrayText;
                    string search = StudyloadSearchText.Text.Trim();
                    SelectStudyload(search);
                    break;
            }
        }
        private void SelectUsers(string text)
        {
            MySqlDataAdapter adap = new MySqlDataAdapter();
            int g_width = 0;
            if (text == UserHintText)
            {
                text = String.Empty;
            }
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("ShowUsers", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_name",text);
                    adap.SelectCommand = cmd;
                    DataTable table = new DataTable();
                    adap.Fill(table);
                    UsersInfoGridView.DataSource = table;
                    UsersInfoGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    foreach (DataGridViewColumn column in UsersInfoGridView.Columns)
                        if (column.Visible == true)
                        {
                            g_width += column.Width;
                        }
                    Width = g_width + 90;
                }
                if (FioSort.Checked)
                {
                    UsersInfoGridView.Sort(UsersInfoGridView.Columns[3], ListSortDirection.Ascending);
                }
            }
            catch(Exception e)
            {
                MyMessages.ErrorMessage(e.Message);
            }
        }

        private void SelectDisciplines(string text)
        {
            MySqlDataAdapter adap = new MySqlDataAdapter();
            DataTable table = new DataTable();
            int g_width = 0;
            if (text == DisciplineHintText)
            {
                text = String.Empty;
            }
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SelectDisciplineLikeEntered", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_name",text);
                    adap.SelectCommand = cmd;
                    adap.Fill(table);
                    DisciplinesDataGrid.DataSource = table;
                    DisciplinesDataGrid.Columns[0].HeaderText = "Навчальна дисципліна";
                    DisciplinesDataGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    foreach (DataGridViewColumn column in DisciplinesDataGrid.Columns)
                        if (column.Visible == true)
                        {
                            g_width += column.Width;
                        }
                    Width = g_width + 95;
                }
            }
            catch (Exception e)
            {
                MyMessages.ErrorMessage(e.Message);
            };
        }

        private void SelectMaterials(string text)
        {
            int g_width = 0;
            int g_height = 0;
            if(text == MaterialsHintText)
            {
                text = String.Empty;
            }
            MySqlDataAdapter adap = new MySqlDataAdapter();
            DataTable table = new DataTable();
            using (MySqlConnection con = DBUtils.GetDBConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("ShowMaterials", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("_name",text);
                adap.SelectCommand = cmd;
                adap.Fill(table);
                MaterialsGridView.DataSource = table;
                MaterialsGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                MaterialsGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            foreach (DataGridViewColumn column in MaterialsGridView.Columns)
                if (column.Visible == true)
                {
                    g_width += column.Width;
                }
            Width = g_width + 75;
            if (UserSort.Checked)
            {
                MaterialsGridView.Sort(MaterialsGridView.Columns[0], ListSortDirection.Ascending);
            }
        }

        private void SelectStudyload(string text)
        {
            int g_width = 0;
            if (text == StudyloadHintText)
            {
                text = String.Empty;
            }
            MySqlDataAdapter adap = new MySqlDataAdapter();
            using (MySqlConnection con = DBUtils.GetDBConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("TeacherDisciplineMatch", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("_name",text);
                adap.SelectCommand = cmd;
                DataTable table = new DataTable();
                adap.Fill(table);
                StudyloadGridView.DataSource = table;
                StudyloadGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                StudyloadGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                foreach (DataGridViewColumn column in StudyloadGridView.Columns)
                    if (column.Visible == true)
                    {
                        g_width += column.Width;
                    }
                Width = g_width + 90;
            }
            if (TeacherSort.Checked)
            {
                StudyloadGridView.Sort(StudyloadGridView.Columns[0], ListSortDirection.Ascending);
            }
        }

        private void DisciplinesStripButton_Click(object sender, EventArgs e)
        {
            DataViewTabControl.SelectTab(0);
        }

        private void UsersStripButton_Click(object sender, EventArgs e)
        {
            DataViewTabControl.SelectTab(1);
        }

        private void MaterialsStripButton_Click(object sender, EventArgs e)
        {
            DataViewTabControl.SelectTab(2);
        }

        private void StudyloadStripButton_Click(object sender, EventArgs e)
        {
            DataViewTabControl.SelectTab(3);
        }

        private void ReturnStripButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                DisciplinesDataGrid.Sort(DisciplinesDataGrid.Columns[0], ListSortDirection.Ascending);
            }
            else
            {
                DisciplinesDataGrid.Sort(DisciplinesDataGrid.Columns[0], ListSortDirection.Descending);
            }
        }

        private void DisciplinesSearchText_Enter(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
            if (DisciplinesSearchText.Text.Trim() == DisciplineHintText)
            {
                DisciplinesSearchText.Text = "";
                DisciplinesSearchText.ForeColor = SystemColors.WindowText;
            }
        }

        private void DisciplinesSearchText_Leave(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
            if (DisciplinesSearchText.Text.Trim().Length == 0)
            {
                DisciplinesSearchText.Text = DisciplineHintText;
                DisciplinesSearchText.ForeColor = SystemColors.GrayText;
            }
        }

        private void DisciplinesSearchText_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void DisciplinesSearchText_TextChanged(object sender, EventArgs e)
        {
            string text = DisciplinesSearchText.Text.Trim();
            SelectDisciplines(text);
        }

        private void UsersSearchText_Enter(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
            if (UsersSearchText.Text.Trim() == UserHintText)
            {
                UsersSearchText.Text = "";
                UsersSearchText.ForeColor = SystemColors.WindowText;
            }
        }

        private void UsersSearchText_TextChanged(object sender, EventArgs e)
        {
            string search = UsersSearchText.Text.Trim();
            SelectUsers(search);
        }

        private void UsersSearchText_Leave(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
            if (UsersSearchText.Text.Trim().Length == 0)
            {
                UsersSearchText.Text = UserHintText;
                UsersSearchText.ForeColor = SystemColors.GrayText;
            }
        }

        private void UsersSearchText_KeyPress(object sender, KeyPressEventArgs e)
        {
            string c = e.KeyChar.ToString();
            if (!(Regex.Match(c, @"[А-Яа-яіІїЇєЄ'`' '-.]").Success | e.KeyChar == 8) || Regex.Match(c, @"[ЫыъЪёЁэЭ]").Success)
            {
                MyMessages.WarningMessage("Дозволяються лише символи українського алфавіту");
                e.Handled = true;
            }
        }

        private void FioSort_CheckedChanged(object sender, EventArgs e)
        {
            if (FioSort.Checked)
            {
                UsersInfoGridView.Sort(UsersInfoGridView.Columns[3], ListSortDirection.Ascending);
            }
        }

        private void LoginSort_CheckedChanged(object sender, EventArgs e)
        {
            if (LoginSort.Checked)
            {
                UsersInfoGridView.Sort(UsersInfoGridView.Columns[0], ListSortDirection.Ascending);
            }
        }

        private void MaterialsSearchText_TextChanged(object sender, EventArgs e)
        {
            string text = MaterialsSearchText.Text.Trim();
            SelectMaterials(text);
        }

        private void MaterialsSearchText_Leave(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
            if (MaterialsSearchText.Text.Trim().Length == 0)
            {
                MaterialsSearchText.Text = MaterialsHintText;
                MaterialsSearchText.ForeColor = SystemColors.GrayText;
            }
        }

        private void MaterialsSearchText_Enter(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
            if (MaterialsSearchText.Text.Trim() == MaterialsHintText)
            {
                MaterialsSearchText.Text = "";
                MaterialsSearchText.ForeColor = SystemColors.WindowText;
            }
        }

        private void StadyloadText_Enter(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
            if (StudyloadSearchText.Text.Trim() == StudyloadHintText)
            {
                StudyloadSearchText.Text = "";
                StudyloadSearchText.ForeColor = SystemColors.WindowText;
            }
        }

        private void StadyloadText_TextChanged(object sender, EventArgs e)
        {
            string text = StudyloadSearchText.Text.Trim();
            SelectStudyload(text);
        }

        private void StadyloadText_Leave(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
            if (StudyloadSearchText.Text.Trim().Length == 0)
            {
                StudyloadSearchText.Text = MaterialsHintText;
                StudyloadSearchText.ForeColor = SystemColors.GrayText;
            }
        }

        private void StadyloadText_KeyPress(object sender, KeyPressEventArgs e)
        {
            string c = e.KeyChar.ToString();
            if (!(Regex.Match(c, @"[A-Za-zА-Яа-яіІїЇєЄҐґ'`' '-]").Success | e.KeyChar == 8) || Regex.Match(c, @"[ЫыъЪёЁэЭ]").Success)
            {
                MyMessages.WarningMessage("Дозволяються лише символи українського або латинського алфавіту");
                e.Handled = true;
            }
        }

        private void DataView_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult affirmative = MyMessages.FormClosing();
            if (affirmative == DialogResult.Yes)
            {
                e.Cancel = false;
                Dispose();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void TeacherSort_CheckedChanged(object sender, EventArgs e)
        {
            if (TeacherSort.Checked)
            {
                StudyloadGridView.Sort(StudyloadGridView.Columns[0], ListSortDirection.Ascending);
            }
        }

        private void DisciplineSort_CheckedChanged(object sender, EventArgs e)
        {
            if(DisciplineSort.Checked)
            {
                StudyloadGridView.Sort(StudyloadGridView.Columns[1], ListSortDirection.Ascending);
            }
        }

        private void UserSort_CheckedChanged(object sender, EventArgs e)
        {
            if(UserSort.Checked)
            {
                MaterialsGridView.Sort(MaterialsGridView.Columns[0],ListSortDirection.Ascending);
            }
        }

        private void TypeSort_CheckedChanged(object sender, EventArgs e)
        {
            if (TypeSort.Checked)
            {
                MaterialsGridView.Sort(MaterialsGridView.Columns[2], ListSortDirection.Ascending);
            }
        }

        private void MaterialSort_CheckedChanged(object sender, EventArgs e)
        {
            if (MaterialSort.Checked)
            {
                MaterialsGridView.Sort(MaterialsGridView.Columns[3], ListSortDirection.Ascending);
            }
        }

        private void DisciplinesSort_CheckedChanged(object sender, EventArgs e)
        {
            if (DisciplinesSort.Checked)
            {
                MaterialsGridView.Sort(MaterialsGridView.Columns[1], ListSortDirection.Ascending);
            }
        }
    }
}
