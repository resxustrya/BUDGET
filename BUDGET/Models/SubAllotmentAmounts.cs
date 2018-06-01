using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BUDGET.Models
{
    public class SubAllotmentAmounts
    {
        [Key]
        public Int32 ID { get; set; }
        public String expensecode { get; set; }
        public Double amount { get; set; }
        public String fundsource { get; set; }
    }
}