using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Monster : BaseNotificationClass
    {
        private int _hitPoints;

        public string Name { get; set; }
        public string ImageName { get; set; }

        public int MaxHitPoints { get; private set; }
        public int HitPoints 
            {
            get
            {
                return _hitPoints;
            }
            set
            {
                _hitPoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }
        }

        public int MaxDamage { get; set; }
        public int MinDamage { get; set; }
        public int RewardExperiencePoints { get; private set; }

        public int RewardGold { get; private set; }

        public ObservableCollection<ItemQuantity> Inventory { get; set; }

        public Monster( string name, string imageName, int maxHitPoints, int hitPoints, int maxDamage, int minDamage, int rewardExperiencePoints, int rewardGold)
        {
            Name = name;
            ImageName = $"/Engine;component/Images/Monsters/{imageName}";
            MaxHitPoints = maxHitPoints;
            HitPoints = hitPoints;
            MaxDamage = maxDamage;
            MinDamage = minDamage;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;
            Inventory = new ObservableCollection<ItemQuantity>();
            MaxDamage = maxDamage;
            MinDamage = minDamage;
        }
    }
}
