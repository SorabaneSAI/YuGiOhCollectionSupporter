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

        public (int newnum,int updatenum) AddPackDataList(List<CardData> carddatalist)
        {
            int newnum = 0;
            int updatenum = 0;
            foreach (var newdata in carddatalist)
            {
                foreach (var olddata in CardDB)
                {
                    if (newdata.URL == olddata.URL)
                    {
                        updatenum += olddata.AddNewData(newdata);
                        goto next;
                    }
                }
                //全部違ったら存在しないデータ
                CardDB.Add(newdata);
                newnum++;
            next:;
            }
            return (newnum,updatenum);
        }

        public void SortAIUEO()
        {
			Program.WriteLog("あいうえお順にソート開始", LogLevel.必須項目);
			CardDB.Sort((a,b) => new AIUEOComparer().Compare(a.読み,b.読み));
			Program.WriteLog("あいうえお順にソート終了", LogLevel.必須項目);
		}

	}
}
