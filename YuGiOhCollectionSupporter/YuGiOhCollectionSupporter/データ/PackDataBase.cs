using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter
{
    public class PackDataBase
    {
        public List<PackData> PackDataList = new List<PackData>();
        public string SaveDataPath = "PackDataBase.json";

        //表示フラグの引き継ぎをし、データを新しいものに更新し、追加と更新した数を返す
        public (int, int) AddPackDataList(List<PackData> packdatalist)
        {
            int updatenum = 0;
            int newcount = 0;
            foreach (var newdata in packdatalist)
            {
                for(int i=0; i< PackDataList.Count; i++)
                {
                    var olddata = PackDataList[i];
                    if (newdata.URL == olddata.URL)
                    {
                        newdata.表示フラグ = olddata.表示フラグ;  //データ引き継ぎ
                        olddata = newdata;  //旧データは新データで上書き
                        updatenum++;
                        goto next;
                    }
                }
                //全部違ったら存在しないデータ
                PackDataList.Add(newdata);
                newcount++;
            next:;
            }
            return (newcount, updatenum);
        }

        public PackData SearchPackData(string URL)
        {
            var list = PackDataList.Where(x => x.URL == URL).ToList();
            if(list.Count >0) return list[0];
            return null;
        }

    }
}
