using System.Windows.Forms;

namespace WorkingWithData
{
   internal class LanguageSets
    {
        internal static void English()
        {
            InputLanguage.CurrentInputLanguage = 
                InputLanguage.FromCulture(new System.Globalization.CultureInfo("en-US"));
        }

        internal static void Ukrainian()
        {
            InputLanguage.CurrentInputLanguage =
                InputLanguage.FromCulture(new System.Globalization.CultureInfo("uk-UA"));
        }
    }
}
