﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
	public class CardData
	{
		public string 名前;
//		public string 読み;
//		public string 英語名;

		public string 略号文字 = "";
		public string 略号地域 = "";
		public int 略号番号桁数=3;
		public int No = 0;

		string 発売パック = "";

		public string Rare;

		public bool 所持フラグ = false;

		public CardData() { }

		public CardData(string name, /*string yomi, string eng,*/ string mark, string place,int num, int digit, string rare,string pack)
		{
			名前 = name;
//			読み = yomi;
//			英語名 = eng;
			略号文字 = mark;
			略号地域 = place;
			略号番号桁数 = digit;
			発売パック = pack;
			No = num;
			Rare = rare;
		}

		public string get略号Full()
		{
			if (略号文字 == "")
				return "";
			string format = "";
			for (int i = 0; i < 略号番号桁数; i++)
			{
				format += "0";
			}
			return 略号文字 + "-" + 略号地域 + string.Format("{0:"+format+"}", No);
		}

		//objと自分自身が等価のときはtrueを返す
		public override bool Equals(object obj)
		{
			//objがnullか、型が違うときは、等価でない
			if (obj == null || this.GetType() != obj.GetType())
			{
				return false;
			}

			CardData c = (CardData)obj;
			if (this.名前 != c.名前) return false;
//			if (this.読み != c.読み) return false;
//			if (this.英語名 != c.英語名) return false;
			if (this.略号文字 != c.略号文字) return false;
			if (this.略号地域 != c.略号地域) return false;
			if (this.略号番号桁数 != c.略号番号桁数) return false;
			if (this.No != c.No) return false;
			if (this.発売パック != c.発売パック) return false;
			if (this.Rare != c.Rare) return false;
			return true;
		}

		//Equalsがtrueを返すときに同じ値を返す
		public override int GetHashCode()
		{
			return this.No;
		}
	}
}
