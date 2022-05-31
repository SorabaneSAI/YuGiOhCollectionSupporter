using System;
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

		public class TreeNodeAIUEOTag
        {
			public 行 Gyou;
			public List<CardData> CardList = new List<CardData> ();
			public TreeNodeAIUEOTag(行 gyou)
            {
				Gyou = gyou;
            }
        }

		public static void SetFormPanelLeft(CardDataBase CardDB, Form1 form)
		{
			//ツリーをクリア
			TreeView treeview = form.treeView1;
			var nodes = treeview.Nodes;
			nodes.Clear();

			//チェック次第でソートする
			if(form.あいうえお順ToolStripMenuItem.CheckState == CheckState.Indeterminate)
            {
				CardDB.SortAIUEO();

				RecursiveAddTreeNode(nodes,CardDB.CardDB,0);

			}
			else if(form.パック順ToolStripMenuItem.CheckState == CheckState.Indeterminate)
            {

            }

			List<TreeNode> TreeNodeList = new List<TreeNode>();



		}

		//Depthは読み取る文字数であり再帰回数
		public static void RecursiveAddTreeNode(TreeNodeCollection treenodes,List<CardData> carddatas, int Depth)
        {
			//あ行～その他までのnodeを追加
			foreach (var gyou in かな.行リスト)
			{
				var treenode = treenodes.Add($"[{gyou.名前}]");
				treenode.Tag = new TreeNodeAIUEOTag(gyou);
			}
			if (Depth ==0)
			{
				//最初だけ非表示ノードも追加
				treenodes.Add($"非表示").Tag = new TreeNodeAIUEOTag(null);
			}



			//カードをツリーに登録する
			foreach (var card in carddatas)
			{
				//最初の文字を取得
				string one_txt = Program.getTextElement(card.読み, Depth);
				//カードの読みが何行に含まれるかを取得
				var gyou = かな.get行(one_txt);

				//その行を持つtreenodeを探す
				foreach (TreeNode node in treenodes)
				{
					var treenodetag = (TreeNodeAIUEOTag)node.Tag;
					if (treenodetag.Gyou.Equals(gyou))
					{
						treenodetag.CardList.Add(card);
						break;
					}
				}

			}

            //要素がないツリーを削除
            for (int i = treenodes.Count-1; i >=0 ; i--)
            {
				TreeNode node = treenodes[i];
				if(((TreeNodeAIUEOTag)node.Tag).CardList.Count ==0)
                {
					treenodes.RemoveAt(i);

				}
            }

			//カード枚数を表示
			foreach (TreeNode node in treenodes)
			{
				var treenodetag = (TreeNodeAIUEOTag)node.Tag;
				int num = treenodetag.CardList.Count;
				node.Text += $"({num})";
			}

			//再帰的にツリーを作成
			foreach (TreeNode node in treenodes)
			{
				var treenodetag = (TreeNodeAIUEOTag)node.Tag;
				if(treenodetag.Gyou.名前 != "非表示")
                {
					RecursiveAddTreeNode(node.Nodes, treenodetag.CardList,Depth+1);
				}
			}


		}

		//右側はパック選択パックの中身
		public static void SetFormPanelRight(CardDataBase CardDB, string packname, Form1 form)
		{
			/*
			//中身あったら削除
			if (form.splitContainer1.Panel2.Controls.Count > 0)
				form.splitContainer1.Panel2.Controls.Clear();
			foreach (var pack in CardDB.PackDB)
			{
				if (pack.Name == packname)
				{
					PackUI packUI = new PackUI();
					packUI.Init(CardDB,pack);
					form.splitContainer1.Panel2.Controls.Add(packUI);
					break;
				}
			}

			//ホーム画面
			HomeUI homeUI = new HomeUI();
			homeUI.Init(CardDB);
			homeUI.BringToFront();
			form.splitContainer1.Panel2.Controls.Add(homeUI);
			*/
		}
	}
}
