using MySql.Data.MySqlClient;
using Shared;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace WorkingWithData
{
    public partial class AddData : Form
    {
        string Type_of_document = null;
        bool MaterialTypeTextChanged = false;
        string target_directory = ProjectData.GetProjectFolder() + @"\";
        Users _user;
        #region Дії над формою додавання даних
        //Створення форми
        public AddData(Users user)
        {
            _user = user;
            InitializeComponent();
            if (_user.Id_role == 2)
            {
                tabControl1.TabPages.Remove(UserTabPage);
                tabControl1.TabPages.Remove(DisciplinesTabPage);
                tabControl1.TabPages.Remove(StudyLoadTabPage);
                toolStrip1.Items.Remove(UserAddStripButton);
                toolStrip1.Items.Remove(DisciplineAddStripButton);
                toolStrip1.Items.Remove(StudyloadAddStripButton);
            }
            else
            {
                tabControl1.TabPages.Remove(MaterialsTabPage);
                toolStrip1.Items.Remove(MaterialsAddStripButton);
            }
        }
        //Задання початкових розмірів форми
        private void BasicAddFormView()
        {
            Width = 330;
            Height = 400;
            //tabControl1.Width = 265;
            //tabControl1.Height = 294;
            tabControl1.SelectedIndex = 0;
            uConfirmButton.Enabled = false;
            Cancel_Button.Enabled = false;
            TeacherRadio.Checked = true;
            LoginTextBox.Focus();
        }
        //Завантаження форми
        private void AddNewUser_Load(object sender, EventArgs e)
        {
            if (_user.Id_role == 2)
            {
                string text = DisciplineComboBox.Text.Trim();
                MaterialsTabPageView();
                UserFillDisciplinesListBox(text);
                this.ActiveControl = LoadMaterialButton;
            }
            else
            {
                BasicAddFormView();
                this.ActiveControl = LoginTextBox;
            }
        }
        //Закриття форми додавання даних
        private void AddNewUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult affirmative = MyMessages.FormClosing();
            if (affirmative == DialogResult.Yes)
            {
                e.Cancel = false;
                AddUserClose();
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region Технічний функціонал
        #region Перевірка введених даних
        #region Вкладка "Користувач"
        //Перевірка даних поля введення прізвища на допустимість
        private void SirnameTextBox_Leave(object sender, EventArgs e)
        {
            string text = SirnameTextBox.Text.Trim();
            int dot = -1;
            if (!(Regex.Match(text, @"([А-Яа-яіІїЇєЄ-]+\s[А-Яа-яіІїЇєЄ]\.[А-Яа-яіІїЇєЄ]\.)").Success))
            {
                SirnameErrorProvider.SetError(SirnameTextBox, "Дані не відповідають стандарту прізвище-ініціали (Іванов І.І.)");
            }
            else
            {
                foreach (char i in text)
                {
                    if (i == '.' && dot == -1)
                    {
                        dot = text.IndexOf(i);
                    }
                    if (i == '\'')
                    {
                        text = ReplaceCharInString(text, text.IndexOf(i), '`');
                    }
                }
                text = ReplaceCharInString(text, dot - 1, char.ToUpper(text[dot - 1]));
                text = ReplaceCharInString(text, dot + 1, char.ToUpper(text[dot + 1]));
                text = char.ToUpper(text[0]) + text.Substring(1);
                SirnameTextBox.Text = text;
                SirnameErrorProvider.SetError(SirnameTextBox, String.Empty);
            }
            LanguageSets.Ukrainian();
        }
        //Заміна символу у рядку
        public String ReplaceCharInString(String str, int index, Char newSymb)
        {
            return str.Remove(index, 1).Insert(index, newSymb.ToString());
        }
        //Метод перевірки даних поля введення логіна
        private void LoginBox_Validating(object sender, CancelEventArgs e)
        {
            string login_in = LoginTextBox.Text.Trim();
            bool login = LoginCheck(login_in);
            if (login)
            {
                if (Cancel_Button.Enabled == false)
                {
                    Cancel_Button.Enabled = true;
                }
            }
        }
        //Перевірка логіну на допустимість
        private bool LoginCheck(string login_in)
        {
            bool flag = false;
            bool userexists = false;
            if (String.IsNullOrEmpty(login_in))
            {
                LoginErrorProvider.SetError(LoginTextBox, "Ви не ввели логін");
            }
            else if (login_in.Length > 45)
            {
                LoginErrorProvider.SetError(LoginTextBox, "Занадто довгий логін");
            }
            else
            {
                try
                {
                    using (MySqlConnection con = DBUtils.GetDBConnection())
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("DoesUserExists", con)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("_login", login_in);
                        using (var dataReader = cmd.ExecuteReader())
                        {
                            userexists = dataReader.Read();
                        }
                        if (userexists)
                        {
                            LoginErrorProvider.SetError(LoginTextBox, "Користувач з таким логіном вже існує у системі");
                        }
                        else
                        {
                            LoginErrorProvider.SetError(LoginTextBox, String.Empty);
                            flag = true;
                        }
                    }
                }
                catch (Exception e1)
                {
                    MyMessages.ErrorMessage(e1.Message);
                }
            }
            return flag;
        }
        //Метод перевірки даних поля введення пароля
        private void PasswordBox_Validating(object sender, CancelEventArgs e)
        {
            string password_in = PasswordTextBox.Text.Trim();
            bool password = PasswordCheck(password_in);
            if (password)
            {
                if (Cancel_Button.Enabled == false)
                {
                    Cancel_Button.Enabled = true;
                }
            }
        }
        //Перевірка допустимості пароля
        private bool PasswordCheck(string password_in)
        {
            bool flag = false;
            if (String.IsNullOrEmpty(password_in))
            {
                PasswordErrorProvider.SetError(PasswordTextBox, "Ви не ввели пароль");
            }
            else
            {
                if (password_in.Length < 8)
                {
                    PasswordErrorProvider.SetError(PasswordTextBox, "Занадто простий пароль");
                }
                else if (password_in.Length > 45)
                {
                    PasswordErrorProvider.SetError(PasswordTextBox, "Занадто довгий пароль");
                }
                else
                {
                    PasswordErrorProvider.SetError(PasswordTextBox, String.Empty);
                    flag = true;
                }
            }
            return flag;
        }
        //Метод перевірки даних поля введення прізвища
        private void SirnameBox_Validating(object sender, CancelEventArgs e)
        {
            string sirname_in = SirnameTextBox.Text.Trim();
            bool sirname = SirnameCheck(sirname_in);
            if (sirname)
            {
                if (Cancel_Button.Enabled == false)
                {
                    Cancel_Button.Enabled = true;
                }
                uConfirmButton.Enabled = true;
            }
        }
        //Перевірка допустимості прізвища
        private bool SirnameCheck(string sirname_in)
        {
            bool flag = false;
            if (String.IsNullOrEmpty(sirname_in))
            {
                SirnameErrorProvider.SetError(SirnameTextBox, "Ви не ввели прізвище користувача!");
            }
            else if (sirname_in.Length > 250)
            {
                SirnameErrorProvider.SetError(SirnameTextBox, "Увага! Занадто довге значення");
            }
            else
            {
                SirnameErrorProvider.SetError(SirnameTextBox, String.Empty);
                flag = true;
            }
            return flag;
        }
        //Перевірка введеного символу на допустимість (поле логіна)
        private void LoginBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string c = e.KeyChar.ToString();
            if (!(Regex.Match(c, @"[A-Za-z1234567890_-]").Success | e.KeyChar == 8 | e.KeyChar == 13))
            {
                LoginErrorProvider.SetError(LoginTextBox, "Дозволяються лише символи латинського алфавіту та цифри");
                e.Handled = true;
            }
            else
            {
                LoginErrorProvider.SetError(LoginTextBox, String.Empty); ;
            }
        }
        //Перевірка введеного символу на допустимість (поле пароля)
        private void PasswordBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string c = e.KeyChar.ToString();
            if (!(Regex.Match(c, @"[A-Za-z1234567890]").Success | e.KeyChar == 8))
            {
                PasswordErrorProvider.SetError(PasswordTextBox, "Дозволяються лише символи латинського алфавіту та цифри");
                e.Handled = true;
            }
            else
            {
                PasswordErrorProvider.SetError(PasswordTextBox, String.Empty); ;
            }
        }
        //Перевірка введеного символу на допустимість (поле прізвища)
        private void SirnameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string c = e.KeyChar.ToString();
            if (!(Regex.Match(c, @"[А-Яа-яіІїЇєЄ'`' '-.]").Success | e.KeyChar == 8) || Regex.Match(c, @"[ЫыъЪёЁэЭ]").Success)
            {
                SirnameErrorProvider.SetError(SirnameTextBox, "Дозволяються лише символи українського алфавіту");
                e.Handled = true;
            }
            else
            {
                SirnameErrorProvider.SetError(SirnameTextBox, String.Empty); ;
            }
        }
        #endregion

        #region Вкладка "Дисципліна"
        //Перевірка даних поля введення назви навчальної дисципліни
        private void DisciplineTextBox_Leave(object sender, EventArgs e)
        {
            string text = DisciplineTextBox.Text.Trim();
                if (!(Regex.Match(text, @"([A-Za-zА-Яа-я`'ієї-]+[A-Za-zА-Яа-я`'ієї]+)").Success))
                {
                    DisciplineErrorProvider.SetError(DisciplineTextBox, "Дані не відповідають прийнятному формату назви");
                }
                else
                {
                    foreach (char i in text)
                    {
                        if (i == '\'')
                        {
                            text = ReplaceCharInString(text, text.IndexOf(i), '`');
                        }
                    }
                    text = char.ToUpper(text[0]) + text.Substring(1);
                    DisciplineTextBox.Text = text;
                    DisciplineErrorProvider.SetError(DisciplineTextBox, String.Empty);
                }
            LanguageSets.Ukrainian();
        }
        //Перевірка даних поля введення назви навчальної дисципліни
        private bool DisciplineEntranceCheck()
        {
            string discipline_entered = DisciplineTextBox.Text.Trim();
            bool flag = false;
            int discipline = -1;

            if (String.IsNullOrEmpty(discipline_entered))
            {
                DisciplineErrorProvider.SetError(DisciplineTextBox, "Ви ввели порожній рядок!");

            }
            else if (discipline_entered.Length > 250)
            {
                DisciplineErrorProvider.SetError(DisciplineTextBox, "Занадто довге значення!");
            }
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SelectDisciplineID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("_name", discipline_entered);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            discipline = (int)r["id"];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MyMessages.ErrorMessage(e.Message);
            }
            if (discipline != -1)
            {
                DisciplineErrorProvider.SetError(DisciplineTextBox, "Навчальна дисципліна з такою назвою вже є!");
            }
            else
            {
                DisciplineErrorProvider.SetError(DisciplineTextBox, String.Empty);
                //dConfirmButton.Enabled = true;
                flag = true;
            }
            return flag;
        }
        //Перевірка введеного у поле назви дисципліни символу на коректність 
        private void DisciplineBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string c = e.KeyChar.ToString();
            if (!(Regex.Match(c, @"[A-Za-zА-Яа-яіІїЇєЄ'`' '-]").Success | e.KeyChar == 8) || Regex.Match(c, @"[ЫыъЪёЁэЭ]").Success)
            {
                DisciplineErrorProvider.SetError(DisciplineTextBox, "Дозволяються лише символи українського алфавіту");
                e.Handled = true;
            }
            else
            {
                DisciplineErrorProvider.SetError(DisciplineTextBox, String.Empty); ;
            }
        }
        //Перевірка введеного у поле назви дисципліни символу на коректність
        //private void DisciplineComboBox_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    string c = e.KeyChar.ToString();
        //    if (!(Regex.Match(c, @"[А-Яа-яіІїЇєЄ'`' ']").Success | e.KeyChar == 8))
        //    {
        //        MaterialDisciplineErrorProvider.SetError(MaterialDisciplineTextBox, "Дозволяються лише символи українського алфавіту");
        //        e.Handled = true;
        //    }
        //    else
        //    {
        //        MaterialDisciplineErrorProvider.SetError(MaterialDisciplineTextBox, String.Empty);
        //        //string discipline_in = MaterialDisciplineTextBox.Text.Trim();
        //        /*if (!(String.IsNullOrEmpty(textBox1.Text.Trim())))
        //        {
        //            FillComboBox(discipline_in);
        //        }*/
        //    }
        //}
        #endregion

        #region Вкладка "Матеріал"
        //Перевірка даних у полі введення навчальної дисципліни для матеріалу на коректність
        private void DisciplineComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(DisciplineComboBox.Text.Trim()))
            {
                DisciplineErrorProvider.SetError(DisciplineComboBox, "Ви не обрали навчальну дисципліну");
            }
            else
            {
                DisciplineErrorProvider.SetError(DisciplineComboBox, String.Empty);
            }
        }
        //Перевірка даних поля введення типу документа
        private void DocumentTypeComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(DocumentTypeComboBox.Text.Trim()))
            {
                DocumentTypeErrorProvider.SetError(DocumentTypeComboBox, "Ви не обрали тип документа");
            }
            else
            {
                DocumentTypeErrorProvider.SetError(DocumentTypeComboBox, String.Empty);
            }
        }
        //Перевірка даних поля введення типу матеріала
        private void MaterialTypeComboBox_Validating(object sender, CancelEventArgs e)
        {
            //Якщо поле введення порожнє - видати помилку
            if (String.IsNullOrEmpty(MaterialTypeComboBox.Text.Trim()))
            {
                MaterialTypeErrorProvider.SetError(MaterialTypeComboBox, "Ви не обрали тип навчального матеріала");
            }
            else
            {
                MaterialTypeErrorProvider.SetError(MaterialTypeComboBox, String.Empty);
            }
        }
        #endregion

        #region Вкладка "Навчальне навантаження"
        //Перевірка введеного у поле прізвища викладача символу на коректність (навчальне навантаження)
        private void StudyloadUserNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string c = e.KeyChar.ToString();
            if (!(Regex.Match(c, @"[А-Яа-яіІїЇєЄ'`]").Success | e.KeyChar == 8) || Regex.Match(c, @"[ЫыъЪёЁэЭ]").Success)
            {
                StudyloadUserNameErrorProvider.SetError(StudyloadUserNameTextBox, "Дозволяються лише символи українського алфавіту");
                e.Handled = true;
            }
            else
            {
                StudyloadUserNameErrorProvider.SetError(StudyloadUserNameTextBox, String.Empty); ;
            }
        }
        ////Перевірка даних у полі введення прізвища викладача дисципліни (навчальне навантаження)
        private void StudyloadUserNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            string sirname_in = StudyloadUserNameTextBox.Text.Trim();
            if (String.IsNullOrEmpty(sirname_in) && StudyloadUsersListBox.SelectedIndex == -1)
            {
                StudyloadUserNameErrorProvider.SetError(StudyloadUserNameTextBox, "Ви не ввели прізвище викладача");
            }
            else if (sirname_in.Length > 250)
            {
                StudyloadUserNameErrorProvider.SetError(StudyloadUserNameTextBox, "Увага! Занадто довге значення");
            }
            else
            {
                StudyloadUserNameErrorProvider.SetError(StudyloadUserNameTextBox, String.Empty);
            }
        }
        //Перевірка введеного у поле назви дисципліни символу на коректність (навчальне навантаження)
        private void StudyloadDisciplineTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string c = e.KeyChar.ToString();
            if (!(Regex.Match(c, @"[A-Za-zА-Яа-яіІїЇєЄ'`' ']").Success | e.KeyChar == 8) || Regex.Match(c, @"[ЫыъЪёЁэЭ]").Success)
            {
                StudyloadDisciplinesErrorProvider.SetError(StudyloadDisciplineTextBox, "Дозволяються лише символи українського алфавіту");
                e.Handled = true;
            }
            else
            {
                StudyloadDisciplinesErrorProvider.SetError(StudyloadDisciplineTextBox, String.Empty); ;
            }
        }
        //Перевірка даних у полі введення назви навчальної дисципліни (навчальне навантаження)
        private void StudyloadDisciplineTextBox_Validating(object sender, CancelEventArgs e)
        {
            string sirname_in = StudyloadDisciplineTextBox.Text.Trim();
            if (String.IsNullOrEmpty(sirname_in) && StudyloadDisciplinesListBox.SelectedIndex == -1)
            {
                StudyloadDisciplinesErrorProvider.SetError(StudyloadDisciplineTextBox, "Ви не ввели назву навчальної дисципліни");
            }
            else if (sirname_in.Length > 250)
            {
                StudyloadDisciplinesErrorProvider.SetError(SirnameTextBox, "Увага! Занадто довге значення");
            }
            else
            {
                StudyloadDisciplinesErrorProvider.SetError(StudyloadDisciplineTextBox, String.Empty);
            }
        }
        #endregion


        #endregion

        #region Дії
        #region Вкладка "Дисципліна"
        //Обробник події зміни тексту поля введення назви навчальної дисципліни
        private void DisciplineBox_TextChanged(object sender, EventArgs e)
        {
            string discipline_in = DisciplineTextBox.Text.Trim();
            if (!(String.IsNullOrEmpty(discipline_in)))
            {
                ListFill(discipline_in);
                DisciplinesCancelButton.Enabled = true;
            }
            else
            {
                DisciplinesCancelButton.Enabled = false;
            }
        }
        //Очищення компонентів вкладки навчальна дисципліна
        private void DisciplineTabClear()
        {
            DisciplineTextBox.Text = String.Empty;
            DisciplineErrorProvider.SetError(DisciplineTextBox, String.Empty);
            string discipline_in = DisciplineTextBox.Text.Trim();
            ListFill(discipline_in);
        }
        //Встановлення початкових розмірів вкладки додавання даних про навчальну дисципліну
        private void DisciplinesTabPageView()
        {
            Width = 380;
            //tabControl1.Width = 265;
            //tabControl1.Height = 330;
            Height = 386;
            string discipline_in = DisciplineTextBox.Text.Trim();
            ListFill(discipline_in);
            if (!(String.IsNullOrEmpty(DisciplineErrorProvider.GetError(DisciplineTextBox))) &&
                String.IsNullOrEmpty(DisciplineTextBox.Text.Trim()))
            {
                DisciplineErrorProvider.SetError(DisciplineTextBox, String.Empty);
            }
            DisciplineTextBox.Focus();
            //dConfirmButton.Enabled = false;
            DisciplinesCancelButton.Enabled = false;
        }
        //Заповнення списку дисциплін
        private void ListFill(string text)
        {
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    listBox1.Items.Clear();
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SelectDisciplineLikeEntered", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("_name", text);
                    MySqlDataReader drd = cmd.ExecuteReader();
                    while (drd.Read())
                    {
                        listBox1.Items.Add(drd["name"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MyMessages.ErrorMessage(e.Message);
            }
        }
        #endregion

        #region Вкладка "Користувач"
        //Очищення компонентів вкладки "Користувач"
        private void UserDataTabClear()
        {
            LoginTextBox.Text = "";
            PasswordTextBox.Text = "";
            SirnameTextBox.Text = "";
            uConfirmButton.Enabled = false;
            Cancel_Button.Enabled = false;
            if (AdminRadio.Checked)
            {
                AdminRadio.Checked = false;
            }
            if (!(TeacherRadio.Checked))
            {
                TeacherRadio.Checked = true;
            }
            LoginErrorProvider.SetError(LoginTextBox, String.Empty);
            PasswordErrorProvider.SetError(PasswordTextBox, String.Empty);
            SirnameErrorProvider.SetError(SirnameTextBox, String.Empty);
        }
        //Активація поля введення логіну користувача
        private void LoginBox_Enter(object sender, EventArgs e)
        {
            LanguageSets.English();
            LoginTextBox.SelectionStart = LoginTextBox.Text.Length + 1;
        }
        //Встановлення початкових розмірів вкладки додавання даних про користувача
        private void UserTabPageView()
        {
            Width = 330;
            Height = 400;
            //tabControl1.Width = 265;
            //tabControl1.Height = 294;
            LoginTextBox.Focus();
            if (!(String.IsNullOrEmpty(LoginErrorProvider.GetError(LoginTextBox))) &&
                String.IsNullOrEmpty(LoginTextBox.Text.Trim()))
            {
                LoginErrorProvider.SetError(LoginTextBox, String.Empty);
            }
            if (!(String.IsNullOrEmpty(PasswordErrorProvider.GetError(PasswordTextBox))) &&
                String.IsNullOrEmpty(PasswordTextBox.Text.Trim()))
            {
                PasswordErrorProvider.SetError(PasswordTextBox, String.Empty);
            }
            if (!(String.IsNullOrEmpty(SirnameErrorProvider.GetError(SirnameTextBox))) &&
                String.IsNullOrEmpty(SirnameTextBox.Text.Trim()))
            {
                SirnameErrorProvider.SetError(SirnameTextBox, String.Empty);
            }
            uConfirmButton.Enabled = false;
            Cancel_Button.Enabled = false;
        }
        #endregion

        #region Вкладка "Матеріал"
        //Обробник події зміни тексту поля обору назви дисципліни для навчального матеріалу
        private void DisciplineComboBox_TextChanged(object sender, EventArgs e)
        {
            string discipline_in = DisciplineComboBox.Text.Trim();
            if (String.IsNullOrEmpty(discipline_in))
            {
                UserFillDisciplinesListBox(discipline_in);
            }
            else if (DisciplineComboBox.SelectedIndex == -1)
            {
                UserFillDisciplinesListBox(discipline_in);
            }
            DisciplineComboBox.Select(DisciplineComboBox.Text.Length, 0);
            if (!String.IsNullOrEmpty(discipline_in))
            {
                LoadMaterialButton.Enabled = true;
            }
            else
            {
                LoadMaterialButton.Enabled = false;
            }
            FileViewButton.Enabled = false;
        }
        //Заповення списку навчальних дисциплін
        private void ListFill1(string text)
        {
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    DisciplineComboBox.Items.Clear();
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SelectDisciplineLikeEntered", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("_name", text);
                    MySqlDataReader drd = cmd.ExecuteReader();
                    while (drd.Read())
                    {
                        DisciplineComboBox.Items.Add(drd["name"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MyMessages.ErrorMessage(e.Message);
            }
        }
        //Заповнення списку дисциплін викладача
        private void UserFillDisciplinesListBox(string text)
        {
            int _user_ID = -1;
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    DisciplineComboBox.Items.Clear();
                    con.Open();
                    if (_user.Id_role == 2)
                    {
                        MySqlCommand cmd = new MySqlCommand("GetUserID", con)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("_username", _user.Fio);
                        cmd.Parameters.AddWithValue("_role",_user.Id_role);
                        MySqlDataReader drd = cmd.ExecuteReader();
                        while (drd.Read())
                        {
                            _user_ID = (int)drd["ID"];
                        }
                        drd.Close();

                        MySqlCommand cmd1 = new MySqlCommand("DisciplinesOfTheUser", con)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        cmd1.Parameters.AddWithValue("_discipline", text);
                        cmd1.Parameters.AddWithValue("_user_ID", _user_ID);
                        drd = cmd1.ExecuteReader();
                        while (drd.Read())
                        {
                            DisciplineComboBox.Items.Add(drd["Discipline"].ToString());
                        }
                    }
                    else
                    {
                        MySqlCommand cmd1 = new MySqlCommand("SelectDisciplineLikeEntered", con)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        cmd1.Parameters.AddWithValue("_name", text);
                        MySqlDataReader drd = cmd1.ExecuteReader();
                        while (drd.Read())
                        {
                            DisciplineComboBox.Items.Add(drd["name"].ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MyMessages.ErrorMessage(e.Message);
            }
        }
        //Обробник події зміни тексту (шлях до файлу)
        private void FilePathLabel_TextChanged(object sender, EventArgs e)
        {
            var target_directory = ConfigurationManager.AppSettings.Get("TargetFileDirectory");
            FileInfo file = new FileInfo(Materials.Filepath);
            string labeltext = FilePathLabel.Text.Trim();
            string folder1 = Discipline.Discipline_name;
            string folder2 = _user.Fio;
            string folder3 = Materials.Materialtype;
            string path = folder1 + @"\" + folder2 + @"\" + folder3 + @"\";
            FileInfo newfile = new FileInfo(target_directory + path + file.Name);
            //змінна-покажчик існування файлу у базі даних
            bool documentxists = false;
            //якщо шлях до файлу вказаний
            if (!(String.IsNullOrEmpty(labeltext)))
            {
                //дозволити використовувати кнопку для перегляду вмісту файлу
                FileViewButton.Enabled = true;
                if (newfile.Exists)
                {
                    documentxists = true;
                }
                else
                { //Отримання розміру файлу
                    Materials.Size = file.Length;
                }
                //якщо документ існує - видати помилку
                if (documentxists)
                {
                  MaterialFilePathErrorProvider.SetError(LoadMaterialButton, "Такий документ вже існує у системі");
                }
                else
                {
                  MaterialFilePathErrorProvider.SetError(LoadMaterialButton, String.Empty);
                  AddTagButton.Enabled = true;
                }
            }
            else
            {
                //заборонити використання кнопки для перегляду вмісту файлу
                FileViewButton.Enabled = false;
            }
        }
        //Активація кнопки перегляду обраного документа
        private void FileViewButton_Enter(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(FilePathLabel.Text.Trim()))
            {
                MaterialFilePathErrorProvider.SetError(FilePathLabel, "Ви не обрали документ для завантаження");
                FileViewButton.Enabled = false;
            }
        }
        //Зміна тексту у полі вибору типу документа
        private void DocumentTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(DocumentTypeComboBox.Text.Trim()))
            {
                DisciplineComboBox.Enabled = false;
            }
            else
            {
                DisciplineComboBox.Enabled = true;
                Type_of_document = DocumentTypeComboBox.Text.Trim();
            }
        }
        //Очищення компонентів вкладки матеріал
        private void MaterialsTabClear()
        {
            FilePathLabel.Text = String.Empty;
            MaterialTypeComboBox.Text = String.Empty;
            DocumentTypeComboBox.Text = String.Empty;
            if (!(String.IsNullOrEmpty(DocumentTypeErrorProvider.GetError(DocumentTypeComboBox))))
            {
              DocumentTypeErrorProvider.SetError(DocumentTypeComboBox, String.Empty);
            }
            DisciplineComboBox.Text = String.Empty;
            if (!(String.IsNullOrEmpty(MaterialFilePathErrorProvider.GetError(LoadMaterialButton))))
            {
                MaterialFilePathErrorProvider.SetError(LoadMaterialButton, String.Empty);
            }
            if (!(String.IsNullOrEmpty(MaterialDisciplineErrorProvider.GetError(DisciplineComboBox))))
            {
                MaterialDisciplineErrorProvider.SetError(DisciplineComboBox, String.Empty);
            }
            if (!(String.IsNullOrEmpty(MaterialTypeErrorProvider.GetError(MaterialTypeComboBox))))
            {
                MaterialTypeErrorProvider.SetError(MaterialTypeComboBox, String.Empty);
            }
            DocumentTypeComboBox.Enabled = false;
            DisciplineComboBox.Enabled = false;
            LoadMaterialButton.Enabled = false;
            FileViewButton.Enabled = false;
            AddTagButton.Enabled = false;
            MaterialTypeComboBox.Focus();
        }
        //Отримання унікального коду доданого документа
        private static int GetDocumentId(MySqlConnection con)
        {
            int documentid = -1;
            MySqlCommand cmd1 = new MySqlCommand("GetIDOfAddedMaterial", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd1.Parameters.AddWithValue("_Path", Materials.Filepath);
            using (MySqlDataReader r = cmd1.ExecuteReader())
            {
                while (r.Read())
                {
                    documentid = (int)r["AddedMaterialID"];
                }
            }
            return documentid;
        }
        //Отримання типу матеріала
        private int GetMaterialTypeId(string text)
        {
            int materialtypeid = -1;
            using (MySqlConnection con = DBUtils.GetDBConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SelectMaterialTypeLikeEntered", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("_name", text);
                using (MySqlDataReader r = cmd.ExecuteReader())
                    while (r.Read())
                    {
                        materialtypeid = (int)r["id"];
                    }
            }
            return materialtypeid;
        }
        //Отримання типу документа
        private int GetDocumentType(MySqlConnection con)
        {
            int typeid = -1;
            MySqlCommand cmd = new MySqlCommand("GetDocumentTypeLikeEntered", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (groupBox9.Visible == true)
            {
                cmd.Parameters.AddWithValue("_name", DocumentTypeComboBox.Text.Trim());
            }
            else
            {
                cmd.Parameters.AddWithValue("_name", Materials.Documenttype);
            }
            using (MySqlDataReader r = cmd.ExecuteReader())
                while (r.Read())
                {
                    typeid = (int)r["id"];
                }
            return typeid;
        }
        //Прив'язування ключових слів до документа
        private static void Bound()
        {
            int documentid = -1;
            int tag_id = -1;
            int i = 0;
            string error = null;
            if (Materials.Tags.Count > 0)
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd4 = new MySqlCommand("GetIDOfAddedMaterial", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd4.Parameters.AddWithValue("_Path", Materials.Filepath);
                    using (MySqlDataReader r = cmd4.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            documentid = (int)r["AddedMaterialID"];
                        }
                    }

                    MySqlCommand cmd5 = new MySqlCommand("BoundMaterialTag", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };


                    MySqlCommand command = new MySqlCommand("GetTagID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    foreach (string item in Materials.Tags)
                    {
                        cmd5.Parameters.Clear();
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("_Tagname", item);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tag_id = (int)reader["Tag_id"];
                            }
                        }
                        cmd5.Parameters.AddWithValue("_TagID", tag_id);
                        cmd5.Parameters.AddWithValue("_MaterialID", documentid);
                        int rows1 = cmd5.ExecuteNonQuery();
                        if (rows1 >= 0)
                        {
                            i++;
                        }
                        else
                        {
                            using (MySqlDataReader reader1 = cmd5.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    error = reader1["@full_error"].ToString();
                                }
                            }
                            MyMessages.ErrorMessage(error);
                        }
                    }
                    MessageBox.Show("За документом було закріплено "+i+" ключових слів");
                    Materials.Tags.Clear();
                }
            }
        }
        //Заповнення списку матеріалів
        private void FillMaterialTypeComboBox()
        {
            MaterialTypeComboBox.Items.Clear();
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SelectMaterialType", con);
                    MySqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        MaterialTypeComboBox.Items.Add(r["name"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MyMessages.ErrorMessage(e.Message);
            }
        }
        //Встановлення початкових розмірів вкладки додавання даних про матеріал
        private void MaterialsTabPageView()
        {
            Width = 566;
            Height = 360;
            tabControl1.Width = 550;
            tabControl1.Height = 300;
            MaterialTypeComboBox.Focus();
            FillMaterialTypeComboBox();
            FillDocumentTypeComboBox();
            if (String.IsNullOrEmpty(MaterialTypeComboBox.Text.Trim()))
            {
                DocumentTypeComboBox.Enabled = false;
            }
            else
            {
                DocumentTypeComboBox.Enabled = true;
            }
            if (String.IsNullOrEmpty(DocumentTypeComboBox.Text.Trim()))
            {
                DisciplineComboBox.Enabled = false;
            }
            else
            {
                DisciplineComboBox.Enabled = true;
            }
            if (String.IsNullOrEmpty(DisciplineComboBox.Text.Trim()))
            {
                LoadMaterialButton.Enabled = false;
                FileViewButton.Enabled = false;
                AddTagButton.Enabled = false;
            }
            else
            {
                LoadMaterialButton.Enabled = true;
            }
            UserFillDisciplinesListBox(DisciplineComboBox.Text.Trim());
        }
        //Заповлення списку типів документів
        private void FillDocumentTypeComboBox()
        {
            DocumentTypeComboBox.Items.Clear();
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SelectDocumentType", con);
                    MySqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        DocumentTypeComboBox.Items.Add(r["name"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MyMessages.ErrorMessage(e.Message);
            }
        }
        #endregion

        #region Вкладка "Навчальне навантаження"
        //Встановлення початкових розмірів вкладки додавання даних про навчальне навантаження
        private void StudyLoadTabPageView()
        {
            Width = 570;
            Height = 295;
            tabControl1.Width = 549;
            tabControl1.Height = 211;
            StudyloadUserNameTextBox.Focus();
            if (!(String.IsNullOrEmpty(StudyloadUserNameErrorProvider.GetError(StudyloadUserNameTextBox))) &&
                String.IsNullOrEmpty(StudyloadUserNameTextBox.Text.Trim()))
            {
                StudyloadUserNameErrorProvider.SetError(StudyloadUserNameTextBox, String.Empty);
            }
            if (!(String.IsNullOrEmpty(StudyloadDisciplinesErrorProvider.GetError(StudyloadDisciplineTextBox))) &&
                String.IsNullOrEmpty(StudyloadDisciplineTextBox.Text.Trim()))
            {
                StudyloadDisciplinesErrorProvider.SetError(StudyloadUserNameTextBox, String.Empty);
            }
            StudyloadUserNamesListFill(StudyloadUserNameTextBox.Text.Trim());
            StudyloadDisciplinesListFill(StudyloadDisciplineTextBox.Text.Trim());
            groupBox8.Visible = false;
            StudyloadToDBlistBox.Items.Clear();
        }
        //Оборобник події зміни тексту поля введення прізвища викладача (навчальне навантаження)
        private void StudyloadUserNameTextBox_TextChanged(object sender, EventArgs e)
        {
            string user_in = StudyloadUserNameTextBox.Text.Trim();
            StudyloadUserNamesListFill(user_in);
        }
        //Оборобник події зміни тексту поля введення навчальної дисципліни (навчальне навантаження)
        private void StudyloadDisciplineTextBox_TextChanged(object sender, EventArgs e)
        {
            string discipline_in = StudyloadDisciplineTextBox.Text.Trim();
            StudyloadDisciplinesListFill(discipline_in);
        }
        //Заповнення списку викладачів (навчальне навантаження)
        private void StudyloadUserNamesListFill(string user_in)
        {
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    StudyloadUsersListBox.Items.Clear();
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SelectTeacherLikeEntered", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("_name", user_in);
                    MySqlDataReader drd = cmd.ExecuteReader();
                    while (drd.Read())
                    {
                        StudyloadUsersListBox.Items.Add(drd["sirname"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MyMessages.ErrorMessage(e.Message);
            }
        }
        //Заповнення списку навчальних дисциплін (навчальне навантаження)
        private void StudyloadDisciplinesListFill(string discipline_in)
        {
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    StudyloadDisciplinesListBox.Items.Clear();
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SelectDisciplineLikeEntered", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("_name", discipline_in);
                    MySqlDataReader drd = cmd.ExecuteReader();
                    while (drd.Read())
                    {
                        StudyloadDisciplinesListBox.Items.Add(drd["name"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MyMessages.ErrorMessage(e.Message);
            }
        }
        //Вибір викладача зі списку (навчальне навантаження)
        private void StudyloadUsersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                StudyloadUserNameTextBox.Text = StudyloadUsersListBox.SelectedItem.ToString();
                StudyloadUserNameTextBox.Focus();
            }
            catch (Exception e1)
            {
                MyMessages.ErrorMessage(e1.Message);
            }
        }
        //Вибір навчальної дисципліни зі списку (навчальне навантаження)
        private void StudyloadDisciplinesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                StudyloadDisciplineTextBox.Text = StudyloadDisciplinesListBox.SelectedItem.ToString();
                StudyloadDisciplineTextBox.Focus();
            }
            catch (Exception e1)
            {
                MyMessages.ErrorMessage(e1.Message);
            }
        }
        //Очищення компоентів вкладки "навчальне навантаження"
        private void StudyloadTabClear()
        {
            StudyloadDisciplineTextBox.Text = String.Empty;
            StudyloadUserNameTextBox.Text = String.Empty;
            StudyloadDisciplinesErrorProvider.SetError(StudyloadDisciplineTextBox, String.Empty);
            StudyloadUserNameErrorProvider.SetError(StudyloadUserNameTextBox, String.Empty);
            if (StudyloadToDBlistBox.Items.Count > 0)
            {
                DialogResult DB_affirmative = MyMessages.DBAbortDataEntrance();
                if (DB_affirmative == DialogResult.Yes)
                {
                    StudyloadToDBlistBox.Items.Clear();
                }
            }
        }
        //Зміна розмірів вкладки навчальне навантаження
        private void StudyloadTabResize()
        {
            Width = 569;
            Height = 500;
            tabControl1.Width = 549;
            tabControl1.Height = 430;
            groupBox8.Visible = true;
        }
        //Отримання унікального коду навчального навантаження
        private /*static*/ int GetStudyloadID(MySqlConnection con)
        {
            int studyload_id = -1;
            MySqlCommand cmd = new MySqlCommand("GetStudyloadID", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("_name", Discipline.Discipline_name);
            cmd.Parameters.AddWithValue("_userfio", _user.Fio);
            cmd.Parameters.AddWithValue("_userrole", _user.Id_role);
            //cmd.Parameters.AddWithValue("_userid",Users.Id);
            using (MySqlDataReader r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    studyload_id = (int)r["studyload_id"];
                }
            }
            return studyload_id;
        }
        #endregion

        //Очищення компонентів всіх вкладок при закриття форми
        private void AddUserClose()
        {
            UserDataTabClear();
            DisciplineTabClear();
            MaterialsTabClear();
            StudyloadTabClear();
        }
        //Вибір вкладки форми додавання даних
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Text)
            {
                case "Користувач":
                    UserTabPageView();
                    break;
                case "Дисципліни":
                    DisciplinesTabPageView();
                    break;
                case "Матеріали":
                    MaterialsTabPageView();
                    break;
                case "Навчальне навантаження":
                    StudyLoadTabPageView();
                    break;
            }
        }
        #endregion

        #endregion

        #region Функціонал користувача
        #region Вкладка "Користувач"
        //Вибір ролі "Адміністратор" для користувача 
        private void AdminRadio_CheckedChanged(object sender, EventArgs e)
        {
			if (AdminRadio.Checked == true)
			{
				AdminRadio.Checked = true;
				TeacherRadio.Checked = false;
			}
        }
        //Вибір ролі "Викладач" для користувача 
        private void TeacherRadio_CheckedChanged(object sender, EventArgs e)
        {
			if (TeacherRadio.Checked == true)
			{
				AdminRadio.Checked = false;
				TeacherRadio.Checked = true;
			}
        }
        //Клік по кнопці введення даних про користувача
        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult affirmative = MyMessages.AbortDataEntrance();
            if (affirmative == DialogResult.Yes)
            {
                UserDataTabClear();
            }
        }
        //Клік по кнопці додавання даних про нового користувача
        private void UConfirmButton_Click(object sender, EventArgs e)
        {
            int user_role = 0;
            string error = null;
            string sirname = SirnameTextBox.Text.Trim();
            if ((String.IsNullOrEmpty(LoginErrorProvider.GetError(LoginTextBox))) &&
                (String.IsNullOrEmpty(PasswordErrorProvider.GetError(PasswordTextBox))) &&
                (String.IsNullOrEmpty(SirnameErrorProvider.GetError(SirnameTextBox))) &&
                !(String.IsNullOrEmpty(LoginTextBox.Text.Trim())) &&
                !(String.IsNullOrEmpty(PasswordTextBox.Text.Trim())) &&
                !(String.IsNullOrEmpty(SirnameTextBox.Text.Trim())))
            {
                try
                {
                    using (MySqlConnection con = DBUtils.GetDBConnection())
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("AddNewUser", con)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("_login", LoginTextBox.Text.Trim());
                        cmd.Parameters.AddWithValue("_password", PasswordTextBox.Text.Trim());
                        if (AdminRadio.Checked)
                        {
                            user_role = 1;
                        }
                        else if (TeacherRadio.Checked)
                        {
                            user_role = 2;
                        }
                        cmd.Parameters.AddWithValue("_role", user_role);
                        cmd.Parameters.AddWithValue("_sirname", sirname);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows >= 0)
                        {
                            MessageBox.Show("Запис було успішно додано");
                            UserDataTabClear();
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
                    }
                }
                catch (Exception e1)
                {
                    MyMessages.ErrorMessage(e1.Message);
                }
            }
            else
            {
                MyMessages.WarningMessage("Введені дані не є коректними. Будь ласка перевірте введення даних");
            }
            LoginTextBox.Focus();
        }
        //Зупинка курсору миші над групою "Роль користувача у системі"
        private void GroupBox2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(groupBox2, "Впливає на кількість привілегій у програмній системі");
        }
        //Зупинка курсору миші над перемикачем "Адміністратор"
        private void AdminRadio_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(AdminRadio, "Повний доступ до даних програмної системи");
        }
        //Зупинка курсору миші над перемикачем "Викладач"
        private void TeacherRadio_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(TeacherRadio, "Привілегії на додавання, редагування та видалення матеріалів з навчальних дисциплін");
        }
        //Зупинка курсору миші над полем введення логіна користувача
        private void LoginTextBox_MouseHover(object sender, EventArgs e)
        {
            //виведення підказки
            toolTip1.SetToolTip(LoginTextBox, "Унікальне ім'я користувача у програмній системі (не більше 45 символів)");
        }
        //Зупинка курсору миші над полем введення пароля користувача
        private void PasswordTextBox_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(PasswordTextBox, "Пароль користувача для авторизації у програмній системі (не менше 8 символів)");
        }
        //Зупинка курсору миші над полем введення прізвища користувача
        private void SirnameTextBox_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip1.SetToolTip(SirnameTextBox, "Прізвище користувача");
        }
        #endregion

        #region Вкладка "Дисципліна"
        //Клік по кнопці відміни введення даних про навчальну дисципліну
        private void DisciplinesCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult affirmative = MyMessages.AbortDataEntrance();
            if (affirmative == DialogResult.No)
            {

            }
            else if (affirmative == DialogResult.Yes)
            {
                DisciplineTabClear();
            }
        }
        //Клік по кнопці додавання даних про нову навчальну дисципліну
        private void DConfirmButton_Click(object sender, EventArgs e)
        {
            bool Discipline_check = DisciplineEntranceCheck();
            string error = null;
            if (Discipline_check)
            {
                try
                {
                    string input = DisciplineTextBox.Text.Trim();
                    input = char.ToUpper(input[0]) + input.Substring(1);
                    using (MySqlConnection con = DBUtils.GetDBConnection())
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("AddNewDiscipline", con)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("_name", input);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows >= 0)
                        {
                            MessageBox.Show("Запис було успішно додано");
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

                    }
                }
                catch (Exception e1)
                {
                    MyMessages.ErrorMessage(e1.Message);
                }
            }
            else
            {
                MyMessages.WarningMessage("Така навчальна дисципліна вже існує");
            }
            DisciplinesTabPageView();
        }
        #endregion

        #region Вкладка "Матеріал"
        //Зупинка курсору миші над полем обору типу документа
        private void DocumentTypeComboBox_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(DocumentTypeComboBox, "Впливає на тип файлу, що завантажується");
        }
        //Клік на кнопці переходу до введення ключових слів
        private void AddTagButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(FilePathLabel.Text.Trim()))
            {
                MaterialFilePathErrorProvider.SetError(LoadMaterialButton, "Документ не обрано!");
            }
            else if (String.IsNullOrEmpty(DisciplineComboBox.Text.Trim()))
            {
                MaterialDisciplineErrorProvider.SetError(DisciplineComboBox, "Навчальна дисципліна не була вказана!");
            }
            else
            {
                if (!(String.IsNullOrEmpty(MaterialFilePathErrorProvider.GetError(LoadMaterialButton))))
                {
                    MaterialFilePathErrorProvider.SetError(LoadMaterialButton, String.Empty);
                }
                if (!(String.IsNullOrEmpty(MaterialDisciplineErrorProvider.GetError(DisciplineComboBox))))
                {
                    MaterialDisciplineErrorProvider.SetError(DisciplineComboBox, String.Empty);
                }
                //Materials.Documentname = System.IO.Path.GetFileNameWithoutExtension(Materials.Filepath);
                Discipline.Discipline_name = DisciplineComboBox.Text.Trim();
                Tags form = new Tags(_user)
                {
                    Owner = this
                };
                form.ShowDialog();
                form.Dispose();
            }
        }
        //Клік на кнопці відміни введення даних про навчальний матеріал
        private void MaterialsCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult affirmative = MyMessages.AbortDataEntrance();
            if (affirmative == DialogResult.Yes)
            {
                MaterialsTabClear();
            }
        }
        //Клік по кнопці введення даних про матеріал до бази даних
        private void MaterialsConfirmButton_Click(object sender, EventArgs e)
        {
            Materials.Materialtype = MaterialTypeComboBox.Text.Trim();
            int TypeID = -1;
            int ID_specification = -1;
            string error = null;
            if ((String.IsNullOrEmpty(FilePathLabel.Text)) ||
                !(String.IsNullOrEmpty(MaterialTypeErrorProvider.GetError(MaterialTypeComboBox))) ||
                !(String.IsNullOrEmpty(DocumentTypeErrorProvider.GetError(DocumentTypeComboBox))) ||
                !(String.IsNullOrEmpty(MaterialDisciplineErrorProvider.GetError(DisciplineComboBox))))
            {
                MyMessages.WarningMessage("Введені дані не є коректними. Будь ласка перевірте введення даних");
            }
            else
            {
                //var target_directory = ConfigurationManager.AppSettings.Get("TargetFileDirecory");
                
                FileInfo file = new FileInfo(Materials.Filepath);
                string labeltext = FilePathLabel.Text.Trim();
                string folder1 = null;
                string folder2 = _user.Fio;
                string folder3 = Materials.Materialtype;

                if (Materials.Tags.Count == 0)
                {
                    Discipline.Discipline_name = DisciplineComboBox.Text.Trim();
                }
                folder1 = Discipline.Discipline_name;
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    Studyload.Studyloadid = GetStudyloadID(con);
                    TypeID = GetDocumentType(con);
                    ID_specification = GetMaterialTypeId(Materials.Materialtype);

                    MySqlCommand cmd = new MySqlCommand("AddNewMaterial", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    string path = folder1 + @"\" + folder2 + @"\" + folder3 + @"\";
                    FileInfo newfile = new FileInfo(target_directory + path + file.Name);
                    try
                    {
                        if (!Directory.Exists(target_directory + path))
                        {
                            Directory.CreateDirectory(target_directory + path);
                        }
                        file.CopyTo(target_directory + path + file.Name, true);
                        newfile = new FileInfo(target_directory + path + file.Name);
                        if (newfile.Exists && newfile.Length == Materials.Size)
                        {
                            MessageBox.Show("Файл було успішно завантажено на сервер." +
                                "Розміщення файлу: " + newfile.FullName);
                            Materials.Filepath = newfile.FullName;
                        }
                        else
                        {
                            MyMessages.WarningMessage("Файл було завантажено з помилками. " +
                                "Будь ласка повторіть завантаження");
                        }
                    }
                    catch (Exception e1)
                    {
                        MyMessages.ErrorMessage(e1.Message);
                    }
                    cmd.Parameters.AddWithValue("_StudyloadID", Studyload.Studyloadid);
                    cmd.Parameters.AddWithValue("_Path", Materials.Filepath);
                    cmd.Parameters.AddWithValue("_NameOfMaterial", Materials.Documentname);
                    cmd.Parameters.AddWithValue("_ID_specification", ID_specification);
                    cmd.Parameters.AddWithValue("_DocumentTypeID", TypeID);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows >= 0)
                    {
                        Bound();
                       MessageBox.Show("Навчальний матеріал було успішно додано до бази даних");
                    }
                    else
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                error = reader["@full_error"].ToString();
                            }
                        }
                        MyMessages.ErrorMessage(error);
                    }
                    MaterialsTabClear();
                }
            }
        }

        private int AddNewSpecification()
        {
            int id_spec = -1;
            string text = MaterialTypeComboBox.Text.Trim();
            string error = null;
            DialogResult affirmative = MyMessages.QuestionMessage("Виявлено тип матеріалу, якого немає у базу даних. " +
                    "Бажаєте додати його до бази даних?");
                if (affirmative == DialogResult.No)
                {
                    MaterialTypeErrorProvider.SetError(MaterialTypeComboBox, "Оберіть тип навчального матеріалу");
                    MaterialTypeComboBox.Text = String.Empty;
                }
                else
                {
                    try
                    {
                        using (MySqlConnection connection = DBUtils.GetDBConnection())
                        {
                            connection.Open();
                            MySqlCommand command = new MySqlCommand("AddNewSpecification", connection)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            command.Parameters.AddWithValue("_name", text);
                            int rows = command.ExecuteNonQuery();
                            if (rows >= 0)
                            {
                                MessageBox.Show("Тип навчального матеріалу було успішно додано до бази даних");
                            }
                            else
                            {
                                using (MySqlDataReader r = command.ExecuteReader())
                                {
                                    while (r.Read())
                                    {
                                        error = r["@full_error"].ToString();
                                    }
                                }
                                MyMessages.ErrorMessage(error);
                            }
                            id_spec = GetMaterialTypeId(text);
                        }
                    }
                    catch (Exception e)
                    {
                        MyMessages.ErrorMessage(e.Message);
                    }
                
                }
            return id_spec;
        }

        //Клік по кнопці завантаження файлу
        private void LoadMaterialButton_Click(object sender, EventArgs e)
        {
            string msword = "Документ MS Word (*.DOC;*.DOCX;*.DOCM;*.DOTM;*.DOTX;*.DOT)|*.DOC;*.DOCX;*.DOCM;*.DOTM;*.DOTX;*.DOT";
            string msexcel = "Таблиця MS Excel (*.XLS;*.XLSX;*.XLSM;*.XLTX;*.XLT;*.XLTM;*.XLSB,*.XLAM;*.XLA)|*.XLS;*.XLSX;*.XLSM;*.XLTX;*.XLT;*.XLTM;*.XLSB,*.XLAM;*.XLA";
            string mspowerpoint = "Презентація MS PowerPoint (*.PPTX;*.PPT;*.PPTM;*.PPSX;*.PPS;*.PPSM;*.POTX;*.POT;*.POTM;*.PPAM;*.PPA)|*.PPTX;*.PPT;*.PPTM;*.PPSX;*.PPS;*.PPSM;*.POTX;*.POT;*.POTM;*.PPAM;*.PPA";
            string txt = "Текстовий файл (*.TXT)|*.TXT";
            string archive = "Архів (*.ZIP;*.7Z;*.RAR;*.JAR;*.GZ)|*.ZIP;*.7Z;*.RAR;*.JAR;*.GZ)";
            string image = "Зображення (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF";
            string video = "Відео (*.MPG;*.MOV;*.WMV;*.AVI;*.MP4)|*.MPG;*.MOV;*.WMV;*.AVI;*.MP4";
            string audio = "Аудіо (*.WAV;*.AIF;*.MP3;*.MID)|*.WAV;*.AIF;*.MP3;*.MID";
            string pdf = "PDF файл (*.PDF)|*.PDF";
            string djvu = "DJVU файл (*.DJVU)|*.DJVU";
            string filter = null;
            switch (Type_of_document)
            {
                case "Аудіо":
                    {
                        filter = audio;
                        break;
                    }
                case "Відео":
                    {
                        filter = video;
                        break;
                    }
                case "Презентація":
                    {
                        filter = mspowerpoint;
                        break;
                    }
                case "Електронна книга":
                    {
                        filter = pdf + "|" + djvu;
                        break;
                    }
                case "Текстовий документ":
                    {
                        filter = msword + "|"+ txt;
                        break;
                    }
                case "Зображення":
                    {
                        filter = image;
                        break;
                    }
                case "Електронна таблиця":
                    {
                        filter = msexcel;
                        break;
                    }
                case "Архів":
                    {
                        filter = archive;
                        break;
                    }
            }

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Оберіть файл",
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = filter,
                FilterIndex = 1,
                RestoreDirectory = true,
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo file = new FileInfo(openFileDialog1.FileName);
                Materials.Filepath = file.FullName;
                Materials.Documentname = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                FilePathLabel.Text = Materials.Filepath;
                FilePathLabel.ForeColor = Color.Black;
            }
        }
        //Клік по кнопці перегляду вмісту обраного файла
        private void FileViewButton_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(Materials.Filepath)))
            {
                Process.Start(Materials.Filepath);
            }
            else
            {
                FilePathLabel.Text = "Оберіть файл для перегляду";
                FilePathLabel.ForeColor = Color.Red;
            }
        }
        //Зупинка курсору миші над компонентом-міткою (матеріал)
        private void FilePathLabel_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(FilePathLabel, FilePathLabel.Text);
        }
        //Обробник події зміни тексту поля вибору типу матеріала
        private void MaterialTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            string materialtype = MaterialTypeComboBox.Text.Trim();
            if (!String.IsNullOrEmpty(materialtype))
            {
                MaterialTypeTextChanged = true;
                DocumentTypeComboBox.Enabled = true;
            }
            else
            {
                MaterialTypeTextChanged = false;
                DocumentTypeComboBox.Enabled = false;
            }
        }
        #endregion

        #region Вкладка "Навчальне навантаження"
        //Клік на кнопці відміни введення даних вкладки "Навчальне навантаження"
        private void StudyLoadCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult affirmative = MyMessages.AbortDataEntrance();
            if (affirmative == DialogResult.Yes)
            {
                StudyloadTabClear();
            }
        }
        //Клік на кнопці з додавання даних про навчальне навантаження до бази даних
        private void StudyloadDBLoadButton_Click(object sender, EventArgs e)
        {
            int userid = -1;
            int disciplineid = -1;
            string error = null;
            try
            {
                using (MySqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open();
                    for (int i = 0; i < Studyload.Teacher.Count; i++)
                    {


                        MySqlCommand cmd1 = new MySqlCommand("SelectUserIDLikeEntered", con)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd1.Parameters.Clear();
                        cmd1.Parameters.AddWithValue("_name", Studyload.Teacher[i]);
                        using (MySqlDataReader r = cmd1.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                userid = (int)r["id"];
                            }
                        }

                        MySqlCommand cmd2 = new MySqlCommand("SelectDisciplineID", con)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.AddWithValue("_name", Studyload.Discipline[i]);
                        using (MySqlDataReader r = cmd2.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                disciplineid = (int)r["id"];
                            }
                        }

                        MySqlCommand cmd = new MySqlCommand("AddStudyload", con)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("_teacherID", userid);
                        cmd.Parameters.AddWithValue("_disciplineID", disciplineid);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows >= 0)
                        {
                            MessageBox.Show("Запис було успішно додано");
                        }
                        else
                        {
                            using (MySqlDataReader reader1 = cmd.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    error = reader1["@full_error"].ToString();
                                }
                            }
                            MyMessages.ErrorMessage(error);
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                MyMessages.ErrorMessage(e1.Message);
            }

            StudyloadToDBlistBox.Items.Clear();
            StudyLoadTabPageView();
        }
        //Клік по кнопці підтвердження додавання даних про навчальне навантаження
        private void StudyloadConfirmButton_Click(object sender, EventArgs e)
        {
            string user = StudyloadUserNameTextBox.Text.Trim();
            string discipline = StudyloadDisciplineTextBox.Text.Trim();
            int studyload = -1;
            //якщо введені дані не є коректними - видати помилку
            if (!(String.IsNullOrEmpty(StudyloadUserNameErrorProvider.GetError(StudyloadUserNameTextBox))) ||
                !(String.IsNullOrEmpty(StudyloadDisciplinesErrorProvider.GetError(StudyloadDisciplineTextBox))))
            {
                MyMessages.WarningMessage("Введені дані не є коректними. Будь ласка перевірте введення даних");
            }
            else
            {
                try
                {
                    using (MySqlConnection con = DBUtils.GetDBConnection())
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("CheckStudyload", con)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("_fio",user);
                        cmd.Parameters.AddWithValue("_role",2);
                        cmd.Parameters.AddWithValue("_discipline",discipline);
                        using (MySqlDataReader r = cmd.ExecuteReader())
                        {
                            while(r.Read())
                            {
                                studyload = (int)r["Studyload_id"];
                            }
                        }
                    }
                }
                catch(Exception e1)
                {
                    MyMessages.ErrorMessage(e1.Message);
                }
                if (studyload != -1)
                {
                    MyMessages.WarningMessage(user + " вже веде навчальну дисципліну " + discipline);
                    StudyloadUserNameTextBox.Text = String.Empty;
                    StudyloadDisciplineTextBox.Text = String.Empty;
                }
                else
                {
                    //додати запис до списку
                    StudyloadToDBlistBox.Items.Add("Користувач " + user + " веде дисципліну " + discipline);
                    Studyload.Teacher.Add(user);
                    Studyload.Discipline.Add(discipline);
                    //очистити поля введення 
                    StudyloadUserNameTextBox.Text = String.Empty;
                    StudyloadDisciplineTextBox.Text = String.Empty;
                    //змінити розмір вікна
                    StudyloadTabResize();
                    //встановити курсор до поля введення прізвища викладача
                    StudyloadUserNameTextBox.Focus();
                }
            }
        }
        #endregion

        //Клік по кнопці закриття форми додавання даних
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void MaterialTypeComboBox_Leave(object sender, EventArgs e)
        {

        }

        private void DisciplineTextBox_TextChanged(object sender, EventArgs e)
        {
            string discipline = DisciplineTextBox.Text.Trim();
            ListFill(discipline);
        }

        private void LoginTextBox_Leave(object sender, EventArgs e)
        {
            string text = LoginTextBox.Text.Trim();

			if (!String.IsNullOrEmpty(text))
			{
				text = char.ToUpper(text[0]) + text.Substring(1);
			}

			LoginTextBox.Text = text;
            LanguageSets.Ukrainian();
        }

        private void PasswordTextBox_Enter(object sender, EventArgs e)
        {
            LanguageSets.English();
        }

        private void PasswordTextBox_Leave(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
        }

        private void SirnameTextBox_Enter(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
        }

        private void StudyloadUserNameTextBox_Enter(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
        }

        private void StudyloadUserNameTextBox_Leave(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
        }

        private void StudyloadDisciplineTextBox_Enter(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
        }

        private void StudyloadDisciplineTextBox_Leave(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
        }

        private void DisciplineTextBox_Enter(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
        }

        private void UserAddStripButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void DisciplineAddStripButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void MaterialsAddStripButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void StudyloadAddStripButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void GoBackStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}