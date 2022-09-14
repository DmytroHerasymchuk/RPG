using Models.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Consumable : GameItem
    {
        public enum ConsumableCategory
        {
            HealingPotion,
            PoisonPotion,
            SimplePotion,
            RecipeList
        }

        public ConsumableCategory ConsumableType { get; }
        public Consumable(ConsumableCategory consumableCategory,ItemCategory category, int itemTypeId, string name, int price, int modifiedPrice, string rarity, bool isUnique = false, IAction action = null) : 
            base(category, itemTypeId, name, price, modifiedPrice, rarity, isUnique, action)
        {
            ConsumableType = consumableCategory;
        }

        
    }
}
