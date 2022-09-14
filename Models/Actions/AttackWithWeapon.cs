using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Shared;
using Core;

namespace Models.Actions
{
    public class AttackWithWeapon : BaseAction, IAction
    {
        private readonly int _minDamage;
        private readonly int _maxDamage;
        public AttackWithWeapon(GameItem itemInUse, int minDamage, int maxDamage) :
            base(itemInUse)
        {
            if (itemInUse.Category != GameItem.ItemCategory.Weapon)
            {
                throw new ArgumentException($"{itemInUse.Name} is not a weapon");
            }
            if (minDamage < 0)
            {
                throw new ArgumentException("minDamage must be 0 or larger");
            }
            if(maxDamage < minDamage)
            {
                throw new ArgumentException("maxDamage must be >= minDamage");
            }
            _minDamage = minDamage;
            _maxDamage = maxDamage;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "you" : $"the {target.Name.ToLower()}";
            if (AttackSucceded(actor,target))
            {
                int weaponDamage = RandomNumberGenerator.NumberBetween(_minDamage, _maxDamage);
                int strengthDamage = (actor.GetAttribute("STR").Value * 2) / ((_minDamage + _maxDamage) / 2);
                int totalDamage = weaponDamage + strengthDamage;
                ReportResult($"{actorName} hit {targetName} for {totalDamage} point{(totalDamage > 1 ? "s" : "")}.");
                target.TakeDamage(totalDamage);

            }
            else
            {
                ReportResult($"{actorName} missed {targetName}.");
                
            }
        }


        private bool AttackSucceded(LivingEntity attacker, LivingEntity target)
        {
            int playerDexterity = attacker.GetAttribute("DEX").Value * attacker.GetAttribute("DEX").Value;
            int opponentDexterity = target.GetAttribute("DEX").Value * target.GetAttribute("DEX").Value;
            decimal dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            int randomOffset = RandomNumberGenerator.NumberBetween(-10, 10);
            decimal totalOffset = dexterityOffset + randomOffset;
            return RandomNumberGenerator.NumberBetween(0, 100) <= 50 + totalOffset;
        }
    }
}
