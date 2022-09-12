using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using System.IO;
using System.Xml;
using Engine.Shared;

namespace Engine.Factories
{
    public static class QuestFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Quests.xml";
        private static readonly List<Quest> _quests = new List<Quest>();

        static QuestFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                LoadQuestsFromNodes(data.SelectNodes("/Quests/Quest"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        private static void LoadQuestsFromNodes(XmlNodeList nodes)
        {
            foreach(XmlNode node in nodes)
            {
                List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
                List<ItemQuantity> rewardItems = new List<ItemQuantity>();
                List<Recipe> recipes = new List<Recipe>();
                foreach (XmlNode childNode in node.SelectNodes("./ItemsToComplete/Item"))
                {
                    GameItem item = GameItemFactory.CreateGameItem(childNode.AttributeAsInt("ID"));
                    itemsToComplete.Add(new ItemQuantity(item, childNode.AttributeAsInt("Quantity")));
                }
                foreach (XmlNode childNode in node.SelectNodes("./RewardItems/Item"))
                {
                    GameItem item = GameItemFactory.CreateGameItem(childNode.AttributeAsInt("ID"));
                    rewardItems.Add(new ItemQuantity(item, childNode.AttributeAsInt("Quantity")));
                }
                foreach (XmlNode childNode in node.SelectNodes("./RewardRecipes/Recipe"))
                {
                    recipes.Add(RecipeFactory.RecipeById(childNode.AttributeAsInt("ID")));
                }
                Quest quest = new Quest(node.AttributeAsInt("ID"),
                                        node.SelectSingleNode("./Name")?.InnerText ?? "",
                                        node.SelectSingleNode("./Description")?.InnerText ?? "",
                                        itemsToComplete,
                                        node.AttributeAsInt("RewardXP"),
                                        node.AttributeAsInt("RewardGold"),
                                        rewardItems, 
                                        recipes);
                _quests.Add(quest);

            }
        }

        //private static void AddRecipes(Quest quest, XmlNodeList recipes)
        //{
        //    if (recipes == null)
        //    {
        //        return;
        //    }
        //    foreach (XmlNode recipeNode in recipes)
        //    {
        //        quest.RewardRecipes.Add(RecipeFactory.RecipeById(recipeNode.AttributeAsInt("ID")));                                   
        //    }
        //}
        internal static Quest GetQuestById(int id)
        {
            return _quests.FirstOrDefault(quest => quest.Id == id);
        }
    }
}
