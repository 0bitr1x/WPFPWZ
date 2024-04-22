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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPFPWZ
{
    /// <summary>
    /// Логика взаимодействия для Admin_Panel.xaml
    /// </summary>
    public partial class Admin_Panel : Page
    {
        DbConnect dbConnect;
        public Admin_Panel()
        {
            InitializeComponent();
            InitializeDbContext();
            LoadDataFromDatabase();
        }
        private void InitializeDbContext()
        {
            dbConnect = new DbConnect();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (pass.Password != relPass.Password)
            {
                triger.Text = "пароли не совпадают";
                triger.Foreground = Brushes.Red;
            }
            else
            if (login.Text.Replace(" ", "") == "")
            {
                triger.Text = "заполните логин";
                triger.Foreground = Brushes.Red;
            }
            else
            if (pass.Password.Replace(" ", "") == "")
            {
                triger.Text = "заполните пароль";
                triger.Foreground = Brushes.Red;
            }
            else
            {
                using (DbConnect db = new DbConnect())
                {
                    User user=db.Users.FirstOrDefault(p=>p.Login==login.Text)!;
                    if (user == null) 
                    {
                        User newUser = new User(login.Text, pass.Password, 2);
                        db.Users.Add(newUser);
                        db.SaveChangesAsync();
                        triger.Text = "пользователь создан";
                        triger.Foreground = Brushes.Green;
                        login.Text = "";
                        pass.Password = "";
                        relPass.Password = "";
                    }
                    else
                    {
                        triger.Text = "пользователь сушествует";
                        triger.Foreground = Brushes.Red;
                        login.Text = "";
                        pass.Password = "";
                        relPass.Password = "";
                    }
                }
            }

        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
             var edit = dbConnect.Defailt_Life.First();
             if (Regex.IsMatch(life.Text, @"^\d+$"))
             {
                edit.id_default_life = life.Text;
                dbConnect.SaveChanges();
                life.Text = "";
                triger2.Foreground = Brushes.Green;
                triger2.Text = "значение измененно!";
                LoadDataFromDatabase();
            }
            else
            {
                triger2.Text = "некорректный ввод";
                triger2.Foreground = Brushes.Red;
            }
        }
        private void LoadDataFromDatabase()
        {
            Defailt_life_order defailt_Life = dbConnect.Defailt_Life.First();
            def_num.Text = defailt_Life.id_default_life.ToString() + "дней";
        }
    }
}
