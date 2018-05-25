using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BUDGET.Models
{
    public class FundSourceHdr
    {
        [Key]
        public Int32 ID { get; set; }
        public String prexc { get; set; }
        public String SourceTitle { get; set; }
        public String allotment { get; set; }
        public String desc { get; set; }
    }
}