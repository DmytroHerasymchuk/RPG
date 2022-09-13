using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models.Actions
{
    public class Poison : BaseAction, IAction
    {
        private int _damageHitPoints;

        public Poison(GameItem itemInUse, int damageHitPoints):
            base(itemInUse)
        {
            if (itemInUse.Category != GameItem.ItemCategory.Consumable)
            {
                throw new ArgumentException($"{itemInUse.Name} is not consumable");
            }
            _damageHitPoints = damageHitPoints;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "yourself" : $"the {target.Name.ToLower()}";
            ReportResult($"{actorName} poisoned {targetName} for {_damageHitPoints} point{(_damageHitPoints > 1 ? "s" : "")}.");
            target.TakeDamage(_damageHitPoints);
        }
    }
}
