using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Engine.ViewModels;
using Models.EventArgs;
using Engine.Models;
using Engine.Services;
using System.ComponentModel;
using Microsoft.Win32;
using RPG.Windows;

namespace RPG
{
    public partial class MainWindow : Window
    {
        private const string SAVE_GAME_FILE_EXTENSION = "save";
        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();
        private GameSession _gameSession;
        private readonly Dictionary<Key, Action> _userInputActions = new Dictionary<Key, Action>();
        public MainWindow(Player player, int xLocation = 0, int yLocation = 0)
        {
            InitializeComponent();
            InitializeUserInputActions();
            SetActiveGameSessionTo(new GameSession(player, xLocation, yLocation));
        }   
        private void OnClickGoNorth(object sender, RoutedEventArgs e)
        {
            _gameSession.GoToNorth();
        }

        private void OnClickGoSouth(object sender, RoutedEventArgs e)
        {
            _gameSession.GoToSouth();
        }
        
        private void OnClickGoWest(object sender, RoutedEventArgs e)
        {
            _gameSession.GoToWest();
        }

        private void OnClickGoEast(object sender, RoutedEventArgs e)
        {
            _gameSession.GoToEast();
        }

        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }

        private void OnCLickAttackMonster(object sender, RoutedEventArgs e)
        {
            _gameSession.AttackCurrentMonster();
        }

        private void OnCLickUseCurrentConsumable(object sender, RoutedEventArgs e)
        {
            _gameSession.UseCurrentConsumable();
        }

        private void OnCLickCraft(object sender, RoutedEventArgs e)
        {
            Recipe recipe = ((FrameworkElement)sender).DataContext as Recipe;
            _gameSession.CraftItem(recipe);

        }
        private void OnClickDisplayTradeScreen(object sender, RoutedEventArgs e)
        {
            if (_gameSession.CurrentTrader != null)
            {
                TradeScreen tradeScreen = new TradeScreen();
                tradeScreen.Owner = this;
                tradeScreen.DataContext = _gameSession;
                tradeScreen.ShowDialog();
            }
        }

        private void InitializeUserInputActions()
        {
            _userInputActions.Add(Key.W, () => _gameSession.GoToNorth());
            _userInputActions.Add(Key.A, () => _gameSession.GoToWest());
            _userInputActions.Add(Key.S, () => _gameSession.GoToSouth());
            _userInputActions.Add(Key.D, () => _gameSession.GoToEast());
            _userInputActions.Add(Key.Z, () => _gameSession.AttackCurrentMonster());
            _userInputActions.Add(Key.C, () => _gameSession.UseCurrentConsumable());
            _userInputActions.Add(Key.I, () => SetTabFocusTo("InventoryTabItem"));
            _userInputActions.Add(Key.Q, () => SetTabFocusTo("QuestsTabItem"));
            _userInputActions.Add(Key.R, () => SetTabFocusTo("RecipesTabItem"));
            _userInputActions.Add(Key.T, () => OnClickDisplayTradeScreen(this, new RoutedEventArgs()));

        }
        private void MainWindowOnKeyDown(object sender, KeyEventArgs e)
        {
            if (_userInputActions.ContainsKey(e.Key)){
                _userInputActions[e.Key].Invoke();
            }
        }

        private void SetTabFocusTo(string tabName)
        {
            foreach(object item in PlayerDataTabControl.Items)
            {
                if(item is TabItem tabItem)
                {
                    if(tabItem.Name == tabName)
                    {
                        tabItem.IsSelected = true;
                        return;
                    }
                }
            }
        }

        private void SetActiveGameSessionTo(GameSession gameSession)
        {
            _messageBroker.OnMessageRaised -= OnGameMessageRaised;
            _gameSession = gameSession;
            DataContext = _gameSession;
            GameMessages.Document.Blocks.Clear();
            _messageBroker.OnMessageRaised += OnGameMessageRaised;
        }


        private void MainWindowOnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            YesNo message = new YesNo("Save Game", "Do you want to save your game?");
            message.Owner = GetWindow(this);
            message.ShowDialog();
            if (message.ClickedYes)
            {
                SaveGame();
            }
        }
        private void OnClickSaveGame(object sender, RoutedEventArgs e)
        {
            SaveGame();
        }

        private void SaveGame()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = $"Saved games (*.{SAVE_GAME_FILE_EXTENSION})|*.{SAVE_GAME_FILE_EXTENSION}"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                SaveGameService.Save(new GameState(_gameSession.CurrentPlayer,
                                                   _gameSession.CurrentLocation.XCoordinate,
                                                   _gameSession.CurrentLocation.YCoordinate,
                                                   _gameSession.GameDetails.Version), saveFileDialog.FileName);
            }
        }

        private void OnMouseClickAddPoint(object sender, MouseButtonEventArgs e)
        {
            if(_gameSession.CurrentPlayer.IsLeveledUp)
            {
                PlayerAttribute playerAttribute = ((FrameworkElement)sender).DataContext as PlayerAttribute;
                playerAttribute.Increment();
                _gameSession.CurrentPlayer.AttributePoints--;
            }

        }

        private void OnCliclOpenMainMenu(object sender, RoutedEventArgs e)
        {
            Startup startup = new Startup();
            startup.ContinueButton.IsEnabled = true;
            startup.ShowDialog();
            
        }
    }
}
