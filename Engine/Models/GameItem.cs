using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameItem
    {
        public int ItemTypeId { get; }
        public string Name { get; }
        public int Price { get; }
        public string Rarity { get; }

        public bool IsUnique { get; }

        public GameItem(int itemTypeId, string name, int price, string rarity, bool isUnique = false)
        {
            ItemTypeId = itemTypeId;
            Name = name;
            Price = price;
            Rarity = rarity;
            IsUnique = isUnique;
        }

        public GameItem Clone()
        {
            return new GameItem(ItemTypeId, Name, Price, Rarity, IsUnique);
        }
    }
}
