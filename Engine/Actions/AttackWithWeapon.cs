﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Services;
using Engine.Shared;
using Core;

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
            if (AttackSucceded(actor,target))
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
