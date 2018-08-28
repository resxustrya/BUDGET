using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BUDGET
{
    public class ExpenseCodeAllotment
    {
        [Key]
        public Int32 ID { get; set; }
        public Int32 fundsource { get; set; }
        public String from_uacs { get; set; }
        public String description { get; set; }


    }
}