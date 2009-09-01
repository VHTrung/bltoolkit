﻿using System;
using System.Linq;

using NUnit.Framework;

using BLToolkit.Data.DataProvider;

namespace Data.Linq
{
	[TestFixture]
	public class TakeSkipTest : TestBase
	{
		static readonly string[] _exclude = new[] {ProviderName.SqlCe};

		[Test]
		public void Take1()
		{
			ForEachProvider(_exclude, db =>
			{
				var q = (from ch in db.Child select ch).Take(3);
				Assert.AreEqual(3, q.ToList().Count);
			});
		}

		void TakeParam(TestDbManager db, int n)
		{
			var q = (from ch in db.Child select ch).Take(n);
			Assert.AreEqual(n, q.ToList().Count);
		}

		[Test]
		public void Take2()
		{
			ForEachProvider(_exclude, db => TakeParam(db, 1));
		}

		[Test]
		public void Take3()
		{
			ForEachProvider(_exclude, db =>
			{
				var q = (from ch in db.Child where ch.ChildID > 3 || ch.ChildID < 4 select ch).Take(3);
				Assert.AreEqual(3, q.ToList().Count);
			});
		}

		[Test]
		public void Take4()
		{
			ForEachProvider(_exclude, db =>
			{
				var q = (from ch in db.Child where ch.ChildID >= 0 && ch.ChildID <= 100 select ch).Take(3);
				Assert.AreEqual(3, q.ToList().Count);
			});
		}
	}
}