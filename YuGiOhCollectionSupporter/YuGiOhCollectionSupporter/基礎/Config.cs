using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
	public class Config
	{
		public string URL = "https://www.db.yugioh-card.com/yugiohdb/card_list.action";
		public string URL2 = "https://www.db.yugioh-card.com/yugiohdb/card_search.action?ope=2&cid=";

		public decimal CardID_MIN = 4000;
		public decimal CardID_MAX = 30000;

		public bool Is捜索打ち切り = true;
		public decimal 捜索打ち切り限界 = 200;


		public static string Domain = "https://www.db.yugioh-card.com";
		public static string ConfigPass = "Config.dat";



		public static void Save(Config config)
		{
			//＜XMLファイルに書き込む＞
			//XmlSerializerオブジェクトを作成
			//書き込むオブジェクトの型を指定する
			System.Xml.Serialization.XmlSerializer serializer1 =
				new System.Xml.Serialization.XmlSerializer(typeof(Config));
			//ファイルを開く（UTF-8 BOM無し）
			System.IO.StreamWriter sw = new System.IO.StreamWriter(
				Config.ConfigPass, false, new System.Text.UTF8Encoding(false));
			//シリアル化し、XMLファイルに保存する
			serializer1.Serialize(sw, config);
			//閉じる
			sw.Close();

		}

		public static Config Load()
		{
			System.IO.StreamReader sr=null;
			Config config = new Config();
			try
			{
				//＜XMLファイルから読み込む＞
				//XmlSerializerオブジェクトの作成
				System.Xml.Serialization.XmlSerializer serializer2 =
					new System.Xml.Serialization.XmlSerializer(typeof(Config));
				//ファイルを開く
				sr = new System.IO.StreamReader(Config.ConfigPass, new System.Text.UTF8Encoding(false));
				//XMLファイルから読み込み、逆シリアル化する
				config = (Config)serializer2.Deserialize(sr);
			}
			catch (System.IO.FileNotFoundException)	//見つからなかったら作成　ほかは知らん
			{
				using (System.IO.FileStream hStream = System.IO.File.Create(Config.ConfigPass))
				{
					if (hStream != null)
					{
						hStream.Close();
					}
				}
				Config c = new Config();
				Save(c);
			}
			finally
			{
				if(sr != null)
					sr.Close();
			}

			return config;
		}

	}
}
