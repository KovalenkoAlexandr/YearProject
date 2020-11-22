using System.Collections.Generic;

namespace WorkingWithData
{
    internal class Studyload
    {
        private static int studyloadid;
        private static string olduser;
        private static string newuser;
        private static List<string> discipline = new List<string>();
        private static List<string> teacher = new List<string>();

        internal static string Olduser { get => olduser; set => olduser = value; }
        internal static string Newuser { get => newuser; set => newuser = value; }
        internal static int Studyloadid { get => studyloadid; set => studyloadid = value; }
        internal static List<string> Discipline { get => discipline; set => discipline = value; }
        internal static List<string> Teacher { get => teacher; set => teacher = value; }
    }
}
