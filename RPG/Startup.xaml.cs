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
using Engine.Services;

namespace RPG
{
    /// <summary>
    /// Логика взаимодействия для Startup.xaml
    /// </summary>
    public partial class Startup : Window
    {
        private GameDetails _gameDetails;
        public Startup()
        {
            InitializeComponent();
            _gameDetails = GameDetailsService.ReadGameDetails();
            DataContext = _gameDetails;
        }

        private void OnClickStartNewGame(object sender, RoutedEventArgs e)
        {
            CharacterCreation characterCreationWindow = new CharacterCreation();
            characterCreationWindow.Show();
            Close();
        }

        private void OnClickExit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
