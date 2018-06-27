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
        
        public void generate_saob()
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
            PdfPtableEvents events = new PdfPtableEvents();
            writer.PageEvent = events;

            doc.Open();

            Double total = 0f;

            


            PdfPTable _thead = new PdfPTable(10);
            _thead.WidthPercentage = 100f;
            float[] columnWidths = { 130,30,40,40,40,40,40,40,40,30};
            _thead.SetWidths(columnWidths);
            // _thead.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin; //this centers [table]

            /*
            //FIRST ROW
            _thead.AddCell(new PdfPCell(new Paragraph("P/A/P/ ALLOTMENT CLASS / OBJECT OF EXPENDITURE", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER , VerticalAlignment = Element.ALIGN_CENTER, Rowspan = 4});
            _thead.AddCell(new PdfPCell(new Paragraph("EXPENSES CODE", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 4 });
            _thead.AddCell(new PdfPCell(new Paragraph("ALLOTMENT RECEIVED", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 4 });
            _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 3 });
            _thead.AddCell(new PdfPCell(new Paragraph("OBLIGATIONS INCURRED", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2 });
            _thead.AddCell(new PdfPCell(new Paragraph("UNOBLIGATED BALANCE OF ALLOTMENT", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER , Rowspan = 4});
            _thead.AddCell(new PdfPCell(new Paragraph("REMARKS", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 4 });


            //SECOND ROW
            
            _thead.AddCell(new PdfPCell(new Paragraph("REALIGNMENT", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            _thead.AddCell(new PdfPCell(new Paragraph("TRANSFER TO", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            _thead.AddCell(new PdfPCell(new Paragraph("TOTAL AFTER REALIGNMENT", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            _thead.AddCell(new PdfPCell(new Paragraph("March", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            _thead.AddCell(new PdfPCell(new Paragraph("As of March", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            */


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
               

                var fsh = db.fsh.Where(p => p.allotment == _allotments.ID.ToString()).ToList();
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
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });

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
                            _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                            _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                        }

                    }
                }
            }
            //_thead.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - 36, writer.DirectContentUnder);
            doc.Add(_thead);
            doc.Close();
        }
    }
}