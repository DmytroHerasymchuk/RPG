﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Services;

namespace Engine.Actions
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
            if (CombatService.AttackSucceded(actor,target))
            {
                int damage = RandomNumberGenerator.NumberBetween(_minDamage, _maxDamage);
                ReportResult($"{actorName} hit {targetName} for {damage} point{(damage > 1 ? "s" : "")}.");
                target.TakeDamage(damage);

            }
            else
            {
                ReportResult($"{actorName} missed {targetName}.");
                
            }
        }
    }
}
