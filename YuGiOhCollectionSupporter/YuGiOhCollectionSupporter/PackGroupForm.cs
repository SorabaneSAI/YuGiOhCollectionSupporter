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
    public partial class PackGroupForm : Form
    {
        public BindingList<PackGroupData> PackGroupDataList = new BindingList<PackGroupData>();
        public BindingList<PackGroupData> oldPackGroupDataList;

        public PackGroupForm(BindingList<PackGroupData> datalist)
        {
            InitializeComponent();

            PackGroupDataList = datalist;


            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = PackGroupDataList;

            oldPackGroupDataList = new BindingList<PackGroupData>(PackGroupDataList);   //コンストラクタにブチ込むとコピーになる
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PackGroupDataList = oldPackGroupDataList;
            Close();
        }
        private void PackGroupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            button2_Click(sender, e);
        }
    }

    public class PackGroupData
    {
        public string 親ノード名 { get; set; }   //なんかしらんけどgetsetがないとバインドされない
        public string 子ノード名 { get; set; }
        public string 含まれる文字 { get; set; }
        public bool 有効フラグ { get; set; } = true;

        public PackGroupData() { }
        public PackGroupData(string 親,string 子,string 文字)
        {
            親ノード名 = 親;
            子ノード名 = 子;
            含まれる文字 = 文字;
        }
    }
}
