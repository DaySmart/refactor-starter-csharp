using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Kaizenko.GildedRose.Tests
{
    public class GildedRoseTests
    {
        [Test]
        public void UpdateQualityWhenConjuredItemQuality25_ExpectsQuailityTo23()
        {
            var gildedRose = new GildedRose(new List<Item>() {
                new Item("Conjured", 50, 25)
            });

            gildedRose.UpdateQuality();

            Assert.That(gildedRose.Items[0].Quality, Is.EqualTo(23));
        }

        [Test]
        public void UpdateQualtiyWhenConjuredItemQuality1_ExpectsQuality0()
        {

            var gildedRose = new GildedRose(new List<Item>() {
                new Item("Conjured", 50, 1)
            });

            gildedRose.UpdateQuality();

            Assert.That(gildedRose.Items[0].Quality, Is.EqualTo(0));
        }

        [Test]
        public void UpdateQualityWhenSellInIsZeroAndQuality25_ExpectsQuality21()
        {

            var gildedRose = new GildedRose(new List<Item>() {
                new Item("Conjured", 0, 25)
            });

            gildedRose.UpdateQuality();

            Assert.That(gildedRose.Items[0].Quality, Is.EqualTo(21));
        }


        [Test]
        public void UpdateQualityWhenNormalItemSellin50Quality25_ExpectsQuality24()
        {

            var gildedRose = new GildedRose(new List<Item>() {
                new Item("Normal Item", 50, 25)
            });

            gildedRose.UpdateQuality();

            Assert.That(gildedRose.Items[0].Quality, Is.EqualTo(24));
        }

        [Test]
        public void UpdateQualityWhenNormalItemSellin0Quality25_ExpectsQuality23()
        {

            var gildedRose = new GildedRose(new List<Item>() {
                new Item("Normal Item", 0, 25)
            });

            gildedRose.UpdateQuality();

            Assert.That(gildedRose.Items[0].Quality, Is.EqualTo(23));
        }


        [Test]
        public void UpdateQualityWhenNormalItemSellin50Quality0_ExpectsQuality0()
        {

            var gildedRose = new GildedRose(new List<Item>() {
                new Item("Normal Item", 50, 0)
            });

            gildedRose.UpdateQuality();

            Assert.That(gildedRose.Items[0].Quality, Is.EqualTo(0));
        }
    }

        
}
