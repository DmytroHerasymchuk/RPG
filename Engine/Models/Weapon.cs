using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Weapon : GameItem
    {
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }

        public Weapon(int itemTypeId, string name, int price, string rarity, int minDamage, int maxDamage) : base(itemTypeId, name, price, rarity, true){
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }

        public new Weapon Clone()
        {
            return new Weapon(ItemTypeId, Name, Price, Rarity, MinDamage, MaxDamage);
        }
    }
}
