using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models
{
    public class Location
    {
        public int XCoordinate { get; }
        public int YCoordinate { get; }
        [JsonIgnore]
        public string Name { get; }
        [JsonIgnore]
        public string Description { get; }
        [JsonIgnore]
        public string ImageName { get; }
        [JsonIgnore]
        public MapPiece Map { get; set; }
        [JsonIgnore]
        public List<Quest>QuestsAvailableHere { get; }
        [JsonIgnore]
        public List<MonsterEncounter> MonstersHere { get; }
        [JsonIgnore]
        public Trader TraderHere { get; set; }
        public NPC NPCHere { get; set; } 
        public Location(int xCoordinate, int yCoordinate, string name, string description, string imageName, bool mapStatus = false)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Name = name;
            Description = description;
            ImageName = imageName;
            Map = new MapPiece(xCoordinate + 15, yCoordinate + 15, imageName, mapStatus);
            QuestsAvailableHere = new List<Quest>();
            MonstersHere = new List<MonsterEncounter>();
        }

        public void AddMonster(int monsterId, int chanceOfEncountering)
        {
            if (MonstersHere.Exists(m => m.MonsterId == monsterId))
            {
                MonstersHere.First(m=>m.MonsterId==monsterId).ChanceOfEncountering = chanceOfEncountering;
            }
            else
            {
                MonstersHere.Add(new MonsterEncounter(monsterId, chanceOfEncountering));
            }
        }
    }
}
