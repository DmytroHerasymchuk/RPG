using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Engine.Services;
using Newtonsoft.Json;

namespace Engine.Models
{
    public abstract class LivingEntity : BaseNotificationClass
    {
        private string _name;
        private int _maxHitPoints;
        private int _currentHitPoints;
        private int _gold;
        private int _level;
        private GameItem _currentWeapon;
        private GameItem _currentConsumable;
        private Inventory _inventory;
        public ObservableCollection<PlayerAttribute> Attributes { get; } = new ObservableCollection<PlayerAttribute>();
        [JsonIgnore]
        public bool IsAlive => CurrentHitPoints > 0;
        [JsonIgnore]
        public bool IsDead => !IsAlive;
        public event EventHandler OnKilled;
        public event EventHandler<string> OnActionPerformed;

        public string Name
        {
            get => _name;
 
            private set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int MaxHitPoints
        {
            get => _maxHitPoints;

            protected set
            {
                _maxHitPoints = value;
                OnPropertyChanged();
            }
        }

        public int CurrentHitPoints
        {
            get => _currentHitPoints;

            private set
            {
                _currentHitPoints = value;
                OnPropertyChanged();
            }
        }

        public int Gold
        {
            get => _gold;

            private set
            {
                _gold = value;
                OnPropertyChanged();
            }
        }

        public int Level
        {
            get => _level;

            set
            {
                _level = value;
                OnPropertyChanged();
            }
        }

        public Inventory Inventory
        {
            get => _inventory;
            private set
            {
                _inventory = value;
                OnPropertyChanged();
            }
        }
        public GameItem CurrentWeapon
        {
            get => _currentWeapon;

            set
            {
                if (_currentWeapon != null)
                {
                    _currentWeapon.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }

                _currentWeapon = value;
                if (_currentWeapon != null)
                {
                    _currentWeapon.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }
                OnPropertyChanged();
            }
        }

        public GameItem CurrentConsumable
        {
            get => _currentConsumable;

            set
            {
                if (_currentConsumable != null)
                {
                    _currentConsumable.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }

                _currentConsumable = value;
                if (_currentConsumable != null)
                {
                    _currentConsumable.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }
                OnPropertyChanged();
            }
        }
        protected LivingEntity(string name, int maxHitPoint, int currentHitPoints, IEnumerable<PlayerAttribute> attributes, int gold, int level = 1)
        {
            Name = name;
            MaxHitPoints = maxHitPoint;
            CurrentHitPoints = currentHitPoints;
            Gold = gold;
            Level = level;
            foreach(PlayerAttribute attribute in attributes)
            {
                Attributes.Add(attribute);
            }
            Inventory = new Inventory();
        }

        public void UseCurrentWeaponOn(LivingEntity target)
        {
            CurrentWeapon.PerformAction(this, target);
        }

        public void UseCurrentConsumable()
        {
            CurrentConsumable.PerformAction(this, this);
            RemoveItemFromInventory(CurrentConsumable);
        }
        public void TakeDamage(int hitPointsOfDamage)
        {
            CurrentHitPoints -= hitPointsOfDamage;
            if (IsDead)
            {
                CurrentHitPoints = 0;
                RaiseOnKilledEvent();
            }
        }

        public void Heal(int hitPointToHeal)
        {
            CurrentHitPoints += hitPointToHeal;
            if (CurrentHitPoints > MaxHitPoints)
            {
                CurrentHitPoints = MaxHitPoints;
            }
        }

        public void FullHeal()
        {
            CurrentHitPoints = MaxHitPoints;
        }

        public void ReceiveGold(int amountOfGold)
        {
            Gold += amountOfGold;
        }

        public void SpendGold(int amountOfGold)
        {
            if (amountOfGold > Gold)
            {
                return;
                //throw new ArgumentOutOfRangeException($"You have only {Gold} gold, and cannot spend {amountOfGold} gold");             
            }
            Gold -= amountOfGold;
        }
        public void AddItemToInventory(GameItem item)
        {
            Inventory = Inventory.AddItem(item);
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory = Inventory.RemoveItem(item);
           
        }
        public void RemoveItemsFromInventory(List<ItemQuantity> itemQuantities)
        {
            Inventory = Inventory.RemoveItems(itemQuantities);
        }

        private void RaiseOnKilledEvent()
        {
            OnKilled?.Invoke(this, new System.EventArgs());
        }

        private void RaiseActionPerformedEvent(object sender, string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}
