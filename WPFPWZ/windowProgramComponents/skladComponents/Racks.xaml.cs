using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFPWZ
{
    /// <summary>
    /// Логика взаимодействия для Racks.xaml
    /// </summary>
    public partial class Racks : UserControl
    {
        public event RoutedEventHandler Add_cell_click;
        public event RoutedEventHandler Delate_cell_click;
        public event RoutedEventHandler Open_click;
        public event RoutedEventHandler Regist_сlick;
        public Racks(int id, int kol_cell)
        {
            InitializeComponent();
            rack.Text = "Стеллаж № " + id;
            cell.Text = "Всего ячеек " + kol_cell;
        }
        

        private void Btn_Regist_click(object sender, RoutedEventArgs e)
        {
            if (Regist_сlick != null) { Add_cell_click.Invoke(sender, e); }
        }
        private void Btn_add_cell_click(object sender, RoutedEventArgs e)
        {
            if (Add_cell_click != null) { Add_cell_click(sender, e); }
        }

        private void Btn_delate_cell_click(object sender, RoutedEventArgs e)
        {
            if (Delate_cell_click != null) { Delate_cell_click.Invoke(sender, e); }
        }
        private void Btn_open_click(object sender, RoutedEventArgs e)
        {
            if (Open_click != null) { Open_click.Invoke(sender, e); }
        }

        //public Racks(int id, int kol_cell) 
        //{
        //    this.id = id;
        //    this.kol_cell = kol_cell;
        //}

    }
}
