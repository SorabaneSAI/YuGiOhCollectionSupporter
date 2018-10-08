using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
	public class CardDataBase
	{
		private List<CardData> CardDB= new List<CardData>();
		private string SaveDataPath = "CardDataBase.dat";

		public int AddCardDataBase(CardData card , CardDataBase oldDB)
		{
			//重複チェック
			foreach (var item in CardDB)
			{
				if (item.Equals(card))
					return -1;
			}
			//旧DBとの比較で所持情報を引き継ぐ
			foreach (var item in oldDB.CardDB)
			{
				if (item.Equals(card))
				{
					card.所持フラグ = item.所持フラグ;
				}
			}

			CardDB.Add(card);
			return 0;
		}

		public static void Save(CardDataBase cdb)
		{
			//＜XMLファイルに書き込む＞
			//XmlSerializerオブジェクトを作成
			//書き込むオブジェクトの型を指定する
			System.Xml.Serialization.XmlSerializer serializer1 =
				new System.Xml.Serialization.XmlSerializer(typeof(CardDataBase));
			//ファイルを開く（UTF-8 BOM無し）
			System.IO.StreamWriter sw = new System.IO.StreamWriter(
				cdb.SaveDataPath, false, new System.Text.UTF8Encoding(false));
			//シリアル化し、XMLファイルに保存する
			serializer1.Serialize(sw, cdb);
			//閉じる
			sw.Close();

		}

		public static CardDataBase Load()
		{
			System.IO.StreamReader sr = null;
			CardDataBase cdb = new CardDataBase();
			try
			{
				//＜XMLファイルから読み込む＞
				//XmlSerializerオブジェクトの作成
				System.Xml.Serialization.XmlSerializer serializer2 =
					new System.Xml.Serialization.XmlSerializer(typeof(CardDataBase));
				//ファイルを開く
				sr = new System.IO.StreamReader(cdb.SaveDataPath, new System.Text.UTF8Encoding(false));
				//XMLファイルから読み込み、逆シリアル化する
				cdb = (CardDataBase)serializer2.Deserialize(sr);
			}
			catch (System.IO.FileNotFoundException) //見つからなかったら作成　ほかは知らん
			{
				Save(new CardDataBase());
			}
			finally
			{
				if (sr != null)
					sr.Close();
			}

			return cdb;
		}
	}
}
