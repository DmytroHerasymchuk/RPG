using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Actions;
using Engine.Shared;

namespace Engine.Factories
{
    public static class GameItemFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\GameItems.xml";
        private static readonly List<GameItem> _standartGameItem = new List<GameItem>();
        
        static GameItemFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/Weapons/Weapon"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/Potions/HealingPotions/HealingPotion"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/Potions/PoisonPotions/PoisonPotion"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/Miscellaneous/MiscellaneousItem"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
            
        }

        public static GameItem CreateGameItem(int itemTypeId)
        {
            return _standartGameItem.FirstOrDefault(item => item.ItemTypeId == itemTypeId)?.Clone(); 
        }
        public static string GetItemName(int itemId)
        {
            return _standartGameItem.FirstOrDefault(i => i.ItemTypeId == itemId)?.Name ?? "";
        }

        private static void LoadItemsFromNodes(XmlNodeList nodes)
        {
            if (nodes == null)
            {
                return;
            }
            foreach(XmlNode node in nodes)
            {
                GameItem.ItemCategory itemCategory = DetermineItemCategory(node.Name);
                GameItem gameItem =
                    new GameItem(itemCategory,
                                 node.AttributeAsInt("ID"),
                                 node.AttributeAsString("Name"),
                                 node.AttributeAsInt("Price"),
                                 node.AttributeAsString("Rarity"),
                                 itemCategory == GameItem.ItemCategory.Weapon); ;
                if(itemCategory == GameItem.ItemCategory.Weapon)
                {
                    gameItem.Action =
                        new AttackWithWeapon(gameItem,
                                             node.AttributeAsInt("MinDamage"),
                                             node.AttributeAsInt("MaxDamage"));
                }
                if(itemCategory == GameItem.ItemCategory.Potion)
                {
                    Potion.PotionCategory potionCategory = DeterminePotionCategory(node.Name);
                    if (potionCategory == Potion.PotionCategory.PoisonPotion)
                    {
                        gameItem.Action =
                        new Poison(gameItem,
                                   node.AttributeAsInt("DamageHitPoints"));
                    }
                    if (potionCategory == Potion.PotionCategory.HealingPotion)
                    {
                        gameItem.Action =
                        new Heal(gameItem,
                                 node.AttributeAsInt("HitPointsToHeal"));
                    }                  
                }
                _standartGameItem.Add(gameItem);
            }
        }

        private static GameItem.ItemCategory DetermineItemCategory(string itemType)
        {
            switch (itemType)
            {
                case "Weapon":
                    return GameItem.ItemCategory.Weapon;
                case "HealingPotion":
                    return GameItem.ItemCategory.Potion;
                case "PoisonPotion":
                    return GameItem.ItemCategory.Potion;
                default:
                    return GameItem.ItemCategory.Miscellaneous;
            }
        }

        private static Potion.PotionCategory DeterminePotionCategory(string itemType)
        {
            switch (itemType)
            {
                case "HealingPotion":
                    return Potion.PotionCategory.HealingPotion;
                case "PoisonPotion":
                    return Potion.PotionCategory.PoisonPotion;
                default:
                    return Potion.PotionCategory.SimplePotion;
            }
        }

    }
}
    