using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class PlayerClass
    {
        public enum Class{
            Warrior,
            Mage,
            Thief
        }
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public List<PlayerAttribute> PlayerAttributes { get; } = new List<PlayerAttribute>();
    }
}
