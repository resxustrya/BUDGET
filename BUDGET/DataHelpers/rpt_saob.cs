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
                System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("/rpt_saob/saob.pdf"));
            }
            catch
            { }

            Document doc = new Document(PageSize.LEGAL.Rotate());
            var output = new FileStream(System.Web.HttpContext.Current.Server.MapPath("/rpt_saob/saob.pdf"), FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, output);
            
            doc.Open();


            PdfPTable header_table = new PdfPTable(3);
            header_table.WidthPercentage = 100f;
            float[] _header_columnWidths = { 50,100,50 };
            header_table.SetWidths(_header_columnWidths);

            Image logo1 = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("/Content/img/ro7.png"));
            logo1.ScaleAbsolute(60f, 60f);

            header_table.AddCell(new PdfPCell(logo1) { Rowspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, Border = 0 });

           

            Paragraph p1 = new Paragraph("DEPARTMENT OF HEALTH - REGIONAL OFFICE VII");
            Paragraph p2 = new Paragraph("STATEMENT OF ALLOTMENT, OBLIGATIONS AND BALANCES");


            DateTime date1 = Convert.ToDateTime(date_from);
            DateTime date2 = Convert.ToDateTime(date_to);

            Paragraph p3 = new Paragraph("As of " + date1.ToString("MMMM") + " " + date1.ToString("dd") + " - " + date2.ToString("MMMM") + " " + date2.ToString("dd") + " " + date2.ToString("yyyy"));



            header_table.AddCell(new PdfPCell(p1) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 0 });
            

            Image logo2 = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("/Content/img/ro7.png"));
            logo2.ScaleAbsolute(60f, 60f);

            header_table.AddCell(new PdfPCell(logo2) { Rowspan = 3, HorizontalAlignment = Element.ALIGN_CENTER , Border = 0});

            header_table.AddCell(new PdfPCell(p2) { HorizontalAlignment = Element.ALIGN_CENTER ,Border = 0});
            header_table.AddCell(new PdfPCell(p3) { HorizontalAlignment = Element.ALIGN_CENTER ,Border = 0});

            doc.Add(header_table);

            doc.NewPage();

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
            foreach(Allotments _allotments in allotments)
            {
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
                        _thead.AddCell(new PdfPCell(new Paragraph(_fsa.Title.ToString().ToUpper(), new Font(Font.FontFamily.HELVETICA, 7f, Font.ITALIC))) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 25f });
                        _thead.AddCell(new PdfPCell(new Paragraph(_fsa.ExpenseCode.ToString(), new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_CENTER });
                        _thead.AddCell(new PdfPCell(new Paragraph(_fsa.Amount.ToString("N",new CultureInfo("en-US")), new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        //realignment
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        //realignment to
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        //total realignment
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });



                        var uacs_amounts = (from uacs_amt in db.ors_expense_codes
                                            join ors in db.ors on uacs_amt.ors_obligation equals ors.ID
                                            join ors_master in db.orsmaster on ors.ors_id equals ors_master.ID
                                            join allotments_hdr in db.allotments on ors_master.allotments equals allotments_hdr.ID
                                            where uacs_amt.uacs == _fsa.ExpenseCode &&
                                            ors.Date >= date1 && ors.Date <= date2
                                            select new
                                            {
                                                Amount = uacs_amt.amount
                                            }).ToList();
                        Double month_total = 0;
                        foreach(var amount in uacs_amounts)
                        {
                            month_total += amount.Amount;
                        }
                        
                        //total for the month
                        _thead.AddCell(new PdfPCell(new Paragraph(month_total > 0 ? month_total.ToString("N",new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_CENTER });
                        //total as of this month
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        //total unobligated
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        //remarks
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });

                        total += _fsa.Amount;
                    }

                    _thead.AddCell(new PdfPCell(new Paragraph("TOTAL " + _allotments.Code.ToUpper() + " " + _fsh.SourceTitle, new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 20f });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph(total.ToString("N", new CultureInfo("en-US")), new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });


                }
                
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
                            _thead.AddCell(new PdfPCell(new Paragraph(_saa_amt.Title.ToString().ToUpper(), new Font(Font.FontFamily.HELVETICA, 7f, Font.ITALIC))) { HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = 25f });
                            _thead.AddCell(new PdfPCell(new Paragraph(_saa_amt.ExpenseCode.ToString(), new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_CENTER });
                            _thead.AddCell(new PdfPCell(new Paragraph(_saa_amt.Amount.ToString("N", new CultureInfo("en-US")), new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                            _thead.AddCell(new PdfPCell(new Paragraph("realignment", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });
                            _thead.AddCell(new PdfPCell(new Paragraph("realignment to", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD
                                ))) { HorizontalAlignment = Element.ALIGN_LEFT });
                            _thead.AddCell(new PdfPCell(new Paragraph("total realignment", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                            _thead.AddCell(new PdfPCell(new Paragraph("Month", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                            _thead.AddCell(new PdfPCell(new Paragraph("As of month", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                            _thead.AddCell(new PdfPCell(new Paragraph("unobligated", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                            _thead.AddCell(new PdfPCell(new Paragraph("remarks", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        }
                    }
                }
            }
            
            doc.Add(_thead);
            doc.Close();
        }
    }
}