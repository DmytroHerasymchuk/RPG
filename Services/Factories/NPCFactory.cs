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
                AddDialogs(npc, node.SelectNodes("./Dialog/TextValue"));
                _npc.Add(npc);
            }
        }
        private static void AddDialogs(NPC npc, XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {

                Dialog dialog = new Dialog(node.AttributeAsString("Key"),
                                           node?.InnerText ?? "",
                                           node.AttributeAsInt("ID"));
                npc.Dialogs.Add(dialog);
                
            }
        }


        public static NPC GetNPCById(int id)
        {
            return _npc.FirstOrDefault(n => n.Id == id);
        }

        public static string GetAnswerOfNPCDialog(int idOfNPC, int idOFDialog)
        {
            return GetNPCById(idOfNPC).Dialogs.FirstOrDefault(d => d.IDShort == idOFDialog).AnswerDialog;

        }

        public static string GetShortOfNPCDialog(int idOfNPC, int idOFDialog)
        {
            return GetNPCById(idOfNPC).Dialogs.FirstOrDefault(d => d.IDShort == idOFDialog).ShortDialog;

        }
    }


}
