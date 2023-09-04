using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter
{
    public partial class RarityPairForm : Form
    {
        public BindingList<RarityPairData> RarityPairDataList = new BindingList<RarityPairData>();
        public BindingList<RarityPairData> oldRarityPairDataList = new BindingList<RarityPairData>();

        public Form1 form;

        public RarityPairForm(BindingList<RarityPairData> datalist, Form1 form1)
        {
            InitializeComponent();

            form = form1;
			RarityPairDataList = datalist;


            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = RarityPairDataList;

			//コンストラクタにブチ込むとコピーになるのはガセ　普通にコピー
			oldRarityPairDataList = Program.DeepCopy(RarityPairDataList);
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
			form.RarityPairDataList = Program.DeepCopy(oldRarityPairDataList);
			Close();
        }
        private void PackGroupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
	}

	public class RarityPairData
    {
        public string Rarity_Kanabell { get; set; }   //なんかしらんけどgetsetがないとバインドされない
        public string Rarity_Konami { get; set; }
        public bool 有効フラグ { get; set; } = true;

        public RarityPairData() { }
        public RarityPairData(string kanabell,string konami)
        {
			Rarity_Kanabell = kanabell;
			Rarity_Konami = konami;
        }

        public static void InitialDataSet(BindingList<RarityPairData> RarityPairDataList)
		{
			RarityPairDataList.Add(new RarityPairData("QCシク","QCSE"));
			RarityPairDataList.Add(new RarityPairData("20thシク", "20th SE"));
			RarityPairDataList.Add(new RarityPairData("プリシク", "PSE"));
			RarityPairDataList.Add(new RarityPairData("コレレア", "CR"));

			RarityPairDataList.Add(new RarityPairData("アル", "UL"));
			RarityPairDataList.Add(new RarityPairData("ウル", "UR"));
			RarityPairDataList.Add(new RarityPairData("シク", "SE"));
			RarityPairDataList.Add(new RarityPairData("ホロ", "HR"));

			RarityPairDataList.Add(new RarityPairData("パラ", "P"));
			RarityPairDataList.Add(new RarityPairData("スー", "SR"));
			RarityPairDataList.Add(new RarityPairData("レア", "R"));
			RarityPairDataList.Add(new RarityPairData("ノレ", "N"));
			RarityPairDataList.Add(new RarityPairData("ノー", "N"));



			RarityPairDataList.AllowNew = true;

        }

    }
}
