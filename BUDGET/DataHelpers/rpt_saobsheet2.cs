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


namespace BUDGET
{
    public class rpt_saobsheet2
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

            Document doc = new Document(PageSize.LEGAL);
            var output = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/rpt_saob/saobsheet2.pdf"), FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, output);

            doc.Open();


            PdfPTable header_table = new PdfPTable(3);
            header_table.WidthPercentage = 100f;
            float[] _header_columnWidths = { 50, 100, 50 };
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

            header_table.AddCell(new PdfPCell(logo2) { Rowspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, Border = 0 });

            header_table.AddCell(new PdfPCell(p2) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 0 });
            header_table.AddCell(new PdfPCell(p3) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 0 });

            doc.Add(header_table);

            doc.NewPage();

            SaobSheet2 saobsheet2 = new SaobSheet2();
            writer.PageEvent = saobsheet2;
            saobsheet2.setHeaderMonth(date1.ToString("MMMM"));


            PdfPTable _thead = new PdfPTable(7);
            _thead.WidthPercentage = 100f;
            float[] _columnWidths = { 50, 100, 50, 50, 50, 50, 50 };
            _thead.SetWidths(_columnWidths);

            Double grand_total = 0;


            var allotments = db.allotments.Where(p => p.year == GlobalData.Year).ToList();

            Double allotment_total = 0;
            foreach (Allotments _allotments in allotments)
            {
                allotment_total = 0;
                _thead.AddCell(new PdfPCell(new Paragraph(_allotments.Title.ToUpper(), new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0, Colspan = 7 });

                var fsh = db.fsh.Where(p => p.allotment == _allotments.ID.ToString() && p.type == "REG").ToList();

                Double fundsource_total = 0;
                foreach (FundSourceHdr _fsh in fsh)
                {
                    fundsource_total = 0;
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });
                    _thead.AddCell(new PdfPCell(new Paragraph(_fsh.SourceTitle.ToUpper().ToString(), new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });

                    var fsa = (from list in db.fsa
                               join expensecode
                                in db.uacs on list.expense_title equals expensecode.Code
                               where list.fundsource == _fsh.ID.ToString()
                               select new
                               {
                                   Amount = list.amount
                               }
                       ).ToList();
                    Double fsa_total_amount = 0;
                    foreach (var _fsa in fsa)
                    {
                        fsa_total_amount += _fsa.Amount;
                    }

                    fundsource_total += fsa_total_amount;
                    allotment_total += fundsource_total;

                    _thead.AddCell(new PdfPCell(new Paragraph(fsa_total_amount > 0 ? fsa_total_amount.ToString("N",new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });


                }

                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                _thead.AddCell(new PdfPCell(new Paragraph("SUBTOTAL", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph(fundsource_total > 0 ? fundsource_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });


                Double saa_allotment_total = 0;
                var _sub_allotments = db.fsh.Where(p => p.allotment == _allotments.ID.ToString() && p.type == "SUB").ToList();
                if (_sub_allotments.Count > 0)
                {
                    saa_allotment_total = 0;
                    _thead.AddCell(new PdfPCell(new Paragraph(_allotments.Code.ToUpper().ToString() +" SUB-ALLOTMENT", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0,Colspan = 7 });

                    Double saa_fund_source_total = 0;
                    foreach (FundSourceHdr _fsh_saa in _sub_allotments)
                    {
                        saa_fund_source_total = 0;
                        _thead.AddCell(new PdfPCell(new Paragraph(_fsh_saa.SourceTitle.ToString(), new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });
                        _thead.AddCell(new PdfPCell(new Paragraph(_fsh_saa.desc.ToString(), new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });


                        var saa_amt = (from list in db.fsa
                                       join expensecode
                                        in db.uacs on list.expense_title equals expensecode.Code
                                       where list.fundsource == _fsh_saa.ID.ToString()
                                       select new
                                       {
                                           Amount = list.amount
                                       }
                           ).ToList();
                        Double saa_fsa_amt_total = 0;
                        foreach (var _saa_amt in saa_amt)
                        {
                            saa_fsa_amt_total += _saa_amt.Amount;
                        }

                        saa_fund_source_total += saa_fsa_amt_total;
                        allotment_total += saa_fund_source_total;

                        _thead.AddCell(new PdfPCell(new Paragraph(saa_fsa_amt_total > 0 ? saa_fsa_amt_total.ToString("N",new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });
                    }

                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                    _thead.AddCell(new PdfPCell(new Paragraph("SUBTOTAL", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                    _thead.AddCell(new PdfPCell(new Paragraph(saa_fund_source_total > 0 ? saa_fund_source_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });

                }

                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                _thead.AddCell(new PdfPCell(new Paragraph("TOTAL " + _allotments.Code.ToString(), new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph(allotment_total > 0 ? allotment_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });

            }



            doc.Add(_thead);
            doc.Close();
        }
    }
}