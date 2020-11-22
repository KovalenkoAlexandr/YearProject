using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormSeek
{
	public class ClassSeekByKeyworks
	{
		public int firstLevel;
		int[] masNodeNumber;
		int[] masNodeNumberSecond;
		public ClassSeekByKeyworks() { masNodeNumber = new int[3]; masNodeNumberSecond = new int[2]; firstLevel = -1; }

		public void fillMasNodeNumber(int i, int j, int m)
		{
			masNodeNumber[0] = i;
			masNodeNumber[1] = j;
			masNodeNumber[2] = m;
		}

		public void fillMasNodeNumberSecond(int i, int j)
		{
			masNodeNumberSecond[0] = i;
			masNodeNumberSecond[1] = j;
		}

		public int checkAllNode(int i, int j, int m)
		{
			if (masNodeNumber[0] == i && masNodeNumber[1] == j && masNodeNumber[2] == m) return 3;
			if (masNodeNumber[0] == i && masNodeNumber[1] == j && masNodeNumber[2] != m) return 2;
			else return 0;
		}

		public int checkAllNodeSecond(int i, int j)
		{
			if (masNodeNumberSecond[0] == i && masNodeNumberSecond[1] == j) return 2;
			if (masNodeNumberSecond[0] == i && masNodeNumberSecond[1] != j) return 1;
			else return 0;
		}
	}
}
