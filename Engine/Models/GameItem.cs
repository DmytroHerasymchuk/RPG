﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Actions;

namespace Engine.Models
{
    public class GameItem
    {
        public enum ItemCategory
        {
            Miscellaneous,
            Weapon,
            Consumable
        }
        public ItemCategory Category { get; }
        public int ItemTypeId { get; }
        public string Name { get; }
        public int Price { get; }
        public string Rarity { get; }
        public bool IsUnique { get; }
        public IAction Action { get; set; }

        public GameItem(ItemCategory category, int itemTypeId, string name, int price, string rarity,
            bool isUnique = false, IAction action = null)
        {
            Category = category;
            ItemTypeId = itemTypeId;
            Name = name;
            Price = price;
            Rarity = rarity;
            IsUnique = isUnique;
            Action = action;    
        }

        public void PerformAction(LivingEntity actor, LivingEntity target)
        {
            Action?.Execute(actor, target);
        }
        public GameItem Clone()
        {
            return new GameItem(Category, ItemTypeId, Name, Price, Rarity, 
                                IsUnique, Action);
        }
    }
}
