using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YuGiOhCollectionSupporter.KanabellForm;

namespace YuGiOhCollectionSupporter.データ
{
	internal class PriceDataBase
	{
		public List<KanabellCard> PackDataList = new List<KanabellCard>();
		public static string SaveDataPath = "PriceDataBase.json";

	}
}
