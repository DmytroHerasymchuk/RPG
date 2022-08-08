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
        public string ImageName { get; set; }

        public int MaxDamage { get; set; }
        public int MinDamage { get; set; }
        public int RewardExperiencePoints { get; private set; }

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
