using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameDetails
    {
        public string Name { get; }
        public string Version { get; }
        public List<PlayerAttribute> PlayerAttributes { get; } = new List<PlayerAttribute>();
        public List<PlayerClass> Classes { get; } = new List<PlayerClass>();
        public GameDetails(string name, string version)
        {
            Name = name;
            Version = version;          
        }
    }
}
