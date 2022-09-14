using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Actions
{
    public class LearnRecipe : BaseAction, IAction
    {
        private Recipe _recipe;
        public LearnRecipe(GameItem itemInUse, Recipe recipe) :
            base(itemInUse)
        {
            if (itemInUse.Category != GameItem.ItemCategory.Consumable)
            {
                throw new ArgumentException($"{itemInUse.Name} is not consumable");
            }
            _recipe = recipe;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            _messageBroker.RaiseMessage("You learned new recipe!");
            Player targetPlayet = (target as Player);
            targetPlayet.LearnRecipe(_recipe);           
        }
    }
}
