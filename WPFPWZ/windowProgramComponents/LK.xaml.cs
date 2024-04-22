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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFPWZ
{
    /// <summary>
    /// Логика взаимодействия для LK.xaml
    /// </summary>
    public partial class LK : Page
    {
        public LK()
        {
            InitializeComponent();
            User DATA = Data_user.GetData();
            username.Text = DATA.Login;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            using (DbConnect db = new DbConnect())
            {
                //получение данных с класса храняший данные аккаунта
                //Data_user data_user = new Data_user();
                User DATA = Data_user.GetData();


                User? user = db.Users.FirstOrDefault(p => p.Login == DATA.Login && p.Password == DATA.Password);
                if(oldPass.Password == "" || newPass.Password == "")
                {
                    triger.Text = "пароли совпадают";
                }
                if (oldPass.Password == newPass.Password)
                {
                    triger.Text = "пароли совпадают";
                }
                else
                if (DATA.Password != oldPass.Password)
                {
                    triger.Text = "неверный старый пароль";
                }
                else
                if (DATA.Password == newPass.Password)
                {
                    triger.Text = "новый пароль должен отличаться от старого";
                }
                else
                { 
                    user!.Password = newPass.Password;
                    db.SaveChanges();
                    oldPass.Password = "";
                    newPass.Password = "";
                    triger.Text = "пароль изменён";
                    triger.Foreground = Brushes.Green;
                    
                }

            }
        }
    }
}
