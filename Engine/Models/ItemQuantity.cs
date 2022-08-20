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
        public int ItemId { get; }
        public int Quantity { get; }

        public string QuantityItemDescription => $"{Quantity} {GameItemFactory.GetItemName(ItemId)}";

        public ItemQuantity(int itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}
