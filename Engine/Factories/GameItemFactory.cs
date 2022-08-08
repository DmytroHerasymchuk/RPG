using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class GameItemFactory
    {
        private static readonly List<GameItem> _standartGameItem = new List<GameItem>();
        
        static GameItemFactory()
        {
            _standartGameItem = new List<GameItem>();
            _standartGameItem.Add(new Weapon(10001, "Sword", 10, "Common", 3, 5));
            _standartGameItem.Add(new Weapon(10002, "Katana", 25, "Uncommon", 5, 8));
            _standartGameItem.Add(new Weapon(10003, "Stick", 1, "Common", 1, 2));
            _standartGameItem.Add(new GameItem(20001, "Troll Tooth", 2, "Common"));
            _standartGameItem.Add(new Weapon(10004, "Troll Club", 50, "Rare", 10, 12));
            
        }

        public static GameItem CreateGameItem(int itemTypeId)
        {
            GameItem standartItem = _standartGameItem.FirstOrDefault(item => item.ItemTypeId == itemTypeId);
            if(standartItem != null)
            {
                if(standartItem is Weapon)
                {
                    return (standartItem as Weapon).Clone();
                }
                return standartItem.Clone();
            }
            return null;
        }
    }
}
