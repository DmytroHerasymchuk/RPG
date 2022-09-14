using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.IO;
using System.Xml;
using Models.Shared;
using Services;
using Core;

namespace Services.Factories
{
    public static class MonsterFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Monsters.xml";
        private static readonly GameDetails gameDetails;
        private static readonly List<Monster> s_baseMonster = new List<Monster>();

        static MonsterFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                gameDetails = GameDetailsService.ReadGameDetails();

                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                string rootImagePath =
                    data.SelectSingleNode("/Monsters").AttributeAsString("RootImagePath");
                LoadMonstersFromNodes(data.SelectNodes("./Monsters/Monster"), rootImagePath);
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        private static void LoadMonstersFromNodes(XmlNodeList nodes, string rootImagePath)
        {
            if (nodes == null)
            {
                return;
            }
            foreach (XmlNode node in nodes)
            {
                var attributes = gameDetails.PlayerAttributes;
                attributes.First(a => a.Key.Equals("DEX")).Value = Convert.ToInt32(node.SelectSingleNode("./Dexterity").InnerText);
                attributes.First(a => a.Key.Equals("CON")).Value = Convert.ToInt32(node.SelectSingleNode("./Constitution").InnerText);
                Monster monster = new Monster(
                    node.AttributeAsInt("ID"),
                    node.AttributeAsString("Name"),
                    $".{rootImagePath}{node.AttributeAsString("ImageName")}",
                    node.AttributeAsInt("MaxHitPoints"),
                    attributes,
                    GameItemFactory.CreateGameItem(node.AttributeAsInt("WeaponId")),
                    node.AttributeAsInt("RewardXP"),
                    node.AttributeAsInt("Gold")); ;
                XmlNodeList lootItemNodes = node.SelectNodes("./LootItems/LootItem");
                if (lootItemNodes != null)
                {
                    foreach (XmlNode lootItemNode in lootItemNodes)
                    {
                        monster.AddItemToLootTable(lootItemNode.AttributeAsInt("ID"),
                                                   lootItemNode.AttributeAsInt("Percentage"));
                    }
                }
                s_baseMonster.Add(monster);
            }
        }

        public static Monster GetMonsterFromLocation(Location location)
        {

            if (!location.MonstersHere.Any())
            {
                return null;
            }

            int totalChances = location.MonstersHere.Sum(m => m.ChanceOfEncountering);

            int randomNumber = RandomNumberGenerator.NumberBetween(1, totalChances);

            int runningTotal = 0;

            foreach (MonsterEncounter monsterEncounter in location.MonstersHere)
            {
                runningTotal += monsterEncounter.ChanceOfEncountering;
                if (randomNumber <= runningTotal)
                {
                    return GetMonster(monsterEncounter.MonsterId);
                }
            }

            return null;
            //return GetMonster(location.MonstersHere.Last().MonsterId);
        }
        public static Monster GetMonster(int id)
        {
            Monster newMonster = s_baseMonster.FirstOrDefault(m => m.Id == id).Clone();
            foreach (ItemPercentage itemPercentage in newMonster.LootTable.ToArray())
            {
                newMonster.AddItemToLootTable(itemPercentage.Id, itemPercentage.Percentage);
                if (RandomNumberGenerator.NumberBetween(1, 100) <= itemPercentage.Percentage)
                {
                    newMonster.AddItemToInventory(GameItemFactory.CreateGameItem(itemPercentage.Id));
                }
            }
            return newMonster;
        }
    }
}
