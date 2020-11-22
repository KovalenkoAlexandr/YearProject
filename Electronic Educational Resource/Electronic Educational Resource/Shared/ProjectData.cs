using System.IO;
using System.Windows.Forms;

namespace Shared
{
    class ProjectData
    {
        internal static string GetProjectFolder()
        {
            //return AppDomain.CurrentDomain.BaseDirectory;//C:\Users\Алекс\Desktop\Електронний освітній ресурс\Диплом программа\bin\Debug
            string directory = Directory.GetCurrentDirectory();//C:\Users\Алекс\Desktop\Електронний освітній ресурс\Диплом программа\bin\Debug
            string parent = System.IO.Directory.GetParent(directory).FullName;
            //return parent;
            //MessageBox.Show(directory);
            return directory;
        }
    }
}
