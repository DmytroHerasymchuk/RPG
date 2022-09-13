using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models
{
    public class Recipe
    {
        public int Id { get; }
        [JsonIgnore]
        public string Name { get; }
        [JsonIgnore]
        public List<ItemQuantity> Ingredients { get; }
        [JsonIgnore]
        public List<ItemQuantity> OutputItems { get; }
        [JsonIgnore]
        public string ToolTipContents =>
            "Ingredients" + Environment.NewLine +
            "===========================" + Environment.NewLine +
            string.Join(Environment.NewLine, Ingredients.Select(i => i.QuantityItemDescription)) +
            Environment.NewLine + Environment.NewLine +
            "Creates" + Environment.NewLine +
            "===========================" + Environment.NewLine +
            string.Join(Environment.NewLine, OutputItems.Select(i => i.QuantityItemDescription));
      
        public Recipe(int id, string name, List<ItemQuantity> ingredients, List<ItemQuantity> outputItems)
        {
            Id = id;
            Name = name;
            Ingredients = ingredients;
            OutputItems = outputItems;
        }

    }
}
