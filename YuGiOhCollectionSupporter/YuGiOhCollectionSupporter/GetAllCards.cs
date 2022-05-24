using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
    class GetAllCards
    {
        public static async Task<CardDataBase> getAllCardsAsync(Config config, Form1 form)
        {
            form.UpdateLabel("カードデータ取得中");
            form.logform.AddLog("カードデータ取得中", LogLevel.必須項目);

            CardDataBase carddatabase = new CardDataBase();

            for (decimal i = config.CardID_MIN; i < config.CardID_MAX + 1; i++)
            {
                await Task.Delay(1000); //負荷軽減のため１秒待機;

                //カードリストはjavascriptを経由しないと入手できないっぽいので直接idを入力(当てずっぽう)
                string url = config.URL2 + i;
                var html = await Program.GetHtml(url);

                if (html.Source.Text.IndexOf("カード情報がありません。") >= 0)
                {
                    form.logform.AddLog(config.URL2 + i + ":カードデータなし", LogLevel.情報);
                    continue;
                }

                var DivNameNode = html.QuerySelector("div[id='cardname']>h1");
                var ListSpanNodes = DivNameNode.QuerySelectorAll("span");
                string 読み = ListSpanNodes[0].TextContent;
                string 英語 = ListSpanNodes[1].TextContent;

                //タグがないので面倒だが、正規表現を使って１回だけ置き換える（全部置き換えるとカタカナの名前は全部なくなってしまう）
                var re1 = new Regex(読み);
                var re2 = new Regex(英語);
                string str1 = re1.Replace(DivNameNode.TextContent, "", 1);
                string str2 = re2.Replace(str1, "", 1);
                string 名前 = str2.Trim(); 

                var CardTextSetNode = html.QuerySelector("div[id='CardTextSet']");
                var ItemBoxNodes = CardTextSetNode.QuerySelectorAll("div[class*='item_box']");

                Dictionary<string, string> dic = new Dictionary<string, string>();
                string 種族 = "";
                foreach (var node in ItemBoxNodes)
                {
                    //種族欄のとき
                    var speciesnode = node.QuerySelector("p[class='species']");
                    if (speciesnode != null)
                    {
                        種族 = speciesnode.TextContent;
                        continue;
                    }
                    //その他の欄のとき
                    var titlenode = node.QuerySelector("span[class='item_box_title']");
                    var valuenode = node.QuerySelector("span[class='item_box_value']");
                    if (titlenode != null && valuenode != null)  //ペンデュラムだと中身がないことがある
                        dic.Add(titlenode.TextContent.Trim(), valuenode.TextContent.Trim());
                }

                //ペンデュラムテキスト
                var pendulumnode = CardTextSetNode.QuerySelector("div[class='CardText pen']>div[class='item_box_text']");

                //カードテキスト
                var cardtextnode = CardTextSetNode.QuerySelector("div[class='CardText']>div[class='item_box_text']");
                string cardtext = CardTextSetNode.QuerySelector("div[class='CardText']>div[class='item_box_text']").TextContent.Replace("カードテキスト", "").Trim();//タグないので面倒


                //カード情報を取得したので、次は収録シリーズを取得

                List<CardData.CardVariation> listvaridations = new List<CardData.CardVariation>();

                var SeriesNode = html.QuerySelector("div[id='update_list']");
                var SeriesNodes = SeriesNode.QuerySelectorAll("div[class='t_row']");

                foreach (var node in SeriesNodes)
                {
                    var 誕生日node = node.QuerySelector("div[class='time']");
                    string 誕生日 = getTextContent(誕生日node);
                    var 略号node = node.QuerySelector("div[class='card_number']");
                    string 略号 = getTextContent(略号node);
                    var パック名node = node.QuerySelector("div[class='pack_name flex_1']");
                    string パック名 = getTextContent(パック名node);
                    var URLnode = node.QuerySelector("input[class='link_value']");
                    string URL = Config.Domain + URLnode.GetAttribute("value");
                    var Rarenode = node.QuerySelector("div[class*='lr_icon']");
                    string レア記号 = getTextContent(Rarenode.QuerySelector("p"));
                    string レアリティ = getTextContent(Rarenode.QuerySelector("span"));

                    PackData packdata = new PackData(URL,パック名,"","",誕生日);
                    CardData.Rarity rarity = new CardData.Rarity(レア記号, レアリティ);

                    //リストと比較して同じならレアリティ違いに統合
                    foreach (var vari in listvaridations)
                    {
                        if(packdata.Name == vari.発売パック.Name)
                        {
                            vari.ListRarity.Add(rarity);
                            goto nextloop;
                        }
                    }

                    //なかったら追加
                    List<CardData.Rarity> listrarity = new List<CardData.Rarity>();
                    listrarity.Add(rarity);

                    CardData.CardVariation variation = new CardData.CardVariation(packdata, 略号, listrarity);
                    listvaridations.Add(variation);

                nextloop:;
                }

                CardData carddata = new CardData(url, 名前,読み, 英語, dic, getTextContent(pendulumnode), cardtext, listvaridations);

                form.logform.AddLog(carddata.名前 + " : " + carddata.読み + " : " + carddata.英語名 +" : " +config.URL2 + i, LogLevel.情報);
                form.logform.AddLog(Program.ToJson(carddata.ValuePairs) + " : " + carddata.テキスト + " : " + carddata.ペンデュラム効果, LogLevel.情報);
                form.logform.AddLog(Program.ToJson(listvaridations) , LogLevel.情報);
                form.UpdateLabel((i- config.CardID_MIN) + "/" + (config.CardID_MAX + 1 - config.CardID_MIN) + ":" + carddata.名前);

                carddatabase.CardDB.Add(carddata);
            }
            form.UpdateLabel("カードデータ取得終了");
            form.logform.AddLog("カードデータ取得終了", LogLevel.必須項目);


            return carddatabase;

        }

        //nodeがnullでなければ中身を返す nullなら無をかえす
        public static string getTextContent(AngleSharp.Dom.IElement node)
        {
            return (node != null ? node.TextContent.Trim() : "");
        }

    }
}
