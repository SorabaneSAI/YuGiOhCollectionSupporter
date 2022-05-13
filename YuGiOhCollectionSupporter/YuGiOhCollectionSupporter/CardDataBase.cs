using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
	public class CardDataBase
	{
		public List<PackData> PackDB = new List<PackData>();
		public string SaveDataPath = "CardDataBase.json";

		public int getAllCardCount()
		{
			int num = 0;
			foreach (var pack in PackDB)
			{
				num += pack.CardDB.Count;
			}
			return num;
		}
		/*
		public int AddCardDataBase(CardData card , CardDataBase oldDB)
		{
			//重複チェック
			foreach (var pack in PackDB)
			{
			}
			//旧DBとの比較で所持情報を引き継ぐ
			foreach (var pack in oldDB.PackDB)
			{
			}

			CardDB.Add(card);
			return 0;
		}
		*/
		public CardDataBase() {}
	}
}
