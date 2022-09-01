using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Kaizenko.GildedRose.Tests
{
    public class GildedRoseTests
    {
        private GildedRose gildedRose;
        private List<Item> items;
        private const string genericItemName = "GenericItem";

        [SetUp]
        public void SetUp()
        {
            items = new List<Item>();
            string[] itemNames = { genericItemName, GildedRose.Constants.AGED_BRIE, GildedRose.Constants.BACKSTAGE_PASSES, GildedRose.Constants.SULFURAS, GildedRose.Constants.CONJURED };
            int minQuality = -2;
            int maxQuality = 52;
            int minSellInDate = -2;
            int maxSellInDate = 13;

            foreach (string itemName in itemNames)
                for (int quality = minQuality; quality <= maxQuality; quality++)
                    for (int sellIn = minSellInDate; sellIn <= maxSellInDate; sellIn++)
                        items.Add(new Item(itemName, sellIn, quality));


            gildedRose = new GildedRose(items);
        }

        [Test]
        public void UpdateQuality_WhenRan_ExpectSellByDateToDecreaseBy1()
        {
            // Arrange
            Item item = items.FirstOrDefault(i => i.Name == genericItemName && i.SellIn == 8);
            int currentSellIn = item.SellIn;
            // Action
            gildedRose.UpdateQuality();
            // Assert
            Assert.AreEqual(currentSellIn - 1, item.SellIn);
        }

        [Test]
        public void UpdateQuality_WhenSellByDatePassed_ExpectQualityDegrades2x()
        {
            Item item = items.FirstOrDefault(i => i.Name == genericItemName && i.Quality == 4 && i.SellIn == 0);
            int currentQuality = item.Quality;
            gildedRose.UpdateQuality();
            Assert.AreEqual(currentQuality - 2, item.Quality);
        }

        [Test]
        public void UpdateQuality_WhenSellByDateNotPassed_ExpectQualityDegrades1x()
        {
            Item item = items.FirstOrDefault(i => i.Name == genericItemName && i.Quality == 4 && i.SellIn == 1);
            int currentQuality = item.Quality;
            gildedRose.UpdateQuality();
            Assert.AreEqual(currentQuality - 1, item.Quality);
        }

        [Test]
        public void UpdateQuality_WhenQualityIs0_ExpectQualityToNotBeNegative()
        {
            Item item = items.FirstOrDefault(i => i.Name == genericItemName && i.Quality == 0);
            gildedRose.UpdateQuality();
            Assert.AreEqual(0, item.Quality);
        }

        [Test]
        public void UpdateQuality_WhenItemIsAgedBrieAndQualityIsLessThan50AndSellByDateNotPassed_ExpectQualityToIncreaseBy1()
        {
            Item item = items.FirstOrDefault(i => i.Name == GildedRose.Constants.AGED_BRIE && i.Quality == 25 && i.SellIn == 9);
            int currentQuality = item.Quality;
            gildedRose.UpdateQuality();
            Assert.AreEqual(currentQuality + 1, item.Quality);
        }

        [Test]
        public void UpdateQuality_WhenItemIsAgedBrieAndQualityIsLessThan50AndSellByDatePassed_ExpectQualityToIncreaseBy2()
        {
            Item item = items.FirstOrDefault(i => i.Name == GildedRose.Constants.AGED_BRIE && i.Quality == 25 && i.SellIn == 0);
            int currentQuality = item.Quality;
            gildedRose.UpdateQuality();
            Assert.AreEqual(currentQuality + 2, item.Quality);
        }

        [Test]
        public void UpdateQuality_WhenItemIsAgedBrieAndQualityIs50_ExpectQualityToStay50()
        {
            Item item = items.FirstOrDefault(i => i.Name == GildedRose.Constants.AGED_BRIE && i.Quality == 50);
            gildedRose.UpdateQuality();
            Assert.AreEqual(50, item.Quality);
        }

        [Test]
        public void UpdateQuality_WhenItemIsSulfuras_ExpectQualityAndSellInToStaySame()
        {
            Item item = items.FirstOrDefault(i => i.Name == GildedRose.Constants.SULFURAS);
            int currentSellIn = item.SellIn;
            int currentQuality = item.Quality;
            gildedRose.UpdateQuality();
            Assert.AreEqual(currentQuality, item.Quality);
            Assert.AreEqual(currentSellIn, item.SellIn);
        }

        [Test]
        public void UpdateQuality_WhenItemIsBackstagePassAndSellByDateMoreThan10Days_ExpectQualityToIncreaseBy1()
        {
            Item item = items.FirstOrDefault(i => i.Name == GildedRose.Constants.BACKSTAGE_PASSES && i.Quality == 10 && i.SellIn == 12);
            int currentQuality = item.Quality;
            gildedRose.UpdateQuality();
            Assert.AreEqual(currentQuality + 1, item.Quality);
        }

        [Test]
        public void UpdateQuality_WhenItemIsBackstagePassAndSellByDate10DaysAway_ExpectQualityToIncreaseBy2()
        {
            Item item = items.FirstOrDefault(i => i.Name == GildedRose.Constants.BACKSTAGE_PASSES && i.Quality == 10 && i.SellIn == 10);
            int currentQuality = item.Quality;
            gildedRose.UpdateQuality();
            Assert.AreEqual(currentQuality + 2, item.Quality);
        }

        [Test]
        public void UpdateQuality_WhenItemIsBackstagePassAndSellByDateLessThan10MoreThan5DaysAway_ExpectQualityToIncreaseBy2()
        {
            Item item = items.FirstOrDefault(i => i.Name == GildedRose.Constants.BACKSTAGE_PASSES && i.Quality == 10 && i.SellIn == 7);
            int currentQuality = item.Quality;
            gildedRose.UpdateQuality();
            Assert.AreEqual(currentQuality + 2, item.Quality);
        }

        [Test]
        public void UpdateQuality_WhenItemIsBackstagePassAndSellByDate5DaysAway_ExpectQualityToIncreaseBy3()
        {
            Item item = items.FirstOrDefault(i => i.Name == GildedRose.Constants.BACKSTAGE_PASSES && i.Quality == 10 && i.SellIn == 5);
            int currentQuality = item.Quality;
            gildedRose.UpdateQuality();
            Assert.AreEqual(currentQuality + 3, item.Quality);
        }

        [Test]
        public void UpdateQuality_WhenItemIsBackstagePassAndSellByDateLessThan5MoreThan0DaysAway_ExpectQualityToIncreaseBy3()
        {
            Item item = items.FirstOrDefault(i => i.Name == GildedRose.Constants.BACKSTAGE_PASSES && i.Quality == 10 && i.SellIn == 3);
            int currentQuality = item.Quality;
            gildedRose.UpdateQuality();
            Assert.AreEqual(currentQuality + 3, item.Quality);
        }

        [Test]
        public void UpdateQuality_WhenItemIsBackstagePassAndSellByDatePassed_ExpectQualityToBe0()
        {
            Item item = items.FirstOrDefault(i => i.Name == GildedRose.Constants.BACKSTAGE_PASSES && i.Quality == 10 && i.SellIn == 0);
            gildedRose.UpdateQuality();
            Assert.AreEqual(0, item.Quality);
        }

        [Test]
        public void UpdateQuality_WhenItemIsBackstagePassAndQualityIs50_ExpectQualityToStay50()
        {
            Item item = items.FirstOrDefault(i => i.Name == GildedRose.Constants.BACKSTAGE_PASSES && i.Quality == 50 && i.SellIn == 3);
            gildedRose.UpdateQuality();
            Assert.AreEqual(50, item.Quality);
        }

        public void UpdateQuality_WhenItemIsConjured_ExpectQualityToDecreaseBy2()
        {
            Item item = items.FirstOrDefault(i => i.Name == GildedRose.Constants.CONJURED && i.Quality == 50 && i.SellIn == 6);
            int currentQuality = item.Quality;
            gildedRose.UpdateQuality();
            Assert.AreEqual(currentQuality - 2, item.Quality);
        }

        public void UpdateQuality_ApprovalTests()
        {
            gildedRose.UpdateQuality();
            string output = string.Join<string>('\n', items.ConvertAll(i => i.ToString()).ToArray());
            ApprovalTests.Approvals.Verify(output);
        }
    }


}
