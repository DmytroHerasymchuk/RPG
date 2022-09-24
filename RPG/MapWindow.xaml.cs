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
using RPG.CustomConverters;

namespace RPG
{
    /// <summary>
    /// Логика взаимодействия для MapWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        public GameSession Session => DataContext as GameSession;

        public MapWindow(GameSession gameSession)
        {            
            
            InitializeComponent();
            InitializeMap(gameSession);
        }

        private void OnClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void InitializeMap(GameSession gameSession)
        {
            foreach(MapPiece mapPiece in gameSession.CurrentWorld.MapPieces)
            {
                
                Image image = new Image();
                image.Source = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}{mapPiece.ImageName}",
                                                       UriKind.Absolute));
                image.Stretch = Stretch.Fill;
                if (mapPiece.YCoordinate > 0)
                {
                    Grid.SetColumn(image, mapPiece.XCoordinate + 11);
                    Grid.SetRow(image, -mapPiece.YCoordinate + 11);
                }
                else
                {
                    Grid.SetColumn(image, mapPiece.XCoordinate + 11);
                    Grid.SetRow(image, mapPiece.YCoordinate + 11);
                }
                if (mapPiece.Status)
                {
                    image.Visibility = Visibility.Visible;
                }
                else
                {
                    image.Visibility = Visibility.Hidden;
                }
                
                map.Children.Add(image);
            }
        }

        private void OnClickShowPlayer(object sender, RoutedEventArgs e)
        {
            Rectangle rect = new Rectangle();
            
            rect.StrokeThickness = 4;
            rect.Stroke = new SolidColorBrush(Color.FromRgb(246, 252, 70));
            int x = Session.CurrentLocation.XCoordinate;
            int y = Session.CurrentLocation.YCoordinate;
            if (y > 0)
            {
                Grid.SetColumn(rect, x + 11);
                Grid.SetRow(rect, -y + 11);
            }
            else
            {
                Grid.SetColumn(rect, x + 11);
                Grid.SetRow(rect, y + 11);
            }
            
            map.Children.Add(rect);
        }
    }
}
