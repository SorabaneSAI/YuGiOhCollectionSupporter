using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
	public class CardData
	{
		public string URL;
		public string 名前;
		public string 読み;
		public string 英語名;

		public Dictionary<string, string> ValuePairs;   //存在するパラメータのペアだけ登録される

		/*
		public string 属性;
		public string レベル;
		public string 攻撃力;
		public string 守備力;
		public string 種族;
		*/

		public string ペンデュラム効果;
		public string テキスト;


		public List<CardVariation> ListVariations = new List<CardVariation>();

		public CardData(string URL, string 名前, string 読み, string 英語名, Dictionary<string, string> valuepairs,string ペンデュラム効果, string テキスト, List<CardVariation> listvariations)
        {
			this.URL = URL;
			this.名前 = 名前;
			this.読み = 読み;
			this.英語名 = 英語名;
			this.ValuePairs = valuepairs;
			this.ペンデュラム効果 = ペンデュラム効果;
			this.テキスト = テキスト;
			this.ListVariations = listvariations;
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


		public class CardVariation
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

		}

	}
}
