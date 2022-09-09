using Engine.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Potion : GameItem
    {
        public enum PotionCategory
        {
            HealingPotion,
            PoisonPotion,
            SimplePotion
        }

        public PotionCategory PotionType { get; }
        public Potion(PotionCategory potionCategory,ItemCategory category, int itemTypeId, string name, int price, string rarity, bool isUnique = false, IAction action = null) : 
            base(category, itemTypeId, name, price, rarity, isUnique, action)
        {
            PotionType = potionCategory;
        }

        
    }
}
