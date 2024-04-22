using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WPFPWZ
{
    /// <summary>
    /// Логика взаимодействия для Sklad.xaml
    /// </summary>
    public partial class Sklad : Page
    {
        private DbConnect dbConnect;


        public Sklad()
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
            var groups = dbConnect.Sklad.GroupBy(u => u.Id_rack).Select(g => new
            {
                Rack = g.Key,
                Cells = g.Count()
            });
            foreach (var group in groups)
            {
                Create_rack(group.Rack, group.Cells);
            }
        }

        private void AddCellClick(object sender, RoutedEventArgs e)
        {
            int id_rack = Search_index_rack(sender, e);
            using (DbConnect db = new DbConnect())
            {
                int cellCount = db.Sklad.Count(u => u.Id_rack == id_rack);
                Object_sklad nowCell = new Object_sklad(id_rack, cellCount + 1);
                db.Sklad.Add(nowCell);
                db.SaveChanges();
                stelazhContainer.Children.Clear();
                InitializeDbContext();
                LoadDataFromDatabase();
            }
        }
        private void DelateCellClick(object sender, RoutedEventArgs e)
        {
            int id_rack = Search_index_rack(sender, e);
            using (DbConnect db = new DbConnect())
            {
                int cellCount = db.Sklad.Count(u => u.Id_rack == id_rack);
                if(cellCount == 1)
                {
                    MessageBox.Show("последнюю ячейку удалить нельзя");
                    return;
                } else
                {
                    Object_sklad delCell = db.Sklad.FirstOrDefault(u => u.Id_rack == id_rack && u.Id_cell == cellCount)!;
                    db.Sklad.Remove(delCell);
                    db.SaveChanges();
                    stelazhContainer.Children.Clear();
                    InitializeDbContext();
                    LoadDataFromDatabase();
                }
            }
        }
        private void OpenCellClick(object sender, RoutedEventArgs e)
        {

        }
        private void Btn_add(object sender, RoutedEventArgs e)
        {
            using (DbConnect db = new DbConnect())
            {
                int id_rack = db.Sklad.Select(u => u.Id_rack).Distinct().Count();
                Object_sklad rack = new Object_sklad(id_rack + 1, 1);
                db.Sklad.Add(rack);
                db.SaveChanges();
                stelazhContainer.Children.Clear();
                InitializeDbContext();
                LoadDataFromDatabase();
            }
        }
        private void Btn_delate(object sender, RoutedEventArgs e)
        {
            using (DbConnect db = new DbConnect())
            {
                int id_rack = db.Sklad.Select(u => u.Id_rack).Distinct().Count();
                Object_sklad delCell = db.Sklad.FirstOrDefault(u => u.Id_rack == id_rack)!;
                db.Sklad.Remove(delCell);
                db.SaveChanges();
                stelazhContainer.Children.Clear();
                InitializeDbContext();
                LoadDataFromDatabase();
            }
        }
    
        private void Btn_reg_click(object sender, RoutedEventArgs e)
        {

        }
        private int Search_index_rack(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender; // Получаем ссылку на нажатую кнопку
            //получаем родительские элэменты под дереву иэрархии
            Grid rod1 = (Grid)clickedButton.Parent;
            Grid rod2 = (Grid)rod1.Parent;
            Grid rod3 = (Grid)rod2.Parent;
            Border rod4 = (Border)rod3.Parent;
            Grid rod5 = (Grid)rod4.Parent;
            Racks rod6 = (Racks)rod5.Parent;

            int index = stelazhContainer.Children.IndexOf(rod6); // Получаем индекс нажатой кнопки
            return index + 1;
        }
        private int Search_count_cell(int id_rack)
        {
            var groups = dbConnect.Sklad.GroupBy(u => u.Id_rack == id_rack).Select(g => new
            {
                Rack = g.Key,
                Cells = g.Count()
            });
            return groups.Count();
        }

        private void Create_rack(int id, int kol_cell)
        {  
            Racks racks = new Racks(id, kol_cell);
            stelazhContainer.Children.Add(racks);
            racks.Add_cell_click += AddCellClick;
            racks.Delate_cell_click += DelateCellClick;
            racks.Open_click += OpenCellClick;
        }
    }
}
