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
using System.Windows.Shapes;
using Engine.Models;
using Engine.ViewModels;
using Engine.Services;
using Microsoft.Win32;
using RPG.Windows;

namespace RPG
{
    /// <summary>
    /// Логика взаимодействия для Startup.xaml
    /// </summary>
    public partial class Startup : Window
    {
        private const string SAVE_GAME_FILE_EXTENSION = "save";
        private GameDetails _gameDetails;
        public Startup()
        {
            InitializeComponent();
            _gameDetails = GameDetailsService.ReadGameDetails();
            DataContext = _gameDetails;
        }

        private void OnClickStartNewGame(object sender, RoutedEventArgs e)
        {
            //if (App.Current.Windows.Count > 1)
            //{
            //    App.Current.Windows[0].Close();
            //}
            CharacterCreation characterCreationWindow = new CharacterCreation();
            characterCreationWindow.Show();
            Close();
        }

        private void OnClickExit(object sender, RoutedEventArgs e)
        {
            YesNo message = new YesNo("Exit Game", "Are you sure?");
            message.Owner = GetWindow(this);
            message.ShowDialog();
            if (message.ClickedYes)
            {
                foreach(Window w in App.Current.Windows)
                {
                    w.Close();
                }
            }           
        }

        private void OnClickHelp(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();            
            helpWindow.ShowDialog();
        }

        private void OnClickLoadGame(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = $"Saved games (*.{SAVE_GAME_FILE_EXTENSION})|*.{SAVE_GAME_FILE_EXTENSION}"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                GameState gameState = SaveGameService.LoadLastSaveOrCreateNew(openFileDialog.FileName);
                
                
                foreach(Window window in App.Current.Windows)
                {
                    if (!window.IsActive)
                    {
                        window.Close();
                    }                   
                }
                MainWindow mainWindow = new MainWindow(gameState.Player,
                                                       gameState.XCoordinate,
                                                       gameState.YCoordinate);
                mainWindow.Show();
                Close();
            }
        }

        private void OnClickContinue(object sender, RoutedEventArgs e)
        {
            Close();
        }

       

    }
}
