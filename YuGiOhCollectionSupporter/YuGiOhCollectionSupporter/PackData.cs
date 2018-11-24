using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
	public class PackData
	{
		public string URL = "";
		public string Name = "";
		public string TypeName = "";
		public string SeriesName = "";
		public string Code = "";

		public List<CardData> CardDB = new List<CardData>();

		public int AddCardDataBase(CardData card, PackData oldDB)
		{
			//重複チェック
			foreach (var item in CardDB)
			{
				if (item.Equals(card))
					return -1;
			}
			//旧DBとの比較で所持情報を引き継ぐ
			if (oldDB != null)
			{
				foreach (var item in oldDB.CardDB)
				{
					if (item.Equals(card))
					{
						card.所持フラグ = item.所持フラグ;
					}
				}
			}

			CardDB.Add(card);
			return 0;
		}

		public PackData() { }
		public PackData(string url, string name, string type, string series)
		{
			URL = url;
			Name = name;
			TypeName = type;
			SeriesName = series;
		}
	}
}
