using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
    class GetAllCards
    {
        public static async Task<List<CardData>> getAllCardsAsync(Config config, Form1 form)
        {
            form.UpdateLabel("カードデータ取得中");
            form.AddLog("カードデータ取得中", LogLevel.必須項目);
            List<CardData> ListCardData = new List<CardData>();

            for (decimal i = config.CardID_MIN; i < config.CardID_MAX+1; i++)
            {
                await Task.Delay(1000); //負荷軽減のため１秒待機;

                //カードリストはjavascriptを経由しないと入手できないっぽいので直接idを入力(当てずっぽう)
                string url = "https://www.db.yugioh-card.com/yugiohdb/card_search.action?ope=2&cid=4007";
                var html = await Program.GetHtml(url); //config.URL2 + i

                if (html.Source.Text.IndexOf("カード情報がありません。") >= 0)
                {
                    form.AddLog(config.URL2 + i + ":カードデータなし", LogLevel.情報);
                    continue;
                }

                var DivNameNode = html.QuerySelector("div[id='cardname']>h1");
                var ListSpanNodes = DivNameNode.QuerySelectorAll("span");
                string 読み = ListSpanNodes[0].TextContent;
                string 英語 = ListSpanNodes[1].TextContent;
                string 名前 = DivNameNode.TextContent.Replace(読み, "").Replace(英語, "").Replace("\n","").Replace("\t","");  //タグないので面倒

            }



            //

            //まず全てのカードのURLを取得



            return null;

        }

    }
}
