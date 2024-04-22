using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPWZ.components.objects
{
    class Object_tg_chat
    {
        [Key]
        public string? Num_phone {  get; set; }
        public string? Id_chat_tg {  get; set; }

        public Object_tg_chat() { }
        public Object_tg_chat(string num_phone) 
        {
            Num_phone = num_phone;
        }
    }
}
