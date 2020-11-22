using System.Collections.Generic;

namespace WorkingWithData
{
    internal class Materials
    {
        private static string documenttype;
        private static string documentname;
        private static string filepath;
        private static string materialtype;
        private static long size;
        internal static List<string> tags = new List<string>();
        internal static List<string> Tags { get => tags; set => tags = value; }
        internal static string Documenttype { get => documenttype; set => documenttype = value; }
        internal static string Documentname { get => documentname; set => documentname = value; }
        internal static string Filepath { get => filepath; set => filepath = value; }
        internal static string Materialtype { get => materialtype; set => materialtype = value; }
        internal static long Size { get => size; set => size = value; }
    }
}
