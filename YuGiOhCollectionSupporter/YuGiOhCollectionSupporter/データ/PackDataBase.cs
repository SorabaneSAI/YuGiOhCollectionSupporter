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

        //存在しないデータを追加した数を返す
        public int AddPackDataList(List<PackData> packdatalist)
        {
            int count = 0;
            //パックデータは変化しないはずなので新しいのを追加するだけ
            foreach (var newdata in packdatalist)
            {
                foreach (var olddata in PackDataList)
                {
                    if(newdata.URL == olddata.URL)
                    {
                        goto next;
                    }
                }
                //全部違ったら存在しないデータ
                PackDataList.Add(newdata);
                count++;
            next:;
            }
            return count;
        }

    }
}
