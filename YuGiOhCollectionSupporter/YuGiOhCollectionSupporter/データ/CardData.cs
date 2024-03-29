﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter
{
	public class CardData
	{
		public int ID;
		public DateTimeOffset 誕生日;

		public string URL;
		public string 名前;
		public string 読み;	//ひらがなはカタカナに直される
		public string 英語名;

		public Dictionary<string, string> ValuePairs;   //存在するパラメータのペアだけ登録される

		/*
		public string 属性;
		public string レベル;
		public string 攻撃力;
		public string 守備力;
		public string 種族;
		*/

		public string 種族;
		public string ペンデュラム効果;
		public string テキスト;


		public List<CardVariation> ListVariations = new List<CardVariation>();

		public bool 表示フラグ = true;

		//そのカード名のカードを持っているかを返す
		public bool IsCardNameHave()
        {
            foreach (var variation in ListVariations)
            {
				if (variation.IsCardPackHave())
					return true;
            }
			return false;
        }

		//略号別で持ってるカード数を返す
		public int getCardNumCodeHave()
        {
			int num = 0;
            foreach (var variation in ListVariations)
            {
				if (variation.IsCardPackHave())
					num++;
            }
			return num;
        }

		//略号別で存在するカード数を返す
		public int getCardNumCode()
        {
			return ListVariations.Count;
        }

		//レアリティ別で持ってるカードを返す
		public int getCardNumRarityHave()
        {
			int num = 0;
            foreach (var variation in ListVariations)
            {
				num += variation.getCardNumRarityHave();
            }
			return num;
        }

		//レアリティ別で存在するカードを返す
		public int getCardNumRarity()
		{
			int num = 0;
			foreach (var variation in ListVariations)
			{
				num += variation.getCardNumRarity();
			}
			return num;
		}

		public (Brushes upcolor,Brushes downcolor) getCardColor()
        {
			return (null,null);
        }

		public CardData() { }

		public CardData(int id,string URL, string 名前, string 読み, string 英語名, Dictionary<string, string> valuepairs,string ペンデュラム効果, string テキスト,string 種族,List<CardVariation> listvariations)
        {
			ID = id;
			this.URL = URL;
			this.名前 = 名前;
			this.読み = 読み;
			this.英語名 = 英語名;
			this.ValuePairs = valuepairs;
			this.ペンデュラム効果 = ペンデュラム効果;
			this.テキスト = テキスト;
			this.ListVariations = listvariations;
			this.種族 = 種族;
			誕生日 = getEarlyDate();
		}

		//単一variationのカード作成
		public CardData(CardData card,CardVariation variation) :this(card.ID, card.URL,card.名前 ,card.読み, card.英語名, card.ValuePairs, card.ペンデュラム効果, card.テキスト, card.種族,null)
        {
			ListVariations = new List<CardVariation>();
			ListVariations.Add(variation);
		}

        internal int AddNewData(CardData newdata)
        {
			int updatenum = 1;	//かけ算するのであったときが０ないときが１　最後に反転する

			/*
			warnnum *= ConflictMsg("誕生日", 誕生日, newdata.誕生日);
			warnnum *= ConflictMsg("ID", ID, newdata.ID);   //これはありえない
			warnnum *= ConflictMsg("名前", 名前, newdata.名前);
			warnnum *= ConflictMsg("読み", 読み, newdata.読み);
			warnnum *= ConflictMsg("英語名", 英語名, newdata.英語名);
			warnnum *= ConflictMsg("ステータス", ValuePairs, newdata.ValuePairs);
			チェックいらなくね？
			*/
			/*新しいものは無条件更新
			//テキストは普通に変わることあるし
			if(!ペンデュラム効果.Equals(newdata.ペンデュラム効果))
            {
				Program.WriteLog($"{名前}のペンデュラム効果を更新しました[{ペンデュラム効果}]↔[{newdata.ペンデュラム効果}]", LogLevel.必須項目);
				ペンデュラム効果 = newdata.ペンデュラム効果;
				updatenum = 0;
			}
			if (!テキスト.Equals(newdata.テキスト))
			{
				Program.WriteLog($"{名前}のテキストを更新しました[{テキスト}]↔[{newdata.テキスト}]", LogLevel.必須項目);
				テキスト = newdata.テキスト;
				updatenum = 0;
			}
			*/
            //パックを比較
            foreach (var newval in newdata.ListVariations)
            {
                for(int i=0; i<ListVariations.Count; i++)
                {
					var oldval = ListVariations[i];
					if (oldval.発売パック.URL.Equals(newval.発売パック.URL))
					{
						updatenum *= oldval.AddVariationData(名前,newval);    //ここで所持フラグをコピー
						oldval = newval;										//データを新しいものに
						goto next;
					}
                }
				ListVariations.Add(newval);
				Program.WriteLog($"{名前}のパックを追加しました[{newval.発売パック.Name}]", LogLevel.必須項目);
				updatenum = 0;
			next:;
            }

			return -(updatenum - 1);
		}

		private int ConflictMsg(string valname,object oldobj,object newobj)
        {
			if (!oldobj.Equals(newobj))
			{
				Program.WriteLog($"{名前}の{valname}が以前のデータと競合しています[{Program.ToJson(oldobj, Formatting.None)}]↔[{Program.ToJson(newobj, Formatting.None)}]", LogLevel.警告);
				return 0;
			}
			return 1;
		}

		public class Rarity
        {
			public string Initial;
			public string Name;
			public bool 所持フラグ = false;

			public Rarity(string initial, string name)
            {
                Initial = initial;
                Name = name;
            }
        }


		public class CardVariation : IComparer<CardVariation>
        {

			public PackData 発売パック;
			public Code 略号;

			public List<Rarity> ListRarity = new List<Rarity>();

			public CardVariation() { }

			public CardVariation(PackData packdata, string code, List<Rarity> rarity)
            {
				発売パック = packdata;
				略号 = new Code(code);
				ListRarity = rarity;
			}

			//略号別に持っているかを返す
			public bool IsCardPackHave()
            {
                foreach (var rarity in ListRarity)
                {
					if (rarity.所持フラグ)
						return true;
                }
				return false;
            }

			//持っているレアリティの数を返す
			public int getCardNumRarityHave()
            {
				int num = 0;
                foreach (var rare in ListRarity)
                {
					if (rare.所持フラグ)
						num++;
                }
				return num;
            }

			//存在するレアリティの数を返す
			public int getCardNumRarity()
            {
				return ListRarity.Count;
            }


			public int AddVariationData(string cardname,CardVariation newvariation)
            {
				int updatenum = 1;  //紛らわしいけどアプデしてたら０してなかったら１

				//なんかめんどくさくなったので略号、パックデータ、レアリティの違いのチェックはしない

				foreach (var newrarity in newvariation.ListRarity)
				{
					for(int i=0; i< ListRarity.Count; i++) 
					{
						var oldrarity = ListRarity[i];
						if (oldrarity.Name.Equals(newrarity.Name))
						{
							newrarity.所持フラグ = oldrarity.所持フラグ;	//所持フラグ引き継ぎ
							oldrarity = newrarity;			//データを新しいものに
							goto next;
						}
					}
					ListRarity.Add(newrarity);
					Program.WriteLog($"{cardname}のあるパック{newvariation.発売パック.Name}のレアリティを追加しました[{newrarity}]", LogLevel.必須項目);
					updatenum = 0;
				next:;
				}

				return updatenum;
            }

			public int Compare(CardVariation x, CardVariation y)
			{
				//その略号のパックの発売日順にする
				int num = x.発売パック.BirthDay.CompareTo(y.発売パック.BirthDay);
				if (num > 0) return 1;
				if (num < 0) return -1;


				//略号記号が同じ場合は数字で決める
				if (x.略号.No < y.略号.No) return -1;
				if (x.略号.No > y.略号.No) return 1;

				return 0;
			}

		}

		//最も若い誕生日を返す
		public DateTimeOffset getEarlyDate()
        {
			DateTimeOffset date = DateTimeOffset.Now;

			if (ListVariations == null) return date;
			foreach (var vari in ListVariations)
            {
				if(vari.発売パック.BirthDay.CompareTo(date)<0)
                {
					date = vari.発売パック.BirthDay;

				}
            }
			return date;
        }


	}
}
