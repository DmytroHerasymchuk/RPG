using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class PlayerAttribute
    {
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public int BaseValue { get; set; }
        public int ModifiedValue { get; set; }

        public PlayerAttribute(string key, string displayName)
        {
            Key = key;
            DisplayName = displayName;
            BaseValue = 5;
            ModifiedValue = 5;
        }
    }
}
