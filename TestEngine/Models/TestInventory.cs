//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Engine.Factories;
//using Engine.Models;
//using Engine.Services;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//namespace TestEngine.Models
//{
//    [TestClass]
//    public class TestInventory
//    {
//        [TestMethod]
//        public void Test_Instantiate()
//        {
//            Inventory inventory = new Inventory();
//            Assert.AreEqual(0, inventory.Items.Count);
//        }
//        [TestMethod]
//        public void Test_AddItem()
//        {
//            Inventory inventory = new Inventory();
//            Inventory inventory1 = inventory.AddItemFromFactory(10001);
//            Assert.AreEqual(1, inventory1.Items.Count);
//        }
//        [TestMethod]
//        public void Test_AddItems()
//        {
//            Inventory inventory = new Inventory();
//            List<GameItem> itemsToAdd = new List<GameItem>();
//            itemsToAdd.Add(GameItemFactory.CreateGameItem(20001));
//            itemsToAdd.Add(GameItemFactory.CreateGameItem(20002));
//            Inventory inventory1 =
//                inventory.AddItems(itemsToAdd);
//            Assert.AreEqual(2, inventory1.Items.Count);
//            // Notice the used of chained AddItemFromFactory() calls
//            Inventory inventory2 =
//                inventory1
//                    .AddItemFromFactory(20001)
//                    .AddItemFromFactory(20002);
//            Assert.AreEqual(4, inventory2.Items.Count);
//        }
//        [TestMethod]
//        public void Test_AddItemQuantities()
//        {
//            Inventory inventory = new Inventory();
//            Inventory inventory1 =
//                inventory.AddItems(new List<ItemQuantity> { new ItemQuantity(10001, 3) });
//            Assert.AreEqual(3, inventory1.Items.Count(i => i.ItemTypeId == 10001));
//            Inventory inventory2 =
//                inventory1.AddItemFromFactory(10001);
//            Assert.AreEqual(4, inventory2.Items.Count(i => i.ItemTypeId == 10001));
//            Inventory inventory3 =
//                inventory2.AddItems(new List<ItemQuantity> { new ItemQuantity(10002, 1) });
//            Assert.AreEqual(4, inventory3.Items.Count(i => i.ItemTypeId == 10001));
//            Assert.AreEqual(1, inventory3.Items.Count(i => i.ItemTypeId == 10002));
//        }
//        [TestMethod]
//        public void Test_RemoveItem()
//        {
//            Inventory inventory = new Inventory();
//            GameItem item1 = GameItemFactory.CreateGameItem(10001);
//            GameItem item2 = GameItemFactory.CreateGameItem(10002);
//            Inventory inventory1 =
//                inventory.AddItems(new List<GameItem> { item1, item2 });
//            Inventory inventory2 =
//                inventory1.RemoveItem(item1);
//            Assert.AreEqual(1, inventory2.Items.Count);
//        }
//        [TestMethod]
//        public void Test_RemoveItems()
//        {
//            Inventory inventory = new Inventory();
//            GameItem item1 = GameItemFactory.CreateGameItem(20001);
//            GameItem item2 = GameItemFactory.CreateGameItem(20002);
//            GameItem item3 = GameItemFactory.CreateGameItem(20002);
//            Inventory inventory1 =
//                inventory.AddItems(new List<GameItem> { item1, item2, item3 });
//            Inventory inventory2 =
//                inventory1.RemoveItems(new List<GameItem> { item2, item3 });
//            Assert.AreEqual(1, inventory2.Items.Count);
//        }
//        [TestMethod]
//        public void Test_CategorizedItemProperties()
//        {
            
//            Inventory inventory = new Inventory();
//            Assert.AreEqual(0, inventory.Weapons.Count);
//            Assert.AreEqual(0, inventory.Consumables.Count);
            
//            Inventory inventory1 = inventory.AddItemFromFactory(10001);
//            Assert.AreEqual(1, inventory1.Weapons.Count);
//            Assert.AreEqual(0, inventory1.Consumables.Count);
            
//            Inventory inventory2 = inventory1.AddItemFromFactory(20001);
//            Assert.AreEqual(1, inventory2.Weapons.Count);
//            Assert.AreEqual(0, inventory2.Consumables.Count);
            
//            Inventory inventory3 = inventory2.AddItemFromFactory(10002);
//            Assert.AreEqual(2, inventory3.Weapons.Count);
//            Assert.AreEqual(0, inventory3.Consumables.Count);
            
//            Inventory inventory4 = inventory3.AddItemFromFactory(40001);
//            Assert.AreEqual(2, inventory4.Weapons.Count);
//            Assert.AreEqual(1, inventory4.Consumables.Count);
//        }
//        [TestMethod]
//        public void Test_RemoveItemQuantities()
//        {
            
//            Inventory inventory = new Inventory();
//            Assert.AreEqual(0, inventory.Weapons.Count);
//            Assert.AreEqual(0, inventory.Consumables.Count);
//            Inventory inventory2 =
//                inventory
//                    .AddItemFromFactory(10001)
//                    .AddItemFromFactory(10002)
//                    .AddItemFromFactory(10002)
//                    .AddItemFromFactory(10002)
//                    .AddItemFromFactory(10002)
//                    .AddItemFromFactory(40001)
//                    .AddItemFromFactory(40001);
//            Assert.AreEqual(1, inventory2.Items.Count(i => i.ItemTypeId == 10001));
//            Assert.AreEqual(4, inventory2.Items.Count(i => i.ItemTypeId == 10002));
//            Assert.AreEqual(2, inventory2.Items.Count(i => i.ItemTypeId == 40001));
//            Inventory inventory3 =
//                inventory2
//                    .RemoveItems(new List<ItemQuantity> { new ItemQuantity(10002, 2) });
//            Assert.AreEqual(1, inventory3.Items.Count(i => i.ItemTypeId == 10001));
//            Assert.AreEqual(2, inventory3.Items.Count(i => i.ItemTypeId == 10002));
//            Assert.AreEqual(2, inventory3.Items.Count(i => i.ItemTypeId == 40001));
//            Inventory inventory4 =
//                inventory3
//                    .RemoveItems(new List<ItemQuantity> { new ItemQuantity(10002, 1) });
//            Assert.AreEqual(1, inventory4.Items.Count(i => i.ItemTypeId == 10001));
//            Assert.AreEqual(1, inventory4.Items.Count(i => i.ItemTypeId == 10002));
//            Assert.AreEqual(2, inventory4.Items.Count(i => i.ItemTypeId == 40001));
//        }
//        [TestMethod]
//        [ExpectedException(typeof(InvalidOperationException))]
//        public void Test_RemoveItemQuantities_RemoveTooMany()
//        {
            
//            Inventory inventory = new Inventory();
//            Assert.AreEqual(0, inventory.Weapons.Count);
//            Assert.AreEqual(0, inventory.Consumables.Count);
//            Inventory inventory2 =
//                inventory
//                    .AddItemFromFactory(10001)
//                    .AddItemFromFactory(10002)
//                    .AddItemFromFactory(10002)
//                    .AddItemFromFactory(10002)
//                    .AddItemFromFactory(10002)
//                    .AddItemFromFactory(40001)
//                    .AddItemFromFactory(40001);
//            Assert.AreEqual(1, inventory2.Items.Count(i => i.ItemTypeId == 10001));
//            Assert.AreEqual(4, inventory2.Items.Count(i => i.ItemTypeId == 10002));
//            Assert.AreEqual(2, inventory2.Items.Count(i => i.ItemTypeId == 40001));
            
//            Inventory inventory3 =
//                inventory2
//                    .RemoveItems(new List<ItemQuantity> { new ItemQuantity(10002, 999) });
//        }
//    }
//}