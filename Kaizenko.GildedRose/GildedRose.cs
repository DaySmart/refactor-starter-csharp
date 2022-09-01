using System.Collections.Generic;

namespace Kaizenko.GildedRose
{
    public class GildedRose
    {
        public static class Constants
        {
            public const string AGED_BRIE = "Aged Brie";
            public const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
            public const string SULFURAS = "Sulfuras, Hand of Ragnaros";
            public const string CONJURED = "Conjured";
            public const int MIN_QUALITY = 0;
            public const int MAX_QUALITY = 50;
        }
        readonly IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                Item item = Items[i];
                if (item.Name == Constants.AGED_BRIE)
                {
                    IncrementQuality(item);
                    if (--item.SellIn < 0)
                        IncrementQuality(item);
                }
                else if (item.Name == Constants.BACKSTAGE_PASSES)
                {
                    IncrementQuality(item);
                    if (item.SellIn < 11)
                    {
                        IncrementQuality(item);
                    }
                    if (item.SellIn < 6)
                    {
                        IncrementQuality(item);
                    }
                    if (--item.SellIn < 0)
                        item.Quality = Constants.MIN_QUALITY;
                }
                else if (item.Name != Constants.SULFURAS)
                {
                    DecrementQuality(item);
                    if (--item.SellIn < 0)
                        DecrementQuality(item);
                }
            }
        }

        private void DecrementQuality(Item item)
        {
            if (item.Quality > Constants.MIN_QUALITY)
                item.Quality--;
        }

        private void IncrementQuality(Item item)
        {
            if (item.Quality < Constants.MAX_QUALITY)
                item.Quality++;

        }
    }
}

