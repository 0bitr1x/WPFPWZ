using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WPFPWZ.components.objects;
using static System.Net.Mime.MediaTypeNames;

namespace WPFPWZ
{
    /// <summary>
    /// Логика взаимодействия для Page_Orders.xaml
    /// </summary>
    public partial class Page_Orders : Page
    {
        private DbConnect _dbConnect;
        public Page_Orders()
        {
            InitializeComponent();
            InitializeDbContext();
            LoadDataFromDatabase();
        }
        private void InitializeDbContext()
        {
            _dbConnect = new DbConnect();
        }
        private void LoadDataFromDatabase()
        {
            var _sklad = _dbConnect.Sklad.Where(e => e.Id_order != 0);
            var _history_orders = _dbConnect.History_orders.Where(e => e.Id_status == 1);
            var groups = _history_orders.Join(_sklad, x => x.Id_order, y => y.Id_order, (x, y) => new {
                Id_order = x.Id_order,
                Num_phone = x.Num_phone,
                Id_rack = y.Id_rack,
                Id_cell = y.Id_cell,
                Data_end = x.Data_end.ToLongDateString()
            });
                List<Row_Object_Accept_Orders> list_orders = new List<Row_Object_Accept_Orders>();
            foreach (var group in groups) 
            {
                Row_Object_Accept_Orders order = new Row_Object_Accept_Orders
                {
                    Id_order = group.Id_order,
                    Num_phone = group.Num_phone,
                    Rack = group.Id_rack,
                    Cell = group.Id_cell,
                    Data_end = group.Data_end
                };
                list_orders.Add(order);
                //var list_orders = (group.Id_order.ToString(), group.Num_phone, group.Id_rack.ToString(), group.Id_cell.ToString(), group.Data_end.ToString());
            }
            foreach(var row in list_orders)
            {
                datagrid_panel.Items.Add(row);
            }
            
        }

        private void TabControl_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Doc_Open_Click(object sender, RoutedEventArgs e)
        {
            Row_Object_Accept_Orders selectedItem = (Row_Object_Accept_Orders)datagrid_panel.SelectedItem;
            if (selectedItem != null)
            {
                
            }


            //Process.Start("WINWORD.EXE", fileName);
        }
        private void accept_order_Click(object sender, RoutedEventArgs e)
        {
            var accept_order_window = new Accept_order();
            accept_order_window.ShowDialog();
            datagrid_panel.Items.Clear();
            LoadDataFromDatabase();
        }

        private void give_order_Click(object sender, RoutedEventArgs e)
        {
            Row_Object_Accept_Orders selectedItem = (Row_Object_Accept_Orders)datagrid_panel.SelectedItem;
            if(selectedItem != null)
            {
                var delate_row = selectedItem.Id_order;
                //int selectedIndex = datagrid_panel.SelectedIndex;
                //var delate_row = datagrid_panel.Items[selectedIndex];
                var cell_order = (_dbConnect.History_orders.FirstOrDefault(e => e.Id_order == delate_row));
                cell_order.Id_status = 2;
                _dbConnect.SaveChanges();
                MessageBox.Show("заказ выдан");
                datagrid_panel.Items.Clear();
                LoadDataFromDatabase();
            }
            else
            {
                MessageBox.Show("Ни одна строка не выбрана.");
            }
        }
    }
}
