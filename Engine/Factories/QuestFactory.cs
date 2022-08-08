using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quests = new List<Quest>();

        static QuestFactory()
        {
            // Declare the items need to complete the quest, and its reward items
            List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
            List<ItemQuantity> rewardItems = new List<ItemQuantity>();
            itemsToComplete.Add(new ItemQuantity(20001, 3));
            rewardItems.Add(new ItemQuantity(10002, 1));
            // Create the quest
            _quests.Add(new Quest(1, "Creepy Trolls", "Kill trolls in a cave", itemsToComplete, 50, 20, rewardItems));
        }

        internal static Quest GetQuestById(int id)
        {
            return _quests.FirstOrDefault(quest => quest.Id == id);
        }
    }
}
