using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BUDGET.Models
{
    public class PersonnelServices
    {
        [Key]
        [Column(Order=0)]
        public Int32 ID { get; set; }
        public Int32 Line { get; set; }
        [Required]
        public String Particulars { get; set; }
        [Required]
        public String UACS { get; set; }
        public Double STO_Operations { get; set; }
        public Double PHM { get; set; }
        public Double RRHFS { get; set; }
    }
}