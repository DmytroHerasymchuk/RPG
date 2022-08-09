using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        public string ImageName { get; }

        public int MaxDamage { get; }
        public int MinDamage { get; }
        public int RewardExperiencePoints { get; }

        public Monster( string name, string imageName, int maxHitPoints, int currentHitPoints, int maxDamage, int minDamage, int rewardExperiencePoints, int gold) :
             base(name, maxHitPoints, currentHitPoints, gold)
        {
            ImageName = $"/Engine;component/Images/Monsters/{imageName}";
            MaxDamage = maxDamage;
            MinDamage = minDamage;
            RewardExperiencePoints = rewardExperiencePoints;
        }
    }
}
