using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
    class かな
    {
        public static List<行> 行リスト = new List<行>();

        static かな()
        {
            行リスト.Add(new 行("あ行", "あぁいぃうゔぅえぇおぉ"));
            行リスト.Add(new 行("か行", "かがきぎくぐけげこご"));
            行リスト.Add(new 行("さ行", "さざしじすずせぜそぞ"));
            行リスト.Add(new 行("た行", "ただちぢつづってでとど"));
            行リスト.Add(new 行("な行", "なにぬねの"));
            行リスト.Add(new 行("は行", "はばぱひびぴふぶぷへべぺほぼぽ"));
            行リスト.Add(new 行("ま行", "まみむめも"));
            行リスト.Add(new 行("や行", "やゃゆゅよょ"));
            行リスト.Add(new 行("ら行", "らりるれろ"));
            行リスト.Add(new 行("わ行", "わをん"));
            行リスト.Add(new 行("その他", "ー－・　 "));
        }

        public static string getAllTxt()
        {
            string str = "";
            foreach (var gyou in 行リスト)
            {
                str += gyou.文字;
            }
            return str;
        }

        //最初の１文字から何行かを返す
        public static 行 get行(string one_txt)
        {
            foreach (var gyou in 行リスト)
            {
                for (int i = 0; i < Program.getTextElementLength(gyou.文字); i++)
                {
                    var nextstr = Program.getTextElement(gyou.文字, i); 
                    if(nextstr == one_txt)
                    {
                        return gyou;
                    }
                }
            }
            return 行リスト.Last(); //どれでもなければその他
        }

    }
    class 行
    {
        public string 名前;
        public string 文字;
        public 行(string name, string character)
        {
            名前 = name;
            文字 = character;
        }
    }

    public class AIUEOComparer : IComparer<string>
    {
        private string aiueo = かな.getAllTxt();

        public int Compare(string x, string y)
        {

            // null の方が小さい
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            // 1文字ずつ比較
            var xsi = new StringInfo(x);
            var ysi = new StringInfo(y);
            var xlen = xsi.LengthInTextElements;
            var ylen = ysi.LengthInTextElements;
            var xidx = StringInfo.ParseCombiningCharacters(x);
            var yidx = StringInfo.ParseCombiningCharacters(y);

            for (int i = 0; i < Math.Min(xlen, ylen); i++)
            {
                var xc = StringInfo.GetNextTextElement(x, xidx[i]);
                var yc = StringInfo.GetNextTextElement(y, yidx[i]);
                if (xc == yc) continue;

                var xi = aiueo.IndexOf(xc);
                if (xi < 0) return xc.CompareTo(yc);
                var yi = aiueo.IndexOf(yc);
                if (yi < 0) return xc.CompareTo(yc);

                if (xi < yi) return -1;
                if (xi > yi) return 1;
            }

            // 文字数の短い方が小さい
            if (xlen < ylen) return -1;
            if (xlen > ylen) return 1;
            return 0;
        }
    }
}
