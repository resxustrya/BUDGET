﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BUDGET
{
    public class ORS
    {
        [Key]
        public Int32 ID { get; set; }
        public Int32 allotment { get; set; }
        public Int32 Row { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public String Date1 { get; set; }
        public String DateReceived { get; set; }
        public String TimeReceived { get; set; }
        public String DateReleased { get; set; }
        public String TimeReleased { get; set; }
        public String DB { get; set; }
        public String PO { get; set; }
        public String PR { get; set; }
        public String PAYEE { get; set; }
        public String Adress { get; set; }
        public String Particulars { get; set; }
        public Double Gross { get; set; }
        public String ORS_NO { get; set; }
        public String FundSource { get; set; }
        public String AE { get; set; }
        public String AF { get; set; }
        public String AG { get; set; }
        public String AH { get; set; }
        public String AI { get; set; }
        public String Created_By { get; set; }
        public DateTime Date_Added { get; set; }
        public String dateadded { get; set; }
        public String head_requesting_office { get; set; }
        public String is_obligated { get; set; }

    }
}