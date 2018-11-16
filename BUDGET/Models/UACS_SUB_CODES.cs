using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BUDGET
{
    public class UACS_SUB_CODES
    {
        [Key]
        public Int32 ID { get; set; }
        public Int32 headerID { get; set; }
        public String ExpenseTitle { get; set; }
        public String ExpenseCode { get; set; }
        public Double Amount { get; set; }
    }
}