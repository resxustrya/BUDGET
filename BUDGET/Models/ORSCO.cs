using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BUDGET.Models
{
    public class ORSCO
    {
        [Key]
        public Int32 ID { get; set; }
        [Required]
        public Int32 Row { get; set; }
        public String Date { get; set; }
        public String DB { get; set; }
        public String PO { get; set; }
        public String PR { get; set; }
        public String PAYEE { get; set; }
        public String Adress { get; set; }
        public String Particulars { get; set; }
        public String ORS_NO { get; set; }
        public String FundSource { get; set; }
        public String Gross { get; set; }
        public String EXP_CODE_1 { get; set; }
        public Double Amount_1 { get; set; }
        public String EXP_CODE_2 { get; set; }
        public Double Amount_2 { get; set; }
        public String EXP_CODE_3 { get; set; }
        public Double Amount_3 { get; set; }
        public String EXP_CODE_4 { get; set; }
        public Double Amount_4 { get; set; }
        public String EXP_CODE_5 { get; set; }
        public Double Amount_5 { get; set; }
        public String EXP_CODE_6 { get; set; }
        public Double Amount_6 { get; set; }
        public String EXP_CODE_7 { get; set; }
        public Double Amount_7 { get; set; }
        public String EXP_CODE_8 { get; set; }
        public Double Amount_8 { get; set; }
        public String EXP_CODE_9 { get; set; }
        public Double Amount_9 { get; set; }
        public String AE { get; set; }
        public String AF { get; set; }
        public String AG { get; set; }
        public String AH { get; set; }
        public String AI { get; set; }
        public String Created_By { get; set; }
    }
}