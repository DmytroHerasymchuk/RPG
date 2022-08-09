using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameItem
    {
        public enum ItemCategory
        {
            Miscellaneous,
            Weapon
        }
        public ItemCategory Category { get; }
        public int ItemTypeId { get; }
        public string Name { get; }
        public int Price { get; }
        public string Rarity { get; }
        public bool IsUnique { get; }
        public int MinDamage { get; }
        public int MaxDamage { get; }

        public GameItem(ItemCategory category,int itemTypeId, string name, int price, string rarity,
            bool isUnique = false, int minDamage = 0, int maxDamage = 0)
        {
            Category = category;
            ItemTypeId = itemTypeId;
            Name = name;
            Price = price;
            Rarity = rarity;
            IsUnique = isUnique;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }

        public GameItem Clone()
        {
            return new GameItem(Category, ItemTypeId, Name, Price, Rarity, 
                                IsUnique, MinDamage, MaxDamage);
        }
    }
}
