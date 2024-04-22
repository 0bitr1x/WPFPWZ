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

namespace WPFPWZ
{
    /// <summary>
    /// Логика взаимодействия для WindowProgram.xaml
    /// </summary>
    public partial class WindowProgram : Window
    {
        public WindowProgram()
        {
            InitializeComponent();
            User DATA = Data_user.GetData();
            username.Text = DATA.Login;
            if (DATA.Id_role == 2)
            {
                admin_panel.Visibility = Visibility.Hidden;
                feedback.Visibility = Visibility.Hidden;
                
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }

        private void Sklad_Click(object sender, RoutedEventArgs e)
        {
            slider.Navigate(new Sklad());
        }
        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            slider.Navigate(new Page_Orders());
        }

        private void admin_panel_Click(object sender, RoutedEventArgs e)
        {
            slider.Navigate(new Admin_Panel());
        }
        public void Feedback_Click(object sender, RoutedEventArgs e)
        {
            //slider.Navigate(new Feedback());
        }

        private void lK_Click(object sender, RoutedEventArgs e)
        {
            slider.Navigate(new LK());
        }
    }
}
