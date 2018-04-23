using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUDGET.Models
{
    public class PREXC
    {
        [Key]
        public Int32 ID { get; set; }
        public Int32 Line { get; set; }
        public String Desc { get; set; }
        public String Code1 { get; set; }
        public String Code2 { get; set; }
        public String Yearly { get; set; }
    }
}