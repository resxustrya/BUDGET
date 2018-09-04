using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUDGET
{
    public class ORS_EXPENSE_CODES
    {
        public Int32 ID { get; set; }
        public Int32 ors_obligation { get; set; }

        public String uacs { get; set; }
        public Double amount { get; set; }
        public Double Disboursement { get; set; }
    }
}