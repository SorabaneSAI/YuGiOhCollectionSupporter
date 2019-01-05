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
	public partial class PackUI : UserControl
	{
		public PackUI()
		{
			InitializeComponent();
		}

		public void Init(CardDataBase CardDB,PackData pack)
		{
			label2.Text = pack.Name;
			label1.Text = pack.TypeName;
			label3.Text = pack.getHaveCardNum_Name() + "/" + pack.getAllCardNum_Name() + "枚 ("+ pack.getHaveCardNum_Rare() + "/" + pack.getAllCardNum_Rare() + ")";
			Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
			Dock = DockStyle.Fill;
		}
	}
}
