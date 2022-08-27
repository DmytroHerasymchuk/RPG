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

namespace RPG.Windows
{
    /// <summary>
    /// Логика взаимодействия для YesNo.xaml
    /// </summary>
    public partial class YesNo : Window
    {
        public bool ClickedYes { get; private set; }
        public YesNo(string title, string message)
        {
            InitializeComponent();
            Title = title;
            Message.Content = message;
        }

        private void YesOnClick(object sender, RoutedEventArgs e)
        {
            ClickedYes = true;
            Close();
        }

        private void NoOnClick(object sender, RoutedEventArgs e)
        {
            ClickedYes = false;
            Close();
        }
    }
}
