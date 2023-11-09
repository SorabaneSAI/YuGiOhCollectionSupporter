using YuGiOhCollectionSupporter;
using YuGiOhCollectionSupporter.データ;

namespace DataUpdate
{
	class Program
	{

		static void Main()
		{
			UserCardDataBase UserCardDB = new UserCardDataBase();
			PriceDataBase PriceDB = new PriceDataBase();

			string UserCardDBPath = "Save\\UserCardData.json";
			string PriceDBPath = "Save\\PriceDataBase.json";

			YuGiOhCollectionSupporter.Program.Load(UserCardDBPath, ref UserCardDB);
			YuGiOhCollectionSupporter.Program.Load(PriceDBPath, ref PriceDB.PriceDataList);

			YuGiOhCollectionSupporter.Program.Save(UserCardDBPath + "_old", UserCardDB);
			YuGiOhCollectionSupporter.Program.Save(PriceDBPath + "_old", PriceDB.PriceDataList);


		}
	}

}