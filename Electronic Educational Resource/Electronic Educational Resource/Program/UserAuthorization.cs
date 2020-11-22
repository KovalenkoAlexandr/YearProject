using Shared;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WorkingWithData
{
    public partial class UserAuthorization : Form
    {
        Users _user;
        public UserAuthorization(Users user)
        {
            _user = user;
            InitializeComponent();
        }
        
        private void Aut_LoginTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string c = e.KeyChar.ToString();
            //перевірка введеного символу на допустимість
            if (!(Regex.Match(c, @"[A-Za-z1234567890]").Success | e.KeyChar == 8))
            {
                //виведення помилки до елементу ErrorProvider
                Aut_LoginErrorProvider.SetError(Aut_LoginTextBox, "Дозволяються лише символи латинського алфавіту та цифри");
                e.Handled = true;
            }
            else //якщо символ допустимий
            {
                //прибрати повідомлення про помилку
                Aut_LoginErrorProvider.SetError(Aut_LoginTextBox, String.Empty); ;
            }
        }

        private void Aut_PasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string c = e.KeyChar.ToString();
            if (!(Regex.Match(c, @"[A-Za-z1234567890]").Success | e.KeyChar == 8))
            {
                Aut_PasswordErrorProvider.SetError(Aut_PasswordTextBox, "Дозволяються лише символи латинського алфавіту та цифри");
                e.Handled = true;
            }
            else
            {
                Aut_PasswordErrorProvider.SetError(Aut_PasswordTextBox, String.Empty); ;
            }
        }

        private void Aut_ConfirmButon_Click(object sender, EventArgs e)
        {
            string login_in = Aut_LoginTextBox.Text.Trim();
            string password_in = Aut_PasswordTextBox.Text.Trim();
            bool uservalid = _user.IsValidUser(login_in,password_in);
            if (uservalid)
            {
                MessageBox.Show("Ви авторизувались");
                Close();
            }
            else
            {
                MyMessages.WarningMessage("Неможливо авторизуватись! Будь ласка перевірте логін та пароль");
            }
        }

        private void Aut_CancelButton_Click(object sender, EventArgs e)
        {
            Aut_LoginTextBox.Text = String.Empty;
            Aut_PasswordTextBox.Text = String.Empty;
            if (!(String.IsNullOrEmpty(Aut_LoginErrorProvider.GetError(Aut_LoginTextBox))))
            {
                Aut_LoginErrorProvider.SetError(Aut_LoginTextBox, String.Empty);
            }
            if (!(String.IsNullOrEmpty(Aut_PasswordErrorProvider.GetError(Aut_PasswordTextBox))))
            {
                Aut_PasswordErrorProvider.SetError(Aut_PasswordTextBox, String.Empty);
            }
            Close();
        }

        private void Aut_LoginTextBox_Validating(object sender, CancelEventArgs e)
        {
            string login_in = Aut_LoginTextBox.Text.Trim();
            if (String.IsNullOrEmpty(login_in))
            {
                Aut_LoginErrorProvider.SetError(Aut_LoginTextBox, "Ви не ввели логін");
            }
        }

        private void Aut_PasswordTextBox_Validating(object sender, CancelEventArgs e)
        {
            string password_in = Aut_PasswordTextBox.Text.Trim();
            if (String.IsNullOrEmpty(password_in))
            {
                Aut_PasswordErrorProvider.SetError(Aut_PasswordTextBox, "Ви не ввели пароль");
            }
            else
            {
               Aut_PasswordErrorProvider.SetError(Aut_PasswordTextBox, String.Empty);
            }
        }

        private void UserAuthorization_Load(object sender, EventArgs e)
        {
            Aut_LoginTextBox.Focus();
        }

        private void Aut_LoginTextBox_Enter(object sender, EventArgs e)
        {
            LanguageSets.English();
        }

        private void Aut_LoginTextBox_Leave(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
        }

        private void Aut_PasswordTextBox_Enter(object sender, EventArgs e)
        {
            LanguageSets.English();
        }

        private void Aut_PasswordTextBox_Leave(object sender, EventArgs e)
        {
            LanguageSets.Ukrainian();
        }
    }
}
