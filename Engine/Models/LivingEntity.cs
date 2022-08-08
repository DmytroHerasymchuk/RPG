using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Engine.Models
{
    public abstract class LivingEntity : BaseNotificationClass
    {
        private string _name;
        private int _maxHitPoints;
        private int _currentHitPoints;
        private int _gold;
        public ObservableCollection<GameItem> Inventory { get; set; }
        public List<GameItem> Weapons => Inventory.Where(i => i is Weapon).ToList();
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int MaxHitPoints
        {
            get
            {
                return _maxHitPoints;
            }
            set
            {
                _maxHitPoints = value;
                OnPropertyChanged(nameof(MaxHitPoints));
            }
        }

        public int CurrentHitPoints
        {
            get
            {
                return _currentHitPoints;
            }
            set
            {
                _currentHitPoints = value;
                OnPropertyChanged(nameof(CurrentHitPoints));
            }
        }

        public int Gold
        {
            get
            {
                return _gold;
            }
            set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }

        protected LivingEntity()
        {
            Inventory = new ObservableCollection<GameItem>();
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);
            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemToInventory(GameItem item)
        {
            Inventory.Remove(item);
            OnPropertyChanged(nameof(Weapons));
        }
    }
}
