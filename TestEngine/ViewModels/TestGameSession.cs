using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine.ViewModels;
using System;

namespace TestEngine.ViewModels
{
    [TestClass]
    public class TestGameSession
    {
        [TestMethod]
        public void TestCreateGameSession()
        {
            GameSession gameSession = new GameSession();
            Assert.IsNotNull(gameSession.CurrentPlayer);
            Assert.AreEqual("Tavern", gameSession.CurrentLocation.Name);
        }
        [TestMethod]
        public void TestPlayerDeathAndMovingToStart()
        {
            GameSession gameSession = new GameSession();
            gameSession.CurrentPlayer.TakeDamage(9999);
            Assert.AreEqual("Tavern", gameSession.CurrentLocation.Name);
            Assert.AreEqual(gameSession.CurrentPlayer.Level * 10, gameSession.CurrentPlayer.CurrentHitPoints);
        }
    }
}
