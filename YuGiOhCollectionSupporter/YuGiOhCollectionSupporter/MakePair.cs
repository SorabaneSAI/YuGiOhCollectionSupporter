using AngleSharp.Dom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YuGiOhCollectionSupporter.データ;
using static YuGiOhCollectionSupporter.KanabellForm;

namespace YuGiOhCollectionSupporter
{
	internal class MakePair
	{
//		static string PriceDictionaryPath = "PriceDictionary.json";

		public class KanabellCard2
		{
			public List<KanabellCard> VariationList = new List<KanabellCard>();    //KanabellCardはValiationとして使用される
			public string Name; //ID

			public KanabellCard2(KanabellCard card)
			{
				VariationList.Add(card);
				Name = card.Name;
			}
		}

		public static async void MakeDictionary(Form1 form, KanabellForm formK)
		{
			//CardDataとKanabellCardの連携を果たす

			var sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			formK.InvalidMenuItem();

			var errorlist = new List<string>();

			(Dictionary < CardDataKey, List < KanabellCard >> pricedictionary, int cardcount,int kanabellcount) 
				= MakeDictionary2(errorlist, form, formK);  //なんかawaitできなそう　別スレッドにするか？

			//			Program.Save(PriceDictionaryPath, pricedictionary);
			await Program.SaveCardDataAsync();


			formK.ValidMenuItem();

			sw.Stop();
			TimeSpan ts = sw.Elapsed;

			string msg = $"ペア情報の取得が完了しました。\n全データ件数{pricedictionary.Count}" + 
				$"エラー件数:{errorlist.Count}件\n" + Program.ToJson(errorlist, Newtonsoft.Json.Formatting.None) + 
				$"カード側余り:{cardcount}件、カーナベル側余り{kanabellcount}件"+
				$"\nかかった時間:{ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒 {ts.Milliseconds}ミリ秒";
			Program.WriteLog(msg, LogLevel.必須項目);
			MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);



		}

		public static (Dictionary<CardDataKey, List<KanabellCard>>,int,int )MakeDictionary2(List<string> errorlist, Form1 form, KanabellForm formK)
		{
			Dictionary<CardDataKey, List<KanabellCard>> PricePairDictionary = new Dictionary<CardDataKey, List<KanabellCard>>();

			var CardListCopy = new List<CardData>();
			CardListCopy = Program.DeepCopy(form.CardDB.CardList);	//本データを削除するわけにはいかないのでコピー

			var Kanabell略号分裂List = new List<KanabellCard>();
			var Kanabell2List = new List<KanabellCard2>();  //このリストは削除してOK!

			{
				var KanabellList = form.PriceDB.PriceDataList;

				//まずKanabellの略号はスペースと改行で区切られていることがあるので、それらを別Kanabellにする
				foreach (var kanabell in KanabellList)
				{
					string[] 略号List = kanabell.略号Full.Split('\n');
					if (略号List.Length == 1)
					{
						Kanabell略号分裂List.Add(kanabell);
						continue;
					}

					for (int i = 0; i < 略号List.Length; i++)
					{
						var newkanabell = new KanabellCard(kanabell.Rare, kanabell.Name, kanabell.Rank, kanabell.Price, kanabell.URL);
						newkanabell.略号Full = 略号List[i].Trim();
						newkanabell.備考詳細 = kanabell.備考詳細;
						Kanabell略号分裂List.Add(newkanabell);
					}
				}
			}

			//Kanabellのデータをアップグレード カード名ごとにまとめる
			foreach (var kanabell in Kanabell略号分裂List)
			{

				//すでにあるリストと同じ名前ならValiation追加
				foreach (var card in Kanabell2List)
				{
					if (card.Name == kanabell.Name)
					{
						card.VariationList.Add(kanabell);
						goto goto_next;
					}
				}

				Kanabell2List.Add(new KanabellCard2(kanabell));
			goto_next:;
			}

			int 不一致CardCount = 0;
			int 不一致KanabellCount = 0;
			//中身を削除するので逆順
			for (int i = CardListCopy.Count - 1; i >= 0; i--)
			{
				CardData card = CardListCopy[i];

				for (int j = Kanabell2List.Count - 1; j >= 0; j--)
				{
					KanabellCard2 kanabell = Kanabell2List[j];

					if (card.名前 == kanabell.Name)
					{
						(int cardcount,int kanabellcount) = Compare(card, kanabell, form, PricePairDictionary);
						不一致CardCount += cardcount;
						不一致KanabellCount += kanabellcount;

						//中身がなくなったら削除
						if (kanabell.VariationList.Count ==0)
							Kanabell2List.Remove(kanabell);
					}
				}
				if (card.ListVariations.Count == 0)
					CardListCopy.Remove(card);

			}

            //最後にCardDB更新
            foreach (var card in form.CardDB.CardList)
            {
                foreach (var variation in card.ListVariations)
                {
					var key = new CardDataKey(card, variation);
					if (PricePairDictionary.ContainsKey(key))
					{
						var kanabell = PricePairDictionary[key];

						variation.KanabellList = kanabell;
					}
				}

			}


            return (PricePairDictionary, 不一致CardCount, 不一致KanabellCount);
		}

		//同名カードによるリンクを作成
		public static (int ,int) Compare(CardData card, KanabellCard2 kanabell, Form1 form, Dictionary<CardDataKey, List<KanabellCard>> dictionary)
		{
			List<KanabellCard>[] PairListList = new List<KanabellCard>[card.ListVariations.Count]; // そのCardDataVariationと一致したKanabellCardのリスト
			int count = 0;
			for (int i = card.ListVariations.Count - 1; i >= 0; i--)
			{
				var card_variation = card.ListVariations[i];
				PairListList[i] = new List<KanabellCard>();

				for (int j = kanabell.VariationList.Count - 1; j >= 0; j--)
				{
					var kanabell_variation = kanabell.VariationList[j];

					//略号一致してるか見る
					if(card_variation.略号.get略号Full() == kanabell_variation.略号Full)
					{
						//レアリティ一致してるか見る(両方変換)
						if (getRareity_fromCard(card_variation, form.RarityPairDataList) == 
							getRareity_fromKanabell(kanabell_variation, form.RarityPairDataList))
						{
							//でも油断できない　同じものが複数ある可能性あり
							PairListList[i].Add(kanabell_variation);

						}
					}
				}
			}
			for (int i = PairListList.Length-1; i >= 0; i--)
            {
				List<KanabellCard> pairlist = PairListList[i];

				if (pairlist.Count == 0) continue;
                if(pairlist.Count >= 1 && pairlist.Count <= 3)
				{
					dictionary.Add(new CardDataKey(card, card.ListVariations[i]), pairlist);  //めでたく辞書登録
					count++;

					//登録したものを除外
					card.ListVariations.Remove(card.ListVariations[i]);
                    foreach (var pair in pairlist)
                    {
						kanabell.VariationList.Remove(pair);
					}

					//					return true;    //もっかい最初から
				}
				else
				{
					string str2 = $"略号レアリティが同じもの4つ以上あり\n {card.名前}, {card.ListVariations[i].略号.get略号Full()}, {card.ListVariations[i].rarity.Initial}";
                    foreach (var kb in PairListList[i])
                    {
						str2 += $"[{kb.Note},{kb.備考詳細},{kb.Rare},{kb.URL}]";
                    }

                    Program.WriteLog(str2, LogLevel.警告);
				}
			}


            string str = $"{kanabell.Name} {count}ペア生成({dictionary.Count})  ";

			if (card.ListVariations.Count >0 || kanabell.VariationList.Count >0)
			{
				str += "不一致データ:CardData";
				//レアリティ不一致は本当に不一致だった？最後に確認してログ出力
				foreach (var card2 in card.ListVariations)
				{
					str += "{" + card2.略号.get略号Full() + " " + card2.rarity.Initial + "}";
				}
				str += "  KanabellData";

				foreach (var kanabell2 in kanabell.VariationList)
				{
					str += "{" + kanabell2.略号Full + " " + kanabell2.Rare + "}";
				}
			}

			Program.WriteLog(str, LogLevel.情報);
			form.UpdateLabel(str);

			return (card.ListVariations.Count, kanabell.VariationList.Count);
		}

		public static string getRareity_fromKanabell(KanabellCard kanabell, BindingList<RarityPairData> raritypairs)
		{
			foreach (var raritypair in raritypairs)
			{
				if (raritypair.Rarity_Kanabell == kanabell.Rare)
				{
					return raritypair.Rarity_Konami;
				}
			}

			Program.WriteLog($"登録されてないレアリティ{kanabell.Rare}\n {Program.ToJson(kanabell)}", LogLevel.警告);
			return kanabell.Rare;
		}
		public static string getRareity_fromCard(CardVariation variation, BindingList<RarityPairData> raritypairs)
		{
			foreach (var raritypair in raritypairs)
			{
				if (raritypair.Rarity_Kanabell == variation.rarity.Initial)
				{
					return raritypair.Rarity_Konami;
				}
			}

			return variation.rarity.Initial;	//こっちはなくても警告なし
		}

	}

	public class CardDataKey
	{
		public int ID = 0;
		public CardVariation Variation;

		public CardDataKey(CardData data, CardVariation variation)
		{
			ID = data.ID;
			Variation = variation;	//ディープコピーをしたほうがいい？ でも値が変わる予定はない・・・
		}

		//DictionaryでKeyとして使うために必要
		public override int GetHashCode()
		{
			return this.ID.GetHashCode() + this.Variation.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			CardDataKey comp = obj as CardDataKey;
			if (comp == null) return false;

			return this.ID == comp.ID && this.Variation.Equals(comp.Variation);
		}

	}
}
