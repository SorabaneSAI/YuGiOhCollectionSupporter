using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter
{
	public class TreeNodeAIUEOTag
	{
		public 行 Gyou;
		public CardDataBase CardDB = new CardDataBase();
		public TreeNodeAIUEOTag(行 gyou)
		{
			Gyou = gyou;
		}
	}

	class formPanel
	{
		//パネルにいろいろ設置する


		public async static void SetFormPanelLeft(CardDataBase CardDB, Form1 form)
		{
			Program.WriteLog("ツリーノード作成中",LogLevel.必須項目);
			form.InvalidMenuItem();

			//ツリーをクリア
			TreeView treeview = form.treeView1;
			var nodes = treeview.Nodes;
			nodes.Clear();

			TreeView tmptreeview = new TreeView();  //TreeNodeに別スレッドが触らないためにこれを使用

			//チェック次第でソートする
			if (form.あいうえお順ToolStripMenuItem.CheckState == CheckState.Indeterminate)
            {
				CardDB.SortAIUEO();

				await RecursiveAddTreeNodeAIUEO(tmptreeview.Nodes, CardDB, 0);
				/*
				//別スレッドで動かすためにTask.Run
				var task = Task.Run(() => RecursiveAddTreeNode(tmptreeview.Nodes, CardDB.CardDB, 0));

				//別スレッドの例外をここで取得
				_ = task.ContinueWith((compt) =>
				{
					Program.WriteLog("A:" + compt.Exception.GetType(), LogLevel.エラー);
					if (compt.Exception is AggregateException age)
					{
						Program.WriteLog("C:" + age.InnerException.GetType(), LogLevel.エラー);
					}
				});
				*/

			}
			else if(form.パック順ToolStripMenuItem.CheckState == CheckState.Indeterminate)
            {

            }

			//面倒だけどTreeNodeCollectionのせいでこうするしかない！
			foreach (TreeNode node in tmptreeview.Nodes)
            {
				treeview.Nodes.Add((TreeNode)node.Clone());	//クローンしないとなぜか表示されない
			}


			Program.WriteLog("ツリーノード作成終了", LogLevel.必須項目);
			form.ValidMenuItem();

		}

		//Depthは読み取る文字数であり再帰回数 
		public async static Task RecursiveAddTreeNodeAIUEO(TreeNodeCollection treenodes, CardDataBase carddb, int Depth)
        {
			//あ～んなどまでの全部のNodeをここにも保存
			List<TreeNode> AIUEONodeList = new List<TreeNode>();

			//親のノードの名前を持ってくる
			string ParentName = "";
			if(treenodes.Count >0)
				ParentName = treenodes[0].Text;

			//あ行～その他までのnodeを追加
			foreach (var gyou in かな.行リスト)
			{
				var GyouNode = treenodes.Add($"{ParentName}[{gyou.名前}]");
				GyouNode.Tag = new TreeNodeAIUEOTag(gyou);

				//各行のひらがなをノードとして行ノードに追加
                for (int i = 0; i < Program.getTextLength(gyou.文字); i++)
                {
					string one_text = Program.getTextElement(gyou.文字,i);
					var KanaNode = GyouNode.Nodes.Add(one_text);
					KanaNode.Tag = new TreeNodeAIUEOTag(new 行(ParentName + one_text, one_text));  //１文字だけの行扱いだけど特に意味はない
					AIUEONodeList.Add(KanaNode);
				}
			}
			if (Depth ==0)
			{
				//最初だけ非表示ノードも追加
				TreeNode node = treenodes.Add($"非表示");
				node.Tag = new TreeNodeAIUEOTag(null);

				foreach (var card in carddb.CardList)
                {
					if (card.表示フラグ == false)
                    {
						((TreeNodeAIUEOTag)node.Tag).CardDB.CardList.Add(card);
					}
				}
			}



			//カードをツリーに登録する
			foreach (var card in carddb.CardList)
			{
				if (card.表示フラグ == false) continue;
				//文字数をオーバーしたらスキップ
				if (Program.getTextLength(card.読み) <= Depth)
					continue;
				//最初の文字を取得
				string one_txt = Program.getTextElement(card.読み, Depth);

                foreach (var node in AIUEONodeList)
                {
					if(node.Text.Equals(one_txt))
                    {
						//行ノードとその下のかなノード両方登録
						((TreeNodeAIUEOTag)node.Tag).CardDB.CardList.Add(card);
						((TreeNodeAIUEOTag)node.Parent.Tag).CardDB.CardList.Add(card);
						goto nextloop;
                    }
                }

                //どこにも所属出来ない場合、その他に
                foreach (TreeNode node in treenodes)
                {
					if(node.Text.Contains("その他"))
                    {
						((TreeNodeAIUEOTag)node.Tag).CardDB.CardList.Add(card);
						break;
					}
				}

			nextloop:;
			}

            //要素がないツリーを削除
            for (int i = treenodes.Count-1; i >=0 ; i--)
            {
				TreeNode node = treenodes[i];

                for (int j = node.Nodes.Count - 1; j >=0; j--)
                {
					TreeNode childnode = node.Nodes[j];
					if (((TreeNodeAIUEOTag)childnode.Tag).CardDB.CardList.Count == 0)
                    {
						node.Nodes.RemoveAt(j);
					}
				}
				if (((TreeNodeAIUEOTag)node.Tag).CardDB.CardList.Count ==0)
                {
					treenodes.RemoveAt(i);

				}
            }

			//カード枚数を表示
			foreach (TreeNode node in treenodes)
			{
				var treenodetag = (TreeNodeAIUEOTag)node.Tag;
				int num = treenodetag.CardDB.CardList.Count;
				node.Text += $"({num})";

                foreach (TreeNode childnode in node.Nodes)
                {
					var childnodetag = (TreeNodeAIUEOTag)childnode.Tag;
					int childnum = childnodetag.CardDB.CardList.Count;
					childnode.Text += $"({childnum})";
				}
			}

			//再帰的にツリーを作成
			for (int i = treenodes.Count - 1; i >= 0; i--)
			{
				TreeNode node = treenodes[i];

				for (int j = node.Nodes.Count - 1; j >= 0; j--)
				{
					TreeNode childnode = node.Nodes[j];
					var treenodetag = (TreeNodeAIUEOTag)childnode.Tag;
					if (treenodetag.Gyou.名前 != "非表示" && treenodetag.CardDB.CardList.Count > 50)    //50しかなかったらツリーを作成しない
					{
						await RecursiveAddTreeNodeAIUEO(childnode.Nodes, treenodetag.CardDB, Depth + 1);

						//１つしか子を持っていない場合、自分を削除して親は孫を子にする
						NodeUp(childnode,node.Nodes);                
					}
				}

				NodeUp(node,treenodes);
			}

		}

		//Nodeが１つしか子を持っていない場合、自分を削除してnodeの子を親の子にする
		public static void NodeUp(TreeNode node, TreeNodeCollection ParentNodeCollection)   //一番上のノードは親が存在せず、TreeNodeCollectionが元締めなので親はCollectionで取得
		{
			if (node.Nodes.Count != 1) return;

			int num = ParentNodeCollection.IndexOf(node);   //そのままaddすると順番が狂うので番号を控えて、追加のたびに増やす
			foreach (TreeNode n in node.Nodes)
            {
				ParentNodeCollection.Insert(num,(TreeNode)n.Clone());
				num++;
			}


			node.Remove();
		}

		//右側は左側で何をクリックしたかで変わる
		public static void SetFormPanelRight(TreeNode treenode, Form1 form)
		{
			form.splitContainer1.Panel2.Controls.Clear();

			if (form.あいうえお順ToolStripMenuItem.CheckState == CheckState.Indeterminate)
			{

			}
			else if (form.パック順ToolStripMenuItem.CheckState == CheckState.Indeterminate)
			{

			}

			PackUI packUI = new PackUI(treenode, form);
			form.splitContainer1.Panel2.Controls.Add(packUI);

		}

		public static void ShowHome(Form1 form)
        {
			form.splitContainer1.Panel2.Controls.Clear();
			//ホーム画面
			CollectDataUI homeUI = new CollectDataUI(form.CardDB);
			form.splitContainer1.Panel2.Controls.Add(homeUI);
		}
	}
}
