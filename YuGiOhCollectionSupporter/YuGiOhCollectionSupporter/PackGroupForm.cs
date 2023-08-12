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

        public static void InitialDataSet(BindingList<PackGroupData> PackGroupDataList)
		{
            PackGroupDataList.Add(new PackGroupData("構築済みデッキ", "スターターデッキ", "STARTER DECK"));
            PackGroupDataList.Add(new PackGroupData("構築済みデッキ", "ストラクチャーデッキ", "STRUCTURE DECK"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "COLLECTORS TIN", "COLLECTORS TIN"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "アニメーションクロニクル", "アニメーションクロニクル"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "WORLD PREMIERE PACK", "WORLD PREMIERE PACK"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "デッキビルドパック", "デッキビルドパック"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "ブースターSP", "ブースターSP"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "コレクターズパック", "コレク"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "デュエリストエディション", "DUELIST EDITION"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "ビギナーズエディション", "BEGINNER'S EDITION"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "エキスパートエディション", "EXPERT EDITION"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "ゴールドシリーズ", "GOLD"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "デュエリストパック", "デュエリストパック"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "プレミアムパック", "PREMIUM PACK"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "エクストラパック", "EXTRA PACK"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "デュエリストレガシー", "DUELIST LEGACY"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "ベンダー版", "ベンダー版"));
            PackGroupDataList.Add(new PackGroupData("その他ブースターパック", "ブースター", "Booster"));
            PackGroupDataList.Add(new PackGroupData("書籍", "Vジャンプ", "Vジャンプ"));
            PackGroupDataList.Add(new PackGroupData("書籍", "遊戯王OCGストラクチャーズ", "遊戯王OCGストラクチャーズ"));
            PackGroupDataList.Add(new PackGroupData("書籍", "ザ・ヴァリュアブル・ブック", "ザ・ヴァリュアブル・ブック"));
            PackGroupDataList.Add(new PackGroupData("書籍", "マスターガイド", "MASTER GUIDE"));
            PackGroupDataList.Add(new PackGroupData("書籍", "V JUMPエディション", "V JUMP EDITION"));
            PackGroupDataList.Add(new PackGroupData("書籍", "リミテッドエディション", "LIMITED EDITION"));
            PackGroupDataList.Add(new PackGroupData("書籍", "週刊少年ジャンプ", "週刊少年ジャンプ"));
            PackGroupDataList.Add(new PackGroupData("書籍", "最強ジャンプ", "最強ジャンプ"));
            PackGroupDataList.Add(new PackGroupData("書籍", "漫画　遊戯王R", "遊戯王R"));
            PackGroupDataList.Add(new PackGroupData("書籍", "漫画　遊戯王GX", "遊戯王GX"));
            PackGroupDataList.Add(new PackGroupData("書籍", "漫画　遊戯王5D's", "遊戯王5D's"));
            PackGroupDataList.Add(new PackGroupData("書籍", "漫画　遊戯王ZEXAL", "遊戯王ZEXAL"));
            PackGroupDataList.Add(new PackGroupData("書籍", "漫画　遊戯王ARC-V", "遊戯王ARC-V 第"));
            PackGroupDataList.Add(new PackGroupData("大会", "トーナメントパック", "トーナメントパック"));
            PackGroupDataList.Add(new PackGroupData("大会", "ワールドチャンピオンシップ", "Yu-Gi-Oh! WORLD CHAMPIONSHIP"));
            PackGroupDataList.Add(new PackGroupData("プロモーション", "ジャンプフェスタ", "ジャンプフェスタ"));
            PackGroupDataList.Add(new PackGroupData("プロモーション", "スペシャルパック", "SPECIAL PACK"));
            PackGroupDataList.Add(new PackGroupData("プロモーション", "プロモーションパック", "プロモーションパック"));

            PackGroupDataList.AllowNew = true;

        }

    }
}
