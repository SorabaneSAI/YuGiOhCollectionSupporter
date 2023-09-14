using Microsoft.VisualStudio.TestTools.UnitTesting;
using YuGiOhCollectionSupporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhCollectionSupporter.Tests
{
	[TestClass()]
	public class MakePairTests
	{
		[TestMethod()]
		public void MakeDictionary2Test()
		{
			
			Assert.AreEqual(expected: true, actual: MakePair.IsSameName("神風のバリア －エア・フォース－", "神風のバリア-エア・フォース-"));
		}
	}
}