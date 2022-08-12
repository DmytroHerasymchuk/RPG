using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class TraderFactory
    {
        private static readonly List<Trader> _traders = new List<Trader>();
        static TraderFactory()
        {
            Trader mark = new Trader("Mark");
            mark.AddItemToInventory(GameItemFactory.CreateGameItem(10004));
            mark.AddItemToInventory(GameItemFactory.CreateGameItem(40001));
            mark.AddItemToInventory(GameItemFactory.CreateGameItem(40002));
            mark.AddItemToInventory(GameItemFactory.CreateGameItem(40003));
            //mark.AddItemToInventory(GameItemFactory.CreateGameItem(20003));
            //mark.AddItemToInventory(GameItemFactory.CreateGameItem(20004));
            //mark.AddItemToInventory(GameItemFactory.CreateGameItem(20005));
            mark.AddItemToInventory(GameItemFactory.CreateGameItem(20006));
            mark.AddItemToInventory(GameItemFactory.CreateGameItem(20007));
            //mark.AddItemToInventory(GameItemFactory.CreateGameItem(20008));
            mark.AddItemToInventory(GameItemFactory.CreateGameItem(20009));
            //mark.AddItemToInventory(GameItemFactory.CreateGameItem(20010));
            //mark.AddItemToInventory(GameItemFactory.CreateGameItem(20011));
            //mark.AddItemToInventory(GameItemFactory.CreateGameItem(20012));
            //mark.AddItemToInventory(GameItemFactory.CreateGameItem(20013));
            AddTraderToList(mark);
        }

        public static Trader GetTraderByName(string name)
        {
            return _traders.FirstOrDefault(x => x.Name == name);
        }

        public static void AddTraderToList(Trader trader)
        {
            if (_traders.Any(t => t.Name == trader.Name))
            {
                throw new ArgumentException($"There is already a trader named {trader.Name})");
            }
            _traders.Add(trader);
        }
    }

    
}
