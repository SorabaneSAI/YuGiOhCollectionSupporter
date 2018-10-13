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
		public string TypeName = "";
		public string SeriesName = "";
		public string Code = "";

		public PackData(string url, string name, string type, string series)
		{
			URL = url;
			Name = name;
			TypeName = type;
			SeriesName = series;
		}
	}
}
