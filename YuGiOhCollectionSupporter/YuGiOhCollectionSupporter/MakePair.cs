﻿using AngleSharp.Dom;
using System;
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
		static string PriceDictionaryPath = "PriceDictionary.json";

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

		public static void MakeDictionary(Form1 form, KanabellForm formK)
		{
			//CardDataとKanabellCardの連携を果たす

			var sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			formK.InvalidMenuItem();

			var errorlist = new List<string>();
			var pricedictionary = MakeDictionary2(errorlist, form, formK);  //なんかawaitできなそう　別スレッドにするか？

			Program.Save(PriceDictionaryPath, pricedictionary);


			formK.ValidMenuItem();

			sw.Stop();
			TimeSpan ts = sw.Elapsed;

			string msg = $"販売情報の取得が完了しました。\n全データ件数{pricedictionary.Count}" + $"エラー件数:{errorlist.Count}件\n" + Program.ToJson(errorlist, Newtonsoft.Json.Formatting.None) + $"\nかかった時間:{ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒 {ts.Milliseconds}ミリ秒";
			Program.WriteLog(msg, LogLevel.必須項目);
			MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);



		}

		public static Dictionary<CardDataKey, KanabellCard> MakeDictionary2(List<string> errorlist, Form1 form, KanabellForm formK)
		{
			Dictionary<CardDataKey, KanabellCard> PricePairDictionary = new Dictionary<CardDataKey, KanabellCard>();

			var CardListCopy = new List<CardData>();
			CardListCopy = Program.DeepCopy(form.CardDB.CardList);	//本データを削除するわけにはいかないのでコピー

			var KanabellList = form.PriceDB.PriceDataList;

			var Kanabell2List = new List<KanabellCard2>();	//このリストは削除してOK!

			//Kanabellのデータをアップグレード カード名ごとにまとめる
			foreach (var kanabell in KanabellList)
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

			//中身を削除するので逆順
			for (int i = CardListCopy.Count - 1; i >= 0; i--)
			{
				CardData card = CardListCopy[i];

				for (int j = Kanabell2List.Count - 1; j >= 0; j--)
				{
					KanabellCard2 kanabell = Kanabell2List[j];

					if (card.名前 == kanabell.Name)
					{
						Compare(card, kanabell, form, PricePairDictionary);

						//中身がなくなったら削除
						if (kanabell.VariationList.Count ==0)
							Kanabell2List.Remove(kanabell);
					}
				}
				if (card.ListVariations.Count == 0)
					CardListCopy.Remove(card);

			}


			//ノーレアはレアだが、この場合に一意的にならないものは要報告

			//レアリティが対応表にないものを報告

			return PricePairDictionary;
		}

		//同名カードによるリンクを作成 ただし簡単情報用
		public static void Compare(CardData card, KanabellCard2 kanabell, Form1 form, Dictionary<CardDataKey, KanabellCard> dictionary)
		{
			List<KanabellCard>[] PairList = new List<KanabellCard>[card.ListVariations.Count]; // そのCardDataVariationと一致したKanabellCardのリスト
			int count = 0;
			for (int i = card.ListVariations.Count - 1; i >= 0; i--)
			{
				var card_variation = card.ListVariations[i];
				PairList[i] = new List<KanabellCard>();

				for (int j = kanabell.VariationList.Count - 1; j >= 0; j--)
				{
					var kanabell_variation = kanabell.VariationList[j];

					//略号一致してるか見る
					if(card_variation.略号.get略号Full() == kanabell_variation.略号Full)
					{
						//レアリティ一致してるか見る
						if (card_variation.rarity.Initial == getRareity_fromKanabell(kanabell_variation, form.RarityPairDataList))
						{
							//でも油断できない　同じものが複数ある可能性あり
							PairList[i].Add(kanabell_variation);

						}
					}
				}
			}
			for (int i = 0; i < PairList.Length; i++)
            {
				List<KanabellCard> pair = PairList[i];

				if (pair.Count == 0) continue;
                if(pair.Count == 1)
				{
					dictionary.Add(new CardDataKey(card, card.ListVariations[i]), PairList[i][0]);  //めでたく辞書登録
					count++;

					//登録したものを除外
					card.ListVariations.Remove(card.ListVariations[i]);
					kanabell.VariationList.Remove(PairList[i][0]);

//					return true;    //もっかい最初から
				}
				else
				{
					string str2 = $"略号レアリティが同じもの２つ以上あり\n{card.名前},{card.ListVariations[i].略号.get略号Full()},{card.ListVariations[i].rarity.Initial}";
                    foreach (var kb in PairList[i])
                    {
						str2 += $"[{kb.Note},{kb.備考詳細},{kb.Rare},{kb.URL}]";
                    }

                    Program.WriteLog(str2, LogLevel.警告);
				}
			}


            string str = $"{kanabell.Name} {count}ペア生成({dictionary.Count})  不一致データ:CardData";
			//レアリティ不一致は本当に不一致だった？最後に確認してログ出力
			foreach (var card2 in card.ListVariations)
			{
				str += "{"+ card2.略号.get略号Full() + " " + card2.rarity.Initial + "}";
			}
			str += "  KanabellData";

			foreach (var kanabell2 in kanabell.VariationList)
			{
				str += "{" + kanabell2.略号Full + " " + kanabell2.Rare + "}";
			}


			Program.WriteLog(str, LogLevel.情報);
			form.UpdateLabel(str);

		}

		public static string getRareity_fromKanabell(KanabellCard kanabell, BindingList<RarityPairData> raritypairs)
		{
			foreach (var raritypair in raritypairs)
			{
				if(raritypair.Rarity_Kanabell == kanabell.Rare)
				{
					return raritypair.Rarity_Konami;
				}
			}

			Program.WriteLog($"登録されてないレアリティ{kanabell.Rare}\n {Program.ToJson(kanabell)}", LogLevel.警告);
			return "";
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