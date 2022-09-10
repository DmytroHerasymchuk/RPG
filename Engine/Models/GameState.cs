using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameState
    {
        public Player Player { get; set; } //change to init
        public int XCoordinate { get; set; } //change to init
        public int YCoordinate { get; set; } //change to init
        public string Version { get; set; } // change to init
        public GameState(Player player, int xCoordinate, int yCoordinate, string version)
        {
            Player = player;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Version = version;
        }
    }
}
