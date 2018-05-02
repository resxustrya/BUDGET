using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BUDGET.Models
{
    public class BudgetSourceFund
    {
        [Key]
        public Int32 ID { get; set; }  
        public String prexc { get; set; }
        public Int32 Line { get; set; }
       
        public Int32 allotment { get; set;}
    }
}