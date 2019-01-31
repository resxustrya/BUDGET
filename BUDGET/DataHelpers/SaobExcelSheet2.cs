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
    public class SaobExcelSheet2
    {
        public void CreateExcel(BudgetDB db,FileInfo newFile, string DateFrom, string DateTo)
        {
            DateTime date1 = Convert.ToDateTime(DateFrom);
            DateTime date2 = Convert.ToDateTime(DateTo);

            ExcelPackage pck = new ExcelPackage(newFile);
            ExcelWorksheet worksheet = pck.Workbook.Worksheets[2];

            Int32 startRow = 6;

            Double grand_total = 0;


            worksheet.Cells[3, 1].Value = "As of " + date2.ToString("MMMM dd, yyyy");
            var allotments = db.allotments.Where(p => p.year == GlobalData.Year && p.active == 1).OrderBy(p => p.Code2).ToList();

            Double allotment_total = 0;
            foreach (Allotments _allotments in allotments)
            {
                allotment_total = 0;
                //_thead.AddCell(new PdfPCell(new Paragraph(_allotments.Title.ToUpper(), new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0, Colspan = 7 });
                // DISPLAY ALLOTMENT TITLE

                worksheet.Cells[startRow, 1].Style.Font.Name = "TAHOMA";
                worksheet.Cells[startRow, 1].Style.Font.Size = 10;
                worksheet.Cells[startRow, 1].Style.Font.Bold = true;
                worksheet.Cells[startRow, 1].Value = _allotments.Title.ToUpper();
                startRow++;

                var fsh = db.fsh.Where(p => p.allotment == _allotments.ID.ToString() && p.type == "REG" && p.active == 1).ToList();

                Double fundsource_total = 0;
                foreach (FundSourceHdr _fsh in fsh)
                {
                    fundsource_total = 0;
                    //_thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });
                    //_thead.AddCell(new PdfPCell(new Paragraph(_fsh.SourceTitle.ToUpper().ToString(), new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });
                    // DISPLAY FUNDSOURCE TITLES


                    worksheet.Cells[startRow, 2].Style.Font.Name = "TAHOMA";
                    worksheet.Cells[startRow, 2].Style.Font.Size = 10;
                    worksheet.Cells[startRow, 2].Value = _fsh.SourceTitle.ToUpper();


                    var fsa = (from list in db.fsa
                               join expensecode
                                in db.uacs on list.expense_title equals expensecode.Title
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

                    //DISPLAY FUNDSOURCE ALLOTMENT & AMOUNT
                    worksheet.Cells[startRow, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    worksheet.Cells[startRow, 3].Style.Numberformat.Format = "#,##0.00";
                    worksheet.Cells[startRow, 3].Value = fsa_total_amount;

                   // var realignments = (from )

                    startRow++;
                }
                

                /*
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                _thead.AddCell(new PdfPCell(new Paragraph("SUBTOTAL", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph(fundsource_total > 0 ? fundsource_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                */

                Double saa_allotment_total = 0;
                var _sub_allotments = db.fsh.Where(p => p.allotment == _allotments.ID.ToString() && p.type == "SUB" && p.active == 1).ToList();
                if (_sub_allotments.Count > 0)
                {
                    saa_allotment_total = 0;
                    //_thead.AddCell(new PdfPCell(new Paragraph(_allotments.Code.ToUpper().ToString() + " SUB-ALLOTMENT", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0, Colspan = 7 });
                    worksheet.Cells[startRow, 1].Style.Font.Name = "TAHOMA";
                    worksheet.Cells[startRow, 1].Style.Font.Size = 10;
                    worksheet.Cells[startRow, 1].Style.Font.Bold = true;
                    worksheet.Cells[startRow, 1].Value = _allotments.Code.ToUpper() + " SUB-ALLOTMENT";
                    startRow++;


                    Double saa_fund_source_total = 0;
                    foreach (FundSourceHdr _fsh_saa in _sub_allotments)
                    {
                        saa_fund_source_total = 0;

                        //_thead.AddCell(new PdfPCell(new Paragraph(_fsh_saa.SourceTitle.ToString(), new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });
                        //_thead.AddCell(new PdfPCell(new Paragraph(_fsh_saa.desc.ToString(), new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });
                        // DISPLAY SUB-ALLOTMENT FUNDSOURCE
                        worksheet.Cells[startRow, 1].Style.Font.Name = "TAHOMA";
                        worksheet.Cells[startRow, 1].Style.Font.Size = 10;
                        worksheet.Cells[startRow, 1].Style.Font.Bold = true;
                        worksheet.Cells[startRow, 1].Value = _fsh_saa.SourceTitle.ToString();

                        worksheet.Cells[startRow, 2].Style.Font.Name = "TAHOMA";
                        worksheet.Cells[startRow, 2].Style.Font.Size = 10;
                        worksheet.Cells[startRow, 2].Value = _fsh_saa.desc.ToString();

                       

                        var saa_amt = (from list in db.fsa
                                       join expensecode
                                        in db.uacs on list.expense_title equals expensecode.Title
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



                        worksheet.Cells[startRow, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells[startRow, 3].Style.Numberformat.Format = "#,##0.00";
                        worksheet.Cells[startRow, 3].Value = saa_fsa_amt_total;
                        /*
                        _thead.AddCell(new PdfPCell(new Paragraph(saa_fsa_amt_total > 0 ? saa_fsa_amt_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });
                        _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0 });
                        */

                        startRow++;
                    }
                    
                    /*
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                    _thead.AddCell(new PdfPCell(new Paragraph("SUBTOTAL", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                    _thead.AddCell(new PdfPCell(new Paragraph(saa_fund_source_total > 0 ? saa_fund_source_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                    _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                    */
                }
                /*
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                _thead.AddCell(new PdfPCell(new Paragraph("TOTAL " + _allotments.Code.ToString(), new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph(allotment_total > 0 ? allotment_total.ToString("N", new CultureInfo("en-US")) : "", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = 1 });
                */
            }
            pck.Save();
            pck.Dispose();
        }
    }
}