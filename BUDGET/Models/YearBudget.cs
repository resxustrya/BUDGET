using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUDGET
{
    public class YearBudget
    {
       [Key]
        public Int32 ID { get; set; }
        public Int32 Year { get; set; }
        public String CreatedBy { get; set; }
        public Int32 active { get; set; }
    }
}