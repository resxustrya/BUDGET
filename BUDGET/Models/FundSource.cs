using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BUDGET.Models
{
    public class FundSource
    {
        [Key]
        public Int32 ID { get; set; }
        public String SourceTitle { get; set; }
        public String prexc { get; set; }
        public String uacs { get; set; }
        public String Year { get; set; }
        public String ABR {get;set;}
    }
}