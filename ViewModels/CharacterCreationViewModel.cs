using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Services;
using Engine.Factories;
using System.ComponentModel;
using Core;

namespace ViewModels.ViewModels
{
    public class CharacterCreationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();

        public GameDetails GameDetails { get; }
        public PlayerClass SelectedClass { get; set; }
        public int AttributePoints { get; set; }
        public string Name { get; set; }
        public ObservableCollection<PlayerAttribute> PlayerAttributes { get; } = 
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
            Player player = new Player(Name, SelectedClass, 100, 100, PlayerAttributes, 0, 100, AttributePoints);
            player.AddItemToInventory(SelectedClass.GetClassItem(SelectedClass.Key));
            player.AddItemToInventory(GameItemFactory.CreateGameItem(41001));
            player.LearnRecipe(RecipeFactory.RecipeById(1));

            return player;
        }

        public void GetClassInfo(string key)
        {
            switch (key)
            {
                case "WARRIOR":
                    _messageBroker.RaiseMessage("   Warrior");
                    _messageBroker.RaiseMessage("Items you get: ");
                    _messageBroker.RaiseMessage("   " + SelectedClass.GetClassItem(key).Name);
                    _messageBroker.RaiseMessage("");
                    _messageBroker.RaiseMessage("Skills you get: ");                 
                    _messageBroker.RaiseMessage("   " + "Stan enemy");
                    break;
                case "THIEF":
                    _messageBroker.RaiseMessage("   Thief");
                    _messageBroker.RaiseMessage("Items you get: ");
                    _messageBroker.RaiseMessage("   " + SelectedClass.GetClassItem(key).Name);
                    _messageBroker.RaiseMessage("");
                    _messageBroker.RaiseMessage("Skills you get: ");              
                    _messageBroker.RaiseMessage("   " + "Steal");
                    break;
                case "MAGE":
                    _messageBroker.RaiseMessage("   Mage");
                    _messageBroker.RaiseMessage("Items you get: ");
                    _messageBroker.RaiseMessage("   " + SelectedClass.GetClassItem(key).Name);
                    _messageBroker.RaiseMessage("");
                    _messageBroker.RaiseMessage("Skills you get: ");
                    _messageBroker.RaiseMessage("   " + "Fireball");
                    break;
                default:
                    _messageBroker.RaiseMessage("Error");
                    break;
            }
        }
    }
}
