using System.Web;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using BUDGET.Models;
using System.Globalization;
namespace BUDGET
{
    public class PdfPtableEvents : PdfPageEventHelper
    {
        private string month;
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            
            PdfPTable _thead_page_break = new PdfPTable(10);
            _thead_page_break.WidthPercentage = 100f;
            float[] _columnWidths = { 130, 30, 40, 40, 40, 40, 40, 40, 40, 30 };
            _thead_page_break.SetWidths(_columnWidths);
            _thead_page_break.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin; //this centers [table]


            //FIRST ROW
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("P/A/P/ ALLOTMENT CLASS / OBJECT OF EXPENDITURE", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_CENTER, Rowspan = 4 });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("EXPENSES CODE", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 4 });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("ALLOTMENT RECEIVED", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 4 });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 3 });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("OBLIGATIONS INCURRED", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2 });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("UNOBLIGATED BALANCE OF ALLOTMENT", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 4 });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("REMARKS", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 4 });


            //SECOND ROW

            _thead_page_break.AddCell(new PdfPCell(new Paragraph("REALIGNMENT", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("TRANSFER TO", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("TOTAL AFTER REALIGNMENT", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph(month, new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("As of " + month, new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 3 });

            

            _thead_page_break.WriteSelectedRows(0,-1,36,608, writer.DirectContentUnder);

            
           
            /*
            PdfPTable table = new PdfPTable(1);
            //table.WidthPercentage = 100; //PdfPTable.writeselectedrows below didn't like this
            table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin; //this centers [table]
            PdfPTable table2 = new PdfPTable(2);

            //logo
            PdfPCell cell2 = new PdfPCell(new Phrase("\nTITLE", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD)));
            cell2.Colspan = 2;
            table2.AddCell(cell2);

            //title
            cell2 = new PdfPCell(new Phrase("\nTITLE", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD)));
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.Colspan = 2;
            table2.AddCell(cell2);

            PdfPCell cell = new PdfPCell(table2);
            table.AddCell(cell);

            table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 36, writer.DirectContentUnder);
            */
        }
        public void setHeaderMonth(string month)
        {
            this.month = month;
        }
        
    }
}