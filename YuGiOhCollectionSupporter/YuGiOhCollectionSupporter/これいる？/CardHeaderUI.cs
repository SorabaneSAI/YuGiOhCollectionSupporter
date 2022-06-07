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
	public partial class CardHeaderUI : UserControl
	{
		CardData Data;
		public CardHeaderUI(CardData carddata)
		{
			InitializeComponent();
			Data = carddata;

			if (carddata == null) return;

			linkLabel1.Text = carddata.名前;
			label1.Text = $"{carddata.getCardNumCodeHave()} / {carddata.getCardNumCode()}";
			label2.Text = $"{carddata.getCardNumRarityHave()} / {carddata.getCardNumRarity()}";

			Color red = Color.FromArgb(255, 128, 128);
			Color yellow = Color.FromArgb(255, 255, 128);
			Color green = Color.FromArgb(128, 255, 128);

			tableLayoutPanel1.GetControlFromPosition(0,0).BackColor = carddata.IsCardNameHave() ? green : red;

			Color c;
			if (carddata.getCardNumCodeHave() == 0) c = red;
			else if (carddata.getCardNumCodeHave() == carddata.getCardNumCode()) c = green;
			else c = yellow;

			tableLayoutPanel1.GetControlFromPosition(1, 0).BackColor =c;

			if (carddata.getCardNumRarityHave() == 0) c = red;
			else if (carddata.getCardNumRarityHave() == carddata.getCardNumRarity()) c = green;
			else c = yellow;

			tableLayoutPanel1.GetControlFromPosition(2, 0).BackColor =c;

			//左の色を決定

		}

	}
}
