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
using Models;
using ViewModels;

namespace RPG
{
    /// <summary>
    /// Логика взаимодействия для TradeScreen.xaml
    /// </summary>
    public partial class TradeScreen : Window
    {
        public GameSession Session => DataContext as GameSession;
        public TradeScreen()
        {
            InitializeComponent();
        }

        private void OnClickSell(object sender, RoutedEventArgs e)
        {
            GroupedInventoryItem groupedInventoryItem = ((FrameworkElement)sender).DataContext as GroupedInventoryItem;
            if(groupedInventoryItem != null)
            {
                Session.CurrentPlayer.ReceiveGold(groupedInventoryItem.Item.ModifiedPrice);
                Session.CurrentTrader.AddItemToInventory(groupedInventoryItem.Item);
                Session.CurrentPlayer.RemoveItemFromInventory(groupedInventoryItem.Item);
                Session.CurrentPlayer.Inventory.UpdateModifiedPricePlayer(Session.CurrentPlayer);
                Session.CurrentTrader.Inventory.UpdateModifiedPriceTrader(Session.CurrentPlayer);

            }
        }

        private void OnClickBuy(object sender, RoutedEventArgs e)
        {
            GroupedInventoryItem groupedInventoryItem = ((FrameworkElement)sender).DataContext as GroupedInventoryItem;
            if (groupedInventoryItem != null)
            {
                if (Session.CurrentPlayer.Gold < groupedInventoryItem.Item.ModifiedPrice)
                {
                    MessageBox.Show("You don't have enough gold");
                }
                else
                {
                    Session.CurrentPlayer.SpendGold(groupedInventoryItem.Item.ModifiedPrice);
                    Session.CurrentTrader.RemoveItemFromInventory(groupedInventoryItem.Item);
                    Session.CurrentPlayer.AddItemToInventory(groupedInventoryItem.Item);
                    Session.CurrentTrader.Inventory.UpdateModifiedPriceTrader(Session.CurrentPlayer);
                    Session.CurrentPlayer.Inventory.UpdateModifiedPricePlayer(Session.CurrentPlayer);
                }   
            }
        }

        private void OnClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
