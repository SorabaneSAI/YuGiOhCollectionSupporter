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
        public CardData getCard(int ID)
        {
            foreach (var card in CardList)
            {
                if (card.ID == ID)
                    return card;
            }
            return null;
        }

        public int getAllCardNum()
        {
            return CardList.Count;
        }




        public (int newnum, int updatenum) AddCardDataList(List<CardData> carddatalist)
        {
            int newnum = 0;
            int updatenum = 0;
            foreach (var newdata in carddatalist)
            {
                for (int i = 0; i < CardList.Count; i++)
                {
                    var olddata = CardList[i];
                    if (newdata.URL == olddata.URL)
                    {
                        updatenum += olddata.AddNewData(newdata);
                        olddata = newdata;                              //データを新しいものに更新
                        goto next;
                    }
                }
                //全部違ったら存在しないデータ
                CardList.Add(newdata);
                newnum++;
            next:;
            }
            return (newnum, updatenum);
        }

        public void SortAIUEO()
        {
            Program.WriteLog("あいうえお順にソート開始", LogLevel.必須項目);
            CardList.Sort((a, b) => new AIUEOComparer().Compare(a.読み, b.読み));
            Program.WriteLog("あいうえお順にソート終了", LogLevel.必須項目);
        }

        //カードリストからそのパックに入っているカードを返す
        public List<CardData> getPackCardList(PackData pack)
        {
            List<CardData> list = new List<CardData>();
            foreach (var card in CardList)
            {
                foreach (var variation in card.ListVariations)
                {
                    if (variation.発売パック.URL == pack.URL)
                    {
                        list.Add(card);
                        break;
                    }
                }
            }

            return list;
        }

    }

    public class UserCardDataBase
    {
        public List<UserCardData> UserCardDataList = new List<UserCardData>();
        public string SaveUserDataPath = "UserCardData.json";

        public void AddCardDataList(List<UserCardData> userdatalist)
        {
            foreach (var newdata in userdatalist)
            {
                for (int i = 0; i < UserCardDataList.Count; i++)
                {
                    var olddata = UserCardDataList[i];
                    if (newdata.ID == olddata.ID)
                    {
                        olddata = newdata;                              //データを新しいものに更新
                        goto next;
                    }
                }
                //全部違ったら存在しないデータ
                UserCardDataList.Add(newdata);
            next:;
            }
        }

    }
}
