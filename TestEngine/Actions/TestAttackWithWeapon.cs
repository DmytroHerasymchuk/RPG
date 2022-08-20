using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Engine.Actions;
using Engine.Factories;
using Engine.Models;

namespace TestEngine.Actions
{
    [TestClass]
    public class TestAttackWithWeapon
    {
        [TestMethod]
        public void TestConstructorGoodParametrs()
        {
            GameItem brokenSword = GameItemFactory.CreateGameItem(10001);
            AttackWithWeapon attackWithWeapon = new AttackWithWeapon(brokenSword, 1, 3);
            Assert.IsNotNull(attackWithWeapon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructorItemIsNotAWeapon()
        {
            GameItem healPotion = GameItemFactory.CreateGameItem(40002);
            AttackWithWeapon attackWithWeapon = new AttackWithWeapon(healPotion, 1, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructorMinDamageLessThanZero()
        {
            GameItem brokenSword = GameItemFactory.CreateGameItem(10001);
            AttackWithWeapon attackWithWeapon = new AttackWithWeapon(brokenSword, -1, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructorMaxDamageLessThanMinDamage()
        {
            GameItem brokenSword = GameItemFactory.CreateGameItem(10001);
            AttackWithWeapon attackWithWeapon = new AttackWithWeapon(brokenSword, 2, 1);
        }
    }
}
