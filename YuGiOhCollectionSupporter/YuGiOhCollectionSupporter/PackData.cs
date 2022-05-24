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
		public string TypeName = "";    //基本ブースターパックなどのタイプ
		public string SeriesName = "";  //10期などのシリーズ
		public string BirthDay = "";

//		public List<CardData> CardDB = new List<CardData>();
        /*
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
        //						card.所持フラグ = item.所持フラグ;
                            }
                        }
                    }

                    CardDB.Add(card);
                    return 0;
                }
        */

        public PackData(string url, string name, string type, string series, string birthDay)
        {
            URL = url;
            Name = name;
            TypeName = type;
            SeriesName = series;
            BirthDay = birthDay;
        }

        /*
		public int getAllCardNum_Name()
		{
			List<string> namelist = new List<string>();

			for (int i = 0; i < CardDB.Count; i++)
			{
//				if (CardDB[i].所持フラグ == true)
				{
					//同じ名前のカードは登録しない
					if (!namelist.Contains(CardDB[i].名前))
					{
						namelist.Add(CardDB[i].名前);
					}
				}
			}
			return namelist.Count;
		}
		public int getAllCardNum_Rare() { return CardDB.Count; }
		//同名で持ってる数
		public int getHaveCardNum_Name()
		{
			List<string> namelist = new List<string>();

			for (int i = 0; i < CardDB.Count; i++)
			{
//				if (CardDB[i].所持フラグ == true)
				{
					//同じ名前のカードは登録しない
					if (!namelist.Contains(CardDB[i].名前))
					{
						namelist.Add(CardDB[i].名前);
					}
				}
			}
			return namelist.Count;
		}

		//レアリティも別
		public int getHaveCardNum_Rare()
		{
			int count = 0;
			for (int i = 0; i < CardDB.Count; i++)
			{
//				if (CardDB[i].所持フラグ == true)
				{
					count++;
				}
			}
			return count;
		}
		*/
    }
}
