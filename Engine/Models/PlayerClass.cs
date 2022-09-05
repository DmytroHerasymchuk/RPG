using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Factories;

namespace Engine.Models
{
    public class PlayerClass
    {
        public enum GameClass{
            Warrior,
            Mage,
            Thief
        }
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public string ImageName { get; set; }
        public List<PlayerAttribute> PlayerAttributes { get; } = new List<PlayerAttribute>();

        public GameItem GetClassItem(string key)
        {
            switch (key)
            {
                case "WARRIOR":
                    return GameItemFactory.CreateGameItem(10003);
                case "THIEF":
                    return GameItemFactory.CreateGameItem(10005);
                case "MAGE":
                    return GameItemFactory.CreateGameItem(10010);
                default:
                    return GameItemFactory.CreateGameItem(10001);
            }
        }
    }
}
