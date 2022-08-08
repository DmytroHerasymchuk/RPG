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
        private int _level;       
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
            set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }
        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }
        public ObservableCollection<QuestStatus> Quests { get; set; }
        public Player(string name, string characterClass, int hitPoints, int experiencePoints, int level, int gold)
        {
            this.Name = name;
            this.CharacterClass = characterClass;
            this.MaxHitPoints = hitPoints;
            this.CurrentHitPoints = hitPoints;
            this.ExperiencePoints = experiencePoints;
            this.Level = level;
            this.Gold = gold;
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
    }
}
