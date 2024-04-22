using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using WPFPWZ.components.objects;

namespace WPFPWZ
{
    /// <summary>
    /// Логика взаимодействия для Accept_order.xaml
    /// </summary>
    public partial class Accept_order : Window
    {
        private DbConnect dbConnect;
        List<Object_sklad> sklad;
        public Accept_order()
        {
            InitializeComponent();
            InitializeDbContext();
            LoadDataFromDatabase();
        }
        private void InitializeDbContext()
        {
            dbConnect = new DbConnect();
        }
        private void LoadDataFromDatabase()
        {
            sklad = dbConnect.Sklad.Where(u => u.Id_order == 0).ToList();
            


            foreach (var i in sklad)
            {
                rackAndCell.Items.Add("Стеллаж " + i.Id_rack + ", " + "ячейка " + i.Id_cell);
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(idPhone.Text, "^9\\d{9}$") == false)
            {
                triger.Text = "некорректный номер телефона";
            } else 
            if(Regex.IsMatch(idPhone.Text, "^\\d+$") == false)
            {
                triger.Text = "неверно введён срок";
            } else
            {
                Regex regex = new Regex(@"\d+");
                MatchCollection collection = regex.Matches(rackAndCell.Text);
                DateTime start = DateTime.Now;
                DateTime end = start.AddDays(int.Parse(srok.Text));
                Object_sklad id = dbConnect.Sklad.FirstOrDefault(u => u.Id_rack == int.Parse(collection[0].Value) && u.Id_cell == int.Parse(collection[1].Value))!;
                int id_rackAndCell = id.Id_rackAndCell;
                Object_Orders orders = new Object_Orders(int.Parse(idOrder.Text), id_rackAndCell, idPhone.Text, Data_user.GetData().Login, start, end, 1);
                dbConnect.History_orders.Add(orders);
                //dbConnect.Tg_bot.Add(new Object_tg_chat(idPhone.Text));

                Object_sklad cell = dbConnect.Sklad.FirstOrDefault(u => u.Id_rack == int.Parse(collection[0].Value) && u.Id_cell == int.Parse(collection[1].Value))!;
                cell.Id_order = int.Parse(idOrder.Text);
                Object_sklad text = new Object_sklad(cell.Id_rackAndCell, cell.Id_rack, cell.Id_cell, cell.Id_order);
                dbConnect.SaveChanges();
                MessageBox.Show("заказ добавлен");
                this.Close();
            }
        }
    }
}
