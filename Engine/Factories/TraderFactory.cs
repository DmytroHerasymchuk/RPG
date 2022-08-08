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
