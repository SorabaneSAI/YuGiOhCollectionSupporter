using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
	public class PackData
	{
		public string URL = "";
		public string Name = "";
		public string TypeName = "";    //基本ブースターパックなどのタイプ
		public string SeriesName = "";  //10期などのシリーズ
		public DateTimeOffset BirthDay;

        public int CardCount;   //取得したカード枚数


        public PackData(string url, string name, string type, string series, DateTimeOffset birthDay, int cardCount)
        {
            URL = url;
            Name = name;
            TypeName = type;
            SeriesName = series;
            BirthDay = birthDay;
            CardCount = cardCount;
        }

    }
}
