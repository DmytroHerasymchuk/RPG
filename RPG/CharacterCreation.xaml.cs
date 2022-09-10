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
using ViewModels.ViewModels;
using Models.EventArgs;

namespace RPG
{
    /// <summary>
    /// Логика взаимодействия для CharacterCreation.xaml
    /// </summary>
    public partial class CharacterCreation : Window
    {
        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();
        private CharacterCreationViewModel ViewModel { get; set; }
        public CharacterCreation()
        {
            InitializeComponent();
            ViewModel = new CharacterCreationViewModel();
            DataContext = ViewModel;
            _messageBroker.OnMessageRaised += OnGameMessageRaised;
        }

        private void OnClickStart(object sender, RoutedEventArgs e)
        {
            if (Name.Text != "")
            {
                MainWindow mainWindow = new MainWindow(ViewModel.GetPlayer());
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("You must choose a name", "Name");
            }
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

        private void OnClickOpenMainMenu(object sender, RoutedEventArgs e)
        {
            Startup startup = new Startup();
            startup.Show();
            Close();
        }

        private void OnClickSetDefault(object sender, RoutedEventArgs e)
        {
            ViewModel.CreateNewCharacter();
        }

        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            CreationMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            CreationMessages.ScrollToEnd();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreationMessages.Document.Blocks.Clear();
            ViewModel.GetClassInfo(ViewModel.SelectedClass.Key);
        }
    }
}
