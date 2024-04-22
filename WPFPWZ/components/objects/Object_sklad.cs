using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPWZ
{
    internal class Object_sklad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_rackAndCell { get; set; }

        [Required]
        public int Id_rack { get; set; }
        [Required]
        public int Id_cell { get; set; }
        public int Id_order { get; set; }

        public Object_sklad() { }
        public Object_sklad(int id_rack, int _id_cell)
        {
            Id_rack = id_rack;
            Id_cell = _id_cell;
        }
        public Object_sklad(int id_rack, int _id_cell, int id_order)
        {
            Id_rack = id_rack;
            Id_cell = _id_cell;
            Id_order = id_order;
        }
        public Object_sklad(int id_rackAndCell, int id_rack, int _id_cell, int id_order)
        {
            Id_rackAndCell = id_rackAndCell;
            Id_rack = id_rack;
            Id_cell = _id_cell;
            Id_order = id_order;
        }

    }
}
