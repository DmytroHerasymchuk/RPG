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
        public List<ItemPercentage> LootTable { get; } 
            = new List<ItemPercentage>();
        public int Id { get; }  
        public string ImageName { get; }
        public int RewardExperiencePoints { get; }

        public Monster( int id, string name, string imageName, int maxHitPoints, IEnumerable<PlayerAttribute> attributes, GameItem currentWeapon, int rewardExperiencePoints, int gold) :
             base(name, maxHitPoints, maxHitPoints, attributes, gold)
        {
            Id = id;
            ImageName = imageName;
            CurrentWeapon = currentWeapon;
            RewardExperiencePoints = rewardExperiencePoints;
        }

        public void AddItemToLootTable(int id, int percentage)
        {
            LootTable.RemoveAll(ip => ip.Id == id);
            LootTable.Add(new ItemPercentage(id, percentage));
        }

        public Monster Clone()
        {
            Monster newMonster =
                new Monster(Id, Name, ImageName, MaxHitPoints, Attributes, CurrentWeapon, RewardExperiencePoints, Gold);
            newMonster.LootTable.AddRange(LootTable);
            return newMonster;
        }


    }
}
