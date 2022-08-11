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
            // Player's weapons
            BuildWeapon(10001, "Broken sword", 1, "Common", 1, 3);
            BuildWeapon(10002, "Short sword", 15, "Common", 3, 5);
            BuildWeapon(10003, "Long sword", 17, "Common", 4, 7);
            BuildWeapon(10004, "Dagger", 15, "Common", 2, 6);
            BuildWeapon(10005, "Spear", 13, "Common", 3, 6);
            BuildWeapon(10006, "Greatsword", 23, "Common", 8, 10);
            BuildWeapon(10007, "Сudgel", 10, "Common", 2, 5);
            BuildWeapon(10008, "Bow", 10, "Common", 0, 7);
            BuildWeapon(10009, "Crossbow", 20, "Common", 0, 11);
            BuildWeapon(10010, "Staff", 10, "Common", 2, 4);

            // Different items
            BuildMiscellaneous(20001, "Troll Tooth", 2, "Common");
            BuildMiscellaneous(20002, "Stick", 1, "Common"); // палка
            BuildMiscellaneous(20003, "Thistle", 2, "Common"); // чертополох
            BuildMiscellaneous(20004, "Sagebrush", 3, "Common"); // полынь
            BuildMiscellaneous(20005, "Fly agaric", 2, "Common"); // мухомор 
            BuildMiscellaneous(20006, "Dandelion", 1, "Common"); // одуванчик
            BuildMiscellaneous(20007, "Сhamomile", 1, "Common"); // ромашка
            BuildMiscellaneous(20007, "Thyme", 1, "Common"); // чабрец
            BuildMiscellaneous(20008, "Juniper", 1, "Common"); // можжевельник
            BuildMiscellaneous(20009, "Rose hip", 1, "Common"); // шиповник
            BuildMiscellaneous(20010, "Ginseng", 1, "Common"); // женьшень
            BuildMiscellaneous(20011, "Nettle", 1, "Common"); // крапива
            BuildMiscellaneous(20012, "Firefly", 1, "Common"); // светлячок
            BuildMiscellaneous(20013, "Worm", 1, "Common"); // червь
            BuildMiscellaneous(20014, "Butterfly wing", 1, "Common"); // крыло бабочки
            BuildMiscellaneous(20015, "Hop", 1, "Common"); // хмель

            BuildMiscellaneous(21001, "Glowing Mushroom", 2, "Uncommon");

            // Monsters weapons
            BuildWeapon(30001, "Troll Club", 0, "Common", 7, 9);

            //Consumables
            BuildHealingItem(40001, "Small heal potion", 10, "Common", 10);
            BuildHealingItem(40002, "Heal potion", 20, "Uncommon", 30);
            BuildHealingItem(40003, "Big heal potion", 50, "Rare", 70);
            BuildHealingItem(40004, "Great heal potion", 100, "Epic", 150);
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

        private static void BuildHealingItem(int id, string name, int price, string rarity, int pointsToHeal)
        {
            GameItem item = new GameItem(GameItem.ItemCategory.Consumable, id, name, price, rarity);
            item.Action = new Heal(item, pointsToHeal);
            _standartGameItem.Add(item);
        }
    }
}
    