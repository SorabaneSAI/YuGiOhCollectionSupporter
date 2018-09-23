using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter
{
	public class NonDispBrowser : WebBrowser
	{

		bool done;

		// タイムアウト時間（10秒）
		TimeSpan timeout = new TimeSpan(0, 0, 10);

		protected override void OnDocumentCompleted(
					  WebBrowserDocumentCompletedEventArgs e)
		{
			// ページにフレームが含まれる場合にはフレームごとに
			// このメソッドが実行されるため実際のURLを確認する
			if (e.Url == this.Url)
			{
				done = true;
			}
		}

		protected override void OnNewWindow(CancelEventArgs e)
		{
			// ポップアップ・ウィンドウをキャンセル
			e.Cancel = true;
		}

		public NonDispBrowser()
		{
			// スクリプト・エラーを表示しない
			this.ScriptErrorsSuppressed = true;
		}

		public bool NavigateAndWait(string url)
		{

			base.Navigate(url); // ページの移動

			done = false;
			DateTime start = DateTime.Now;

			while (done == false)
			{
				if (DateTime.Now - start > timeout)
				{
					// タイムアウト
					return false;
				}
				Application.DoEvents();
			}
			return true;
		}
	}
}
