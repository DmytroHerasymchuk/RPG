using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Recipe
    {
        public int Id { get; }
        public string Name { get; }
        public List<ItemQuantity> Ingredients { get; } = new List<ItemQuantity>();
        public List<ItemQuantity> OutputItems { get; } = new List<ItemQuantity>();

        public string ToolTipContents =>
            "Ingredients" + Environment.NewLine +
            "===========================" + Environment.NewLine +
            string.Join(Environment.NewLine, Ingredients.Select(i => i.QuantityItemDescription)) +
            Environment.NewLine + Environment.NewLine +
            "Creates" + Environment.NewLine +
            "===========================" + Environment.NewLine +
            string.Join(Environment.NewLine, OutputItems.Select(i => i.QuantityItemDescription));
      
        public Recipe(int itemId, string name)
        {
            Id = itemId;
            Name = name;
        }

        public void AddIngredient(int itemId, int quantity)
        {
            if(!Ingredients.Any(x => x.ItemId == itemId))
            {
                Ingredients.Add(new ItemQuantity(itemId, quantity));
            }
        }

        public void AddOutputItem(int itemId, int quantity)
        {
            if (!OutputItems.Any(x => x.ItemId == itemId))
            {
                OutputItems.Add(new ItemQuantity(itemId, quantity));
            }
        }
    }
}
