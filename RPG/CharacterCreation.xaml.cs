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
using Engine.ViewModels;

namespace RPG
{
    /// <summary>
    /// Логика взаимодействия для CharacterCreation.xaml
    /// </summary>
    public partial class CharacterCreation : Window
    {
        private CharacterCreationViewModel ViewModel { get; set; }
        public CharacterCreation()
        {
            InitializeComponent();
            ViewModel = new CharacterCreationViewModel();
            DataContext = ViewModel;
        }

        private void OnClickStart(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(ViewModel.GetPlayer());
            mainWindow.Show();
            Close();
        }

        private void OnClickIncrementAttribute(object sender, RoutedEventArgs e)
        {
            PlayerAttribute playerAttribute = ((FrameworkElement)sender).DataContext as PlayerAttribute;
            if (ViewModel.AttributePoints >= 1)
            {
                playerAttribute.Increment();
                ViewModel.AttributePoints--;
            }          
        }

        private void OnClickDecrementAttribute(object sender, RoutedEventArgs e)
        {
            PlayerAttribute playerAttribute = ((FrameworkElement)sender).DataContext as PlayerAttribute;
            if (playerAttribute.Value > 1)
            {
                playerAttribute.Decrement();
                ViewModel.AttributePoints++;
            }           
        }

        private void OnClickSetDefault(object sender, RoutedEventArgs e)
        {
            ViewModel.CreateNewCharacter();
        }
        
    }
}
