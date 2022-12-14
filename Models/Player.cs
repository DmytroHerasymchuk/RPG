using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Models.Shared;

namespace Models
{
    public class Player : LivingEntity
    {        
        private PlayerClass _characterClass;
        private int _experiencePoints;
        private int _attributePoints;

        public event EventHandler OnLeveledUp;
        public bool IsLeveledUp => AttributePoints > 0;
        public int AttributePoints
        {
            get => _attributePoints;
            set
            {
                _attributePoints = value;
            }
        }
        public PlayerClass CharacterClass
        {
            get => _characterClass;

            set
            {
                _characterClass = value;
            }
        }

        public int ExperiencePoints
        {
            get => _experiencePoints;

            private set
            {
                _experiencePoints = value;
                SetLevelAndMaxHitPoints();
            }
        }
        
        public ObservableCollection<QuestStatus> Quests { get; } = new ObservableCollection<QuestStatus>();
        public ObservableCollection<Recipe> Recipes { get; } = new ObservableCollection<Recipe>();
        public Player(string name, PlayerClass characterClass, int maxHitPoints, int currentHitPoints, IEnumerable<PlayerAttribute> attributes, int experiencePoints, int gold, int attributePoints = 0) : 
            base(name, maxHitPoints, currentHitPoints, attributes, gold)
        {           
            this.CharacterClass = characterClass;            
            this.ExperiencePoints = experiencePoints;
            this.AttributePoints = attributePoints;
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

        public void SetMaxHitPoints()
        {
            MaxHitPoints = this.GetAttribute("CON").Value * Level + 5;
            this.FullHeal();
        }

        private void SetLevelAndMaxHitPoints()
        {
            int lvlUpExp = (int)(100 * Math.Pow(1.2, Level));
            if (ExperiencePoints>=lvlUpExp)
            {
                Level++;
                AttributePoints++;
                MaxHitPoints = this.GetAttribute("CON").Value * Level + 5;
                this.FullHeal();
                ExperiencePoints -= lvlUpExp;
                OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
            }
        }

    }
}
