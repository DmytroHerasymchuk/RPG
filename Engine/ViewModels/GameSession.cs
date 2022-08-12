using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Factories;
using Engine.EventArgs;
using Engine.Actions;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {
        public event EventHandler<GameMessageEventArgs> OnMessageRaised;
        private Player _currentPlayer;
        private Location _currentLocation;
        private Monster _currentMonster;
        private Trader _currentTrader;
        public World CurrentWorld { get; }
        public Player CurrentPlayer 
        {
            get 
            {
                return _currentPlayer;
            }
            set 
            {
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnActionPerformed -= OnCurrentPlayerPerformedAction;
                    _currentPlayer.OnLeveledUp -= OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled -= OnCurrentPlayerKilled;
                }
                _currentPlayer = value;
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnActionPerformed += OnCurrentPlayerPerformedAction;
                    _currentPlayer.OnLeveledUp += OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled += OnCurrentPlayerKilled;
                }
            }
        }
        public Location CurrentLocation {
            get
            {
                return _currentLocation;
            }
            set
            {
                _currentLocation = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToSouth));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToEast));
                CompleteQuestsAtLocation();
                GivePlayerQuestAtLocation();
                GetMonsterAtLocation();
                CurrentTrader = CurrentLocation.TraderHere;
                
            }
        }

        public bool HasMonster => CurrentMonster!= null;

        public bool HasTrader => CurrentTrader!= null;
        public Monster CurrentMonster
        {
            get
            {
                return _currentMonster;
            }
            set
            {
                if (_currentMonster != null)
                {
                    _currentMonster.OnActionPerformed -= OnCurrentMonsterPerformedAction;
                    _currentMonster.OnKilled -= OnCurrentMonsterKilled;
                }
                _currentMonster = value;
                if (_currentMonster != null)
                {
                    _currentMonster.OnActionPerformed += OnCurrentMonsterPerformedAction;
                    _currentMonster.OnKilled += OnCurrentMonsterKilled;
                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here!");
                    
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasMonster));
            }
        }

        public Trader CurrentTrader 
        {
            get
            {
                return _currentTrader;
            }
            set
            {
                _currentTrader = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasTrader));
                
            }
        }

        public bool HasLocationToNorth => 
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;
       
        public bool HasLocationToSouth =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;
 
        public bool HasLocationToWest => 
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;

        public bool HasLocationToEast =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;

       
        public GameSession()
        {
            CurrentPlayer = new Player("Katya", "Fairy", 10, 10, 0, 100);
            CurrentWorld = WorldFactory.CreateWorld();
            CurrentLocation = CurrentWorld.LocationAt(0, 0);
            
            CurrentPlayer.AddItemToInventory(GameItemFactory.CreateGameItem(10001));
            CurrentPlayer.LearnRecipe(RecipeFactory.RecipeById(1));
            if (!CurrentPlayer.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(GameItemFactory.CreateGameItem(10003));
            }

        }

        public void GoToNorth()
        {
            if (HasLocationToNorth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
            }
        }
        public void GoToSouth()
        {
            if (HasLocationToSouth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
            }   
        }
        public void GoToWest()
        {
            if (HasLocationToWest)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
            } 
        }
        public void GoToEast()
        {
            if (HasLocationToEast)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
            }
        }
        private void CompleteQuestsAtLocation()
        {
            foreach(Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                QuestStatus questToComplete =
                    CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.Id == quest.Id && !q.IsCompleted);
                if(questToComplete != null)
                {
                    if (CurrentPlayer.HasAllTheseItems(quest.ItemsToComplete))
                    {
                        CurrentPlayer.RemoveItemsFromInventory(quest.ItemsToComplete);
                        RaiseMessage("");
                        RaiseMessage($"You completed the '{quest.Name}' quest!");
                        RaiseMessage($"You receive {quest.RewardExperiencePoints} experience points.");
                        CurrentPlayer.AddExperience(quest.RewardExperiencePoints);
                        RaiseMessage($"You receive {quest.RewardGold} gold.");
                        CurrentPlayer.ReceiveGold(quest.RewardGold);
                        foreach(ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = GameItemFactory.CreateGameItem(itemQuantity.ItemId);
                            RaiseMessage($"You receive {rewardItem.Name} ({rewardItem.Rarity})");
                            CurrentPlayer.AddItemToInventory(rewardItem);                            
                        }
                        questToComplete.IsCompleted = true;
                    }
                }
            }
        }
        public void GivePlayerQuestAtLocation()
        {
            foreach(Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.Id == quest.Id))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
                    RaiseMessage("");
                    RaiseMessage($"You receive the '{quest.Name}' quest.");
                    RaiseMessage(quest.Description);
                    RaiseMessage("return with:");
                    foreach(ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        RaiseMessage($"     {itemQuantity.Quantity} {GameItemFactory.CreateGameItem(itemQuantity.ItemId).Name} ({GameItemFactory.CreateGameItem(itemQuantity.ItemId).Rarity})");
                    }
                    RaiseMessage("And you will receive:");
                    RaiseMessage($"     {quest.RewardExperiencePoints} experience points.");
                    RaiseMessage($"     {quest.RewardGold} gold.");
                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        RaiseMessage($"     {itemQuantity.Quantity} {GameItemFactory.CreateGameItem(itemQuantity.ItemId).Name} ({GameItemFactory.CreateGameItem(itemQuantity.ItemId).Rarity})");
                    }
                }
            }
        }

        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }

        public void CraftItem(Recipe recipe)
        {
            if (CurrentPlayer.HasAllTheseItems(recipe.Ingredients))
            {
                CurrentPlayer.RemoveItemsFromInventory(recipe.Ingredients);
                foreach(ItemQuantity itemQuantity in recipe.OutputItems)
                {
                    for(int i = 0; i < itemQuantity.Quantity; ++i)
                    {
                        GameItem outputItem = GameItemFactory.CreateGameItem(itemQuantity.ItemId);
                        CurrentPlayer.AddItemToInventory(outputItem);
                        RaiseMessage($"You craft 1 {outputItem.Name}");
                    }
                }
            }
            else
            {
                RaiseMessage("You do not have the requaired ingredients:");
                foreach(ItemQuantity itemQuantity in recipe.Ingredients)
                {
                    RaiseMessage($"     {itemQuantity.Quantity} {GameItemFactory.GetItemName(itemQuantity.ItemId)}");
                }
            }
        }
        public void AttackCurrentMonster()
        {
            if (CurrentPlayer.CurrentWeapon == null)
            {
                RaiseMessage("You must select a weapon to attack.");
                return;
            }
            CurrentPlayer.UseCurrentWeaponOn(CurrentMonster);
            if (CurrentMonster.IsDead)
            {
                GetMonsterAtLocation();
            }
            else
            {
                CurrentMonster.UseCurrentWeaponOn(CurrentPlayer);
            }
        }

        public void UseCurrentConsumable()
        {
            CurrentPlayer.UseCurrentConsumable();
        }
        private void OnCurrentPlayerPerformedAction(object sender, string result)
        {
            RaiseMessage(result);
        }

        private void OnCurrentMonsterPerformedAction(object sender, string result)
        {
            RaiseMessage(result);
        }
        private void OnCurrentPlayerKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage($"You was killed");
            CurrentLocation = CurrentWorld.LocationAt(0, 0);
            CurrentPlayer.FullHeal();
        }

        private void OnCurrentMonsterKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage($"You defeated {CurrentMonster.Name}!");
            RaiseMessage($"You receive {CurrentMonster.RewardExperiencePoints} experience points.");
            CurrentPlayer.AddExperience(CurrentMonster.RewardExperiencePoints);
            RaiseMessage($"You receive {CurrentMonster.Gold} gold.");
            CurrentPlayer.ReceiveGold(CurrentMonster.Gold);
            foreach (GameItem gameItem in CurrentMonster.Inventory)
            {
                RaiseMessage($"You receive one {gameItem.Name} ({gameItem.Rarity})");
                CurrentPlayer.AddItemToInventory(gameItem);
            }
        }

        private void OnCurrentPlayerLeveledUp(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage($"You are now level {CurrentPlayer.Level}!");
        }
        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }
    }
}
