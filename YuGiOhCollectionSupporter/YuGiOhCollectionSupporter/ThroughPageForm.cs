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
    public partial class ThroughPageForm : Form
    {
        public BindingList<ThroughPageData> ThroughPageDataList = new BindingList<ThroughPageData>();
        public BindingList<ThroughPageData> oldThroughPageDataList = new BindingList<ThroughPageData>();

        public Form1 form;

        public ThroughPageForm(BindingList<ThroughPageData> datalist, Form1 form1)
        {
            InitializeComponent();

            form = form1;
			ThroughPageDataList = datalist;


            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = ThroughPageDataList;

            //コンストラクタにブチ込むとコピーになるのはガセ　普通にコピー
            oldThroughPageDataList = Program.DeepCopy(ThroughPageDataList);
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
			form.ThroughPageDataList = Program.DeepCopy(oldThroughPageDataList);
			Close();
        }
        private void PackGroupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
	}

    [Serializable]
	public class ThroughPageData
	{
        public string Word { get; set; }   //なんかしらんけどgetsetがないとバインドされない
        public bool 有効フラグ { get; set; } = true;

        public ThroughPageData() { }
        public ThroughPageData(string word)
        {
			Word = word;
        }

        public static void InitialDataSet(BindingList<ThroughPageData> ThroughPageDataList)
		{
			ThroughPageDataList.Add(new ThroughPageData("トークン"));
			ThroughPageDataList.Add(new ThroughPageData("オリパ"));
			ThroughPageDataList.Add(new ThroughPageData("イベント"));

			ThroughPageDataList.AllowNew = true;

        }

    }
}
