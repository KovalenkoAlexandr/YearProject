using MySql.Data.MySqlClient;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WorkingWithData
{
    public partial class Tags : Form
    {
        bool TagTextChanged = false;
        Users _user;
        #region Дії над формою введення ключових слів
        //Створення форми
        public Tags(Users user)
        {
            _user = user;
            InitializeComponent();
        }
        //Завантаження форми
        private void Tags_Load(object sender, EventArgs e)
        {
            TagsFormInitialSize();
        }
        //Встановлення початкових розмірів форми
        private void TagsFormInitialSize()
        {
            TagsTextBox.Focus();
            Height = 365;
            Width = 344;
            ClearTagsControls();
            TagsCancelButton.Enabled = false;
            TagsConfirmButton.Enabled = false;
            ChooseTagButton.Enabled = false;
            TagsUserStatusLabel.Text = _user.Fio;
            TagsDisciplineStatusLabel.Text = Discipline.Discipline_name;
            TagsDocumentStatusLabel.Text = Materials.Documentname;
            TagsListBox_Fill(TagsTextBox.Text.Trim());
            if (Materials.Tags.Count > 0)
            {
                Materials.Tags.Clear();
            }
        }
        //Закриття форми
        private void Tags_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearTagsControls();
        }
        //Зміна розмірів форми
        private void TagsForm_Resize()
        {
            Height = 362;
            Width = 646;
            groupBox2.Visible = true;
            TagsConfirmButton.Enabled = true;
            ChoosedTagDeleteButton.Enabled = true;
            TagsListBox_Fill(TagsTextBox.Text);
        }
        #endregion

        #region Технічний функціонал

        #region Дії
        //Очищення компонентів форми
        private void ClearTagsControls()
        {
            if (TagsListBox.Items.Count != 0)
            {
                TagsListBox.Items.Clear();
            }
            if (ChoosedTagsListBox.Items.Count != 0)
            {
                ChoosedTagsListBox.Items.Clear();
            }
            if (!(String.IsNullOrEmpty(TagsTextBox.Text.Trim())))
            {
                TagsTextBox.Text = String.Empty;
            }
            if (!(String.IsNullOrEmpty(TagsTextErrorProvider.GetError(TagsTextBox))))
            {
                TagsTextErrorProvider.SetError(TagsTextBox, String.Empty);
            }
            if (groupBox2.Visible == true)
            {
                ChoosedTagsListBox.Items.Clear();
            }
        }
        //Зміна тексту поля введення ключового слова
        private void TagsTextBox_TextChanged(object sender, EventArgs e)
        {
            string tag = TagsTextBox.Text.Trim();
            if (!(String.IsNullOrEmpty(tag)))
            {
                TagsCancelButton.Enabled = true;
                ChooseTagButton.Enabled = true;
                TagTextChanged = true;
            }
            else
            {
                TagTextChanged = false;
                TagsCancelButton.Enabled = false;
                ChooseTagButton.Enabled = false;
            }
            TagsListBox_Fill(tag);
        }
        //Заповнення списку ключових слів
        private void TagsListBox_Fill(string tag)
        {
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    TagsListBox.Items.Clear();
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SelectTagLikeEntered", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("_tagname", tag);
                    MySqlDataReader drd = cmd.ExecuteReader();
                    while (drd.Read())
                    {
                        TagsListBox.Items.Add(drd["Tag_name"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        //Вибір ключового слова зі списку
        private void TagsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TagsTextBox.Text = TagsListBox.SelectedItem.ToString();
                TagsTextBox.Focus();
            }
            catch (Exception e1)
            {
                MyMessages.ErrorMessage(e1.Message);
            }
        }
        #endregion

        #region Перевірка даних
        //Перевірка даних поля введення ключового слова
        private void TagsTextBox_Validating(object sender, CancelEventArgs e)
        {
            string tag_in = TagsTextBox.Text.Trim();
            if (String.IsNullOrEmpty(tag_in) && TagsListBox.SelectedIndex == -1)
            {
                TagsTextErrorProvider.SetError(TagsTextBox, "Ви не ввели ключове слово!");
            }
            else if (tag_in.Length > 250)
            {
                TagsTextErrorProvider.SetError(TagsTextBox, "Увага! Занадто довге значення");
            }
            else
            {
                TagsTextErrorProvider.SetError(TagsTextBox, String.Empty);
            }
        }
        //Перевірка введеного символу на коректність
        private void TagsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string c = e.KeyChar.ToString();
            if (/*!(Regex.Match(c, @"[1234567890A-Za-zА-Яа-яіІїЇєЄ'`-' ':]").Success*/ Regex.Match(c, @"[ЫыъЪёЁэЭ]").Success | e.KeyChar == 8)//) /*|| Regex.Match(c, @"[ЫыъЪёЁэЭ]").Success)*/
            {
                TagsTextErrorProvider.SetError(TagsTextBox, "Дозволяються лише символи українського та латинського алфавіту");
                e.Handled = true;
            }
            else
            {
                TagsTextErrorProvider.SetError(TagsTextBox, String.Empty); ;
            }
        }
        #endregion

        #endregion

        #region Функціонал користувача
        //Клік на кнопці відміни введення ключового слова
        private void TagsCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult affirmative = MyMessages.AbortDataEntrance();
            if (affirmative == DialogResult.No)
            {

            }
            else if (affirmative == DialogResult.Yes)
            {
                TagsTextBox.Text = String.Empty;
                TagsTextErrorProvider.SetError(TagsTextBox, String.Empty);
                if (ChoosedTagsListBox.Items.Count > 0)
                {
                    DialogResult dbquestion = MyMessages.DBAbortDataEntrance();
                    if (dbquestion == DialogResult.Yes)
                    {
                        ChoosedTagsListBox.Items.Clear();

                        TagsConfirmButton.Enabled = false;
                        ChoosedTagDeleteButton.Enabled = false;
                        groupBox2.Visible = false;
                    }
                }
            }
        }
        //Закриття форми додавання ключових слів
        private void TagsExitButton_Click(object sender, EventArgs e)
        {
            DialogResult affirmative = MyMessages.AbortDataEntrance();
            if (affirmative == DialogResult.No)
            {

            }
            else if (affirmative == DialogResult.Yes)
            {
                TagsFormInitialSize();
                this.Close();
            }
        }
        //Клік на кнопці додавання ключових слів до бази даних
        private void ChooseTagButton_Click(object sender, EventArgs e)
        {
            AddTagAndClear();
        }

        private void AddTagAndClear()
        {
            string tag = TagsTextBox.Text.Trim();
            bool flag = false;
            List<string> list = new List<string>();
            foreach (string i in ChoosedTagsListBox.Items)
            {
                list.Add(i);
            }
            for (int i = 0; i<list.Count; i++)
            {
                if (list[i].Contains(tag))
                {
                    flag = true;
                }
            }
            if (flag)
            {
                MyMessages.WarningMessage("Ви вже обрали ключове слово "+ tag +" для закріплення за документом");
            }
            else
            {
                ChoosedTagsListBox.Items.Add(tag);
            }
            TagsTextBox.Text = String.Empty;
            TagsForm_Resize();
            TagsTextErrorProvider.SetError(TagsTextBox, String.Empty);
            TagsTextBox.Focus();
            TagsListBox_Fill(TagsTextBox.Text.Trim());
        }

        //Очищення списку обраних ключових слів
        private void ChoosedTagDeleteButton_Click(object sender, EventArgs e)
        {
            DialogResult affirmative = MyMessages.AbortDataEntrance();
            if (affirmative == DialogResult.Yes)
            {
                if (ChoosedTagsListBox.Items.Count > 0)
                {
                    ChoosedTagsListBox.Items.Clear();
                }
            }
        }
        //Клік на копці додавання ключового слова до списку обраних ключових слів
        private void TagsConfirmButton_Click(object sender, EventArgs e)
        {
            foreach (string item in ChoosedTagsListBox.Items)
            {
                Materials.Tags.Add(item.ToString());
            }
            this.Close();
        }
        //Затримання курсору миші над кнопкою видалення ключових слів зі списку на додавання до бази даних
        private void ChoosedTagDeleteButton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(ChoosedTagDeleteButton, "Видалити всі ключові слова, зі списку");
        }
        #endregion

        private int GetTagID(string text)
        {
            int tagid = -1;
            using (MySqlConnection con = DBUtils.GetDBConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("GetTagID", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("_Tagname", text);
                using (MySqlDataReader r = cmd.ExecuteReader())
                    while (r.Read())
                    {
                        tagid = (int)r["Tag_id"];
                    }
            }
            return tagid;
        }
        private int AddNewTag(string tag)
        {
            int id_tag = -1;
            string error = null;
            DialogResult affirmative = MyMessages.QuestionMessage("Виявлено ключове слово, якого немає у базу даних. " +
                    "Бажаєте додати його до бази даних?");
            if (affirmative == DialogResult.No)
            {
                TagsTextErrorProvider.SetError(TagsTextBox, "Оберіть ключове слово");
                TagsTextBox.Text = String.Empty;
            }
            else
            {
                try
                {
                    using (MySqlConnection con = DBUtils.GetDBConnection())
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("AddNewTag", con)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("_Tagname", tag);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows >= 0)
                        {
                            MessageBox.Show("Ключове слово було додано до бази даних");
                        }
                        else
                        {
                            MySqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                error = reader["@full_error"].ToString();
                            }
                            MyMessages.ErrorMessage(error);
                        }
                        id_tag = GetTagID(tag);
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            return id_tag;
        }

        private void TagsTextBox_Leave(object sender, EventArgs e)
        {
            int ID_tag = -1;
            string tag = TagsTextBox.Text.Trim();
            if (TagTextChanged)
            {
                if (!char.IsUpper(tag[0]))
                {
                    tag = char.ToUpper(tag[0]) + tag.Substring(1);
                    TagsTextBox.Text = tag;
                }
                ID_tag = GetTagID(tag);
                if (ID_tag == -1)
                {
                    ID_tag = AddNewTag(tag);
                    if (ID_tag == -1)
                    {
                        TagsTextBox.Text = String.Empty;
                    }
                    else
                    {
                        AddTagAndClear();
                    }
                }
            }
        }
    }
}
