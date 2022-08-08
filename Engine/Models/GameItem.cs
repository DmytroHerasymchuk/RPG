using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameItem
    {
        public int ItemTypeId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Rarity { get; set; }

        public GameItem(int itemTypeId, string name, int price, string rarity)
        {
            ItemTypeId = itemTypeId;
            Name = name;
            Price = price;
            Rarity = rarity;
        }

        public GameItem Clone()
        {
            return new GameItem(ItemTypeId, Name, Price, Rarity);
        }
    }
}
