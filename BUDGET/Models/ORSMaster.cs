using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUDGET.Models
{
    public class ORSMaster
    {
        [Key]
        public Int32 ID { get; set; }
        public String Title { get; set; }
        public String Year { get; set; }
    }
}