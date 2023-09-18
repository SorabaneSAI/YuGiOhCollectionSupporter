using System;
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
	public partial class CollectDataUI : UserControl
	{
		public CollectDataUI()
        {
			InitializeComponent();
		}
		public CollectDataUI(int カード名別持ってるカード, int カード名全カード, int 略号別持ってるカード, int 略号別全カード, int レアリティ別持ってるカード, int レアリティ別全カード)
		{
			InitializeComponent();
//			BringToFront();
//			Dock = DockStyle.Fill;
			Init(カード名別持ってるカード, カード名全カード, 略号別持ってるカード, 略号別全カード, レアリティ別持ってるカード, レアリティ別全カード);
		}

		public void Init(int カード名別持ってるカード, int カード名全カード, int 略号別持ってるカード, int 略号別全カード, int レアリティ別持ってるカード, int レアリティ別全カード)
		{
			/*
			int カード名別持ってるカード = carddb.getAllCardNumHave();
			int カード名全カード = carddb.getAllCardNum();
			int 略号別持ってるカード = carddb.getCardHaveNumCode().Item1;
			int 略号別全カード = carddb.getCardHaveNumCode().Item2;
			int レアリティ別持ってるカード = carddb.getCardHaveNumRarity().Item1;
			int レアリティ別全カード = carddb.getCardHaveNumRarity().Item2;
			*/
			/*
            foreach (var card in cardlist)
            {
                foreach (var variation in card.ListVariations)
                {
                    foreach (var rarity in variation.ListRarity)
                    {
						レアリティ別全カード++;
						if (rarity.所持フラグ)
							レアリティ別持ってるカード++;
					}
					略号別全カード++;
					if (variation.IsCardHavePack())
						略号別持ってるカード++;
				}
				カード名全カード++;
				if (card.IsCardHaveName())
					カード名別持ってるカード++;

			}
			*/
			
			label2.Text = カード名別持ってるカード.ToString();
			label4.Text = カード名全カード.ToString();
			label8.Text = 略号別持ってるカード.ToString();
			label7.Text = 略号別全カード.ToString();
			label13.Text = レアリティ別持ってるカード.ToString();
			label12.Text = レアリティ別全カード.ToString();

			if (カード名全カード > 0 && 略号別全カード > 0 && レアリティ別全カード > 0)
			{
				label5.Text = ((double)カード名別持ってるカード / カード名全カード*100).ToString("F") + "％";
				label10.Text = ((double)略号別持ってるカード / 略号別全カード*100).ToString("F") + "％";
				label15.Text = ((double)レアリティ別持ってるカード / レアリティ別全カード * 100).ToString("F") + "％";
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
