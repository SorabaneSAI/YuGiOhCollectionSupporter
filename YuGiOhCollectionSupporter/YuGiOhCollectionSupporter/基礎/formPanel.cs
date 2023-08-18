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


		public async static void SetFormPanelLeft(Form1 form, bool IsAiueo)
		{
			form.InvalidMenuItem();
			Program.WriteLog("ツリーノード作成中", LogLevel.必須項目);


			TreeView treeview = IsAiueo ? form.treeView1 : form.treeView2;


			treeview.Invoke(new Action(() =>
			{
				//ツリーをクリア
				treeview.Nodes.Clear();
			}));

			var nodes = treeview.Nodes;

			TreeView tmptreeview = new TreeView();  //TreeNodeに別スレッドが触らないためにこれを使用

			//チェック次第でソートする
			if (IsAiueo)
			{
				form.CardDB.SortAIUEO();

				await RecursiveAddTreeNodeAIUEO(tmptreeview.Nodes, form.CardDB, 0, "", form);
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
				treeview.Invoke(new Action(() =>
				{

					//面倒だけどTreeNodeCollectionのせいでこうするしかない！
					foreach (TreeNode node in tmptreeview.Nodes)
					{
						treeview.Nodes.Add((TreeNode)node.Clone()); //クローンしないとなぜか表示されない
					}
				}));

			}
			else
			{
				//typenameの種類を全部取得
				List<string> typenameList = new List<string>();

				foreach (var pack in form.PackDB.PackDataList)
				{
					foreach (var typename in typenameList)
					{
						if (pack.TypeName == typename)
						{
							goto nextloop;
						}
					}
					typenameList.Add(pack.TypeName);

				nextloop:;
				}

				//typenameのツリーを作成
				foreach (var typename in typenameList)
				{
					tmptreeview.Nodes.Add(typename);
				}
				tmptreeview.Nodes.Add("非表示");

				//typenameのツリーにPackGroupのツリーをくっつける
				foreach (var packgroup in form.PackGroupDataList)
				{
					if (packgroup.有効フラグ == false) continue;

					foreach (TreeNode node in tmptreeview.Nodes)
					{
						if (packgroup.親ノード名 == node.Text)
						{
							var n = node.Nodes.Add(packgroup.子ノード名);
							n.Tag = packgroup;
							break;
						}
					}
				}


				//そのツリーにパック名をくっつける
				foreach (var pack in form.PackDB.PackDataList)
				{
					foreach (TreeNode node in tmptreeview.Nodes)
					{
						if (node.Text == pack.TypeName)
						{
							foreach (TreeNode node2 in node.Nodes)
							{
								PackGroupData packgroupdata = node2.Tag as PackGroupData;
								if (packgroupdata == null) break;   //今追加したパックだとキャストできない
								if (pack.Name.Contains(packgroupdata.含まれる文字))
								{
									AddTreeNode(tmptreeview, node2, pack);
									goto next;
								}

							}
							AddTreeNode(tmptreeview, node, pack);
							break;
						}
					}
				next:;
				}

				treeview.Invoke(new Action(() =>
				{
					//面倒だけどTreeNodeCollectionのせいでこうするしかない！
					foreach (TreeNode node in tmptreeview.Nodes)
					{
						treeview.Nodes.Add((TreeNode)node.Clone()); //クローンしないとなぜか表示されない
					}
					treeview.TreeViewNodeSorter = new NodeSorter();
					treeview.Sort();
				}));

			}

			form.ValidMenuItem();
			Program.WriteLog("ツリーノード作成終了", LogLevel.必須項目);

		}

		static void AddTreeNode(TreeView treeview, TreeNode node, PackData pack)
		{
			var n = node.Nodes.Add(pack.Name);
			n.Tag = pack;
			n.Text += $"({pack.CardCount})";

		}

		//Depthは読み取る文字数であり再帰回数 
		public async static Task RecursiveAddTreeNodeAIUEO(TreeNodeCollection treenodes, CardDataBase carddb, int Depth, string ParentName, Form1 form)
		{
			//あ～んなどまでの全部のNodeをここにも保存
			List<TreeNode> AIUEONodeList = new List<TreeNode>();


			//あ行～その他までのnodeを追加
			foreach (var gyou in かな.行リスト)
			{
				var GyouNode = treenodes.Add($"{ParentName} 【{gyou.名前}】");
				GyouNode.Tag = new TreeNodeAIUEOTag(gyou);

				//各行のひらがなをノードとして行ノードに追加
				for (int i = 0; i < Program.getTextLength(gyou.文字); i++)
				{
					string one_text = Program.getTextElement(gyou.文字, i);
					var KanaNode = GyouNode.Nodes.Add(ParentName + one_text);
					KanaNode.Tag = new TreeNodeAIUEOTag(new 行(ParentName + one_text, one_text));  //１文字だけの行扱いだけど特に意味はない
					AIUEONodeList.Add(KanaNode);
				}
			}
			if (Depth == 0)
			{
				//最初だけ非表示ノードも追加
				TreeNode node = treenodes.Add($"非表示");
				node.Tag = new TreeNodeAIUEOTag(null);

				foreach (var card in carddb.CardList)
				{
					var twincarddata = form.getTwinCardData(card);
					if (twincarddata.get表示フラグ() == false)
					{
						((TreeNodeAIUEOTag)node.Tag).CardDB.CardList.Add(card);
					}
				}
			}



			//カードをツリーに登録する
			foreach (var card in carddb.CardList)
			{
				var twincarddata = form.getTwinCardData(card);
				if (twincarddata.get表示フラグ() == false) continue;
				//文字数をオーバーしたらスキップ
				if (Program.getTextLength(card.読み) <= Depth)
					continue;
				//最初の文字を取得
				string one_txt = Program.getTextElement(card.読み, Depth);

				foreach (var node in AIUEONodeList)
				{
					if (((TreeNodeAIUEOTag)(node.Tag)).Gyou.文字.Equals(one_txt))
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
					if (node.Text.Contains("その他"))
					{
						((TreeNodeAIUEOTag)node.Tag).CardDB.CardList.Add(card);
						break;
					}
				}

			nextloop:;
			}

			//要素がないツリーを削除
			for (int i = treenodes.Count - 1; i >= 0; i--)
			{
				TreeNode node = treenodes[i];

				for (int j = node.Nodes.Count - 1; j >= 0; j--)
				{
					TreeNode childnode = node.Nodes[j];
					if (((TreeNodeAIUEOTag)childnode.Tag).CardDB.CardList.Count == 0)
					{
						node.Nodes.RemoveAt(j);
					}
				}
				if (((TreeNodeAIUEOTag)node.Tag).CardDB.CardList.Count == 0)
				{
					treenodes.RemoveAt(i);

				}
			}

			//カード枚数を表示
			foreach (TreeNode node in treenodes)
			{
				var treenodetag = (TreeNodeAIUEOTag)node.Tag;
				int num = treenodetag.CardDB.CardList.Count;
				node.Text += $" ({num})";

				foreach (TreeNode childnode in node.Nodes)
				{
					var childnodetag = (TreeNodeAIUEOTag)childnode.Tag;
					int childnum = childnodetag.CardDB.CardList.Count;
					childnode.Text += $" ({childnum})";
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
						await RecursiveAddTreeNodeAIUEO(childnode.Nodes, treenodetag.CardDB, Depth + 1, ((TreeNodeAIUEOTag)childnode.Tag).Gyou.名前, form);

						//１つしか子を持っていない場合、自分を削除して親は孫を子にする
						NodeUp(childnode, node.Nodes);
					}
				}

				NodeUp(node, treenodes);
			}

		}

		//Nodeが１つしか子を持っていない場合、自分を削除してnodeの子を親の子にする
		public static void NodeUp(TreeNode node, TreeNodeCollection ParentNodeCollection)   //一番上のノードは親が存在せず、TreeNodeCollectionが元締めなので親はCollectionで取得
		{
			if (node.Nodes.Count != 1) return;

			int num = ParentNodeCollection.IndexOf(node);   //そのままaddすると順番が狂うので番号を控えて、追加のたびに増やす
			foreach (TreeNode n in node.Nodes)
			{
				ParentNodeCollection.Insert(num, (TreeNode)n.Clone());
				num++;
			}


			node.Remove();
		}

		//右側は左側で何をクリックしたかで変わる
		public static void SetFormPanelRight(TreeNode treenode, Form1 form, int num)
		{
			if (treenode.Tag == null) return;
			form.splitContainer1.Panel2.Controls.Clear();

			CardDataBase cardDB;
			PackData pack;

			if (num == 1)
			{
				cardDB = ((TreeNodeAIUEOTag)treenode.Tag).CardDB;
				pack = null;
			}
			else
			{
				pack = treenode.Tag as PackData;
				if (pack == null) return;   //PackGroupDataの可能性あり

				cardDB = new CardDataBase();    //新しく作成してはいるが改造されていないCardDataBase

				cardDB.CardList = form.CardDB.getPackCardList(pack);

			}

			CardListUI packUI = new CardListUI(cardDB, pack, form, num == 1);
			form.splitContainer1.Panel2.Controls.Add(packUI);

		}

		public static void ShowHome(Form1 form)
		{
			form.splitContainer1.Panel2.Controls.Clear();
			//ホーム画面

			//FlowLayoutPanelに埋め込む（そのままだとうまくいかなかった）
			FlowLayoutPanel panel = new FlowLayoutPanel();
			panel.FlowDirection = FlowDirection.TopDown;
			panel.Dock = DockStyle.Fill;
			form.splitContainer1.Panel2.Controls.Add(panel);

			CollectDataUI homeUI = new CollectDataUI(form.getAllCardNumHave(form.CardDB), form.CardDB.getAllCardNum(), form.getCardHaveNumCode(form.CardDB).Item1,
				form.getCardHaveNumCode(form.CardDB).Item2, form.getCardHaveNumRarity(form.CardDB).Item1, form.getCardHaveNumRarity(form.CardDB).Item2);
			panel.Controls.Add(homeUI);

			ComboBox comboBox = new ComboBox();
			comboBox.Items.Add("カード名別で");
			comboBox.Items.Add("略号別で");
			comboBox.Items.Add("レアリティ別で");
			comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox.SelectedIndex = 0;
			panel.Controls.Add(comboBox);

			Button button = new Button();
			button.Text = "持ってないカードをPDF出力";
			button.Size = new System.Drawing.Size(200, 30);
			button.Click += form.button_Click;  //変数の都合でformにイベントあり
			button.BringToFront();
			panel.Controls.Add(button);


			button.Tag = comboBox;
		}


		public static void SearchCardNameButton(Form1 form, string txt)
		{
			var treeview = form.treeView1;
			treeview.Nodes.Clear();
			var nodes = treeview.Nodes;

			var 完全一致List = new List<CardData>();
			var 部分一致List = new List<CardData>();
			var テキスト一致List = new List<CardData>();

			var cardlist = form.CardDB.CardList;
			foreach (var card in cardlist)
			{
				if(card.名前.Equals(txt))
				{
					完全一致List.Add(card);
				}
				else if(card.名前.Contains(txt))
				{
					部分一致List.Add(card);
				}
				else if(card.テキスト.Contains(txt) || card.ペンデュラム効果.Contains(txt))
				{
					テキスト一致List.Add(card);
				}

			}

			//それぞれツリーに追加

			var node2 = nodes.Add($"完全一致({完全一致List.Count})");
			node2.Tag = new TreeNodeAIUEOTag(new 行("あ", "あ"));
			var cardlist2 = ((TreeNodeAIUEOTag)node2.Tag).CardDB.CardList;

			foreach (var card in 完全一致List)
			{
				cardlist2.Add(card);
			}

			var node3 = nodes.Add($"部分一致({部分一致List.Count})");
			node3.Tag = new TreeNodeAIUEOTag(new 行("あ", "あ"));
			var cardlist3 = ((TreeNodeAIUEOTag)node3.Tag).CardDB.CardList;

			foreach (var card in 部分一致List)
			{
				cardlist3.Add(card);
			}

			var node4 = nodes.Add($"テキスト一致({テキスト一致List.Count})");
			node4.Tag = new TreeNodeAIUEOTag(new 行("あ", "あ"));
			var cardlist4 = ((TreeNodeAIUEOTag)node4.Tag).CardDB.CardList;

			foreach (var card in テキスト一致List)
			{
				cardlist4.Add(card);
			}

		}
	}
}
