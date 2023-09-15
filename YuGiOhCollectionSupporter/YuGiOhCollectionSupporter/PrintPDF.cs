using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp;
using PdfSharp.Fonts;
using PdfSharp.Pdf;

namespace YuGiOhCollectionSupporter
{
    public class PrintPDF
    {
        static PrintPDF()
        {
            // フォントリゾルバーのグローバル登録
            PdfSharp.Fonts.GlobalFontSettings.FontResolver = new JapaneseFontResolver();
        }
        public static void Print(List<TwinCardData> TwinCardDataList, int num)
        {


            Document document = new Document();

            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(1);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(1);
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(1);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(1);
            document.DefaultPageSetup.Orientation = Orientation.Landscape;

            // Add a section to the document
            Section section = document.AddSection();

            /*
            // Add a paragraph to the section
            Paragraph paragraph = section.AddParagraph();

            paragraph.Format.Font.Color = Color.FromCmyk(100, 30, 20, 50);

            // Add some text to the paragraph
            paragraph.AddFormattedText("Hello, World!", TextFormat.Bold);
            */

            document.LastSection.Add(MakeTable(TwinCardDataList,num));


            string filename = "NotHaveCardList"+num+".pdf";

            //レンダリングしてPDFを出力
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(filename);

            // デフォルトプログラムで表示
//            Process.Start(filename);

        }

        // 日本語フォントのためのフォントリゾルバー
        public class JapaneseFontResolver : IFontResolver
        {
            // 源真ゴシック（ http://jikasei.me/font/genshin/）
            private static readonly string GEN_SHIN_GOTHIC_MEDIUM_TTF =
                "YuGiOhCollectionSupporter.fonts.GenShinGothic-Monospace-Medium.ttf";

            public byte[] GetFont(string faceName)
            {
                switch (faceName)
                {
                    case "GenShinGothic#Medium":
                        return LoadFontData(GEN_SHIN_GOTHIC_MEDIUM_TTF);
                }
                return null;
            }

            public FontResolverInfo ResolveTypeface(
                        string familyName, bool isBold, bool isItalic)
            {
                var fontName = familyName.ToLower();

                switch (fontName)
                {
                    case "gen shin gothic":
                        return new FontResolverInfo("GenShinGothic#Medium");
                }

                // デフォルトのフォント
                return PlatformFontResolver.ResolveTypeface("Arial", isBold, isItalic);
            }

            // 埋め込みリソースからフォントファイルを読み込む
            private byte[] LoadFontData(string resourceName)
            {
                var assembly = Assembly.GetExecutingAssembly();

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                        throw new ArgumentException("No resource with name " + resourceName);

                    int count = (int)stream.Length;
                    byte[] data = new byte[count];
                    stream.Read(data, 0, count);
                    return data;
                }
            }
        }


        public static Table MakeTable(List<TwinCardData> TwinCardDataList,int num)
        {
            Paragraph paragraph = new Paragraph();
            Font font = new Font("gen shin gothic", 5);

            Table table = new Table();
            table.Borders.Visible = true;
            table.Rows.Height = 6;
            //列作成
            var column = table.AddColumn(Unit.FromCentimeter(5));   //名前
            column = table.AddColumn(Unit.FromCentimeter(3));       //読み
            column = table.AddColumn(Unit.FromCentimeter(0.8));     //属性
            column = table.AddColumn(Unit.FromCentimeter(4));       //種族
            column = table.AddColumn(Unit.FromCentimeter(1.2));     //レベル
            column = table.AddColumn(Unit.FromCentimeter(1.3));     //ATK/DEF
            column = table.AddColumn(Unit.FromCentimeter(1.4));     //略号
            column = table.AddColumn(Unit.FromCentimeter(6));       //パック名
			column = table.AddColumn(Unit.FromCentimeter(1));       //レアリティ
			column = table.AddColumn(Unit.FromCentimeter(1));       //持ってたらランク
			column = table.AddColumn(Unit.FromCentimeter(1));       //値段

			foreach (var twincarddata in TwinCardDataList)
            {
				CardVariation oldvariation = null;
				foreach (var variation in twincarddata.carddata.ListVariations)
                {
                    if(num ==1 && oldvariation!=null) //略号までは前と比較
                    {
                        if (variation.略号.get略号Full().Equals(oldvariation.略号.get略号Full()))
                            continue;
					}
					oldvariation = variation;

                    if (twincarddata.get所持フラグ(variation) == true)
                    {
                        //レアリティ別で出力してるときは、ランクがAでないと出力
                        if (num == 2)
                        {
                            if (twincarddata.getRank(variation) == EKanabellRank.A)
								continue;
						}
                        else
    						continue;
                    }

                    List<string> strlist = new List<string>();
                    strlist.Add(twincarddata.carddata.名前);
                    strlist.Add(Kanaxs.Kana.ToHankakuKana( twincarddata.carddata.読み));
                    string str3 = getvalue(twincarddata.carddata, "属性");
					strlist.Add(str3 == "" ? " " : str3);   //""だとなぜか改行が発生する

                    if (twincarddata.carddata.種族 == "") //魔法罠
                    {
                        string type;
                        twincarddata.carddata.ValuePairs.TryGetValue("効果", out type);
                        strlist.Add(type);
                    }
                    else
                    {
                        strlist.Add(twincarddata.carddata.種族);
                    }
                    string lv = getvalue(twincarddata.carddata, "レベル");
                    string rank = getvalue(twincarddata.carddata, "ランク");
                    string link = getvalue(twincarddata.carddata, "リンク");
                    if(lv != "")
                        strlist.Add(lv);
                    else if(rank != "")
                        strlist.Add(rank);
                    else if(link != "")
                        strlist.Add(link);
                    else
						strlist.Add(" ");

					string atk = getvalue(twincarddata.carddata, "攻撃力");
                    string def = getvalue(twincarddata.carddata, "守備力");
                    string str = " ";
                    if (!(atk == "" && def == ""))
                        str = atk + " / " + def;

                    strlist.Add(str);

					if (num >= 1)
						strlist.Add(variation.略号.get略号Full());

					if (num >= 1)
						strlist.Add(variation.発売パック.Name);

                    if (num == 2)
                    {
                        strlist.Add(variation.rarity.Initial);
                        strlist.Add(twincarddata.get所持フラグ(variation) == true ?  twincarddata.getRank(variation).ToString() : " ");
                        foreach (var kanabell in variation.KanabellList)
                        {
                            if(!(kanabell.Rank == EKanabellRank.在庫なし || kanabell.Rank == EKanabellRank.不明))
                            {
								strlist.Add(kanabell.Rank.ToString() + " " + kanabell.Price.ToString());
                                break;
							}
						}
                    }

                    //空欄があると改行がなぜか発生するため欄を埋める
                    while(strlist.Count < 11)
                    {
                        strlist.Add(" ");
                    }

                    AddRow(table, font, strlist);

                    if (num == 0)   //名前だけは次の名前に
                        break;


				}
            }
            //           table.Format.Alignment = ParagraphAlignment.Justify;
            return table;
        }

        public static string getvalue(CardData carddata, string key)
        {
            string outstr;
            if (carddata.ValuePairs.TryGetValue(key,out outstr))
            {

            }
            else
                outstr = "";
            return outstr;
        }

        public static void AddRow(Table table,Font font,List<string> strlist)
        {
            var row = table.AddRow();
            for(int i=0; i< strlist.Count; i++)
            {
                var paragraph = row.Cells[i].AddParagraph();
                paragraph.AddFormattedText(strlist[i], font);
            }
        }
    }
}
