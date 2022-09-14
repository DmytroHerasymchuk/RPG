using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Models
{
    public class PlayerAttribute : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Key { get; }
        public string DisplayName { get; }
        public int Value { get; set; }

        public PlayerAttribute(string key, string displayName)
        {
            Key = key;
            DisplayName = displayName;           
        }

        public PlayerAttribute(string key, string displayName, int value)
        {
            Key = key;
            DisplayName = displayName;
            Value = value;
        }

        public void SetBaseValue()
        {
            Value = 5;
        }
        public void Increment()
        {
            Value++;
        }
        public void Decrement()
        {
            Value--;
        }
    }
}
