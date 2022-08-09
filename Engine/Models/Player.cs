using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Engine.Models
{
    public class Player : LivingEntity
    {        
        private string _characterClass;
        private int _experiencePoints;

        public event EventHandler OnLeveledUp;
        public string CharacterClass
        {
            get
            {
                return _characterClass;
            }
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }

        public int ExperiencePoints
        {
            get
            {
                return _experiencePoints;
            }
            private set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
                SetLevelAndMaxHitPoints();
            }
        }
        
        public ObservableCollection<QuestStatus> Quests { get; set; }
        public Player(string name, string characterClass, int maxHitPoints, int currentHitPoints, int experiencePoints, int gold) : 
            base(name, maxHitPoints, currentHitPoints, gold)
        {           
            this.CharacterClass = characterClass;            
            this.ExperiencePoints = experiencePoints;
            this.Quests = new ObservableCollection<QuestStatus>();          
        }
        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach(ItemQuantity item in items)
            {
                if (Inventory.Count(i => i.ItemTypeId == item.ItemId) < item.Quantity)
                {
                    return false;
                }
            }
            return true;
        }

        public void AddExperience(int expPoints)
        {
            ExperiencePoints += expPoints;
        }

        private void SetLevelAndMaxHitPoints()
        {
            int originalLevel = Level;
            Level = (ExperiencePoints / 100) + 1;
            if (Level != originalLevel)
            {
                MaxHitPoints = Level * 10;
                OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}
