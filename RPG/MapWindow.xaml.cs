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
        public GameSession Session { get; set; }

        public MapWindow(GameSession gameSession)
        {            
            Session = gameSession;
            InitializeComponent();
            InitializeMap();
        }

        private void OnClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void InitializeMap()
        {
            foreach(MapPiece mapPiece in Session.CurrentWorld.MapPieces)
            {
                
                Image image = new Image();
                image.Source = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}{mapPiece.ImageName}",
                                                       UriKind.Absolute));
                Grid.SetColumn(image, mapPiece.XCoordinate);
                Grid.SetRow(image, mapPiece.YCoordinate);
                map.Children.Add(image);
            }
        }
    }
}
