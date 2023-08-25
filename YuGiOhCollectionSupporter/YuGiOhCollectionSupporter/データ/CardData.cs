using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
		public DateTime 誕生日;

		public string URL;
		public string 名前;
		public string 読み;	//ひらがなはカタカナに直される
		public string 英語名;

		public Dictionary<string, string> ValuePairs;   //存在するパラメータのペアだけ登録される

		public string 種族;
		public string ペンデュラム効果;
		public string テキスト;


		public List<CardVariation> ListVariations = new List<CardVariation>();

//		[JsonIgnore]
//		public bool 表示フラグ = true;




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
		public CardData(CardData card,List<CardVariation> variationlist) :this(card.ID, card.URL,card.名前 ,card.読み, card.英語名, card.ValuePairs, card.ペンデュラム効果, card.テキスト, card.種族,null)
        {
			ListVariations = variationlist;
		}

        internal int AddNewData(CardData newdata)
        {
			int updatenum = 1;	//かけ算するのであったときが０ないときが１　最後に反転する


            //パックを比較
            foreach (var newval in newdata.ListVariations)
            {
                for(int i=0; i<ListVariations.Count; i++)
                {
					var oldval = ListVariations[i];
					if (oldval.発売パック.URL.Equals(newval.発売パック.URL))
					{
						//						updatenum *= oldval.AddVariationData(名前,newval);    //ここで所持フラグをコピー
//						newval.所持フラグ = oldval.所持フラグ;
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


		//最も若い誕生日を返す
		public DateTime getEarlyDate()
        {
			DateTime date = DateTime.Now;

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

	public class CardVariation : IComparer<CardVariation>
	{

		public PackData 発売パック;
		public Code 略号;

		//			public List<Rarity> ListRarity = new List<Rarity>();
		public Rarity rarity;
		//           [JsonIgnore]
		//			public bool 所持フラグ = false;

		public CardVariation() { }

		public CardVariation(PackData packdata, string code, Rarity rarity)
		{
			発売パック = packdata;
			略号 = new Code(code);
			this.rarity = rarity;
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

	public class Rarity
	{
		public string Initial;
		public string Name;

		public Rarity(string initial, string name)
		{
			Initial = initial;
			Name = name;
		}
	}


	//カードデータとユーザーデータを分ける苦肉の策
	public class UserCardData
    {
		public int ID;
		public bool 表示フラグ = true;
		public List<UserVariationData> UserVariationDataList = new List<UserVariationData>();
		public bool Is同名予備カード枚数十分 = false;	//自分しか使わない予備のカードが３枚以上あるかフラグ
		public class UserVariationData
        {
			public bool 所持フラグ = false;
			public string 発売パックURL;
			public Rarity rarity;
		}

		public UserCardData() { }
		public UserCardData(CardData card)
        {
			ID = card.ID;

			foreach (var variation in card.ListVariations)
            {
				UserVariationData vardata = new UserVariationData();
				vardata.発売パックURL = variation.発売パック.URL;
				vardata.rarity = variation.rarity;
				UserVariationDataList.Add(vardata);
            }
        }

	}

	//カードデータとユーザーデータの統合
	public class TwinCardData
    {
		public CardData carddata;
		public UserCardData usercarddata;

		public TwinCardData(CardData carddata, UserCardData usercarddata)
        {
            this.carddata = carddata;
            this.usercarddata = usercarddata;
        }

		public bool get表示フラグ() { return usercarddata.表示フラグ; }
		public void set表示フラグ(bool flag) { usercarddata.表示フラグ = flag; }

		public bool getIs同名予備カード枚数十分() { return usercarddata.Is同名予備カード枚数十分; }
		public void setIs同名予備カード枚数十分(bool flag) { usercarddata.Is同名予備カード枚数十分 = flag; }

		public bool get所持フラグ(CardVariation variation)
        {
            foreach (var uservariation in usercarddata.UserVariationDataList)
            {
				if(uservariation.発売パックURL == variation.発売パック.URL && uservariation.rarity.Name == variation.rarity.Name)
                {
					return uservariation.所持フラグ;
                }
            }
			Program.WriteLog("不明なvariation(TwinCardData.get所持フラグ) " + variation.発売パック.Name + " " + variation.rarity.Name
				+ " " + variation.略号.get略号Full(), LogLevel.エラー);
			return false;
        }

		public void set所持フラグ(CardVariation variation,bool flag)
        {
			foreach (var uservariation in usercarddata.UserVariationDataList)
			{
				if (uservariation.発売パックURL == variation.発売パック.URL && uservariation.rarity.Name == variation.rarity.Name)
				{
					uservariation.所持フラグ = flag;
					return ;
				}
			}
			Program.WriteLog("不明なvariation(TwinCardData.set所持フラグ) " + variation.発売パック.Name + " " + variation.rarity.Name
				+ " " + variation.略号.get略号Full(), LogLevel.エラー);
		}

		//そのカード名のカードを持っているかを返す
		public bool IsCardNameHave()
		{
			foreach (var variation in carddata.ListVariations)
			{
				if (get所持フラグ(variation))
					return true;
			}
			return false;
		}

		private (int, int) getNumBase(int pattern, Code code)
		{
			List<List<CardVariation>> varlist2 = new List<List<CardVariation>>();    //略号が同じものは同じリストにいれたもののリスト
			foreach (var base_variation in carddata.ListVariations)
			{
				foreach (var varlist in varlist2)
				{
					switch (pattern)
					{
						case 1:
							if (base_variation.略号.get略号Full() == varlist[0].略号.get略号Full())
							{
								varlist.Add(base_variation);
								goto next;
							}
							break;
					}
				}
				var newvarlist = new List<CardVariation>();
				newvarlist.Add(base_variation);
				varlist2.Add(newvarlist);
			next:;
			}

			int num = 0;
			foreach (var varlist in varlist2)
			{
				//その完全略号カード中で１つでも持ってれば加算
				bool 所持フラグ = false;
                foreach (var variation in varlist)
                {
					if (get所持フラグ(variation))
						所持フラグ = true;
				}

				if (所持フラグ)
					num++;
			}

			return (num, varlist2.Count);
		}


		//略号別で持ってるカード数と存在するカード数を返す	
		public (int, int) getCardHaveNumCode()
		{
			return getNumBase(1, null);	//あんまり関数化した意味なかった
		}

		//レアリティ別で持ってる数と存在するカードを返す	
		public (int, int) getCardHaveNumRarity()
		{
			int num = 0;

			foreach (var variation in carddata.ListVariations)
			{
				if (get所持フラグ(variation))
					num++;
			}
			return (num, carddata.ListVariations.Count);
		}

		//その略号のカードを持っている数と存在する数を返す
		public (int, int) getCardHaveNumCodebyCode(Code code)
		{
			int allnum = 0;
			int havenum = 0;

			foreach (var variation in carddata.ListVariations)
            {
				if(variation.略号.get略号Full() == code.get略号Full())
                {
					allnum++;
					if (get所持フラグ(variation))
						havenum++;
                }
            }

			return (havenum, allnum);

		}

	}
}
