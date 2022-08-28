﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using System.IO;
using System.Xml;
using Engine.Shared;
using Engine.Services;

namespace Engine.Factories
{
    public static class MonsterFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Monsters.xml";
        private static readonly GameDetails gameDetails;
        private static readonly List<Monster> _baseMonster = new List<Monster>();

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
            foreach(XmlNode node in nodes)
            {
                var attributes = gameDetails.PlayerAttributes;
                attributes.First(a => a.Key.Equals("DEX")).Value = Convert.ToInt32(node.SelectSingleNode("./Dexterity").InnerText);
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
                if(lootItemNodes != null)
                {
                    foreach(XmlNode lootItemNode in lootItemNodes)
                    {
                        monster.AddItemToLootTable(lootItemNode.AttributeAsInt("ID"),
                                                   lootItemNode.AttributeAsInt("Percentage"));
                    }
                }
                _baseMonster.Add(monster);
            }
        }
       public static Monster GetMonster(int id)
        {
            return _baseMonster.FirstOrDefault(m => m.Id == id)?.GetNewInstance();
        }
    }
}
