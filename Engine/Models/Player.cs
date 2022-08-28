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
        private PlayerClass _characterClass;
        private int _experiencePoints;

        public event EventHandler OnLeveledUp;
        public PlayerClass CharacterClass
        {
            get => _characterClass;

            set
            {
                _characterClass = value;
                OnPropertyChanged();
            }
        }

        public int ExperiencePoints
        {
            get => _experiencePoints;

            private set
            {
                _experiencePoints = value;
                OnPropertyChanged();
                SetLevelAndMaxHitPoints();
            }
        }
        
        public ObservableCollection<QuestStatus> Quests { get; } = new ObservableCollection<QuestStatus>();
        public ObservableCollection<Recipe> Recipes { get; } = new ObservableCollection<Recipe>();
        public Player(string name, PlayerClass characterClass, int maxHitPoints, int currentHitPoints, IEnumerable<PlayerAttribute> attributes, int experiencePoints, int gold) : 
            base(name, maxHitPoints, currentHitPoints, attributes, gold)
        {           
            this.CharacterClass = characterClass;            
            this.ExperiencePoints = experiencePoints; 
        }
        
        public void AddExperience(int expPoints)
        {
            ExperiencePoints += expPoints;
        }

        public void LearnRecipe(Recipe recipe)
        {
            if (!Recipes.Any(r => r.Id == recipe.Id))
            {
                Recipes.Add(recipe);
            }
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
