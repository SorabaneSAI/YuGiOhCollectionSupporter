using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
    class GetAllPacks
    {
        public static async Task<List<PackData>> getAllPackDatasAsync(string URL, Form1 form)
        {
            form.UpdateLabel("パックデータ取得中");
            form.AddLog("パックデータ取得中", LogLevel.必須項目);
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
                    form.AddLog(ListTitle, LogLevel.情報);

                    var PackNodes = pacsetnode.QuerySelectorAll("div[class='pack pack_ja']");
                    foreach (var packnode in PackNodes)
                    {
                        string name = packnode.QuerySelector("p").TextContent;
                        string 相対パス = packnode.QuerySelector("input").GetAttribute("value");

                        form.AddLog(name + ":" + 相対パス , LogLevel.情報);
                        PackData pd = new PackData(Config.Domain+相対パス,name, ListTitle,"");  //シリーズはとりあえず空欄
                        packDatas.Add(pd);
                    }

                }


            }
            form.UpdateLabel("パックデータ取得終了");
            form.AddLog("パックデータ取得終了", LogLevel.必須項目);

            return packDatas;
        }
    }
}
