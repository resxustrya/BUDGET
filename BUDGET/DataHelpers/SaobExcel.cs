using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Globalization;

namespace BUDGET
{
    public class SaobExcel
    {
        public void CreateExcel(String date_from, String date_to)
        {
            BudgetDB db = new BudgetDB();
            FileInfo excelFile = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/SAOB.xlsx"));
            var excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("~/excel_reports/SAOB.xlsx"));
            Excel.Worksheet worksheet = workbook.Sheets[1];
            Excel.Range range = worksheet.UsedRange;
           
            Int32 startRow = 16;

            DateTime date1 = Convert.ToDateTime(date_from);
            DateTime date2 = Convert.ToDateTime(date_to);

            Double total = 0.00;


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
             * ALLOTMENT SUBTOTAL VARIABLES
             */


            Double allotment_total = 0;
            var allotments = db.allotments.Where(p => p.year == GlobalData.Year).ToList();

            foreach (Allotments _allotments in allotments)
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
                worksheet.Cells[++startRow, 1] = _allotments.Title.ToUpper().ToString();
                

                var fsh = db.fsh.Where(p => p.allotment == _allotments.ID.ToString() && p.type == "REG").ToList();
                foreach (FundSourceHdr _fsh in fsh)
                {
                    total = 0;
                    Double realignment_subtotal = 0.00;
                    Double total_for_the_month = 0.00;
                    Double total_asof_the_month = 0.00;
                    Double unobligated_balance_allotment = 0.00;
                    Double disbursements = 0.00;


                    //DISPLAY PREXC CODE
                    worksheet.Cells[++startRow, 1] = _fsh.prexc.ToUpper().ToString();
                    
                    //DISPLAY FUNDSOURCE HEADER
                    worksheet.Cells[++startRow, 1] = _fsh.SourceTitle.ToUpper().ToString();


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
                        //DISPLAY EXPENSE CODE
                        //DISPLAY FUNDSOURCE AMMOUNT

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
                            total_realignment_str = total_realignment > 0 ? total_realignment.ToString("N", new CultureInfo("en-US")) : "";
                        }

                        //realignments

                       
                        //realignment to

                        
                        //total after realignment
                        realignment_subtotal += _fsa_amount;
                        



                        var uacs_amounts = (from ors_uacs in db.ors_expense_codes
                                            join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                            join ors_master in db.orsmaster on ors.ors_id equals ors_master.ID
                                            join allotments_hdr in db.allotments on ors_master.allotments equals allotments_hdr.ID
                                            where ors.Date >= date1 && ors.Date <= date2 &&
                                            ors.FundSource == _fsh.Code &&
                                            allotments_hdr.ID == _allotments.ID &&
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
                                              join ors_master in db.orsmaster on ors.ors_id equals ors_master.ID
                                              join allotments_hdr in db.allotments on ors_master.allotments equals allotments_hdr.ID
                                              where ors.FundSource == _fsh.Code
                                              && ors.Date <= date2 &&
                                              allotments_hdr.ID == _allotments.ID &&
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

                        var ors_disbursements = (from ors_uacs in db.ors_expense_codes
                                                 join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                                 join ors_master in db.orsmaster on ors.ors_id equals ors_master.ID
                                                 join allotments_hdr in db.allotments on ors_master.allotments equals allotments_hdr.ID
                                                 where ors.Date >= date1 && ors.Date <= date2 &&
                                                 ors.FundSource == _fsh.Code &&
                                                 allotments_hdr.ID == _allotments.ID &&
                                                 ors_uacs.uacs == _fsa.ExpenseTitle
                                                 select new
                                                 {
                                                     Disbursements = ors_uacs.TaxAmount + ors_uacs.NetAmount
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
                    /*
                    _thead.AddCell(new PdfPCell(new Paragraph("SUBTOTAL " + _allotments.Code.ToUpper() + " " + _fsh.SourceTitle.ToUpper(), new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 20f });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph(total.ToString("N", new CultureInfo("en-US")), new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph(realignment_subtotal > 0 ? realignment_subtotal.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    _thead.AddCell(new PdfPCell(new Paragraph(total_for_the_month > 0 ? total_for_the_month.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    _thead.AddCell(new PdfPCell(new Paragraph(total_asof_the_month > 0 ? total_asof_the_month.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    _thead.AddCell(new PdfPCell(new Paragraph(unobligated_balance_allotment > 0 ? unobligated_balance_allotment.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    _thead.AddCell(new PdfPCell(new Paragraph(disbursements > 0 ? disbursements.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
                    */

                }

                allotment_received_grand_total += allotment_received_sub_total;
                after_realignment_grand_total += after_realignment_sub_total;
                current_month_grand_total += current_month_sub_total;
                as_of_month_grand_total += as_of_month_sub_total;
                disbursement_grand_total += disbursement_sub_total;


                //  ALLOTMENT RECEIVED TOTAL

                //_thead.AddCell(new PdfPCell(new Paragraph("TOTAL " + _allotments.Code.ToUpper().ToString(), new Font(Font.FontFamily.HELVETICA, 11f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
                //EXPENSE CODES
                //_thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });


                allotment_received_grand_total = allotment_total;

                var dic_allotment_received = new Dictionary<String, Double>();

                dic_allotment_received.Add("ALLOTMENT_RECEIVED", allotment_received_grand_total);
                dic_allotment_received.Add("TOTAL_AFTER_REALIGNMENT", after_realignment_grand_total);
                dic_allotment_received.Add("CURRENT_MONTH_GRAND_TOTAL", current_month_grand_total);
                dic_allotment_received.Add("AS_OF_MONTH_GRAND_TOTAL", as_of_month_grand_total);
                dic_allotment_received.Add("UNOBLIGATED_GRAND_TOTAL", unobligated_grand_total);
                dic_allotment_received.Add("DISBURSEMENTS", disbursement_grand_total);


                allotments_row_totals.Add(_allotments.Code, dic_allotment_received);


                //ALLOTMENT RECEIVE
                //_thead.AddCell(new PdfPCell(new Paragraph(allotment_total > 0 ? allotment_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                //REALIGNMENT
              //  _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                //TRANSFER TO
               // _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                ////TOTAL AFTER REALIGNMENT
                //_thead.AddCell(new PdfPCell(new Paragraph(after_realignment_sub_total > 0 ? after_realignment_sub_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                //CURRENT MONTH
              //  _thead.AddCell(new PdfPCell(new Paragraph(current_month_sub_total > 0 ? current_month_sub_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                //AS OF MONTH
              //  _thead.AddCell(new PdfPCell(new Paragraph(as_of_month_sub_total > 0 ? as_of_month_sub_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                //UNOBLIGATED BALANCE ALLOTMENT
              //  _thead.AddCell(new PdfPCell(new Paragraph(unobligated_sub_total > 0 ? unobligated_sub_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
              //  //DISBURSEMENTS
             //   _thead.AddCell(new PdfPCell(new Paragraph(disbursement_sub_total > 0 ? disbursement_sub_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });



                /*
                 * GRAND TOTAL VARIABLES
                 * 
                 */

                Double sub_allotment_received_grand_total = 0.00;
                Double sub_allotment_total_after_realignment_grand_total = 0.00;
                Double sub_current_month_grand_total = 0.00;
                Double sub_as_of_month_grand_total = 0.00;
                Double sub_unobligated_grand_total = 0.00;
                Double sub_disbursement_grand_total = 0.00;


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
                 /*

                var _sub_allotments = db.fsh.Where(p => p.allotment == _allotments.ID.ToString() && p.type == "SUB").ToList();
                if (_sub_allotments.Count > 0)
                {
                    _thead.AddCell(new PdfPCell(new Paragraph("SUB-ALLOTMENT-" + _allotments.Code.ToUpper(), new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });

                    foreach (FundSourceHdr _fsh_saa in _sub_allotments)
                    {

                        sub_allotment_received_sub_total = 0.00;
                        sub_allotment_total_after_realignment_sub_total = 0.00;
                        sub_current_month_sub_total = 0.00;
                        sub_as_of_month_sub_total = 0.00;
                        sub_unobligated_sub_total = 0.00;
                        sub_disbursement_sub_total = 0.00;



                        _thead.AddCell(new PdfPCell(new Paragraph(_fsh_saa.prexc, new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });

                        _thead.AddCell(new PdfPCell(new Paragraph(_fsh_saa.SourceTitle, new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 10f });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });


                        _thead.AddCell(new PdfPCell(new Paragraph(_fsh_saa.desc, new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 15f });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });



                        var saa_amt = (from list in db.fsa
                                       join expensecode
                                        in db.uacs on list.expense_title equals expensecode.Code
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
                            _thead.AddCell(new PdfPCell(new Paragraph(_saa_amt.Title.ToString().ToUpper(), new Font(Font.FontFamily.HELVETICA, 7f, Font.ITALIC))) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 25f });
                            // EXPENSE CODES
                            _thead.AddCell(new PdfPCell(new Paragraph(_saa_amt.ExpenseCode.ToString(), new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_CENTER });
                            // ALLOTMENT AMOUNT

                            sub_allotment_received_sub_total += _saa_amt.Amount;

                            _thead.AddCell(new PdfPCell(new Paragraph(_saa_amt.Amount.ToString("N", new CultureInfo("en-US")), new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });


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
                                total_realignment_str = total_realignment > 0 ? total_realignment.ToString("N", new CultureInfo("en-US")) : "";
                            }


                            // REALIGNMENTS

                            _thead.AddCell(new PdfPCell(new Paragraph(total_realignment_str, new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                            //REALIGNMENT TO

                            _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });
                            //TOTAL AFTER REALIGNMENT

                            sub_allotment_total_after_realignment_sub_total += _fsa_amount;
                            _thead.AddCell(new PdfPCell(new Paragraph(_fsa_amount > 0 ? _fsa_amount.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });



                            var uacs_amounts = (from ors_uacs in db.ors_expense_codes
                                                join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                                join ors_master in db.orsmaster on ors.ors_id equals ors_master.ID
                                                join allotments_hdr in db.allotments on ors_master.allotments equals allotments_hdr.ID
                                                where ors.Date >= date1 && ors.Date <= date2 &&
                                                ors.FundSource == _fsh_saa.Code &&
                                                ors_uacs.uacs == _saa_amt.ExpenseCode
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
                            _thead.AddCell(new PdfPCell(new Paragraph(month_total > 0 ? month_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });


                            var total_utilized = (from ors_uacs in db.ors_expense_codes
                                                  join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                                  join ors_master in db.orsmaster on ors.ors_id equals ors_master.ID
                                                  join allotments_hdr in db.allotments on ors_master.allotments equals allotments_hdr.ID
                                                  where ors.FundSource == _fsh_saa.Code &&
                                                  ors_uacs.uacs == _saa_amt.ExpenseCode
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
                            _thead.AddCell(new PdfPCell(new Paragraph(total_utilized_amount > 0 ? total_utilized_amount.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                            //TOTAL UNBLIGATED
                            sub_unobligated_sub_total += (_fsa_amount - total_utilized_amount);

                            _thead.AddCell(new PdfPCell(new Paragraph((_fsa_amount - total_utilized_amount).ToString("N", new CultureInfo("en-US")), new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                            //DISBURSEMENTS

                            var sub_ors_disbursements = (from ors_uacs in db.ors_expense_codes
                                                         join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                                         join ors_master in db.orsmaster on ors.ors_id equals ors_master.ID
                                                         join allotments_hdr in db.allotments on ors_master.allotments equals allotments_hdr.ID
                                                         where ors.Date >= date1 && ors.Date <= date2 &&
                                                         ors.FundSource == _fsh_saa.Code &&
                                                         ors_uacs.uacs == _saa_amt.ExpenseCode
                                                         select new
                                                         {
                                                             Disbursements = ors_uacs.TaxAmount + ors_uacs.NetAmount
                                                         }).FirstOrDefault();

                            if (sub_ors_disbursements != null && sub_ors_disbursements.Disbursements > 0)
                            {
                                sub_disbursements += sub_ors_disbursements.Disbursements;
                            }
                            sub_disbursement_sub_total += sub_disbursements;
                            _thead.AddCell(new PdfPCell(new Paragraph(sub_disbursements > 0 ? sub_disbursements.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_CENTER });

                            total += _saa_amt.Amount;
                        }


                        sub_allotment_received_grand_total += sub_allotment_received_sub_total;
                        sub_allotment_total_after_realignment_grand_total += sub_allotment_total_after_realignment_sub_total;
                        sub_current_month_grand_total += sub_current_month_sub_total;
                        sub_as_of_month_grand_total += sub_as_of_month_sub_total;
                        sub_unobligated_grand_total += sub_unobligated_sub_total;
                        sub_disbursement_grand_total += sub_disbursement_sub_total;



                        var sub_dic_allotment_received = new Dictionary<String, Double>();

                        sub_dic_allotment_received.Add("ALLOTMENT_RECEIVED", sub_allotment_received_grand_total);
                        sub_dic_allotment_received.Add("TOTAL_AFTER_REALIGNMENT", sub_allotment_total_after_realignment_grand_total);
                        sub_dic_allotment_received.Add("CURRENT_MONTH_GRAND_TOTAL", sub_current_month_grand_total);
                        sub_dic_allotment_received.Add("AS_OF_MONTH_GRAND_TOTAL", sub_as_of_month_grand_total);
                        sub_dic_allotment_received.Add("UNOBLIGATED_GRAND_TOTAL", sub_unobligated_grand_total);
                        sub_dic_allotment_received.Add("DISBURSEMENTS", sub_disbursement_grand_total);


                        sub_allotments_row_totals.Add(_allotments.Code, sub_dic_allotment_received);


                        // SUB ALLOTMENT ALLOTMENT TITLE
                        _thead.AddCell(new PdfPCell(new Paragraph("SUBTOTAL " + _fsh_saa.Code.ToString().ToUpper(), new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 20f });
                        // SUB ALLOTMENT EXPENSE CODES
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        // SUB ALLOTMENT RECEIVED SUB TOTAL
                        _thead.AddCell(new PdfPCell(new Paragraph(sub_allotment_received_sub_total > 0 ? sub_allotment_received_sub_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        // SUB ALLOTMENT REALIGNMENTS SUBTOTAL
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        // SUB ALLOTMENT REALIGNMENTS TO SUBTOTAL
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        // SUB ALLOTMENT TOTAL AFTER REALIGNMENTS SUBTOTAL
                        _thead.AddCell(new PdfPCell(new Paragraph(sub_allotment_total_after_realignment_sub_total > 0 ? sub_allotment_total_after_realignment_sub_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        // SUB ALLOTMENT CURRENT MONTH SUBTOTAL
                        _thead.AddCell(new PdfPCell(new Paragraph(sub_current_month_sub_total > 0 ? sub_current_month_sub_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        // SUB ALLOTMENT AS OF THIS MONTH SUBTOTAL
                        _thead.AddCell(new PdfPCell(new Paragraph(sub_as_of_month_sub_total > 0 ? sub_as_of_month_sub_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        // SUB ALLOTMENT UNBLIGATED SUBTOTAL
                        _thead.AddCell(new PdfPCell(new Paragraph(sub_unobligated_sub_total > 0 ? sub_unobligated_sub_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        // SUB ALLOTMENT DISBURSEMENTS SUBTOTAL
                        _thead.AddCell(new PdfPCell(new Paragraph(sub_disbursement_sub_total > 0 ? sub_disbursement_sub_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });

                    }
                }*/
            }

            /*

            // BLANK ROW
            _thead.AddCell(new PdfPCell(new Paragraph(" ", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
            _thead.AddCell(new PdfPCell(new Paragraph(" ", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
            _thead.AddCell(new PdfPCell(new Paragraph(" ", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
            _thead.AddCell(new PdfPCell(new Paragraph(" ", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
            _thead.AddCell(new PdfPCell(new Paragraph(" ", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
            _thead.AddCell(new PdfPCell(new Paragraph(" ", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
            _thead.AddCell(new PdfPCell(new Paragraph(" ", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
            _thead.AddCell(new PdfPCell(new Paragraph(" ", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
            _thead.AddCell(new PdfPCell(new Paragraph(" ", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
            _thead.AddCell(new PdfPCell(new Paragraph(" ", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });

            
            // DISPLAY ALL ALLOTMENT GRAND TOTAL

            foreach (var d in allotments_row_totals.Keys)
            {
                _thead.AddCell(new PdfPCell(new Paragraph("TOTAL " + d, new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 20f });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph(allotments_row_totals[d]["ALLOTMENT_RECEIVED"] > 0 ? allotments_row_totals[d]["ALLOTMENT_RECEIVED"].ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph(allotments_row_totals[d]["TOTAL_AFTER_REALIGNMENT"] > 0 ? allotments_row_totals[d]["TOTAL_AFTER_REALIGNMENT"].ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                _thead.AddCell(new PdfPCell(new Paragraph(allotments_row_totals[d]["CURRENT_MONTH_GRAND_TOTAL"] > 0 ? allotments_row_totals[d]["CURRENT_MONTH_GRAND_TOTAL"].ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                _thead.AddCell(new PdfPCell(new Paragraph(allotments_row_totals[d]["AS_OF_MONTH_GRAND_TOTAL"] > 0 ? allotments_row_totals[d]["AS_OF_MONTH_GRAND_TOTAL"].ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                _thead.AddCell(new PdfPCell(new Paragraph(allotments_row_totals[d]["UNOBLIGATED_GRAND_TOTAL"] > 0 ? allotments_row_totals[d]["UNOBLIGATED_GRAND_TOTAL"].ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                _thead.AddCell(new PdfPCell(new Paragraph(allotments_row_totals[d]["DISBURSEMENTS"] > 0 ? allotments_row_totals[d]["DISBURSEMENTS"].ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });

            }

            // DISPLAY ALL SUB ALLOTMENT GRAND TOTAL


            foreach (var d in sub_allotments_row_totals.Keys)
            {
                _thead.AddCell(new PdfPCell(new Paragraph("TOTAL SAA " + d, new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 20f });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph(sub_allotments_row_totals[d]["ALLOTMENT_RECEIVED"] > 0 ? sub_allotments_row_totals[d]["ALLOTMENT_RECEIVED"].ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph(sub_allotments_row_totals[d]["TOTAL_AFTER_REALIGNMENT"] > 0 ? sub_allotments_row_totals[d]["TOTAL_AFTER_REALIGNMENT"].ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                _thead.AddCell(new PdfPCell(new Paragraph(sub_allotments_row_totals[d]["CURRENT_MONTH_GRAND_TOTAL"] > 0 ? sub_allotments_row_totals[d]["CURRENT_MONTH_GRAND_TOTAL"].ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                _thead.AddCell(new PdfPCell(new Paragraph(sub_allotments_row_totals[d]["AS_OF_MONTH_GRAND_TOTAL"] > 0 ? sub_allotments_row_totals[d]["AS_OF_MONTH_GRAND_TOTAL"].ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                _thead.AddCell(new PdfPCell(new Paragraph(sub_allotments_row_totals[d]["UNOBLIGATED_GRAND_TOTAL"] > 0 ? sub_allotments_row_totals[d]["UNOBLIGATED_GRAND_TOTAL"].ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                _thead.AddCell(new PdfPCell(new Paragraph(sub_allotments_row_totals[d]["DISBURSEMENTS"] > 0 ? sub_allotments_row_totals[d]["DISBURSEMENTS"].ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });

            }


            */










            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(range);
            Marshal.ReleaseComObject(worksheet);
            workbook.Close();
            Marshal.ReleaseComObject(workbook);

            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);

        }
    }
}