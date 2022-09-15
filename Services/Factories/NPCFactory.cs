using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.IO;
using System.Xml;
using Models.Shared;

namespace Services.Factories
{
    public static class NPCFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\NPC.xml";
        private static readonly List<NPC> _npc = new List<NPC>();
        static NPCFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                string rootImagePath =
                    data.SelectSingleNode("/NPCs").AttributeAsString("RootImagePath");
                LoadNPCFromNodes(data.SelectNodes("/NPCs/NPC"), rootImagePath);
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        private static void LoadNPCFromNodes(XmlNodeList nodes, string rootImagePath)
        {
            foreach (XmlNode node in nodes)
            {
                NPC npc = new NPC(node.AttributeAsInt("ID"),
                                  node.AttributeAsString("Name"),
                                  $".{rootImagePath}{node.AttributeAsString("ImageName")}");
                _npc.Add(npc);
            }
        }

        public static NPC GetNPCById(int id)
        {
            return _npc.FirstOrDefault(n => n.Id == id);
        }


    }


}
