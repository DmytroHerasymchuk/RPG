using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Factories;

namespace Engine.Models
{
    public class ItemQuantity
    {
        private readonly GameItem _gameItem;
        public int ItemId => _gameItem.ItemTypeId;
        public int Quantity { get; }

        public string QuantityItemDescription => $"{Quantity} {_gameItem.Name}";

        public ItemQuantity(GameItem item, int quantity)
        {
            _gameItem = item;
            Quantity = quantity;
        }
    }
}
