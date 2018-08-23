using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using BUDGET.Models;
using System.Globalization;

namespace BUDGET.DataHelpers
{
    public class rpt_saob
    {
        BudgetDB db = new BudgetDB();
        
        public void generate_saob(String date_from, String date_to)
        {
            try
            {
                //System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/rpt_saob/saob.pdf"));
            }
            catch
            { }


            Document doc = new Document(PageSize.LEGAL.Rotate());
            var output = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/rpt_saob/saob.pdf"), FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, output);
            
            doc.Open();


            PdfPTable header_table = new PdfPTable(3);
            header_table.WidthPercentage = 100f;
            float[] _header_columnWidths = { 50,100,50 };
            header_table.SetWidths(_header_columnWidths);

            Image logo1 = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Content/img/ro7.png"));
            logo1.ScaleAbsolute(60f, 60f);

            header_table.AddCell(new PdfPCell(logo1) { Rowspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, Border = 0 });

            DateTime date1 = Convert.ToDateTime(date_from);
            DateTime date2 = Convert.ToDateTime(date_to);

            Paragraph p1 = new Paragraph("DEPARTMENT OF HEALTH - REGIONAL OFFICE VII");
            Paragraph p2 = new Paragraph("STATEMENT OF ALLOTMENT, OBLIGATIONS AND BALANCES");


            Paragraph p3 = new Paragraph("As of " + date1.ToString("MMMM") + " " + date1.ToString("dd") + " - " + date2.ToString("MMMM") + " " + date2.ToString("dd") + " " + date2.ToString("yyyy"));



            header_table.AddCell(new PdfPCell(p1) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 0 });
            

            Image logo2 = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Content/img/ro7.png"));
            logo2.ScaleAbsolute(60f, 60f);

            header_table.AddCell(new PdfPCell(logo2) { Rowspan = 3, HorizontalAlignment = Element.ALIGN_CENTER , Border = 0});

            header_table.AddCell(new PdfPCell(p2) { HorizontalAlignment = Element.ALIGN_CENTER ,Border = 0});
            header_table.AddCell(new PdfPCell(p3) { HorizontalAlignment = Element.ALIGN_CENTER ,Border = 0});

            //doc.Add(header_table);

           // doc.NewPage();

            PdfPTable footer = new PdfPTable(3);

            footer.WidthPercentage = 100f;
            float[] _footer_columnWidths = { 100, 100, 100 };
            header_table.SetWidths(_footer_columnWidths);

            Paragraph prepared_by = new Paragraph("Prepared By:");
            Paragraph certified_correct = new Paragraph("Certified Correct:");
            Paragraph approved = new Paragraph("APPROVED:");


            footer.AddCell(new PdfPCell(prepared_by) { Border = 0 });
            footer.AddCell(new PdfPCell(certified_correct) { Border = 0 });
            footer.AddCell(new PdfPCell(approved) { Border = 0 });

            footer.AddCell(new PdfPCell(new Paragraph("\n")) { Border = 0 });
            footer.AddCell(new PdfPCell(new Paragraph("\n")) { Border = 0 });
            footer.AddCell(new PdfPCell(new Paragraph("\n")) { Border = 0 });

            footer.AddCell(new PdfPCell(new Paragraph("\n")) { Border = 0 });
            footer.AddCell(new PdfPCell(new Paragraph("\n")) { Border = 0 });
            footer.AddCell(new PdfPCell(new Paragraph("\n")) { Border = 0 });


            footer.AddCell(new PdfPCell(new Paragraph("\n")) { Border = 0 });
            footer.AddCell(new PdfPCell(new Paragraph("\n")) { Border = 0 });
            footer.AddCell(new PdfPCell(new Paragraph("\n")) { Border = 0 });


            footer.AddCell(new PdfPCell(new Paragraph("Timothy John D. Arriesgado", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD))) { Border = 0 });
            footer.AddCell(new PdfPCell(new Paragraph("Leonora A. Aniel", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD))) { Border = 0 });
            footer.AddCell(new PdfPCell(new Paragraph("Jaime  S. Bernadas, MD, MGM, CESO lll", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD))) { Border = 0 });

            footer.AddCell(new PdfPCell(new Paragraph("Administrative Assistant II")) { Border = 0 });
            footer.AddCell(new PdfPCell(new Paragraph("Budget Officer III")) { Border = 0 });
            footer.AddCell(new PdfPCell(new Paragraph("Director IV")) { Border = 0 });

           // doc.Add(footer);


            //doc.NewPage();



            PdfPtableEvents events = new PdfPtableEvents();
            writer.PageEvent = events;
            events.setHeaderMonth(date1.ToString("MMMM"));

            PdfPTable _thead = new PdfPTable(10);
            _thead.WidthPercentage = 100f;
            float[] columnWidths = { 130,30,40,40,40,40,40,40,40,30};
            _thead.SetWidths(columnWidths);
            
            Double total = 0.00;


            _thead.SpacingAfter = 100f;

            var allotments = db.allotments.Where(p => p.year == GlobalData.Year).ToList();
            Double allotment_total = 0;
            foreach(Allotments _allotments in allotments)
            {
                allotment_total = 0;
                _thead.AddCell(new PdfPCell(new Paragraph(_allotments.Title.ToUpper(), new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
               

                var fsh = db.fsh.Where(p => p.allotment == _allotments.ID.ToString() && p.type == "REG").ToList();
                foreach(FundSourceHdr _fsh in fsh)
                {
                    total = 0;
                    Double realignment_subtotal = 0.00;
                    Double total_for_the_month = 0.00;
                    Double total_asof_the_month = 0.00;
                    Double unobligated_balance_allotment = 0.00;

                    _thead.AddCell(new PdfPCell(new Paragraph(_fsh.prexc, new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    


                    _thead.AddCell(new PdfPCell(new Paragraph(_fsh.SourceTitle.ToUpper(), new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT});
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                   


                    var fsa = (from list in db.fsa
                               join expensecode
                                in db.uacs on list.expensecode equals expensecode.Code
                               where list.fundsource == _fsh.ID.ToString()
                               select new
                               {
                                   ID = list.ID,
                                   ExpenseCode = list.expensecode,
                                   Title = expensecode.Title,
                                   Amount = list.amount
                               }
                       ).ToList();
                    total = 0;
                    foreach (var _fsa in fsa)
                    {
                        Double _fsa_amount = _fsa.Amount;
                        _thead.AddCell(new PdfPCell(new Paragraph(_fsa.Title.ToString().ToUpper(), new Font(Font.FontFamily.HELVETICA, 7f, Font.ITALIC))) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 25f });
                        _thead.AddCell(new PdfPCell(new Paragraph(_fsa.ExpenseCode.ToString(), new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_CENTER });
                        _thead.AddCell(new PdfPCell(new Paragraph(_fsa.Amount.ToString("N",new CultureInfo("en-US")), new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });


                        var realignments_from = (from realignment in db.realignment
                                                 join _rel_fsh in db.fsh on realignment.fundsource equals _rel_fsh.ID.ToString()
                                                 join _rel_allotment in db.allotments on _rel_fsh.allotment equals _rel_allotment.ID.ToString()
                                                 where realignment.uacs_from == _fsa.ExpenseCode
                                                 && realignment.fundsource == _fsh.ID.ToString()
                                                 && _rel_allotment.ID == _allotments.ID
                                                 select new
                                                 {
                                                     Amount = realignment.amount
                                                 }).ToList();


                        Double total_realignment = 0;
                        String total_realignment_str = "";
                        

                        if(realignments_from.Count > 0)
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
                                                   where realignment.uacs_to == _fsa.ExpenseCode
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

                        _thead.AddCell(new PdfPCell(new Paragraph(total_realignment_str, new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        //realignment to

                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        //total after realignment
                        realignment_subtotal += _fsa_amount;
                        _thead.AddCell(new PdfPCell(new Paragraph(_fsa_amount > 0 ? _fsa_amount.ToString("N",new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });



                        var uacs_amounts = (from ors_uacs in db.ors_expense_codes
                                            join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                            join ors_master in db.orsmaster on ors.ors_id equals ors_master.ID
                                            join allotments_hdr in db.allotments on ors_master.allotments equals allotments_hdr.ID
                                            where ors.Date >= date1 && ors.Date <= date2 &&
                                            ors.FundSource == _fsh.Code &&
                                            allotments_hdr.ID == _allotments.ID &&
                                            ors_uacs.uacs == _fsa.ExpenseCode                                           
                                            select new
                                            {
                                                Amount = ors_uacs.amount
                                            }).ToList();



                        Double month_total = 0;
                        foreach(var amount in uacs_amounts)
                        {
                            month_total += amount.Amount;
                        }


                        total_for_the_month += month_total;


                        //total for the month
                        _thead.AddCell(new PdfPCell(new Paragraph(month_total > 0 ? month_total.ToString("N",new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });



                        var total_utilized = (from ors_uacs in db.ors_expense_codes
                                            join ors in db.ors on ors_uacs.ors_obligation equals ors.ID
                                            join ors_master in db.orsmaster on ors.ors_id equals ors_master.ID
                                            join allotments_hdr in db.allotments on ors_master.allotments equals allotments_hdr.ID
                                            where ors.FundSource == _fsh.Code
                                            && ors.Date <= date2 &&
                                            allotments_hdr.ID == _allotments.ID &&
                                            ors_uacs.uacs == _fsa.ExpenseCode

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
                        _thead.AddCell(new PdfPCell(new Paragraph(total_utilized_amount > 0 ? total_utilized_amount.ToString("N",new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                       
                        //unobligated computation - subtotal per fund source
                        
                        unobligated_balance_allotment += (_fsa_amount - total_utilized_amount);
                        //total unobligated
                        _thead.AddCell(new PdfPCell(new Paragraph((_fsa_amount - total_utilized_amount).ToString("N", new CultureInfo("en-US")), new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        //remarks
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });

                        total += _fsa.Amount;
                    }
                    allotment_total += total;

                    _thead.AddCell(new PdfPCell(new Paragraph("SUBTOTAL " + _allotments.Code.ToUpper() + " " + _fsh.SourceTitle.ToUpper(), new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 20f });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph(total.ToString("N", new CultureInfo("en-US")), new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph(realignment_subtotal > 0 ? realignment_subtotal.ToString("N",new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    _thead.AddCell(new PdfPCell(new Paragraph(total_for_the_month > 0 ? total_for_the_month.ToString("N",new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    _thead.AddCell(new PdfPCell(new Paragraph(total_asof_the_month > 0 ? total_asof_the_month.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    _thead.AddCell(new PdfPCell(new Paragraph(unobligated_balance_allotment > 0 ? unobligated_balance_allotment.ToString("N",new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });


                }

                
                //  ALLOTMENT TOTAL

                _thead.AddCell(new PdfPCell(new Paragraph("TOTAL " + _allotments.Code.ToUpper().ToString(), new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT});
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph(allotment_total > 0 ? allotment_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });





                var _sub_allotments = db.fsh.Where(p => p.allotment == _allotments.ID.ToString() && p.type == "SUB").ToList();
                if(_sub_allotments.Count > 0)
                {
                    _thead.AddCell(new PdfPCell(new Paragraph("SUB-ALLOTMENT-"+_allotments.Code.ToUpper(), new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
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

                        _thead.AddCell(new PdfPCell(new Paragraph(_fsh_saa.SourceTitle, new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 10f });
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
                                        in db.uacs on list.expensecode equals expensecode.Code
                                       where list.fundsource == _fsh_saa.ID.ToString()
                                       select new
                                       {
                                           ID = list.ID,
                                           ExpenseCode = list.expensecode,
                                           Title = expensecode.Title,
                                           Amount = list.amount
                                       }
                           ).ToList();
                        

                        foreach (var _saa_amt in saa_amt)
                        {
                            Double _fsa_amount = _saa_amt.Amount;
                            _thead.AddCell(new PdfPCell(new Paragraph(_saa_amt.Title.ToString().ToUpper(), new Font(Font.FontFamily.HELVETICA, 7f, Font.ITALIC))) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 25f });
                            _thead.AddCell(new PdfPCell(new Paragraph(_saa_amt.ExpenseCode.ToString(), new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph(_saa_amt.Amount.ToString("N", new CultureInfo("en-US")), new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });


                            var realignments_from = (from realignment in db.realignment
                                                     join _rel_fsh in db.fsh on realignment.fundsource equals _rel_fsh.ID.ToString()
                                                     join _rel_allotment in db.allotments on _rel_fsh.allotment equals _rel_allotment.ID.ToString()
                                                     where realignment.uacs_from == _saa_amt.ExpenseCode
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
                                                       where realignment.uacs_to == _saa_amt.ExpenseCode
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


                            //realignments

                            _thead.AddCell(new PdfPCell(new Paragraph(total_realignment_str, new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                            //realignment to

                            _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });
                            //total after realignment


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



                            //total for the month
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



                            //total as of this month
                            _thead.AddCell(new PdfPCell(new Paragraph(total_utilized_amount > 0 ? total_utilized_amount.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                            //total unobligated
                            _thead.AddCell(new PdfPCell(new Paragraph( (_fsa_amount - total_utilized_amount).ToString("N",new CultureInfo("en-US")) , new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                            //remarks
                            _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });

                            total += _saa_amt.Amount;
                        }
                    }
                }
            }
            
            doc.Add(_thead);
            doc.Close();
        }
    }
}