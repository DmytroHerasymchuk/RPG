using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Services;
using Engine.Factories;

namespace Engine.ViewModels
{
    public class CharacterCreationViewModel : BaseNotificationClass
    {
        private PlayerClass _playerClass;
        private int _attributePoints;
        public GameDetails GameDetails { get; }
        public PlayerClass SelectedClass
        {
            get => _playerClass;
            set
            {
                _playerClass = value;
                OnPropertyChanged();
            }
        }
        public int AttributePoints
        {
            get => _attributePoints;
            set
            {
                _attributePoints = value;
                OnPropertyChanged();
            }
        }
        public string Name { get; set; }
        public ObservableCollection<PlayerAttribute> PlayerAttributes { get; set; } = 
            new ObservableCollection<PlayerAttribute>();
        public bool HasClasses =>
            GameDetails.Classes.Any();

        public CharacterCreationViewModel()
        {
            GameDetails = GameDetailsService.ReadGameDetails();
            if (HasClasses)
            {
                SelectedClass = GameDetails.Classes.First();
            }
            CreateNewCharacter();
           
        }

        public void CreateNewCharacter()
        {
            PlayerAttributes.Clear();
            foreach(PlayerAttribute playerAttribute in GameDetails.PlayerAttributes)
            {
                playerAttribute.SetBaseValue();
                PlayerAttributes.Add(playerAttribute);
            }
            AttributePoints = 5;
        }

        public Player GetPlayer()
        {
            Player player = new Player(Name, SelectedClass, 10, 10, PlayerAttributes, 0, 100, AttributePoints);
            player.AddItemToInventory(GameItemFactory.CreateGameItem(10004));
            player.LearnRecipe(RecipeFactory.RecipeById(1));
            if (!player.Inventory.Weapons.Any())
            {
                player.AddItemToInventory(GameItemFactory.CreateGameItem(10003));
            }
            return player;
        }
    }
}
