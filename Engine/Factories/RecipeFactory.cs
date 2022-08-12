using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class RecipeFactory
    {
        private static readonly List<Recipe> _recipes = new List<Recipe>();

        static RecipeFactory()
        {
            Recipe healPotion = new Recipe(1, "Small heal potion");
            healPotion.AddIngredient(20006, 1);
            healPotion.AddIngredient(20007, 1);
            healPotion.AddIngredient(20009, 1);
            healPotion.AddOutputItem(40001, 1);
            _recipes.Add(healPotion);
        }

        public static Recipe RecipeById(int id)
        {
            return _recipes.FirstOrDefault(x => x.Id == id);
        }
    }
}
