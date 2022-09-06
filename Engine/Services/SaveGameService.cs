using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Engine.Factories;
using Engine.Models;
using Engine.ViewModels;

namespace Engine.Services
{
    public static class SaveGameService
    {
        public static void Save(GameSession gameSession, string fileName)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(gameSession, Formatting.Indented));
        }
        public static GameSession LoadLastSaveOrCreateNew(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"Filename: {fileName}");
            }
            try
            {
                JObject data = JObject.Parse(File.ReadAllText(fileName));
                Player player = CreatePlayer(data);
                int x = (int)data[nameof(GameSession.CurrentLocation)][nameof(Location.XCoordinate)];
                int y = (int)data[nameof(GameSession.CurrentLocation)][nameof(Location.YCoordinate)];
                return new GameSession(player, x, y);
            }
            catch (Exception ex)
            {
                throw new FormatException($"Error reading: {fileName}");
            }

        }

        private static Player CreatePlayer(JObject data)
        {
            string fileVersion = FileVersion(data);
            Player player;
            switch (fileVersion)
            {
                case "0.1.000":
                    player =
                        new Player((string)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Name)],
                                   GetPlayerClass(data),
                                   (int)data[nameof(GameSession.CurrentPlayer)][nameof(Player.MaxHitPoints)],
                                   (int)data[nameof(GameSession.CurrentPlayer)][nameof(Player.CurrentHitPoints)],
                                   GetPlayerAttributes(data),
                                   (int)data[nameof(GameSession.CurrentPlayer)][nameof(Player.ExperiencePoints)],
                                   (int)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Gold)],
                                   (int)data[nameof(GameSession.CurrentPlayer)][nameof(Player.AttributePoints)]);
                    break;
                default:
                    throw new InvalidDataException($"File version '{fileVersion}' not recognized");
            }
            PopulatePlayerInventory(data, player);
            PopulatePlayerQuests(data, player);
            PopulatePlayerRecipe(data, player);
            return player;
        }

        private static PlayerClass GetPlayerClass(JObject data)
        {
            PlayerClass playerClass = new PlayerClass
            {
                Key = (string)data[nameof(GameSession.CurrentPlayer)][nameof(Player.CharacterClass)][nameof(PlayerClass.Key)],
                DisplayName = (string)data[nameof(GameSession.CurrentPlayer)][nameof(Player.CharacterClass)][nameof(PlayerClass.DisplayName)],
            };
            return playerClass;

        }
        private static IEnumerable<PlayerAttribute> GetPlayerAttributes(JObject data)
        {
            List<PlayerAttribute> playerAttributes = new List<PlayerAttribute>();
            foreach (JToken token in (JArray)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Attributes)])
            {
                playerAttributes.Add(new PlayerAttribute((string)token[nameof(PlayerAttribute.Key)],
                                                         (string)token[nameof(PlayerAttribute.DisplayName)],
                                                         (int)token[nameof(PlayerAttribute.Value)]));
            }
            return playerAttributes;
        }

        private static void PopulatePlayerInventory(JObject data, Player player)
        {
            string fileVersion = FileVersion(data);
            switch (fileVersion)
            {
                case "0.1.000":
                    foreach (JToken itemToken in (JArray)data[nameof(GameSession.CurrentPlayer)]
                                                             [nameof(Player.Inventory)]
                                                             [nameof(Inventory.Items)])
                    {
                        int itemId = (int)itemToken[nameof(GameItem.ItemTypeId)];
                        player.AddItemToInventory(GameItemFactory.CreateGameItem(itemId));
                    }
                    break;
                default:
                    throw new InvalidDataException($"File version '{fileVersion}' not recognized");
            }
        }

        private static void PopulatePlayerQuests(JObject data, Player player)
        {
            string fileVersion = FileVersion(data);
            switch (fileVersion)
            {
                case "0.1.000":
                    foreach (JToken questToken in (JArray)data[nameof(GameSession.CurrentPlayer)]
                                                             [nameof(Player.Quests)])
                                                             
                    {
                        int questId = (int)questToken[nameof(QuestStatus.PlayerQuest)][nameof(QuestStatus.PlayerQuest.Id)];
                        Quest quest = QuestFactory.GetQuestById(questId);
                        QuestStatus questStatus = new QuestStatus(quest);
                        questStatus.IsCompleted = (bool)questToken[nameof(QuestStatus.IsCompleted)];
                        player.Quests.Add(questStatus);                       
                    }
                    break;
                default:
                    throw new InvalidDataException($"File version '{fileVersion}' not recognized");
            }
        }

        
        private static void PopulatePlayerRecipe(JObject data, Player player)
        {
            string fileVersion = FileVersion(data);
            switch (fileVersion)
            {
                case "0.1.000":
                    foreach (JToken recipeToken in (JArray)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Recipes)])                                                             
                    {
                        int recipeId = (int)recipeToken[nameof(Recipe.Id)];
                        Recipe recipe = RecipeFactory.RecipeById(recipeId);
                        player.Recipes.Add(recipe);
                    }
                    break;
                default:
                    throw new InvalidDataException($"File version '{fileVersion}' not recognized");
            }
        }

        private static string FileVersion(JObject data)
        {
            return (string)data[nameof(GameSession.GameDetails.Version)];
        }
    }
}
