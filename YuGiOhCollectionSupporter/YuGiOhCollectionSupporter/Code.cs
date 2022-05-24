using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
	public class Code
	{
		public bool Have略号 = true;
		public string 略号文字 = "";
		public string 略号地域 = "";
		public int 略号番号桁数 = 3;
		public int No = 0;

		public string get略号Full()
		{
			if (Have略号 == false)
				return "なし";
			string format = "";
			for (int i = 0; i < 略号番号桁数; i++)
			{
				format += "0";
			}
			return 略号文字 + "-" + 略号地域 + string.Format("{0:" + format + "}", No);
		}

		public Code() { }
		public Code(string code)
		{
			//略号を分解
			if (code != "")
			{
				string[] strarray = code.TrimEnd().Split('-');
				略号文字 = strarray[0];
				string 番号数字 = "";

				foreach (char c in strarray[1])
				{
					if (!Char.IsLetter(c))
						番号数字 += c;
					else
						略号地域 += c;
				}
				if (番号数字 == null || 番号数字 == "")
				{
					番号数字 = "エラー";
					return;
				}
				No = int.Parse(番号数字);
				略号番号桁数 = 番号数字.Length;
			}
			else
			{
				Have略号 = false;

			}
		}
	}
}
