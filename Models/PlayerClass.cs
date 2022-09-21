using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
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

    }
}
