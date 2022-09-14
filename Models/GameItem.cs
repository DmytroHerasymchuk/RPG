using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Actions;
using Newtonsoft.Json;
using Models.Shared;

namespace Models
{
    public class GameItem
    {
        public enum ItemCategory
        {
            Miscellaneous,
            Weapon,
            Consumable
        }
        [JsonIgnore]
        public ItemCategory Category { get; }
        public int ItemTypeId { get; }
        [JsonIgnore]
        public string Name { get; }
        [JsonIgnore]
        public int Price { get; }
        [JsonIgnore]
        public int ModifiedPrice { get; set; }
        [JsonIgnore]
        public string Rarity { get; }
        [JsonIgnore]
        public bool IsUnique { get; }
        [JsonIgnore]
        public IAction Action { get; set; }

        public GameItem(ItemCategory category, int itemTypeId, string name, int price, int modifiedPrice, string rarity,
            bool isUnique = false, IAction action = null)
        {
            Category = category;
            ItemTypeId = itemTypeId;
            Name = name;
            Price = price;
            ModifiedPrice = modifiedPrice;
            Rarity = rarity;
            IsUnique = isUnique;
            Action = action;           
        }

        public void PerformAction(LivingEntity actor, LivingEntity target)
        {
            Action?.Execute(actor, target);
        }
        public GameItem Clone()
        {
            return new GameItem(Category, ItemTypeId, Name, Price, ModifiedPrice, Rarity, 
                                IsUnique, Action);
        }
    }
}
