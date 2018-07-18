using System.Web;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using BUDGET.Models;
using System.Globalization;


namespace BUDGET
{
    public class SaobSheet2 : PdfPageEventHelper
    {
        private string month;
        public override void OnEndPage(PdfWriter writer, Document document)
        {

            PdfPTable _thead_page_break = new PdfPTable(7);
            _thead_page_break.WidthPercentage = 100f;
            float[] _columnWidths = { 50, 100, 50, 50, 50, 50, 50 };
            _thead_page_break.SetWidths(_columnWidths);
            _thead_page_break.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin; //this centers [table]


            //FIRST ROW
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("P/P/A", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_CENTER });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("PARTICULARS", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("ALLOTMENT ", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("TRANSFER TO", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("TOTAL AFTER REALIGNMENT", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("OBLIGATION", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
            _thead_page_break.AddCell(new PdfPCell(new Paragraph("BALANCE", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });



            _thead_page_break.WriteSelectedRows(0, -1, 36, 993, writer.DirectContentUnder);

        }
        public void setHeaderMonth(string month)
        {
            this.month = month;
        }
    }
}