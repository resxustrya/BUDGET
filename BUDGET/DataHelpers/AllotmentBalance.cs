using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
namespace BUDGET
{
    public static class AllotmentBalance
    {

        public static Double AllotmentTotalRealignment(Int32 ID, out Double after_realignment, out Double AsOfCurrentDate)
        {
            BudgetDB db = new BudgetDB();
            Double total = 0.00;
            Double percentage = 0.00;

            var fsh = db.fsh.Where(p => p.allotment == ID.ToString() && p.type == "REG" && p.active == 1).ToList();

            

            Double realignment_subtotal = 0.00;
            Double total_for_the_month = 0.00;
            Double total_asof_the_month = 0.00;
            Double unobligated_balance_allotment = 0.00;
            Double disbursements = 0.00;
            
            foreach (FundSourceHdr _fsh in fsh)
            {

                total = 0;

                //DISPLAY PREXC CODE

                var fsa = (from list in db.fsa
                           join expensecode
                            in db.uacs on list.expense_title equals expensecode.Title
                           where list.fundsource == _fsh.ID.ToString()
                           select new
                           {
                               ID = list.ID,
                               ExpenseTitle = list.expense_title,
                               ExpenseCode = expensecode.Code,
                               Amount = list.amount
                           }
                   ).ToList();
                total = 0;

                foreach (var _fsa in fsa)
                {

                    Double _fsa_amount = _fsa.Amount;
                   
                    var realignments_from = (from realignment in db.realignment
                                             join _rel_fsh in db.fsh on realignment.fundsource equals _rel_fsh.ID.ToString()
                                             join _rel_allotment in db.allotments on _rel_fsh.allotment equals _rel_allotment.ID.ToString()
                                             where realignment.uacs_from == _fsa.ExpenseTitle
                                             && realignment.fundsource == _fsh.ID.ToString()
                                             && _rel_allotment.ID == ID
                                             select new
                                             {
                                                 Amount = realignment.amount
                                             }).ToList();



                    Double total_realignment = 0;
                    String total_realignment_str = "";


                    if (realignments_from.Count > 0)
                    {
                        foreach (var amount in realignments_from)
                        {
                            total_realignment += amount.Amount;
                        }
                        _fsa_amount -= total_realignment;
                        total_realignment_str = total_realignment > 0 ? "(" + total_realignment.ToString("N", new CultureInfo("en-US")) + ")" : "";
                    }
                    else
                    {
                        var realignments_to = (from realignment in db.realignment
                                               join _rel_fsh in db.fsh on realignment.fundsource equals _rel_fsh.ID.ToString()
                                               join _rel_allotment in db.allotments on _rel_fsh.allotment equals _rel_allotment.ID.ToString()
                                               where realignment.uacs_to == _fsa.ExpenseTitle
                                               && realignment.fundsource == _fsh.ID.ToString()
                                               && _rel_allotment.ID == ID
                                               select new
                                               {
                                                   Amount = realignment.amount
                                               }).ToList();


                        foreach (var amount in realignments_to)
                        {
                            total_realignment += amount.Amount;
                        }
                        _fsa_amount += total_realignment;
                        
                    }

                    //realignments

                    //realignment to

                    //total after realignment
                    realignment_subtotal += _fsa_amount;
                    

                    var uacs_amounts = (from ors_uacs in db.ors_expense_codes
                                        join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                        join allotment in db.allotments on ors.allotment equals allotment.ID
                                        where ors.Date <= DateTime.Now &&
                                        ors.FundSource == _fsh.Code &&
                                        allotment.ID == ID &&
                                        ors_uacs.uacs == _fsa.ExpenseTitle
                                        select new
                                        {
                                            Amount = ors_uacs.amount
                                        }).ToList();


                    Double month_total = 0;
                    foreach (var amount in uacs_amounts)
                    {
                        month_total += amount.Amount;
                    }


                    total_for_the_month += month_total;


                    //total for the month



                    var total_utilized = (from ors_uacs in db.ors_expense_codes
                                          join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                          join allotment in db.allotments on ors.allotment equals allotment.ID
                                          where ors.FundSource == _fsh.Code
                                          && ors.Date <= DateTime.Now &&
                                          allotment.ID == ID &&
                                          ors_uacs.uacs == _fsa.ExpenseTitle
                                          select new
                                          {
                                              Amount = ors_uacs.amount
                                          }).ToList();


                    Double total_utilized_amount = 0;
                    foreach (var amount in total_utilized)
                    {
                        total_utilized_amount += amount.Amount;
                    }
                    total_asof_the_month += total_utilized_amount;

                    //total as of this month

                    
                    //unobligated computation - subtotal per fund source

                    unobligated_balance_allotment += (_fsa_amount - total_utilized_amount);
                    //total unobligated


                    //disbursements
                    DateTime date2 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                    var ors_disbursements = (from ors_uacs in db.ors_expense_codes
                                             join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                             join allotment in db.allotments on ors.allotment equals allotment.ID
                                             where ors.FundSource == _fsh.Code &&
                                             allotment.ID == ID &&
                                             ors_uacs.uacs == _fsa.ExpenseTitle
                                             select new
                                             {
                                                 Disbursements = ors_uacs.TaxAmount + ors_uacs.NetAmount + ors_uacs.Others
                                             }).ToList();


                    Double uacs_disbursement_total = 0.00;
                    if (ors_disbursements != null && ors_disbursements.Count > 0)
                    {
                        foreach (var _ors_disbursements in ors_disbursements)
                        {
                            uacs_disbursement_total += _ors_disbursements.Disbursements;
                        }
                    }
                    disbursements += uacs_disbursement_total;
                    total += _fsa.Amount;
                }

            }

            after_realignment = realignment_subtotal;
            AsOfCurrentDate = disbursements;
            return total;
        }
    }
}