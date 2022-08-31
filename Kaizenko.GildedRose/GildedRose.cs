using System;
using System.Collections.Generic;

namespace Kaizenko.GildedRose
{
    public class GildedRose
    {
        public IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                // normal item, not special conditions
                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (Items[i].Quality > 0)
                    {
                        // when we are are a normal item, with larger than 0 quantity, reduce our quality
                        // by 1
                        this.UpdateItemQuality(Items[i]);
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    // we past the the sell by date now
                    if (Items[i].Name != "Aged Brie")
                    {
                        // We are either normal item, conjured, legendary item, or a backstage pass
                        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            // we either normal item, curjured, or legendary
                            if (Items[i].Quality > 0)
                            {
                                this.UpdateItemQuality(Items[i]);
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }

                // for all items we should never go below 0 in quality
                if (Items[i].Quality < 0 )
                {
                    Items[i].Quality = 0;
                }
            }
        }


        protected void UpdateItemQuality(Item item)
        {
            if (item.Name == "Conjured")
            {
                item.Quality = item.Quality - 2;
            }
            else if (item.Name != "Sulfuras, Hand of Ragnaros")
            {
                item.Quality = item.Quality - 1;
            }
        }
    }
}

