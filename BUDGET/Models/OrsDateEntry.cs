using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BUDGET
{
    public class OrsDateEntry
    {
        [Key]
        public Int32 ID { get; set; }
        public String ExpenseTitle { get; set; }
        public String ExpenseCode { get; set; }
        public Int32 ors_id { get; set; }
        public DateTime Date { get; set; }
        public String StringDate { get; set; }
        public Double amount { get; set; }
        public Double NetAmount { get; set; }
        public Double TaxAmount { get; set; }
        public Double Others { get; set; }

    }
}