using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    internal static class WorldFactory
    {
        internal static World CreateWorld()
        {
            World newWorld = new World();
            newWorld.AddLocation(0, 0, "Tavern", "Your home for now", "tavern.jpg");
            newWorld.AddLocation(1, 0, "Alley", "An inconspicuous alley in a busy town", "alley.jpg");
            newWorld.LocationAt(1, 0).QuestsAvailableHere.Add(QuestFactory.GetQuestById(1));
            newWorld.AddLocation(2, 0, "Forest", "Darkwood forest near the town", "forest.jpg");
            newWorld.LocationAt(2, 0).TraderHere = TraderFactory.GetTraderByName("Mark");
            newWorld.AddLocation(3, 0, "Cave", "Deep dark cave", "cave.jpg");
            newWorld.LocationAt(3, 0).AddMonster(1, 100);
            newWorld.AddLocation(3,-1, "Teleport", "A strange light comes from here", "teleport.jpg");
            return newWorld;
        }
    }
}
