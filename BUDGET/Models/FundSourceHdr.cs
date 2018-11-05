using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUDGET
{
    public class FundSourceHdr
    {
        [Key]
        public Int32 ID { get; set; }
        public String prexc { get; set; }
        public String Code { get; set; }
        public String SourceTitle { get; set; }
        public String allotment { get; set; }
        public String desc { get; set; }
        public String type { get; set; }
        public Int32 ors_head { get; set; }
        public String Responsibility_Number { get; set; }
        public Int32 active { get; set; }
    }
}