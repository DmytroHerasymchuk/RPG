using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Services;
using Newtonsoft.Json;
using Engine.Shared;

namespace Engine.Models
{
    public class Inventory
    {
        private readonly List<GameItem> _backingInventory = 
            new List<GameItem>();
        private readonly List<GroupedInventoryItem> _backingGroupedInventoryItems = 
            new List<GroupedInventoryItem>();

        public IReadOnlyCollection<GameItem> Items => _backingInventory.AsReadOnly();
        [JsonIgnore]
        public IReadOnlyCollection<GroupedInventoryItem> GroupedInventory => _backingGroupedInventoryItems.AsReadOnly();
        [JsonIgnore]
        public IReadOnlyCollection<GameItem> Weapons => _backingInventory.ItemsThatAre(GameItem.ItemCategory.Weapon).AsReadOnly();
        [JsonIgnore]
        public IReadOnlyCollection<GameItem> Consumables => _backingInventory.ItemsThatAre(GameItem.ItemCategory.Potion).AsReadOnly();
        [JsonIgnore]
        public bool HasConsumable => Consumables.Any();

        public Inventory(IEnumerable<GameItem> items = null)
        {
            if(items == null)
            {
                return;
            }
            foreach(GameItem item in items)
            {
                _backingInventory.Add(item);
                AddItemToGroupedInventory(item);
            }
        }

        public bool HasAllTheseItems(IEnumerable<ItemQuantity> items)
        {
            foreach(ItemQuantity item in items)
            {
                if (Items.Count(i => i.ItemTypeId == item.ItemId) < item.Quantity)
                {
                    return false;
                }
            }
            return true;
        }

        private void AddItemToGroupedInventory(GameItem item)
        {
            if (item.IsUnique)
            {
                _backingGroupedInventoryItems.Add(new GroupedInventoryItem(item, 1));
            }
            else
            {
                if (_backingGroupedInventoryItems.All(gi => gi.Item.ItemTypeId != item.ItemTypeId))
                {
                    _backingGroupedInventoryItems.Add(new GroupedInventoryItem(item, 0));
                }
                _backingGroupedInventoryItems.First(gi => gi.Item.ItemTypeId == item.ItemTypeId).Quantity++;
            }
        }
    }
}
