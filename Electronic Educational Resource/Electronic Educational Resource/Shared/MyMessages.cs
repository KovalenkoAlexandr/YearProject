using System.Windows.Forms;

namespace Shared
{
    internal class MyMessages
    {
        #region Повідомлення класу "Увага!"
        //Повідомлення-запит на відміну введення даних
        internal static DialogResult AbortDataEntrance()
        {
            return MessageBox.Show(
                "Усі незбережені дані будуть втрачені. Продовжити?",
                "Увага!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2
                );
        }
        //Повідомлення-попередження про помилку
        internal static void WarningMessage(string message)
        {
            MessageBox.Show(
            message,
            "Увага!",
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation,
            MessageBoxDefaultButton.Button1);
        }
        //Повідомлення-запит на видалення даних зі списків на запис до бази даних
        internal static DialogResult DBAbortDataEntrance()
        {
            return MessageBox.Show(
                "Ви бажаєте видалити дані, які чекають на занесення до бази даних?",
                "Увага!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
                );
        }
        //Повідомлення-запит на закриття форми
        internal static DialogResult FormClosing()
        {
            return MessageBox.Show(
                "Виявлено спробу закриття форми. Ви дійсно бажаєте закрити форму (усі незбережені дані " +
                "буде втрачено)?",
                "Увага!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
                );
        }
        //Повідомлення-запит на вихід з облікового запису
        internal static DialogResult LogOut()
        {
            return MessageBox.Show(
                "Ви збираєтесь вийти з облікового запису." +
                "Ви не зможете здійснювати більшість операцій у програмній системі. Продовжити?",
                "Увага!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2
                );
        }
        //Повідомлення-запит на завершення програми
        internal static DialogResult ProgramExit()
        {
            return MessageBox.Show(
                "Ви збираєтесь закрити програму. Ви не зможете використовувати її функціонал. Продовжити?",
                "Увага!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
                );
        }
        //Повідомлення запит на підтверждення дії
        internal static DialogResult QuestionMessage(string text)
        {
            return MessageBox.Show(
                text,
                "Увага!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
                );
        }
        #endregion

        #region Повідомлення типу "Помилка!"
        //виведення повідомлення з помилкою 
        internal static DialogResult ErrorMessage(string message)
        {
            return MessageBox.Show(
                message,
                "Помилка!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1
                );
        }
        #endregion
    }
}
