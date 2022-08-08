using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class MonsterFactory
    {
        public static Monster GetMonster(int monsterId)
        {
            switch (monsterId)
            {
                case 1:
                    Monster troll = new Monster("Troll", "troll.png", 20, 15, 7, 5, 50, 5);
                    AddLootItem(troll, 20001, 85);
                    AddLootItem(troll, 10004, 10);
                    return troll;
                default:
                    throw new ArgumentException(string.Format("Monster Type '{0}' does not exist", monsterId));
            }
        }

        private static void AddLootItem(Monster monster, int itemId, int percentage)
        {
            if (RandomNumberGenerator.NumberBetween(1, 100) <= percentage)
            {
                monster.Inventory.Add(new ItemQuantity(itemId, 1));
            }
        }
    }
}
