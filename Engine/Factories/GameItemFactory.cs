using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Actions;

namespace Engine.Factories
{
    public static class GameItemFactory
    {
        private static readonly List<GameItem> _standartGameItem = new List<GameItem>();
        
        static GameItemFactory()
        {
            BuildWeapon(10001, "Sword", 10, "Common", 3, 5);
            BuildWeapon(10002, "Katana", 50, "Rare", 10, 15);
            BuildWeapon(10003, "Stick", 1, "Common", 1, 2);
            BuildWeapon(10004, "Troll Club", 30, "Uncommon", 7, 9);
            BuildMiscellaneous(20001, "Troll Tooth", 2, "Common");         
        }

        public static GameItem CreateGameItem(int itemTypeId)
        {
            return _standartGameItem.FirstOrDefault(item => item.ItemTypeId == itemTypeId)?.Clone(); 
        }
        private static void BuildMiscellaneous(int id, string name, int price, string rarity)
        {
            _standartGameItem.Add(new GameItem(GameItem.ItemCategory.Miscellaneous, id, name, price, rarity));
        }
        private static void BuildWeapon(int id, string name, int price, string rarity, int minDamage, int maxDamage)
        {
            GameItem weapon = new GameItem(GameItem.ItemCategory.Weapon, id, name, price, rarity, true);
            weapon.Action = new AttackWithWeapon(weapon, minDamage, maxDamage);
            _standartGameItem.Add(weapon);
        }
    }
}
    