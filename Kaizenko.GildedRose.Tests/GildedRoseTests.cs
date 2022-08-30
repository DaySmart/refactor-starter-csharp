using NUnit.Framework;
using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.TestFrameworks;

namespace Kaizenko.GildedRose.Tests
{
    public class GildedRoseTests
    {
        
        public void MyFirstTest()
        {
            Assert.Fail("Not yet implemented");
        }

        [Test]
        public void UpdateQuality_WhenEndOfDay_QualityDegradesBy1 ()
        {
            Item item = new Item("Regular Item", 30, 25);
            IList<Item> items = new List<Item>();
            items.Add(item);
            GildedRose inn = new GildedRose(items);
            inn.UpdateQuality();
            Assert.That(items[0].Quality, Is.EqualTo(24));
            Assert.That(items[0].SellIn, Is.EqualTo(29));
        }

		[UseReporter(typeof(NUnitReporter))]
		[Test]
		public void UpdateQuality_ApprovalTests()
        {
			List<Item> items = createItems();
			GildedRose inn = new GildedRose(items);
			inn.UpdateQuality();
			string output = string.Join<Item>(Environment.NewLine, items.ToArray());
			Approvals.Verify(output);
		}		

		List<Item> createItems()
		{
			String[] itemNames = {
				"Aged Brie",
				"Backstage passes to a TAFKAL80ETC concert",
				"Sulfuras, Hand of Ragnaros",
				"Regular"
				};
           
			int minQuality = -2;
			int maxQuality = 52;
			int minSellIn = -2;
			int maxSellIn = 13;

			List<Item> itemList = new List<Item>();
			for (int i = 0; i < itemNames.Length; i++)
			{
				for (int sellIn = minSellIn; sellIn < maxSellIn; sellIn++)
				{
					for (int quality = minQuality; quality < maxQuality; quality++)
					{
						itemList.Add(new Item(itemNames[i], sellIn, quality));
					}
				}
			}
			return itemList;
		}
	}
}
