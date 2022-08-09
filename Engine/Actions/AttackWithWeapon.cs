using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Actions
{
    public class AttackWithWeapon
    {
        private readonly int _minDamage;
        private readonly int _maxDamage;
        private readonly GameItem _weapon;

        public event EventHandler<string> OnActionPerformed;
        public AttackWithWeapon(GameItem weapon, int minDamage, int maxDamage)
        {
            if (weapon.Category != GameItem.ItemCategory.Weapon)
            {
                throw new ArgumentException($"{weapon.Name} is not a weapon");
            }
            if (_minDamage < 0)
            {
                throw new ArgumentException("minDamage must be 0 or larger");
            }
            if(_maxDamage < _minDamage)
            {
                throw new ArgumentException("maxDamage must be >= minDamage");
            }
            _weapon = weapon;
            _minDamage = minDamage;
            _maxDamage = maxDamage;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            int damage = RandomNumberGenerator.NumberBetween(_minDamage, _maxDamage);
            if (damage == 0)
            {
                ReportResult($"You missed the {target.Name.ToLower()}.");
            }
            else
            {
                ReportResult($"You hit the {target.Name.ToLower()} for {damage} points.");
                target.TakeDamage(damage);
            }
        }

        private void ReportResult(string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}
