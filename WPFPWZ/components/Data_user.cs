using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFPWZ
{
    public class Data_user
    {
        private static User? save_data_user;

        public static void SetData(string login, string password, int id_role)
        {

            save_data_user = new User
            {
                Login = login,
                Password = password,
                Id_role = id_role
            };
        }

        public static User GetData()
        {
            return save_data_user!;
        }

    }
}
