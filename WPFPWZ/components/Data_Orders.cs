using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPWZ.components.objects;

namespace WPFPWZ
{
    internal class Data_Orders
    {
        private static List<Object_Orders> Save_Data_Orders;

        //int id_order, int id_rackAndCell, string? num_phone, string? user_Add,
        //    DateTime datetime_accept, DateTime dateTime_end, string? user_remove, DateTime datetime_remove, int id_status
        public static void SetData(List<Object_Orders> save_data_orders)
        {
            Save_Data_Orders = new List<Object_Orders>
            {
                

            };
            ////save_data_sklad = new Object_sklad
            //{
            //    Id_rack = id_rack,
            //    Id_cell = id_cell,
            //    Id_order = id_order,
            //    //Id_order_life = id_order_life
            //};
        }

        public static List<Object_Orders> GetData()
        {
            return Save_Data_Orders;
        }

    }
}
