using MaterialDesignThemes.Wpf;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            using (DbConnect db =  new DbConnect())
            {
                User? user = db.Users.FirstOrDefault(p => p.Login == login.Text.Replace(" ", ""));
                //сохранение
                //User? user = db.Users.FirstOrDefault(p => p.Login == login.Text.Replace(" ", "") && p.Password == pass.Password);
                if(login.Text == "" || pass.Password == "")
                {
                    triger.Text = "Введите логин и пароль";
                } else
                if (user == null)
                {
                    triger.Text = "такого аккаунта нету";
                } else
                if(pass.Password != user.Password)
                {
                    triger.Text = "неверный пароль";
                } else
                {
                    string? name = user.Login;
                    string? password = user.Password;
                    int role = user.Id_role;
                    // передача данных
                    Data_user.SetData(name!, password!, role);
                    //
                    var UserWin = new WindowProgram();
                    UserWin.Show();
                    this.Close();
                }
            }

        }
    }
}