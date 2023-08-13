using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace YuGiOhCollectionSupporter
{
	public static class SQLite
	{
		public static string DBSourcePath = "Database.sqlite";
		public static string CardTableName = "CardTable";

		public static void CreateTable()
		{
			//テーブルなければ作る　
			DBExecuteNonQuery($"create table if not exists {CardTableName}(" +
							"id integer primary key," +
							"json text)");
		}

		public static void DBExecuteNonQuery(string query)
		{
			try
			{
				var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = DBSourcePath };

				using (var connect = new SQLiteConnection(sqlConnectionSb.ToString()))
				{
					connect.Open();

					using (var cmd = new SQLiteCommand(connect))
					{
						cmd.CommandText = query;
						cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				Program.WriteLog(ex.Message, LogLevel.エラー);
			}
		}

		public static void InsertRecord(int id, CardData data)
		{
			string str = Program.CardDataToJson(data).Replace("'", "''");
			var query = $"INSERT INTO {CardTableName} (id,json) VALUES ({id},'{str}')";	//{}のために'で囲み、'のために'を''にする
			DBExecuteNonQuery(query.ToString());
		}

		public static void UpdateRecord(int id, CardData data)
		{
			string str = Program.CardDataToJson(data).Replace("'", "''");
			var query = $"UPDATE {CardTableName} SET json = '{str}' WHERE id = {id};";
			DBExecuteNonQuery(query.ToString());
		}
	}
}
