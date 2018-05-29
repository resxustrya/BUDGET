using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using BUDGET.Models;

namespace BUDGET.DataHelpers
{
    public class rpt_saob
    {
        BudgetDB db = new BudgetDB();
        

        public void generate_saob()
        {
            
            Document doc = new Document(PageSize.LEGAL.Rotate());
            try
            {
                System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("/rpt_saob/saob.pdf"));
            }
            catch
            { }
            var output = new FileStream(System.Web.HttpContext.Current.Server.MapPath("/rpt_saob/saob.pdf"), FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, output);
            doc.Open();

            doc.Add( new Paragraph("DEPARTMENT OF HEALTH - REGIONAL OFFICE VII", new Font(Font.FontFamily.HELVETICA, 11f, Font.BOLD)) { Alignment = Element.ALIGN_CENTER });
            doc.Add(new Paragraph("STATEMENT OF ALLOTMENT, OBLIGATIONS AND BALANCES ", new Font(Font.FontFamily.HELVETICA, 11f, Font.BOLD)) { Alignment = Element.ALIGN_CENTER });
            doc.Add(new Paragraph("As of March 31, 2018", new Font(Font.FontFamily.HELVETICA, 11f, Font.BOLD)) { Alignment = Element.ALIGN_CENTER });


            doc.Add(new Chunk("\n"));

            PdfPTable _thead = new PdfPTable(11);
            _thead.WidthPercentage = 100f;
            float[] columnWidths = { 100,40,50,50,50,50,50,50,50,50,50};
            _thead.SetWidths(columnWidths);

            //FIRST ROW
            _thead.AddCell(new PdfPCell(new Paragraph("P/A/P/ ALLOTMENT CLASS / OBJECT OF EXPENDITURE", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER , Rowspan = 4});
            _thead.AddCell(new PdfPCell(new Paragraph("EXPENSES CODE", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 4 });
            _thead.AddCell(new PdfPCell(new Paragraph("ALLOTMENT RECEIVED", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 4 });
            _thead.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 3 });
            _thead.AddCell(new PdfPCell(new Paragraph("OBLIGATIONS INCURRED", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 3 });
            _thead.AddCell(new PdfPCell(new Paragraph("UNOBLIGATED BALANCE OF ALLOTMENT", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER , Rowspan = 4});
            _thead.AddCell(new PdfPCell(new Paragraph("REMARKS", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 4 });


            //SECOND ROW
            
            _thead.AddCell(new PdfPCell(new Paragraph("REALIGNMENT", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            _thead.AddCell(new PdfPCell(new Paragraph("TRANSFER TO", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            _thead.AddCell(new PdfPCell(new Paragraph("TOTAL AFTER REALIGNMENT", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            _thead.AddCell(new PdfPCell(new Paragraph("March", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            _thead.AddCell(new PdfPCell(new Paragraph("JAN-DEC TOTAL OBLIGATION", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            _thead.AddCell(new PdfPCell(new Paragraph("As of March", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            


            doc.Add(_thead);
            doc.Close();
        }
    }
}