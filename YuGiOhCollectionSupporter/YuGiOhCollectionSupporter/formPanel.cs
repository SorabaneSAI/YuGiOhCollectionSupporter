﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter
{
	class formPanel
	{
		//パネルにいろいろ設置する


		//左側はシーズンとパック名
		public static void SetFormPanelLeft(CardDataBase CardDB, Form1 form)
		{
			SplitterPanel panel = form.splitContainer1.Panel1;
			TreeView treeview = form.treeView1;
			List<PackData> packlist = form.CardDB.PackDB;

			List<string> serieslist = new List<string>();

			//重複ないようにシリーズ取得
			foreach (var item in packlist)
			{
				if (serieslist.Find(m => m == item.SeriesName) == null)
				{
					serieslist.Add(item.SeriesName);
				}
			}

			List<TreeNode> TreeNodeList = new List<TreeNode>();
			TreeNode SeriesTreeNode = new TreeNode();
			string seriesname = "";
			foreach (var pack in packlist)
			{
				//新しいシリーズの名前になったらラベル作成
				if (pack.SeriesName != seriesname)
				{
					seriesname = pack.SeriesName;
					treeview.Nodes.Add(seriesname);
				}

				for (int i = 0; i < treeview.Nodes.Count; i++)
				{
					var node = treeview.Nodes[i];
					if (node.Text == seriesname)
					{
						node.Nodes.Add(pack.Name);
						break;
					}
				}

			}
			treeview.Nodes.AddRange(TreeNodeList.ToArray());

		}
		//左側はシーズンとパック名
		public static void SetFormPanelRight(CardDataBase CardDB)
		{

		}
	}
}
