using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static YuGiOhCollectionSupporter.MakePair;

namespace YuGiOhCollectionSupporter
{
	public partial class NotMatchForm : Form
	{
		public NotMatchForm(List<MatchData> matchdata)
		{
			InitializeComponent();


            foreach (var data in matchdata)
            {
				var card = data.Card;
				var kanabell = data.Kanabell;

				//２つの多い方基準
				for (int i = 0; i < Math.Max(card.ListVariations.Count,kanabell.VariationList.Count); i++)
				{
					string carddata = "";
					if(card.ListVariations.Count >= i+1)
					{
						carddata = card.ListVariations[i].略号.get略号Full() + " " + card.ListVariations[i].rarity.Initial;
					}
					string kanabelldata = "";
					if(kanabell.VariationList.Count >=i+1)
					{
						kanabelldata = kanabell.VariationList[i].略号Full + " " + kanabell.VariationList[i].Rare + " " + kanabell.VariationList[i].備考詳細;
					}
					dataGridView1.Rows.Add(card.名前 == null ? kanabell.Name : card.名前,carddata,kanabelldata);
				}
			}
        }
	}

}
