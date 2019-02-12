using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace BUDGET
{
    public class SaobExcelSheet1
    {
        public void CreateExcel(BudgetDB db,FileInfo newFile, String date_from, String date_to)
        {
            ExcelPackage pck = new ExcelPackage(newFile);
            ExcelWorksheet worksheet = pck.Workbook.Worksheets[1];

            Int32 startRow = 15;

            DateTime date1 = Convert.ToDateTime(date_from);
            DateTime date2 = Convert.ToDateTime(date_to);


            worksheet.Cells[11, 1, 11, 10].Merge = true;
            worksheet.Cells[12, 1, 12, 10].Merge = true;

            String str_this_month = date2.ToString("MM") + "/01/" + date2.ToString("yyyy");
            DateTime this_month = Convert.ToDateTime(str_this_month);

            Double total = 0.00;
            Double percent = 0.00;

            var allotments_row_totals = new Dictionary<String, Dictionary<String, Double>>();
            var sub_allotments_row_totals = new Dictionary<String, Dictionary<String, Double>>();

            /*
             * GRAND TOTAL VARIABLES
             * 
             */

            Double allotment_received_grand_total = 0.00;
            Double after_realignment_grand_total = 0.00;
            Double current_month_grand_total = 0.00;
            Double as_of_month_grand_total = 0.00;
            Double unobligated_grand_total = 0.00;
            Double disbursement_grand_total = 0.00;


            /*
             *  SUB TOTAL VARIABLES
             * 
            */


            Double allotment_received_sub_total = 0.00;
            Double after_realignment_sub_total = 0.00;
            Double current_month_sub_total = 0.00;
            Double as_of_month_sub_total = 0.00;
            Double unobligated_sub_total = 0.00;
            Double disbursement_sub_total = 0.00;


            /*
            * SUB GRAND TOTAL VARIABLES
            * 
            */

            Double sub_allotment_received_grand_total = 0.00;
            Double sub_allotment_total_after_realignment_grand_total = 0.00;
            Double sub_current_month_grand_total = 0.00;
            Double sub_as_of_month_grand_total = 0.00;
            Double sub_unobligated_grand_total = 0.00;
            Double sub_disbursement_grand_total = 0.00;

            /*
             * ALLOTMENT SUBTOTAL VARIABLES
             */

            worksheet.Cells[5, 15].Value = date2.ToString("MMMM") + " " + date2.ToString("dd") + ", " + date2.ToString("yyyy");

            worksheet.Cells[12, 17].Value = date2.ToString("MMMM");
            worksheet.Cells[12, 18].Value = "As of " + date2.ToString("MMMM");

            worksheet.Cells[12,21].Value = "As of " + date2.ToString("MMMM");

            Double allotment_total = 0;
            //var allotments = db.allotments.Where(p => p.year == GlobalData.Year && p.active == 1).ToList();
            var allotments = (from allotments_list in db.allotments where allotments_list.year == GlobalData.Year && allotments_list.active == 1 orderby allotments_list.Code2 ascending select allotments_list).ToList();
            /*
            var allotments = (from allotment in db.allotments
                              join fundsource in db.fsh on allotment.ID.ToString() equals fundsource.allotment
                              where allotment.year == GlobalData.Year &&
                              allotment.active == 1 &&
                              allotment.ID.ToString() == fundsource.allotment
                              select new
                              {
                                  ID = allotment.ID,
                                  Code = allotment.Code,
                                  Year = allotment.year,
                                  Title = allotment.Title
                              }).ToList();
            */

            foreach (var _allotments in allotments)
            {

                allotment_received_sub_total = 0.00;
                after_realignment_sub_total = 0.00;
                current_month_sub_total = 0.00;
                as_of_month_sub_total = 0.00;
                unobligated_sub_total = 0.00;
                disbursement_sub_total = 0.00;


                allotment_received_grand_total = 0.00;
                after_realignment_grand_total = 0.00;
                current_month_grand_total = 0.00;
                as_of_month_grand_total = 0.00;
                unobligated_grand_total = 0.00;
                disbursement_grand_total = 0.00;


                allotment_total = 0;


                // DISPLAY ALLOTMENTS

                worksheet.Cells[startRow, 1].Style.Font.Name = "TAHOMA";
                worksheet.Cells[startRow, 1].Style.Font.Size = 12;
                worksheet.Cells[startRow, 1].Style.Font.Bold = true;
                worksheet.Cells[startRow, 1].Value = _allotments.Title.ToUpper().ToString();
                startRow++;


                //var fsh = from fsh_list in db.fsh group fsh_list by fsh_list.prexc into p select p;
                var fsh = db.fsh.Where(p => p.allotment == _allotments.ID.ToString() && p.type == "REG" && p.active == 1)
                                .OrderBy(p => p.prexc)
                                .ToList();

                //var group_fsh = db.Database.ExecuteSqlCommand("SELECT * FROM ");
                foreach (FundSourceHdr _fsh in fsh)
                {
                    total = 0;
                    Double realignment_subtotal = 0.00;
                    Double total_for_the_month = 0.00;
                    Double total_asof_the_month = 0.00;
                    Double unobligated_balance_allotment = 0.00;
                    Double disbursements = 0.00;


                    //DISPLAY PREXC CODE
                    worksheet.Cells[startRow, 1].Value = _fsh.prexc.ToUpper().ToString();
                    startRow++;

                    //DISPLAY FUNDSOURCE HEADER
                    worksheet.Cells[startRow, 1].Style.Font.Name = "TAHOMA";
                    worksheet.Cells[startRow, 1].Style.Font.Size = 10;
                    worksheet.Cells[startRow, 1].Style.Font.Bold = true;
                    worksheet.Cells[startRow, 1].Value = _fsh.SourceTitle.ToUpper().ToString();
                    startRow++;


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

                        // DISPLAY FUNDSOURCE EXPENSE TITLE
                       
                        worksheet.Cells[startRow, 3].Value = _fsa.ExpenseTitle.ToUpper().ToString();

                        //DISPLAY EXPENSE CODE
                        worksheet.Cells[startRow, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[startRow, 11].Style.Numberformat.Format = "#";
                        worksheet.Cells[startRow, 11].Value = Convert.ToInt64(_fsa.ExpenseCode);

                        //DISPLAY FUNDSOURCE AMMOUNT

                        worksheet.Cells[startRow, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells[startRow, 13].Style.Numberformat.Format = "#,##0.00";
                        if (_fsa.Amount > 0)
                            worksheet.Cells[startRow, 13].Value = _fsa.Amount;
                        else
                            worksheet.Cells[startRow, 13].Value = null;

                        //realignments
                        var realignments_from = (from realignment in db.realignment
                                                 join _rel_fsh in db.fsh on realignment.fundsource equals _rel_fsh.ID.ToString()
                                                 join _rel_allotment in db.allotments on _rel_fsh.allotment equals _rel_allotment.ID.ToString()
                                                 where realignment.uacs_from == _fsa.ExpenseTitle
                                                 && realignment.fundsource == _fsh.ID.ToString()
                                                 && _rel_allotment.ID == _allotments.ID
                                                 select new
                                                 {
                                                     Amount = realignment.amount
                                                 }).ToList();


                        Double total_realignment = 0;

                        if (realignments_from.Count > 0)
                        {
                            foreach (var amount in realignments_from)
                            {
                                total_realignment += amount.Amount;
                            }
                            _fsa_amount -= total_realignment;

                            worksheet.Cells[startRow, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            worksheet.Cells[startRow, 14].Style.Numberformat.Format = "(#,##0.00)";
                            if(total_realignment > 0)
                                worksheet.Cells[startRow, 14].Value = total_realignment;
                            else
                                worksheet.Cells[startRow, 14].Value = null;
                        }
                        else
                        {
                            var realignments_to = (from realignment in db.realignment
                                                   join _rel_fsh in db.fsh on realignment.fundsource equals _rel_fsh.ID.ToString()
                                                   join _rel_allotment in db.allotments on _rel_fsh.allotment equals _rel_allotment.ID.ToString()
                                                   where realignment.uacs_to == _fsa.ExpenseTitle
                                                   && realignment.fundsource == _fsh.ID.ToString()
                                                   && _rel_allotment.ID == _allotments.ID
                                                   select new
                                                   {
                                                       Amount = realignment.amount
                                                   }).ToList();

                            if(realignments_to.Count > 0)
                            {
                                foreach (var amount in realignments_to)
                                {
                                    total_realignment += amount.Amount;
                                }
                                _fsa_amount += total_realignment;
                                worksheet.Cells[startRow, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                worksheet.Cells[startRow, 14].Style.Numberformat.Format = "#,##0.00";
                                if (total_realignment > 0)
                                    worksheet.Cells[startRow, 14].Value = total_realignment;
                                else
                                    worksheet.Cells[startRow, 14].Value = null;
                            }
                        }
                        //realignment to

                        //total after realignment
                        realignment_subtotal += _fsa_amount;

                        worksheet.Cells[startRow, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        
                        if(_fsa_amount > 0)
                        {
                            worksheet.Cells[startRow, 16].Value = _fsa_amount;
                            worksheet.Cells[startRow, 16].Style.Numberformat.Format = "#,##0.00";
                        }
                        else if(_fsa_amount < 0)
                        {
                            worksheet.Cells[startRow, 16].Value = Math.Abs(_fsa_amount);
                            worksheet.Cells[startRow, 16].Style.Numberformat.Format = "(#,##0.00)";
                        }    
                        else
                            worksheet.Cells[startRow, 16].Value = null;


                        //total for the month
                        var uacs_amounts = (from ors_uacs in db.ors_expense_codes
                                            join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                            join allotment in db.allotments on ors.allotment equals allotment.ID
                                            where ors.Date >= this_month && ors.Date <= date2 &&
                                            ors.FundSource == _fsh.Code &&
                                            allotment.ID == _allotments.ID &&
                                            ors_uacs.uacs == _fsa.ExpenseTitle &&
                                            ors.deleted == false
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

                        
                        worksheet.Cells[startRow, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells[startRow, 17].Style.Numberformat.Format = "#,##0.00";
                        if(month_total > 0)
                            worksheet.Cells[startRow, 17].Value = month_total;
                        else
                            worksheet.Cells[startRow, 17].Value = null;

                        var total_utilized = (from ors_uacs in db.ors_expense_codes
                                              join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                              join allotment in db.allotments on ors.allotment equals allotment.ID
                                              where ors.FundSource == _fsh.Code
                                              && ors.Date <= date2 &&
                                              allotment.ID == _allotments.ID &&
                                              ors_uacs.uacs == _fsa.ExpenseTitle &&
                                              ors.deleted == false
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

                        worksheet.Cells[startRow, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        
                        if(total_utilized_amount > 0)
                        {
                            worksheet.Cells[startRow, 18].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells[startRow, 18].Value = total_utilized_amount;
                        } else if(total_utilized_amount < 0)
                        {
                            worksheet.Cells[startRow, 18].Style.Numberformat.Format = "(#,##0.00)";
                            worksheet.Cells[startRow, 18].Value = Math.Abs(total_utilized_amount);
                        } else if(total_utilized_amount == 0)
                            worksheet.Cells[startRow, 18].Value = null;

                        //unobligated computation - subtotal per fund source

                        unobligated_balance_allotment += (_fsa_amount - total_utilized_amount);

                        //total unobligated

                        worksheet.Cells[startRow, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        

                        Double row_total_unobligated = (_fsa_amount - total_utilized_amount);
                        if (row_total_unobligated > 0)
                        {
                            worksheet.Cells[startRow, 19].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells[startRow, 19].Value = row_total_unobligated;
                        } else if(row_total_unobligated < 0)
                        {
                            worksheet.Cells[startRow, 19].Style.Numberformat.Format = "(#,##0.00)";
                            worksheet.Cells[startRow, 19].Value = Math.Abs(row_total_unobligated);
                        } else if(row_total_unobligated == 0)
                            worksheet.Cells[startRow, 19].Value = null;
                        

                        //disbursements
                        var ors_disbursements = (from ors_uacs in db.ors_expense_codes
                                                 join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                                 join allotment in db.allotments on ors.allotment equals allotment.ID
                                                 where ors.Date <= date2 &&
                                                 ors.FundSource == _fsh.Code &&
                                                 allotment.ID == _allotments.ID &&
                                                 ors_uacs.uacs == _fsa.ExpenseTitle &&
                                                 ors.deleted == false
                                                 select new
                                                 {
                                                     Disbursements = (from ors_date in db.ors_date_entry where ors_date.ors_id == ors.ID && ors_date.ExpenseTitle == _fsa.ExpenseTitle select ors_date.NetAmount + ors_date.TaxAmount + ors_date.Others).DefaultIfEmpty(0).Sum()
                                                 }).ToList();


                        Double uacs_disbursement_total = 0.00;
                        if (ors_disbursements != null && ors_disbursements.Count > 0)
                        {
                            foreach (var _ors_disbursements in ors_disbursements)
                            {
                                uacs_disbursement_total += _ors_disbursements.Disbursements;
                            }
                            //disbursement_sub_total += ors_disbursements.Disbursements;
                        }
                        disbursements += uacs_disbursement_total;

                        worksheet.Cells[startRow, 21].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells[startRow, 21].Style.Numberformat.Format = "#,##0.00";
                        if (uacs_disbursement_total > 0)
                            worksheet.Cells[startRow, 21].Value = uacs_disbursement_total;
                        else
                            worksheet.Cells[startRow, 21].Value = null;

                        // PERCENTAGE REMARKS
                        //percent = ((uacs_disbursement_total - _fsa_amount) / _fsa_amount) * 100;
                        //percent = Math.Round(Math.Abs(percent), 2);

                        percent = uacs_disbursement_total / _fsa_amount;

                        
                        worksheet.Cells[startRow, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells[startRow, 20].Style.Numberformat.Format = "#0.00%";
                        worksheet.Cells[startRow, 20].Value = percent;

                        startRow++;

                        total += _fsa.Amount;
                    }

                    allotment_total += total;

                    allotment_received_sub_total += total;
                    after_realignment_sub_total += realignment_subtotal;
                    current_month_sub_total += total_for_the_month;
                    as_of_month_sub_total += total_asof_the_month;
                    unobligated_sub_total += unobligated_balance_allotment;
                    disbursement_sub_total += disbursements;


                    // DISPLAY SUBTOTAL ROW 
                   
                    worksheet.Cells[startRow, 3].Style.Font.Name = "TAHOMA";
                    worksheet.Cells[startRow, 3].Style.Font.Size = 10;
                    worksheet.Cells[startRow, 3].Style.Font.Bold = true;
                    worksheet.Cells[startRow, 3].Value = "SUBTOTAL " + _fsh.SourceTitle.ToUpper() + " - " + _allotments.Code.ToUpper();

                    worksheet.Cells[startRow, 3, startRow, 10].Merge = true;


                    worksheet.Cells[startRow, 13].Style.Font.Name = "TAHOMA";
                    worksheet.Cells[startRow, 13].Style.Font.Bold = true;
                    
                    worksheet.Cells[startRow, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    if (total > 0)
                    {
                        worksheet.Cells[startRow, 13].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells[startRow, 13].Value = total;
                    }
                    else if (total < 0)
                    {
                        worksheet.Cells[startRow, 13].Style.Numberformat.Format = "(#,##0.00)";
                        worksheet.Cells[startRow, 13].Value = Math.Abs(total);
                    }
                    else if (total == 0)
                        worksheet.Cells[startRow, 13].Value = null;
                    

                    worksheet.Cells[startRow, 16].Style.Font.Name = "TAHOMA";
                    worksheet.Cells[startRow, 16].Style.Font.Bold = true;
                    worksheet.Cells[startRow, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    if(realignment_subtotal > 0)
                    {
                        worksheet.Cells[startRow, 16].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells[startRow, 16].Value = realignment_subtotal;
                    } else if(realignment_subtotal < 0)
                    {
                        worksheet.Cells[startRow, 16].Style.Numberformat.Format = "(#,##0.00)";
                        worksheet.Cells[startRow, 16].Value = Math.Abs(realignment_subtotal);
                    } else if(realignment_subtotal == 0)
                        worksheet.Cells[startRow, 16].Value = null;


                    worksheet.Cells[startRow, 17].Style.Font.Name = "TAHOMA";
                    worksheet.Cells[startRow, 17].Style.Font.Bold = true;
                    worksheet.Cells[startRow, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    if (total_for_the_month > 0)
                    {
                        worksheet.Cells[startRow, 16].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells[startRow, 16].Value = total_for_the_month;
                    }
                    else if (total_for_the_month < 0)
                    {
                        worksheet.Cells[startRow, 16].Style.Numberformat.Format = "(#,##0.00)";
                        worksheet.Cells[startRow, 16].Value = Math.Abs(total_for_the_month);
                    }
                    else if (total_for_the_month == 0)
                        worksheet.Cells[startRow, 16].Value = null;


                    worksheet.Cells[startRow, 18].Style.Font.Name = "TAHOMA";
                    worksheet.Cells[startRow, 18].Style.Font.Bold = true;
                    worksheet.Cells[startRow, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                    if (total_asof_the_month > 0)
                    {
                        worksheet.Cells[startRow, 18].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells[startRow, 18].Value = total_asof_the_month;
                    }
                    else if (total_asof_the_month < 0)
                    {
                        worksheet.Cells[startRow, 18].Style.Numberformat.Format = "(#,##0.00)";
                        worksheet.Cells[startRow, 18].Value = Math.Abs(total_asof_the_month);
                    }
                    else if (total_asof_the_month == 0)
                        worksheet.Cells[startRow, 18].Value = null;


                    worksheet.Cells[startRow, 19].Style.Font.Name = "TAHOMA";
                    worksheet.Cells[startRow, 19].Style.Font.Bold = true;
                    worksheet.Cells[startRow, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                    if(unobligated_balance_allotment > 0)
                    {
                        worksheet.Cells[startRow, 19].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells[startRow, 19].Value = unobligated_balance_allotment;
                    } else if(unobligated_balance_allotment < 0)
                    {
                        worksheet.Cells[startRow, 19].Style.Numberformat.Format = "(#,##0.00)";
                        worksheet.Cells[startRow, 19].Value = Math.Abs(unobligated_balance_allotment);
                    } else if(unobligated_balance_allotment == 0)
                        worksheet.Cells[startRow, 19].Value = null;


                    worksheet.Cells[startRow, 21].Style.Font.Name = "TAHOMA";
                    worksheet.Cells[startRow, 21].Style.Font.Bold = true;
                    worksheet.Cells[startRow, 21].Style.Numberformat.Format = "#,##0.00";
                    worksheet.Cells[startRow, 21].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    worksheet.Cells[startRow, 21].Value = disbursements;

                    if (disbursements > 0)
                    {
                        worksheet.Cells[startRow, 21].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells[startRow, 21].Value = disbursements;
                    }
                    else if (disbursements < 0)
                    {
                        worksheet.Cells[startRow, 21].Style.Numberformat.Format = "(#,##0.00)";
                        worksheet.Cells[startRow, 21].Value = Math.Abs(disbursements);
                    }
                    else if (disbursements == 0)
                        worksheet.Cells[startRow, 21].Value = null;


                    // DISPLAY PERCENTAGE

                    percent = disbursements / allotment_received_sub_total;

                    worksheet.Cells[startRow, 20].Style.Font.Name = "TAHOMA";
                    worksheet.Cells[startRow, 20].Style.Font.Bold = true;
                    worksheet.Cells[startRow, 20].Style.Numberformat.Format = "#0.00%";
                    worksheet.Cells[startRow, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    worksheet.Cells[startRow, 20].Value = percent;


                    startRow++;

                }


                allotment_received_grand_total += allotment_received_sub_total;
                after_realignment_grand_total += after_realignment_sub_total;
                current_month_grand_total += current_month_sub_total;
                as_of_month_grand_total += as_of_month_sub_total;
                unobligated_grand_total += unobligated_sub_total;
                disbursement_grand_total += disbursement_sub_total;


                //  ALLOTMENT RECEIVED TOTAL

                //EXPENSE CODES

                worksheet.Cells[startRow, 10].Style.Font.Name = "TAHOMA";
                worksheet.Cells[startRow, 10].Style.Font.Size = 12;
                worksheet.Cells[startRow, 10].Style.Font.Bold = true;
                worksheet.Cells[startRow, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 10].Value = "TOTAL " + _allotments.Code.ToUpper().ToString();


                allotment_received_grand_total = allotment_total;

                var dic_allotment_received = new Dictionary<String, Double>();

                dic_allotment_received.Add("ALLOTMENT_RECEIVED", allotment_received_grand_total);
                dic_allotment_received.Add("TOTAL_AFTER_REALIGNMENT", after_realignment_grand_total);
                dic_allotment_received.Add("CURRENT_MONTH_GRAND_TOTAL", current_month_grand_total);
                dic_allotment_received.Add("AS_OF_MONTH_GRAND_TOTAL", as_of_month_grand_total);
                dic_allotment_received.Add("UNOBLIGATED_GRAND_TOTAL", unobligated_grand_total);
                dic_allotment_received.Add("DISBURSEMENTS", disbursement_grand_total);


                allotments_row_totals.Add(_allotments.Code, dic_allotment_received);



                worksheet.Cells[startRow, 13].Style.Font.Name = "TAHOMA";
                worksheet.Cells[startRow, 13].Style.Font.Size = 10;
                worksheet.Cells[startRow, 13].Style.Font.Bold = true;
                worksheet.Cells[startRow, 16].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                if (allotment_total > 0)
                    worksheet.Cells[startRow, 13].Value = allotment_total;
                else
                    worksheet.Cells[startRow, 13].Value = null;


                worksheet.Cells[startRow, 16].Style.Font.Name = "TAHOMA";
                worksheet.Cells[startRow, 16].Style.Font.Bold = true;
                worksheet.Cells[startRow, 16].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 16].Style.Font.Size = 10;
                worksheet.Cells[startRow, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                if(after_realignment_sub_total > 0)
                    worksheet.Cells[startRow, 16].Value = after_realignment_sub_total;
                else
                    worksheet.Cells[startRow, 16].Value = null;


                worksheet.Cells[startRow, 17].Style.Font.Name = "TAHOMA";
                worksheet.Cells[startRow, 17].Style.Font.Bold = true;
                worksheet.Cells[startRow, 17].Style.Font.Size = 10;
                
                worksheet.Cells[startRow, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                if(current_month_sub_total > 0)
                {
                    worksheet.Cells[startRow, 17].Style.Numberformat.Format = "#,##0.00";
                    worksheet.Cells[startRow, 17].Value = current_month_sub_total;
                } else if(current_month_sub_total < 0)
                {
                    worksheet.Cells[startRow, 17].Style.Numberformat.Format = "(#,##0.00)";
                    worksheet.Cells[startRow, 17].Value = current_month_sub_total;
                }
                else
                    worksheet.Cells[startRow, 17].Value = null;


                worksheet.Cells[startRow, 18].Style.Font.Name = "TAHOMA";
                worksheet.Cells[startRow, 18].Style.Font.Bold = true;
                worksheet.Cells[startRow, 18].Style.Font.Size = 10;
                worksheet.Cells[startRow, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                if (as_of_month_sub_total > 0)
                {
                    worksheet.Cells[startRow, 18].Style.Numberformat.Format = "#,##0.00";
                    worksheet.Cells[startRow, 18].Value = as_of_month_sub_total;
                } else if(as_of_month_sub_total < 0)
                {
                    worksheet.Cells[startRow, 18].Style.Numberformat.Format = "(#,##0.00)";
                    worksheet.Cells[startRow, 18].Value = as_of_month_sub_total;
                } else
                    worksheet.Cells[startRow, 18].Value = null;


                worksheet.Cells[startRow, 19].Style.Font.Name = "TAHOMA";
                worksheet.Cells[startRow, 19].Style.Font.Bold = true;
                worksheet.Cells[startRow, 19].Style.Font.Size = 10;
                worksheet.Cells[startRow, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                if(unobligated_sub_total > 0)
                {
                    worksheet.Cells[startRow, 19].Style.Numberformat.Format = "#,##0.00";
                    worksheet.Cells[startRow, 19].Value = unobligated_sub_total;
                } else if(unobligated_sub_total < 0)
                {
                    worksheet.Cells[startRow, 19].Style.Numberformat.Format = "(#,##0.00)";
                    worksheet.Cells[startRow, 19].Value = Math.Abs(unobligated_sub_total);
                } else if(unobligated_sub_total == 0)
                {
                    worksheet.Cells[startRow, 19].Value = null;
                }
                

                worksheet.Cells[startRow, 21].Style.Font.Name = "TAHOMA";
                worksheet.Cells[startRow, 21].Style.Font.Bold = true;
                worksheet.Cells[startRow, 21].Style.Font.Size = 10;
                worksheet.Cells[startRow, 21].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 21].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 21].Value = disbursement_sub_total > 0 ? disbursement_sub_total : 0;


                // DISPLAY PERCENTAGE

                //percent = ((disbursement_sub_total - after_realignment_sub_total) / after_realignment_sub_total) * 100;
                //percent = Math.Round(Math.Abs(percent), 2);
                percent = disbursement_sub_total / allotment_received_sub_total;

                worksheet.Cells[startRow, 20].Style.Font.Name = "TAHOMA";
                worksheet.Cells[startRow, 20].Style.Font.Bold = true;
                worksheet.Cells[startRow, 20].Style.Font.Size = 10;
                worksheet.Cells[startRow, 20].Style.Numberformat.Format = "#0.00%";
                worksheet.Cells[startRow, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 20].Value = percent;

                startRow++;
                startRow++;

                /*
                 *  SUB TOTAL VARIABLES
                 * 
                */
                Double sub_allotment_received_sub_total = 0.00;
                Double sub_allotment_total_after_realignment_sub_total = 0.00;
                Double sub_current_month_sub_total = 0.00;
                Double sub_as_of_month_sub_total = 0.00;
                Double sub_unobligated_sub_total = 0.00;
                Double sub_disbursement_sub_total = 0.00;


                /*
                 * SUB ALLOTMENT 
                 */


                var _sub_allotments = db.fsh.Where(p => p.allotment == _allotments.ID.ToString() && p.type == "SUB" && p.active == 1)
                                            .OrderBy(p => p.prexc)
                                            .ToList();
                if (_sub_allotments.Count > 0)
                {
                    // SUB ALLOTMENT TITLE CODE ROW
                    //DISPLAY FUNDSOURCE HEADER
                    worksheet.Cells[startRow, 1].Style.Font.Name = "TAHOMA";
                    worksheet.Cells[startRow, 1].Style.Font.Size = 12;
                    worksheet.Cells[startRow, 1].Style.Font.Bold = true;
                    worksheet.Cells[startRow, 1].Value = "SUB-ALLOTMENT-" + _allotments.Code.ToUpper();
                    startRow++;

                    foreach (FundSourceHdr _fsh_saa in _sub_allotments)
                    {

                        sub_allotment_received_sub_total = 0.00;
                        sub_allotment_total_after_realignment_sub_total = 0.00;
                        sub_current_month_sub_total = 0.00;
                        sub_as_of_month_sub_total = 0.00;
                        sub_unobligated_sub_total = 0.00;
                        sub_disbursement_sub_total = 0.00;


                        // SUB-ALLOTMENT PREXE CODE
                        worksheet.Cells[startRow, 1].Value = _fsh_saa.prexc.ToUpper().ToString();
                        startRow++;


                        // SUB-ALLOTMENT FUNDSOURCE TITLES
                        worksheet.Cells[startRow, 1].Style.Font.Name = "TAHOMA";
                        worksheet.Cells[startRow, 1].Style.Font.Size = 10;
                        worksheet.Cells[startRow, 1].Style.Font.Bold = true;
                        worksheet.Cells[startRow, 1].Value = _fsh_saa.SourceTitle.ToUpper().ToString();
                        startRow++;


                        // SUB-ALLOTMENT FUNDSOURCE DESCRIPTION
                        worksheet.Cells[startRow, 2].Style.Font.Name = "TAHOMA";
                        worksheet.Cells[startRow, 2].Style.Font.Italic = true;
                        worksheet.Cells[startRow, 2].Value = _fsh_saa.desc;
                        startRow++;


                        var saa_amt = (from list in db.fsa
                                       join expensecode
                                        in db.uacs on list.expense_title equals expensecode.Title
                                       where list.fundsource == _fsh_saa.ID.ToString()
                                       select new
                                       {
                                           ID = list.ID,
                                           ExpenseTitle = list.expense_title,
                                           ExpenseCode = expensecode.Code,
                                           Title = expensecode.Title,
                                           Amount = list.amount
                                       }
                           ).ToList();

                        foreach (var _saa_amt in saa_amt)
                        {
                            Double _fsa_amount = _saa_amt.Amount;
                            Double sub_disbursements = 0.00;
                            // ALLOTMENT TITLE
                            worksheet.Cells[startRow, 3].Value = _saa_amt.Title.ToString().ToUpper();
                            // EXPENSE CODES
                            worksheet.Cells[startRow, 11].Value = Convert.ToInt64(_saa_amt.ExpenseCode);
                            // ALLOTMENT AMOUNT

                            sub_allotment_received_sub_total += _saa_amt.Amount;
                            worksheet.Cells[startRow, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            worksheet.Cells[startRow, 13].Style.Numberformat.Format = "#,##0.00";

                            if (_saa_amt.Amount > 0)
                                worksheet.Cells[startRow, 13].Value = _saa_amt.Amount;
                            else
                                worksheet.Cells[startRow, 13].Value = null;


                            //REALIGNMENT TO
                            var realignments_from = (from realignment in db.realignment
                                                     join _rel_fsh in db.fsh on realignment.fundsource equals _rel_fsh.ID.ToString()
                                                     join _rel_allotment in db.allotments on _rel_fsh.allotment equals _rel_allotment.ID.ToString()
                                                     where realignment.uacs_from == _saa_amt.ExpenseTitle
                                                     && realignment.fundsource == _fsh_saa.ID.ToString()
                                                     && _rel_allotment.ID == _allotments.ID
                                                     select new
                                                     {
                                                         Amount = realignment.amount
                                                     }).ToList();

                            Double total_realignment = 0;

                            if (realignments_from.Count > 0)
                            {
                                foreach (var amount in realignments_from)
                                {
                                    total_realignment += amount.Amount;
                                }
                                _fsa_amount -= total_realignment;
                                //total_realignment_str = total_realignment > 0 ? "(" + total_realignment.ToString("N", new CultureInfo("en-US")) + ")" : "";
                                worksheet.Cells[startRow, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                worksheet.Cells[startRow, 14].Style.Numberformat.Format = "(#,##0.00)";
                                
                                if(total_realignment > 0) worksheet.Cells[startRow, 14].Value = total_realignment;
                                else worksheet.Cells[startRow, 14].Value = null;
                            }
                            else
                            {
                                var realignments_to = (from realignment in db.realignment
                                                       join _rel_fsh in db.fsh on realignment.fundsource equals _rel_fsh.ID.ToString()
                                                       join _rel_allotment in db.allotments on _rel_fsh.allotment equals _rel_allotment.ID.ToString()
                                                       where realignment.uacs_to == _saa_amt.ExpenseTitle
                                                       && realignment.fundsource == _fsh_saa.ID.ToString()
                                                       && _rel_allotment.ID == _allotments.ID
                                                       select new
                                                       {
                                                           Amount = realignment.amount
                                                       }).ToList();


                                foreach (var amount in realignments_to)
                                {
                                    total_realignment += amount.Amount;
                                }

                                _fsa_amount += total_realignment;
                                worksheet.Cells[startRow, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                worksheet.Cells[startRow, 14].Style.Numberformat.Format = "#,##0.00";

                                if(total_realignment > 0) worksheet.Cells[startRow, 14].Value = total_realignment;
                                else worksheet.Cells[startRow, 14].Value = null;
                            }

                            // REALIGNMENTS

                            //TOTAL AFTER REALIGNMENT

                            sub_allotment_total_after_realignment_sub_total += _fsa_amount;
                            worksheet.Cells[startRow, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            worksheet.Cells[startRow, 16].Style.Numberformat.Format = "#,##0.00";

                            if (_fsa_amount > 0) worksheet.Cells[startRow, 16].Value = _fsa_amount;
                            else worksheet.Cells[startRow, 16].Value = null;

                            var uacs_amounts = (from ors_uacs in db.ors_expense_codes
                                                join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                                join allotment in db.allotments on ors.allotment equals allotment.ID
                                                where ors.Date >= this_month && ors.Date <= date2 &&
                                                ors.FundSource == _fsh_saa.Code &&
                                                ors_uacs.uacs == _saa_amt.ExpenseTitle &&
                                                ors.deleted == false
                                                select new
                                                {
                                                    Amount = ors_uacs.amount
                                                }).ToList();

                            Double month_total = 0;
                            foreach (var amount in uacs_amounts)
                            {
                                month_total += amount.Amount;
                            }

                            sub_current_month_sub_total += month_total;
                            // TOTAL CURRENT MONTH
                            worksheet.Cells[startRow, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            worksheet.Cells[startRow, 17].Style.Numberformat.Format = "#,##0.00";

                            if(month_total > 0) worksheet.Cells[startRow, 17].Value = month_total;
                            else worksheet.Cells[startRow, 17].Value = null;


                            var total_utilized = (from ors_uacs in db.ors_expense_codes
                                                  join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                                  join allotment in db.allotments on ors.allotment equals allotment.ID
                                                  where ors.FundSource == _fsh_saa.Code &&
                                                  ors_uacs.uacs == _saa_amt.ExpenseTitle &&
                                                  ors.deleted == false
                                                  select new
                                                  {
                                                      Amount = ors_uacs.amount
                                                  }).ToList();

                            Double total_utilized_amount = 0;
                            foreach (var amount in total_utilized)
                            {
                                total_utilized_amount += amount.Amount;
                            }

                            sub_as_of_month_sub_total += total_utilized_amount;
                            // TOTAL AS OF THIS MONTH
                            worksheet.Cells[startRow, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            worksheet.Cells[startRow, 18].Style.Numberformat.Format = "#,##0.00";
                            if(total_utilized_amount > 0) worksheet.Cells[startRow, 18].Value = total_utilized_amount;
                            else worksheet.Cells[startRow, 18].Value = null;
                            //TOTAL UNOBLIGATED
                            sub_unobligated_sub_total += (_fsa_amount - total_utilized_amount);
                            worksheet.Cells[startRow, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            Double unobligated = (_fsa_amount - total_utilized_amount);
                            if(unobligated > 0)
                            {
                                worksheet.Cells[startRow, 19].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells[startRow, 19].Value = unobligated;
                            } else if(unobligated < 0)
                            {
                                worksheet.Cells[startRow, 19].Style.Numberformat.Format = "(#,##0.00)";
                                worksheet.Cells[startRow, 19].Value = unobligated;
                            } else
                                worksheet.Cells[startRow, 19].Value = null;

                            //DISBURSEMENTS

                            var sub_ors_disbursements = (from ors_uacs in db.ors_expense_codes
                                                         join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                                         join allotment in db.allotments on ors.allotment equals allotment.ID
                                                         where ors.Date >= date1 && ors.Date <= date2 &&
                                                         ors.FundSource == _fsh_saa.Code &&
                                                         ors_uacs.uacs == _saa_amt.ExpenseTitle &&
                                                         ors.deleted == false
                                                         select new
                                                         {
                                                             Disbursements = (from ors_date in db.ors_date_entry where ors_date.ors_id == ors.ID && ors_date.ExpenseTitle == _saa_amt.ExpenseTitle select ors_date.NetAmount + ors_date.TaxAmount + ors_date.Others).DefaultIfEmpty(0).Sum()
                                                         }).FirstOrDefault();


                            if (sub_ors_disbursements != null && sub_ors_disbursements.Disbursements > 0)
                            {
                                sub_disbursements += sub_ors_disbursements.Disbursements;
                            }
                            sub_disbursement_sub_total += sub_disbursements;

                            // DISPLAY DISBURSEMENTS
                            worksheet.Cells[startRow, 21].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            if(sub_disbursements > 0)
                            {
                                worksheet.Cells[startRow, 21].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells[startRow, 21].Value = sub_disbursements;
                            } else  if(sub_disbursements < 0)
                            {
                                worksheet.Cells[startRow, 21].Style.Numberformat.Format = "(#,##0.00)";
                                worksheet.Cells[startRow, 21].Value = sub_disbursements;
                            } else
                                worksheet.Cells[startRow, 21].Value = null;

                            //DISPLAY PERCENTAGE
                            // percent = ((sub_disbursements - _fsa_amount) / _fsa_amount) * 100;
                            // percent = Math.Round(Math.Abs(percent), 2);
                            percent = sub_disbursements / _fsa_amount;
                            worksheet.Cells[startRow, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            worksheet.Cells[startRow, 20].Style.Numberformat.Format = "#0.00%";
                            worksheet.Cells[startRow, 20].Value = percent;

                            startRow++;

                            total += _saa_amt.Amount;
                        }

                        sub_allotment_received_grand_total += sub_allotment_received_sub_total;
                        sub_allotment_total_after_realignment_grand_total += sub_allotment_total_after_realignment_sub_total;
                        sub_current_month_grand_total += sub_current_month_sub_total;
                        sub_as_of_month_grand_total += sub_as_of_month_sub_total;
                        sub_unobligated_grand_total += sub_unobligated_sub_total;
                        sub_disbursement_grand_total += sub_disbursement_sub_total;




                        // SUB ALLOTMENT ALLOTMENT TITLE
                        worksheet.Cells[startRow, 1].Style.Font.Name = "TAHOMA";
                        worksheet.Cells[startRow, 1].Style.Font.Size = 12;
                        worksheet.Cells[startRow, 1].Style.Font.Bold = true;
                        worksheet.Cells[startRow, 1].Value = "SUBTOTAL " + _fsh_saa.Code.ToString().ToUpper();


                        // SUB ALLOTMENT RECEIVED SUB TOTAL
                        worksheet.Cells[startRow, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells[startRow, 13].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells[startRow, 13].Style.Font.Size = 10;
                        worksheet.Cells[startRow, 13].Style.Font.Bold = true;
                        worksheet.Cells[startRow, 13].Value = sub_allotment_received_sub_total > 0 ? sub_allotment_received_sub_total : 0;

                        // SUB ALLOTMENT REALIGNMENTS SUBTOTAL
                        // SUB ALLOTMENT REALIGNMENTS TO SUBTOTAL

                        // SUB ALLOTMENT TOTAL AFTER REALIGNMENTS SUBTOTAL
                        worksheet.Cells[startRow, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells[startRow, 16].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells[startRow, 16].Style.Font.Size = 10;
                        worksheet.Cells[startRow, 16].Style.Font.Bold = true;
                        worksheet.Cells[startRow, 16].Value = sub_allotment_total_after_realignment_sub_total > 0 ? sub_allotment_total_after_realignment_sub_total : 0;


                        // SUB ALLOTMENT CURRENT MONTH SUBTOTAL
                        // SUB ALLOTMENT AS OF THIS MONTH SUBTOTAL
                        worksheet.Cells[startRow, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells[startRow, 19].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells[startRow, 19].Style.Font.Size = 10;
                        worksheet.Cells[startRow, 19].Style.Font.Bold = true;
                        worksheet.Cells[startRow, 19].Value = sub_current_month_sub_total > 0 ? sub_current_month_sub_total : 0;


                        //_thead.AddCell(new PdfPCell(new Paragraph(sub_as_of_month_sub_total > 0 ? sub_as_of_month_sub_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        worksheet.Cells[startRow, 30].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                        worksheet.Cells[startRow, 30].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells[startRow, 30].Style.Font.Size = 10;
                        worksheet.Cells[startRow, 30].Style.Font.Bold = true;
                        worksheet.Cells[startRow, 30].Value = sub_as_of_month_sub_total > 0 ? sub_as_of_month_sub_total : 0;
                        // SUB ALLOTMENT UNBLIGATED SUBTOTAL
                        worksheet.Cells[startRow, 31].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells[startRow, 31].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells[startRow, 31].Style.Font.Size = 10;
                        worksheet.Cells[startRow, 31].Style.Font.Bold = true;
                        worksheet.Cells[startRow, 31].Value = sub_unobligated_sub_total > 0 ? sub_unobligated_sub_total : 0;
                        // SUB ALLOTMENT DISBURSEMENTS SUBTOTAL
                        worksheet.Cells[startRow, 32].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells[startRow, 32].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells[startRow, 32].Style.Font.Size = 10;
                        worksheet.Cells[startRow, 32].Style.Font.Bold = true;
                        worksheet.Cells[startRow, 32].Value = sub_disbursement_sub_total > 0 ? sub_disbursement_sub_total : 0;

                        //percent = ((sub_disbursement_sub_total - sub_allotment_total_after_realignment_sub_total) / sub_allotment_total_after_realignment_sub_total) * 100;
                        //percent = Math.Round(Math.Abs(percent), 2);
                        percent = sub_disbursement_sub_total / sub_allotment_received_sub_total;

                        worksheet.Cells[startRow, 33].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells[startRow, 33].Style.Numberformat.Format = "#0.00%";
                        worksheet.Cells[startRow, 33].Style.Font.Size = 10;
                        worksheet.Cells[startRow, 33].Style.Font.Bold = true;
                        worksheet.Cells[startRow, 33].Value = percent;

                        startRow++;
                    }

                    var sub_dic_allotment_received = new Dictionary<String, Double>();

                    sub_dic_allotment_received.Add("ALLOTMENT_RECEIVED", sub_allotment_received_grand_total);
                    sub_dic_allotment_received.Add("TOTAL_AFTER_REALIGNMENT", sub_allotment_total_after_realignment_grand_total);
                    sub_dic_allotment_received.Add("CURRENT_MONTH_GRAND_TOTAL", sub_current_month_grand_total);
                    sub_dic_allotment_received.Add("AS_OF_MONTH_GRAND_TOTAL", sub_as_of_month_grand_total);
                    sub_dic_allotment_received.Add("UNOBLIGATED_GRAND_TOTAL", sub_unobligated_grand_total);
                    sub_dic_allotment_received.Add("DISBURSEMENTS", sub_disbursement_grand_total);

                    sub_allotments_row_totals.Add(_allotments.Code, sub_dic_allotment_received);
                }
            }
            // INCREMENT startRow to add blank row before displaying grand totals
            startRow++;

            // DISPLAY ALL ALLOTMENT GRAND TOTAL


            foreach (var d in allotments_row_totals.Keys)
            {

                worksheet.Cells[startRow, 2].Style.Font.Name = "TAHOMA";
                worksheet.Cells[startRow, 2].Style.Font.Size = 9;
                worksheet.Cells[startRow, 2].Style.Font.Bold = true;
                worksheet.Cells[startRow, 2].Value = "TOTAL " + d;


                worksheet.Cells[startRow, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 13].Style.Font.Size = 10;
                worksheet.Cells[startRow, 13].Style.Font.Bold = true;
                worksheet.Cells[startRow, 13].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 13].Value = allotments_row_totals[d]["ALLOTMENT_RECEIVED"] > 0 ? allotments_row_totals[d]["ALLOTMENT_RECEIVED"].ToString("N", new CultureInfo("en-US")) : "";

                worksheet.Cells[startRow, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 16].Style.Font.Size = 10;
                worksheet.Cells[startRow, 16].Style.Font.Bold = true;
                worksheet.Cells[startRow, 16].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 16].Value = allotments_row_totals[d]["TOTAL_AFTER_REALIGNMENT"] > 0 ? allotments_row_totals[d]["TOTAL_AFTER_REALIGNMENT"].ToString("N", new CultureInfo("en-US")) : "";


                worksheet.Cells[startRow, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 17].Style.Font.Size = 10;
                worksheet.Cells[startRow, 17].Style.Font.Bold = true;
                worksheet.Cells[startRow, 17].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 17].Value = allotments_row_totals[d]["CURRENT_MONTH_GRAND_TOTAL"] > 0 ? allotments_row_totals[d]["CURRENT_MONTH_GRAND_TOTAL"].ToString("N", new CultureInfo("en-US")) : "";


                worksheet.Cells[startRow, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 18].Style.Font.Size = 10;
                worksheet.Cells[startRow, 18].Style.Font.Bold = true;
                worksheet.Cells[startRow, 18].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 18].Value = allotments_row_totals[d]["AS_OF_MONTH_GRAND_TOTAL"] > 0 ? allotments_row_totals[d]["AS_OF_MONTH_GRAND_TOTAL"].ToString("N", new CultureInfo("en-US")) : "";

                worksheet.Cells[startRow, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 19].Style.Font.Size = 10;
                worksheet.Cells[startRow, 19].Style.Font.Bold = true;
                worksheet.Cells[startRow, 19].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 19].Value = allotments_row_totals[d]["UNOBLIGATED_GRAND_TOTAL"] > 0 ? allotments_row_totals[d]["UNOBLIGATED_GRAND_TOTAL"].ToString("N", new CultureInfo("en-US")) : "";


                worksheet.Cells[startRow, 21].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 21].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 21].Style.Font.Size = 10;
                worksheet.Cells[startRow, 21].Style.Font.Bold = true;
                worksheet.Cells[startRow, 21].Value = allotments_row_totals[d]["DISBURSEMENTS"] > 0 ? allotments_row_totals[d]["DISBURSEMENTS"].ToString("N", new CultureInfo("en-US")) : "";


                //percent = ((Convert.ToDouble(allotments_row_totals[d]["DISBURSEMENTS"]) - Convert.ToDouble(allotments_row_totals[d]["TOTAL_AFTER_REALIGNMENT"])) / Convert.ToDouble(allotments_row_totals[d]["TOTAL_AFTER_REALIGNMENT"])) * 100;
                //percent = Math.Round(Math.Abs(percent), 2);
                percent = Convert.ToDouble(allotments_row_totals[d]["DISBURSEMENTS"].ToString()) / Convert.ToDouble(allotments_row_totals[d]["ALLOTMENT_RECEIVED"].ToString());

                worksheet.Cells[startRow, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 20].Style.Numberformat.Format = "#0.00%";
                worksheet.Cells[startRow, 20].Style.Font.Size = 10;
                worksheet.Cells[startRow, 20].Style.Font.Bold = true;
                worksheet.Cells[startRow, 20].Value = percent;

                startRow++;
            }

            foreach (var d in sub_allotments_row_totals.Keys)
            {

                worksheet.Cells[startRow, 2].Style.Font.Name = "TAHOMA";
                worksheet.Cells[startRow, 2].Style.Font.Size = 9;
                worksheet.Cells[startRow, 2].Style.Font.Bold = true;
                worksheet.Cells[startRow, 2].Value = "TOTAL SAA " + d;


                worksheet.Cells[startRow, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 13].Style.Font.Size = 10;
                worksheet.Cells[startRow, 13].Style.Font.Bold = true;
                worksheet.Cells[startRow, 13].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 13].Value = sub_allotments_row_totals[d]["ALLOTMENT_RECEIVED"] > 0 ? sub_allotments_row_totals[d]["ALLOTMENT_RECEIVED"].ToString("N", new CultureInfo("en-US")) : "";

                worksheet.Cells[startRow, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 16].Style.Font.Size = 10;
                worksheet.Cells[startRow, 16].Style.Font.Bold = true;
                worksheet.Cells[startRow, 16].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 16].Value = sub_allotments_row_totals[d]["TOTAL_AFTER_REALIGNMENT"] > 0 ? sub_allotments_row_totals[d]["TOTAL_AFTER_REALIGNMENT"].ToString("N", new CultureInfo("en-US")) : "";


                worksheet.Cells[startRow, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 17].Style.Font.Size = 10;
                worksheet.Cells[startRow, 17].Style.Font.Bold = true;
                worksheet.Cells[startRow, 17].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 17].Value = sub_allotments_row_totals[d]["CURRENT_MONTH_GRAND_TOTAL"] > 0 ? sub_allotments_row_totals[d]["CURRENT_MONTH_GRAND_TOTAL"].ToString("N", new CultureInfo("en-US")) : "";


                worksheet.Cells[startRow, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 18].Style.Font.Size = 10;
                worksheet.Cells[startRow, 18].Style.Font.Bold = true;
                worksheet.Cells[startRow, 18].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 18].Value = sub_allotments_row_totals[d]["AS_OF_MONTH_GRAND_TOTAL"] > 0 ? sub_allotments_row_totals[d]["AS_OF_MONTH_GRAND_TOTAL"].ToString("N", new CultureInfo("en-US")) : "";

                worksheet.Cells[startRow, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 19].Style.Font.Size = 10;
                worksheet.Cells[startRow, 19].Style.Font.Bold = true;
                worksheet.Cells[startRow, 19].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 19].Value = sub_allotments_row_totals[d]["UNOBLIGATED_GRAND_TOTAL"] > 0 ? sub_allotments_row_totals[d]["UNOBLIGATED_GRAND_TOTAL"].ToString("N", new CultureInfo("en-US")) : "";


                worksheet.Cells[startRow, 21].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 21].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 21].Style.Font.Size = 10;
                worksheet.Cells[startRow, 21].Style.Font.Bold = true;
                worksheet.Cells[startRow, 21].Value = sub_allotments_row_totals[d]["DISBURSEMENTS"] > 0 ? sub_allotments_row_totals[d]["DISBURSEMENTS"].ToString("N", new CultureInfo("en-US")) : "";


                //percent = ((Convert.ToDouble(sub_allotments_row_totals[d]["DISBURSEMENTS"]) - Convert.ToDouble(sub_allotments_row_totals[d]["TOTAL_AFTER_REALIGNMENT"])) / Convert.ToDouble(sub_allotments_row_totals[d]["TOTAL_AFTER_REALIGNMENT"])) * 100;
                //percent = Math.Round(Math.Abs(percent), 2);

                percent = Convert.ToDouble(sub_allotments_row_totals[d]["DISBURSEMENTS"].ToString()) / Convert.ToDouble(sub_allotments_row_totals[d]["ALLOTMENT_RECEIVED"].ToString());

                worksheet.Cells[startRow, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[startRow, 20].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[startRow, 20].Style.Font.Size = 10;
                worksheet.Cells[startRow, 20].Style.Font.Bold = true;
                worksheet.Cells[startRow, 20].Value = percent > 0 ? percent + "%" : "";

                startRow++;
            }

            pck.Save();
        }
    }
}