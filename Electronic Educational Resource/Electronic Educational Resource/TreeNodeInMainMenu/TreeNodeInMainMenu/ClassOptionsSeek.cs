using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeNodeInMainMenu
{
	class ClassOptionsSeek
	{
		public int radioButton;

		public int radioButtonSecond;

		public bool[] masMessage;

		public bool[] masCheckBox;

		public bool sortFlag;

		public ClassOptionsSeek()
		{
			radioButton = 1;
			radioButtonSecond = 1;
			masCheckBox = new bool[5] { true, true, true, true, true };
			masMessage = new bool[2] { true, true };
			sortFlag = false;
		}

		public void inputCheckBox(bool first, bool second, bool third, bool fourth, bool fifth)
		{
			masCheckBox[0] = first;
			masCheckBox[1] = second;
			masCheckBox[2] = third;
			masCheckBox[3] = fourth;
			masCheckBox[4] = fifth;
		}

		public void inputCheckBox(bool first, int position)
		{
			masCheckBox[position] = first;
		}
	}
}
