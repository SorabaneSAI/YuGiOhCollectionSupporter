using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter
{
	public class PackData
	{
		public string URL = "";
		public string Name = "";
		public string TypeName = "";    //基本ブースターパックなどのタイプ
		public string SeriesName = "";  //10期などのシリーズ
		public DateTime BirthDay;

		public int CardCount;   //取得したカード枚数

		public bool 表示フラグ = true;

		public PackData(string url, string name, string type, string series, DateTime birthDay, int cardCount)
		{
			URL = url;
			Name = name;
			TypeName = type;
			SeriesName = series;
			BirthDay = birthDay;
			CardCount = cardCount;
		}

	}
	public class NodeSorter : IComparer
	{
		//日付ソート
		public int Compare(object x, object y)
		{
			TreeNode tx = x as TreeNode;
			TreeNode ty = y as TreeNode;

			PackData px = tx.Tag as PackData;
			PackData py = ty.Tag as PackData;

			if (px != null && py != null)
				return -(px.BirthDay.CompareTo(py.BirthDay));

			if (px == null) return -1;
			if (py == null) return 1;
			return 0;
		}

	}
}