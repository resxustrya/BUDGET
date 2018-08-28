using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BUDGET
{
    public class ExpenseAllotmentExpenseCodes
    {
        [Key]
        public Int32 ID { get; set; }
        public Int32 ExpenseCodeAllotment { get; set; }
        public String uacs { get; set; }
        public Double amount { get; set; }

    }
}