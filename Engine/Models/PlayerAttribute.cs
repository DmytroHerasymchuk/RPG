using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class PlayerAttribute : BaseNotificationClass
    {
        private int _value;
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
        
        public PlayerAttribute(string key, string displayName)
        {
            Key = key;
            DisplayName = displayName;           
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
