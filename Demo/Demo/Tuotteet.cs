using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class Tuotteet
    {
        [Key]
        public int int_id {  get; set; }
        public int? varastosaldo { get; set; }
        public string? tuotenimi { get; set; }
        public int? tuotehinta { get; set; }
        
    }
}
