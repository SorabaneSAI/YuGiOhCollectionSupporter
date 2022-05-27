using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter
{
    class GetAllPacks
    {
        public static async Task<List<PackData>> getAllPackDatasAsync(string URL, Form1 form)
        {
            form.WriteLog("パックデータ取得中", LogLevel.必須項目);

            List<PackData> packDatas = new List<PackData>();

            var task = Program.GetHtml(URL);    //taskはwait,resultしてはいけない　awaitするべき
            await task;
            var html = task.Result;

            //一般商品タブが要素０、特典・同梱系タブが要素２
            var DivNodes = html.QuerySelectorAll("div[class='card_list']");


            //リストタイトルとそのコンテンツ取得（基本ブースターパックなど）
            foreach (var node in DivNodes)
            {
                var PacSetNodes = node.QuerySelectorAll("div[class='pac_set']");
                foreach (var pacsetnode in PacSetNodes)
                {
                    var ListTitleNode = pacsetnode.QuerySelector("div[class*='list_title open']>span");
                    string ListTitle = ListTitleNode.TextContent;
                    form.WriteLog(ListTitle, LogLevel.情報);

                    var PackNodes = pacsetnode.QuerySelectorAll("div[class='pack pack_ja']");
                    foreach (var packnode in PackNodes)
                    {
                        var namenode = packnode.QuerySelector("p");
                        namenode.RemoveAttribute("rt"); //なんかルビが混ざるので除去
                        string name = namenode.TextContent;
                        /*
                        var rubynodes = packnode.QuerySelectorAll("rt");
                        foreach (var rubynode in rubynodes)
                        {

                        }
                        */
                        string 相対パス = packnode.QuerySelector("input").GetAttribute("value");

                        await Task.Delay(1000); //負荷軽減のため１秒待機;
                        (int num, DateTimeOffset day) tmp = await getPackData(Config.Domain + 相対パス);

                        form.WriteLog($"{name} : {Program.ToString(tmp.day)} : 全{tmp.num}枚 : {相対パス}", LogLevel.情報);
                        PackData pd = new PackData(Config.Domain + 相対パス, name, ListTitle, "", tmp.day,tmp.num);  //シリーズと誕生日はとりあえず空欄
                        packDatas.Add(pd);
                    }

                }


            }

            form.WriteLog("パックデータ取得終了", LogLevel.必須項目);

            return packDatas;
        }

        public static async Task<(int, DateTimeOffset)> getPackData(string url)
        {
            var html = await Program.GetHtml(url);

            var DivNode = html.QuerySelector("div[id='bg']");

            //誕生日取得
            var birthdaynode = DivNode.QuerySelector("header[id='broad_title']>div>p");
            string birthdaystr = Program.getTextContent(birthdaynode);

            string ptn = "[0-9]{4}年[0-9]{2}月[0-9]{2}日";
            string dateStr = Regex.Match(birthdaystr, ptn).Value;   //正規表現で日付だけ抜き出す
            DateTimeOffset birthday = Program.ConvertDate(dateStr, "yyyy年MM月dd日");


            //カード枚数取得
            var BodyNode = DivNode.QuerySelector("div[id='article_body']");
            var TextNode = BodyNode.QuerySelector("div[class='text']");
            string txt = Program.getTextContent(TextNode);
            string numstr = Regex.Match(txt, @"全(.+?)枚").Groups[1].Captures[0].Value;   //正規表現で日付だけ抜き出す
            int num;
            if(int.TryParse(numstr,out num))
            {
            }
            else
            {
                Program.Log.Error("getPackDataで日付変換失敗");
                MessageBox.Show("getPackDataで日付変換失敗", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();

            }
            return (num, birthday);

        }
    }
}
