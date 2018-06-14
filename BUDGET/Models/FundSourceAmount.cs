﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BUDGET
{
    public class FundSourceAmount
    {
       [Key]
        public Int32 ID { get; set; }
        public String expensecode { get; set; }
        public Double amount { get; set; }
        public String fundsource { get; set; }
    }
}