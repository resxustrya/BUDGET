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

            var allotments = db.allotments.Where(p => p.year == GlobalData.Year).ToList();

            foreach (Allotments _allotments in allotments)
            {
                _thead.AddCell(new PdfPCell(new Paragraph(_allotments.Title.ToUpper(), new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_LEFT });
                
            }
            doc.Add(_thead);
            doc.Close();
        }
    }
}