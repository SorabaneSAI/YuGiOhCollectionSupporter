﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter
{
	public partial class HomeUI : UserControl
	{
		CardDataBase cdb;
		public HomeUI()
		{
			InitializeComponent();
		}

		public void Init(CardDataBase db)
		{
			cdb = db;
			int カード名別持ってるカード = 0;
			int カード名全カード = 0;
			int 略号別持ってるカード = 0;
			int 略号別全カード = 0;
			int レアリティ別持ってるカード = 0;
			int レアリティ別全カード = 0;

			var myTable = new Dictionary<string, bool>();
			foreach (var pack in db.PackDB)
			{
				略号別全カード += pack.getAllCardNum_Name();
				略号別持ってるカード += pack.getHaveCardNum_Name();
				レアリティ別全カード += pack.getAllCardNum_Rare();
				レアリティ別持ってるカード += pack.getHaveCardNum_Rare();

				//カード名別だけ大変

				foreach(var card in pack.CardDB)
				{
					//同じ名前のカードは登録しない
					if (!myTable.ContainsKey(card.名前))
					{
						myTable.Add(card.名前, card.所持フラグ);
					}
					else
					{
						//持ってなかったら見てるカードの所持フラグを上書き
						if (myTable.Contains(new KeyValuePair<string, bool>(card.名前, false)))
						{
							myTable[card.名前] = card.所持フラグ;
						}
					}
				}

				カード名全カード = myTable.Count;
				foreach (var item in myTable)
				{
					if (item.Value == true)
						カード名別持ってるカード++;
				}

				label2.Text = カード名別持ってるカード.ToString();
				label4.Text = カード名全カード.ToString();
				label8.Text = 略号別持ってるカード.ToString();
				label7.Text = 略号別全カード.ToString();
				label13.Text = レアリティ別持ってるカード.ToString();
				label12.Text = レアリティ別全カード.ToString();

				if (カード名全カード > 0 && 略号別全カード > 0 && レアリティ別全カード > 0)
				{
					label5.Text = ((double)カード名別持ってるカード / カード名全カード).ToString("F") +"％";
					label10.Text = ((double)略号別持ってるカード / 略号別全カード).ToString("F") + "％";
					label15.Text = ((double)レアリティ別持ってるカード / レアリティ別全カード).ToString("F") + "％";
				}
				else
				{
					label5.Text = "0%";
					label10.Text = "0%";
					label15.Text = "0%";
				}
			}

		}
	}
}
