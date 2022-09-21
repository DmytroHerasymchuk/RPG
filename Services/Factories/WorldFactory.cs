using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using Models;
using Models.Shared;


namespace Services.Factories
{
    public static class WorldFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Locations.xml";
        public static World CreateWorld()
        {
            World world = new World();
            if (File.Exists(GAME_DATA_FILENAME)){
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                string rootImagePath =
                    data.SelectSingleNode("/Locations")
                    .AttributeAsString("RootImagePath");
                LoadLocationsFromNodes(world,
                                       rootImagePath,
                                       data.SelectNodes("/Locations/Location"));
            }
            
            return world;
        }

        private static void LoadLocationsFromNodes(World world, string rootImagePath, XmlNodeList nodes)
        {
            if (nodes == null)
            {
                return;
            }
            foreach (XmlNode node in nodes)
            {
                
                Location location =
                    new Location(node.AttributeAsInt("X"),
                                 node.AttributeAsInt("Y"),
                                 node.AttributeAsString("Name"),
                                 node.SelectSingleNode("./Description")?.InnerText ?? "",
                                 $".{rootImagePath}{node.AttributeAsString("ImageName")}");
                AddMonsters(location, node.SelectNodes("./Monsters/Monster"));
                AddQuests(location, node.SelectNodes("./Quests/Quest"));
                AddTrader(location, node.SelectSingleNode("./Trader"));
                AddNPC(location, node.SelectSingleNode("./NPC"));
                world.AddLocation(location);
            }
        }

        private static void AddMonsters(Location location, XmlNodeList monsters)
        {
            if (monsters == null)
            {
                return;
            }
            foreach(XmlNode monsterNode in monsters)
            {
                location.AddMonster(monsterNode.AttributeAsInt("ID"),
                                    monsterNode.AttributeAsInt("Percent"));
            }
        }

        private static void AddQuests(Location location, XmlNodeList quests)
        {
            if (quests == null)
            {
                return;
            }
            foreach (XmlNode questNode in quests)
            {
                location.QuestsAvailableHere
                        .Add(QuestFactory.GetQuestById(questNode.AttributeAsInt("ID")));
            }
        }

        private static void AddTrader(Location location, XmlNode trader)
        {
            if (trader == null)
            {
                return;
            }

            location.TraderHere =
                    TraderFactory.GetTraderById(trader.AttributeAsInt("ID"));
                    
        }

        private static void AddNPC(Location location, XmlNode npc)
        {
            if(npc == null)
            {
                return;
            }
            location.NPCHere = 
                    NPCFactory.GetNPCById(npc.AttributeAsInt("ID"));

        }
    }
}
