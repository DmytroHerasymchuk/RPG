using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Actions;
using Models.Shared;

namespace Services.Factories
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
                LoadItemsFromNodes(data.SelectNodes("/GameItems/RecipeLists/RecipeList"));
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
                                 node.AttributeAsInt("ModifiedPrice"),
                                 node.AttributeAsString("Rarity"),
                                 itemCategory == GameItem.ItemCategory.Weapon); ;
                if(itemCategory == GameItem.ItemCategory.Weapon)
                {
                    gameItem.Action =
                        new AttackWithWeapon(gameItem,
                                             node.AttributeAsInt("MinDamage"),
                                             node.AttributeAsInt("MaxDamage"));
                }
                
                if(itemCategory == GameItem.ItemCategory.Consumable)
                {
                    Consumable.ConsumableCategory consumableCategory = DeterminePotionCategory(node.Name);
                    if (consumableCategory == Consumable.ConsumableCategory.PoisonPotion)
                    {
                        gameItem.Action =
                        new Poison(gameItem,
                                   node.AttributeAsInt("DamageHitPoints"));
                        
                    }
                    if (consumableCategory == Consumable.ConsumableCategory.HealingPotion)
                    {
                        gameItem.Action =
                        new Heal(gameItem,
                                 node.AttributeAsInt("HitPointsToHeal"));
                    }
                    if (consumableCategory == Consumable.ConsumableCategory.RecipeList)
                    {
                        gameItem.Action =
                            new LearnRecipe(gameItem,
                                            RecipeFactory.RecipeById(node.AttributeAsInt("RecipeID")));
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
                    return GameItem.ItemCategory.Consumable;
                case "PoisonPotion":
                    return GameItem.ItemCategory.Consumable;
                case "RecipeList":
                    return GameItem.ItemCategory.Consumable;
                default:
                    return GameItem.ItemCategory.Miscellaneous;
            }
        }

        private static Consumable.ConsumableCategory DeterminePotionCategory(string itemType)
        {
            switch (itemType)
            {
                case "HealingPotion":
                    return Consumable.ConsumableCategory.HealingPotion;
                case "PoisonPotion":
                    return Consumable.ConsumableCategory.PoisonPotion;
                case "RecipeList":
                    return Consumable.ConsumableCategory.RecipeList;
                default:
                    return Consumable.ConsumableCategory.SimplePotion;
            }
        }

    }
}
    