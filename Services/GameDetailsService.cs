using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Shared;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Services
{
    public static class GameDetailsService
    {
        public static GameDetails ReadGameDetails()
        {
            
            JObject gameDetailsJson = JObject.Parse(File.ReadAllText(".\\GameData\\GameDetails.json"));
            string rootImagePath = gameDetailsJson.StringValueOf("RootImagePath");
            GameDetails gameDetails = new GameDetails(gameDetailsJson.StringValueOf("Name"),
                                                      gameDetailsJson.StringValueOf("Version"));
            foreach (JToken token in gameDetailsJson["PlayerAttributes"])
            {
                gameDetails.PlayerAttributes.Add(new PlayerAttribute(token.StringValueOf("Key"),
                                                                     token.StringValueOf("DisplayName")));
            }
            if (gameDetailsJson["Classes"] != null)
            {
                foreach(JToken token in gameDetailsJson["Classes"])
                {
                    PlayerClass playerClass = new PlayerClass
                    {
                        Key = token.StringValueOf("Key"),
                        DisplayName = token.StringValueOf("DisplayName"),
                        ImageName = $"{rootImagePath}{token.StringValueOf("ImageName")}"
                    };
                    gameDetails.Classes.Add(playerClass);
                }
            }
            return gameDetails;
        }
    }
}
