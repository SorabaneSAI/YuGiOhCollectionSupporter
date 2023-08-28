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
    public partial class SeriesGroupForm : Form
    {
        public BindingList<SeriesGroupData> SeriesGroupDataList = new BindingList<SeriesGroupData>();
        public BindingList<SeriesGroupData> oldSeriesGroupDataList;

        public SeriesGroupForm(BindingList<SeriesGroupData> datalist)
        {
            InitializeComponent();

			SeriesGroupDataList = datalist;


            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = SeriesGroupDataList;

			oldSeriesGroupDataList = new BindingList<SeriesGroupData>(SeriesGroupDataList);   //コンストラクタにブチ込むとコピーになる
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
			SeriesGroupDataList = oldSeriesGroupDataList;
            Close();
        }
        private void PackGroupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            button2_Click(sender, e);
        }
	}

	public class SeriesGroupData
    {
        public string シリーズ名 { get; set; }   //なんかしらんけどgetsetがないとバインドされない
        public string 開始日 { get; set; }
        public bool 有効フラグ { get; set; } = true;

        public SeriesGroupData() { }
        public SeriesGroupData(string シリーズ,string 日付)
        {
			シリーズ名 = シリーズ;
			開始日 = 日付;
        }

        public static void InitialDataSet(BindingList<SeriesGroupData> SeriesGroupDataList)
		{
			SeriesGroupDataList.Add(new SeriesGroupData("第０１期", "1999/02/04"));
			SeriesGroupDataList.Add(new SeriesGroupData("第０２期", "2000/04/20"));
			SeriesGroupDataList.Add(new SeriesGroupData("第０３期", "2002/05/16"));
			SeriesGroupDataList.Add(new SeriesGroupData("第０４期", "2004/05/27"));
			SeriesGroupDataList.Add(new SeriesGroupData("第０５期", "2006/05/18"));
			SeriesGroupDataList.Add(new SeriesGroupData("第０６期", "2008/04/19"));
			SeriesGroupDataList.Add(new SeriesGroupData("第０７期", "2010/04/17"));
			SeriesGroupDataList.Add(new SeriesGroupData("第０８期", "2012/04/14"));
			SeriesGroupDataList.Add(new SeriesGroupData("第０９期", "2014/04/19"));
			SeriesGroupDataList.Add(new SeriesGroupData("第１０期", "2017/04/15"));
			SeriesGroupDataList.Add(new SeriesGroupData("第１１期", "2020/04/18"));
			SeriesGroupDataList.Add(new SeriesGroupData("第１２期", "2023/04/22"));



			SeriesGroupDataList.AllowNew = true;

        }

    }
}
