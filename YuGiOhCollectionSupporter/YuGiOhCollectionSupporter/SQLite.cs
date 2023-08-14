using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace YuGiOhCollectionSupporter
{
	public class SQLite : DbContext
	{
		public static string DBSourcePath = "Database.sqlite";
		public static string CardTableName = "CardTable";

		public DbSet<CardData> CardDataTable { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			SqliteConnectionStringBuilder stringBuilder = new SqliteConnectionStringBuilder();
			stringBuilder.DataSource = DBSourcePath;

			using (var sqliteConnection = new SqliteConnection(stringBuilder.ToString()))
			{
				optionsBuilder.UseSqlite(sqliteConnection);
				;
			}
		}

		public void DBAccess(Action<SQLite, Object> act, Object obj)
		{
			try
			{
				//テーブルなければ作る　あれば接続
				using (var access = new SQLite())
				{
					access.Database.EnsureCreated();

					if (act == null) return;
					act(access, (CardData) obj);
				}
			}
			catch (Exception ex)
			{
				Program.WriteLog(ex.Message, LogLevel.エラー);
			}

		}

		public void InsertCardData(SQLite access, Object data)
		{
			access.CardDataTable.Add((CardData)data);
			access.SaveChanges();
		}

		/*

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

		public static void InsertCardData(int id, CardData data)
		{
			string str = Program.CardDataToJson(data).Replace("'", "''");
			var query = $"INSERT INTO {CardTableName} (id,json) VALUES ({id},'{str}')";	//{}のために'で囲み、'のために'を''にする
			DBExecuteNonQuery(query.ToString());
		}

		public static void UpdateCardData(int id, CardData data)
		{
			string str = Program.CardDataToJson(data).Replace("'", "''");
			var query = $"UPDATE {CardTableName} SET json = '{str}' WHERE id = {id};";
			DBExecuteNonQuery(query.ToString());
		}
		*/
	}
}
