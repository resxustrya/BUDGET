using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BUDGET.Models
{
    public class MOOEs
    {

        [Key]
        [Column(Order = 0)]
        public Int32 ID { get; set; }
        
        public Int32 Line { get; set; }
        [Required]
        public String Paraticulars { get; set; }
        [Required]
        public String UACS { get; set; }
        public Double STO_Operations { get; set; } 
        public Double PHM { get; set; }
        public Double RRHFS { get; set; }
        public Double HSRD { get; set; }
        public Double LHSDA { get; set; }
        public Double HRHICM { get; set; }
        public Double HRDP { get; set; }
        public Double HP { get; set; }
        public Double ES { get; set; }
        public Double HEPR { get; set; }
        public String Year { get; set; }
    }
}