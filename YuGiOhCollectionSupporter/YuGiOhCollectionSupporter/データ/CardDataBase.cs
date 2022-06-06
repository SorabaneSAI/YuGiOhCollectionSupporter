using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
	public class CardDataBase
	{
		public List<CardData> CardList = new List<CardData>();
		public string SaveDataPath = "CardDataBase.json";

		public int getAllCardNum()
		{
			return CardList.Count;
		}

        public int getAllCardNumHave()
        {
            int num = 0;
            foreach (CardData card in CardList)
            {
                if (card.IsCardNameHave())
                    num++;
            }
            return num;
        }

        //略号別で持ってるカード数を返す
        public int getCardNumCodeHave()
        {
            int num = 0;
            foreach (var card in CardList)
            {
                num += card.getCardNumCodeHave();
            }
            return num;
        }

        //略号別で存在するカード数を返す
        public int getCardNumCode()
        {
            int num = 0;
            foreach (var card in CardList)
            {
                num += card.getCardNumCode();
            }
            return num;
        }

        //レアリティ別で持ってるカード数を返す
        public int getCardNumRarityHave()
        {
            int num = 0;
            foreach (var card in CardList)
            {
                num += card.getCardNumRarityHave();
            }
            return num;
        }

        //レアリティ別で存在するカード数を返す
        public int getCardNumRarity()
        {
            int num = 0;
            foreach (var card in CardList)
            {
                num += card.getCardNumRarity();
            }
            return num;
        }


        public (int newnum,int updatenum) AddCardDataList(List<CardData> carddatalist)
        {
            int newnum = 0;
            int updatenum = 0;
            foreach (var newdata in carddatalist)
            {
                foreach (var olddata in CardList)
                {
                    if (newdata.URL == olddata.URL)
                    {
                        updatenum += olddata.AddNewData(newdata);
                        goto next;
                    }
                }
                //全部違ったら存在しないデータ
                CardList.Add(newdata);
                newnum++;
            next:;
            }
            return (newnum,updatenum);
        }

        public void SortAIUEO()
        {
			Program.WriteLog("あいうえお順にソート開始", LogLevel.必須項目);
			CardList.Sort((a,b) => new AIUEOComparer().Compare(a.読み,b.読み));
			Program.WriteLog("あいうえお順にソート終了", LogLevel.必須項目);
		}

	}
}
