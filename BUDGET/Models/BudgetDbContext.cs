﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace BUDGET.Models
{
    public class BudgetDB : DbContext
    {
        public BudgetDB() : base("name=BudgetDB")
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<PersonnelServices> ps { get; set; }
        public DbSet<MOOEs> mooe { get; set; }
        public DbSet<ORSPS> orsps { get; set; }
        public DbSet<UACS> uacs { get; set; }
        public DbSet<ORSMOOE> orsmooe { get; set; }
        public DbSet<ORSVTF> orsvtf { get; set; }
        public DbSet<ORSCO> orsco { get; set; }
        public DbSet<YearBudget> yearbudget { get; set; }
        public DbSet<PREXC> prexc { get; set; }
        public DbSet<Allotments> allotments { get; set; }
        public DbSet<ORSMaster> orsmaster { get; set; }
        public DbSet<ORS> ors { get; set; }
        public DbSet<FundSourceHdr> fsh { get; set; }
        public DbSet<FundSourceAmount> fsa { get; set; }
        public DbSet<ORS_EXPENSE_CODES> ors_expense_codes { get; set; }
    }
}