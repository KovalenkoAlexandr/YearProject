using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormSeek
{
	public class ClassMaterials
	{
		public string name;
		public string discipline;
		public string []materials;
		public int count;
		public int countForTypeDoc;

		public ClassMaterials()
		{
			name = "";
			discipline = "";
			materials = new string[20];
			count = 0;
			countForTypeDoc = 0;
		}

		public bool checkPrint(string tmp)
		{
			for (int i = 0; i < count; i++)
			{
				if (materials[i] == tmp) return true;
			}
			return false;
		}

		public void printToMaterials(string tmp)
		{
			materials[count++] = tmp;
		}
	}
}
