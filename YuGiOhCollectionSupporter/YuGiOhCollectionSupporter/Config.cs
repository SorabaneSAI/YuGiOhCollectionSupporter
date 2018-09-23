using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
	public class Config
	{
		public string URL = "http://yugioh-wiki.net/index.php";
		private string CardListURL = @"?%A5%AB%A1%BC%A5%C9%A5%EA%A5%B9%A5%C8";
		public string CardDataPass = "CardDataPass.dat";
		public string UserDataPass = "UserDataPass.dat";
		public string ConfigPass = "Config.dat";

		public string getCardListURL() { return URL + CardListURL; }

		public static void Save(Config config)
		{
			//＜XMLファイルに書き込む＞
			//XmlSerializerオブジェクトを作成
			//書き込むオブジェクトの型を指定する
			System.Xml.Serialization.XmlSerializer serializer1 =
				new System.Xml.Serialization.XmlSerializer(typeof(Config));
			//ファイルを開く（UTF-8 BOM無し）
			System.IO.StreamWriter sw = new System.IO.StreamWriter(
				config.ConfigPass, false, new System.Text.UTF8Encoding(false));
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
				sr = new System.IO.StreamReader(config.ConfigPass, new System.Text.UTF8Encoding(false));
				//XMLファイルから読み込み、逆シリアル化する
				config = (Config)serializer2.Deserialize(sr);
			}
			catch (System.IO.FileNotFoundException)	//見つからなかったら作成　ほかは知らん
			{
				using (System.IO.FileStream hStream = System.IO.File.Create(config.ConfigPass))
				{
					if (hStream != null)
					{
						hStream.Close();
					}
				}
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
