using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
	public class CardDataBase
	{
		public List<CardData> CardDB = new List<CardData>();
		public string SaveDataPath = "CardDataBase.json";

		public int getAllCardCount()
		{
			return CardDB.Count;
		}

		public int AddCardDataBase(CardDataBase oldDB)
		{
			//カードがなければ追加
			//あるなら略号、レアリティ違いを追加

			return 0;
		}
		

	}
}
