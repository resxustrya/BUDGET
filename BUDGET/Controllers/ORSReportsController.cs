using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using BUDGET.DataHelpers;
using BUDGET.Models;
using System.Globalization;
namespace BUDGET.Controllers
{
    [OutputCache(Duration = 0)]
    public class ORSReportsController : Controller
    {
        ORSReporting rpt_ors = new ORSReporting();
        BudgetDB db = new BudgetDB();
        
        // GET: ORSReports
        public ActionResult  PrintOrsPS(String id)
        {
            
            Int32 ID = Convert.ToInt32(id);
            var ors = db.ors.Where(p => p.ID == ID).FirstOrDefault();
            
            var ors_uacs = db.ors_expense_codes.Where(p => p.ors_obligation == ors.ID).ToList();

            String filename = "ors.pdf";
            Document doc = new Document(PageSize.A4);
            try
            {
                System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/rpt_ors/" + filename));
            }
            catch
            { }

            var output = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/rpt_ors/" + filename), FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, output);
            doc.Open();



            Paragraph header_text = new Paragraph("OBLIGATION REQUEST AND STATUS");

            header_text.Font = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
            header_text.Alignment = Element.ALIGN_CENTER;
            doc.Add(header_text);

            Paragraph nextline = new Paragraph("\n");
            doc.Add(nextline);
            PdfPTable table = new PdfPTable(3);
            table.PaddingTop = 5f;
            table.WidthPercentage = 100f;
            float[] columnWidths = { 5, 25, 15 };
            table.SetWidths(columnWidths);

            Image logo = Image.GetInstance(Server.MapPath("~/Content/img/ro7.png"));
            logo.ScaleAbsolute(60f, 60f);
            PdfPCell logo_cell = new PdfPCell(logo);
            logo_cell.DisableBorderSide(8);

            logo_cell.Padding = 5f;
            table.AddCell(logo_cell);

            Font arial_font_10 = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK);

            var table2 = new PdfPTable(1);
            table2.DefaultCell.Border = 0;
            table2.AddCell(new PdfPCell(new Paragraph("Republic of Philippines", arial_font_10)) { Padding = 6f, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
            table2.AddCell(new PdfPCell(new Paragraph("Department Of Health", arial_font_10)) { Padding = 6f, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
            table2.AddCell(new PdfPCell(new Paragraph("Regional Office 7", arial_font_10)) { Padding = 6f, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
            table2.AddCell(new PdfPCell(new Paragraph("Central Visayas, Osmeña Blvd. Cebu City", arial_font_10)) { Padding = 6f, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });


            var no_left_bor = new PdfPCell(table2);
            no_left_bor.DisableBorderSide(4);
            table.AddCell(no_left_bor);

            var table3 = new PdfPTable(2);
            float[] table3widths = { 4, 10 };
            table3.SetWidths(table3widths);
            table3.DefaultCell.Border = 0;

            Font column3_font = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK);

            table3.AddCell(new PdfPCell(new Paragraph("No :", arial_font_10)) { Padding = 6f, Border = 0 });
            table3.AddCell(new PdfPCell(new Paragraph("02011101-20180", column3_font)) { Border = 2, Padding = 6f, HorizontalAlignment = Element.ALIGN_CENTER, PaddingRight = 5 });

            table3.AddCell(new PdfPCell(new Paragraph("Date :", arial_font_10)) { Padding = 6f, Border = 0 });
            table3.AddCell(new PdfPCell(new Paragraph(DateTime.Now.ToShortDateString(), column3_font)) { Border = 2, Padding = 6f, HorizontalAlignment = Element.ALIGN_CENTER, PaddingRight = 5 });

            table3.AddCell(new PdfPCell(new Paragraph("Fund :", arial_font_10)) { Padding = 6f, Border = 0 });
            table3.AddCell(new PdfPCell(new Paragraph("02101101", column3_font)) { Padding = 6f, Border = 2, HorizontalAlignment = Element.ALIGN_CENTER, PaddingRight = 5 });

            table3.AddCell(new PdfPCell(new Paragraph("", arial_font_10)) { Padding = 6f, Border = 0 });
            table3.AddCell(new PdfPCell(new Paragraph("", column3_font)) { Padding = 6f, Border = 2, HorizontalAlignment = Element.ALIGN_CENTER, PaddingRight = 5, PaddingBottom = 4 });

            table.AddCell(table3);

            doc.Add(table);


            var table_row_2 = new PdfPTable(3);
            float[] tbt_row2_width = { 5, 15, 10 };
            table_row_2.WidthPercentage = 100f;
            table_row_2.SetWidths(tbt_row2_width);
            table_row_2.AddCell(new PdfPCell(new Paragraph("Payee", arial_font_10)) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_2.AddCell(new PdfPCell(new Paragraph(ors.PAYEE, arial_font_10)));
            table_row_2.AddCell(new PdfPCell(new Paragraph("", arial_font_10)));


            doc.Add(table_row_2);

            var table_row_3 = new PdfPTable(3);
            float[] tbt_row3_width = { 5, 15, 10 };
            table_row_3.WidthPercentage = 100f;
            table_row_3.SetWidths(tbt_row3_width);
            table_row_3.AddCell(new PdfPCell(new Paragraph("Office", arial_font_10)) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_3.AddCell(new PdfPCell(new Paragraph("Department of Health", arial_font_10)));
            table_row_3.AddCell(new PdfPCell(new Paragraph()));

            doc.Add(table_row_3);

            var table_row_4 = new PdfPTable(3);
            float[] tbt_row4_width = { 5, 15, 10 };
            table_row_4.WidthPercentage = 100f;
            table_row_4.SetWidths(tbt_row4_width);
            table_row_4.AddCell(new PdfPCell(new Paragraph("Address", arial_font_10)) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_4.AddCell(new PdfPCell(new Paragraph(ors.Adress, arial_font_10)));
            table_row_4.AddCell(new PdfPCell(new Paragraph()));


            doc.Add(table_row_4);

            Font table_row_5_font = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK);

            var table_row_5 = new PdfPTable(5);
            float[] tbt_row5_width = { 5, 10, 5, 5, 5 };
            table_row_5.WidthPercentage = 100f;
            table_row_5.SetWidths(tbt_row5_width);
            table_row_5.AddCell(new PdfPCell(new Paragraph("Responsibility", table_row_5_font)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
            table_row_5.AddCell(new PdfPCell(new Paragraph("Particulars", table_row_5_font)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
            table_row_5.AddCell(new PdfPCell(new Paragraph("MFO/PAP", table_row_5_font)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
            table_row_5.AddCell(new PdfPCell(new Paragraph("UACS Code/ Expenditure", table_row_5_font)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
            table_row_5.AddCell(new PdfPCell(new Paragraph("Amount", table_row_5_font)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

            doc.Add(table_row_5);

            var table_row_6 = new PdfPTable(5);
            float[] tbt_ro6_width = { 5, 10, 5, 5, 5 };
            table_row_6.WidthPercentage = 100f;
            table_row_6.SetWidths(tbt_ro6_width);

            Double total_amt = 0.00;
            String str_amt = "";
            String uacs = "";
            foreach(var u in ors_uacs)
            {
                uacs += u.uacs + "\n";
                str_amt += u.amount.ToString("N", new CultureInfo("en-US")) + "\n";
                total_amt += u.amount;
            }


            table_row_6.AddCell(new PdfPCell(new Paragraph("\n" + ors.FundSource, table_row_5_font)) { Border = 13, FixedHeight = 150f, HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_6.AddCell(new PdfPCell(new Paragraph("\n" + ors.Particulars, table_row_5_font)) { Border = 13, FixedHeight = 150f, HorizontalAlignment = Element.ALIGN_LEFT });
            table_row_6.AddCell(new PdfPCell(new Paragraph("\n", table_row_5_font)) { Border = 13, FixedHeight = 150f, HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_6.AddCell(new PdfPCell(new Paragraph("\n" + uacs, table_row_5_font)) { Border = 13, FixedHeight = 150f, HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_6.AddCell(new PdfPCell(new Paragraph("\n" + str_amt, table_row_5_font)) { Border = 13, FixedHeight = 150f, HorizontalAlignment = Element.ALIGN_RIGHT });
            doc.Add(table_row_6);



            var table_row_7 = new PdfPTable(5);
            float[] tbt_row7_width = { 5, 10, 5, 5, 5 };

            table_row_7.WidthPercentage = 100f;
            table_row_7.SetWidths(tbt_row7_width);
            table_row_7.AddCell(new PdfPCell(new Paragraph("", table_row_5_font)) { Border = 14, HorizontalAlignment = Element.ALIGN_CENTER });



            //REMOVE BORDER
            PdfPTable po_dv = new PdfPTable(2);
            po_dv.WidthPercentage = 100f;

            po_dv.AddCell(new PdfPCell(new Paragraph("PO No." + ors.PO, table_row_5_font)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
            po_dv.AddCell(new PdfPCell(new Paragraph("PR No. " + ors.PR, table_row_5_font)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

            po_dv.AddCell(new PdfPCell(new Paragraph("DV No. " + ors.DB, table_row_5_font)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
            po_dv.AddCell(new PdfPCell(new Phrase("Total", table_row_5_font)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
            // END OF REMOVE BORDER
            table_row_7.AddCell(new PdfPCell(po_dv) { Border = 14 });



            table_row_7.AddCell(new PdfPCell(new Paragraph("", table_row_5_font)) { Border = 14, HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_7.AddCell(new PdfPCell(new Paragraph("", table_row_5_font)) { Border = 14, HorizontalAlignment = Element.ALIGN_CENTER });


            PdfPTable tbt_total_amt = new PdfPTable(1);
            float[] tbt_total_amt_width = { 10 };

            tbt_total_amt.WidthPercentage = 100f;
            tbt_total_amt.SetWidths(tbt_total_amt_width);
            tbt_total_amt.AddCell(new PdfPCell(new Paragraph("\n", table_row_5_font)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
            tbt_total_amt.AddCell(new PdfPCell(new Paragraph(total_amt.ToString("N", new CultureInfo("en-US")), table_row_5_font)) { HorizontalAlignment = Element.ALIGN_RIGHT });
            table_row_7.AddCell(new PdfPCell(tbt_total_amt) { Border = 14 });

            doc.Add(table_row_7);


            PdfPTable table_row_8 = new PdfPTable(2);
            table_row_8.WidthPercentage = 100f;
            table_row_8.SetWidths(new float[] { 20, 20 });


            PdfPTable table_row8_col1 = new PdfPTable(2);
            table_row8_col1.WidthPercentage = 100f;
            table_row8_col1.SetWidths(new float[] { 20, 80 });

            PdfPTable table_row8_col1_col1 = new PdfPTable(1);
            table_row8_col1_col1.WidthPercentage = 100f;
            table_row8_col1_col1.SetWidths(new float[] { 20 });

            table_row8_col1_col1.AddCell(new PdfPCell(new Paragraph("A.", FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK))));
            table_row8_col1_col1.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK))) { Padding = 2, FixedHeight = 48 });
            table_row8_col1_col1.AddCell(new PdfPCell(new Paragraph("Signature :", FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK))) { Padding = 2 });
            table_row8_col1_col1.AddCell(new PdfPCell(new Paragraph("Printed Name :", FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK))) { Padding = 2 });
            table_row8_col1_col1.AddCell(new PdfPCell(new Paragraph("Position :", FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK))) { Padding = 2 });
            table_row8_col1_col1.AddCell(new PdfPCell(new Paragraph("\n")) { FixedHeight = 35, Border = 0 });
            table_row8_col1_col1.AddCell(new PdfPCell(new Paragraph("Date :", FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK))) { Padding = 2 });
            table_row8_col1_col1.AddCell(new PdfPCell(new Paragraph("\n")));

            table_row8_col1.AddCell(new PdfPCell(table_row8_col1_col1));

            PdfPTable table_row8_col1_col2 = new PdfPTable(1);
            table_row8_col1_col2.WidthPercentage = 100f;
            table_row8_col1_col2.SetWidths(new float[] { 80 });

            table_row8_col1_col2.AddCell(new PdfPCell(new Paragraph("Certified:", FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK))));
            table_row8_col1_col2.AddCell(new PdfPCell(new Paragraph("Charges to appropriatio/ allotment necessary, lawful and under my direct supervision; and supporting document valid, proper and legal", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { FixedHeight = 48 });
            table_row8_col1_col2.AddCell(new PdfPCell(new Paragraph("", FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK))));
            table_row8_col1_col2.AddCell(new PdfPCell(new Paragraph("GUY R. PEREZ, MD, RPT, FPSMS, MBAHA, CESE", FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row8_col1_col2.AddCell(new PdfPCell(new Paragraph("OIC - Chief - Regulation, Liscensing, Enforcement Division", FontFactory.GetFont("Arial", 6, Font.BOLD, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2.5f });
            table_row8_col1_col2.AddCell(new PdfPCell(new Paragraph("\n")) { FixedHeight = 25 });

            table_row8_col1_col2.AddCell(new PdfPCell(new Paragraph("Head Requesting Office / Authorized Representative", FontFactory.GetFont("Arial", 5, Font.BOLD, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2.5f });
            table_row8_col1_col2.AddCell(new PdfPCell(new Paragraph(DateTime.Now.ToShortDateString(), FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2.5f });
            table_row8_col1_col2.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2.5f });
            table_row8_col1.AddCell(new PdfPCell(table_row8_col1_col2));


            PdfPTable table_row8_col2 = new PdfPTable(2);
            table_row8_col2.WidthPercentage = 100f;
            table_row8_col2.SetWidths(new float[] { 20, 80 });


            PdfPTable table_row8_col2_col1 = new PdfPTable(1);
            table_row8_col2_col1.WidthPercentage = 100f;
            table_row8_col2_col1.SetWidths(new float[] { 20 });



            table_row8_col2_col1.AddCell(new PdfPCell(new Paragraph("B.", FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK))));
            table_row8_col2_col1.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK))) { Padding = 2, FixedHeight = 48 });
            table_row8_col2_col1.AddCell(new PdfPCell(new Paragraph("Signature :", FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK))) { Padding = 2 });
            table_row8_col2_col1.AddCell(new PdfPCell(new Paragraph("Printed Name :", FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK))) { Padding = 2 });
            table_row8_col2_col1.AddCell(new PdfPCell(new Paragraph("Position :", FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK))) { Padding = 2 });
            table_row8_col2_col1.AddCell(new PdfPCell(new Paragraph("\n")) { FixedHeight = 35, Border = 0 });
            table_row8_col2_col1.AddCell(new PdfPCell(new Paragraph("Date :", FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK))) { Padding = 2 });
            table_row8_col2_col1.AddCell(new PdfPCell(new Paragraph("\n")));

            table_row8_col2.AddCell(new PdfPCell(table_row8_col2_col1));

            PdfPTable table_row8_col2_col2 = new PdfPTable(1);
            table_row8_col2_col2.WidthPercentage = 100f;
            table_row8_col2_col2.SetWidths(new float[] { 20 });

            table_row8_col2_col2.AddCell(new PdfPCell(new Paragraph("Certified:", FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK))));
            table_row8_col2_col2.AddCell(new PdfPCell(new Paragraph("Allotment available and obligated for the purpose/adjustment necesarry as indicated above", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { FixedHeight = 48 });
            table_row8_col2_col2.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK))));
            table_row8_col2_col2.AddCell(new PdfPCell(new Paragraph("LEONORA A. ANIEL", FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row8_col2_col2.AddCell(new PdfPCell(new Paragraph("BUDGET OFFICER III", FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2.5f });
            table_row8_col2_col2.AddCell(new PdfPCell(new Paragraph("\n")) { FixedHeight = 25 });
            table_row8_col2_col2.AddCell(new PdfPCell(new Paragraph("Head, Budget Unit/Authorized Representative", FontFactory.GetFont("Arial", 5, Font.BOLD, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2.5f });
            table_row8_col2_col2.AddCell(new PdfPCell(new Paragraph(DateTime.Now.ToShortDateString(), FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2.5f });
            table_row8_col2_col2.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 2.5f });

            table_row8_col2.AddCell(new PdfPCell(table_row8_col2_col2));

            table_row_8.AddCell(new PdfPCell(table_row8_col1) { Border = 15 });
            table_row_8.AddCell(new PdfPCell(table_row8_col2) { Border = 15 });

            //doc.Add(table_row_8);

            PdfPTable table_row_9 = new PdfPTable(2);
            table_row_9.WidthPercentage = 100f;
            table_row_9.SetWidths(new float[] { 10, 90 });
            table_row_9.AddCell(new PdfPCell(new Paragraph("C.", FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK))));
            table_row_9.AddCell(new PdfPCell(new Paragraph("STATUS OF OBLIGATION", FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });

            doc.Add(table_row_9);

            PdfPTable table_row_10 = new PdfPTable(2);
            table_row_10.WidthPercentage = 100f;
            table_row_10.SetWidths(new float[] { 50, 50 });
            table_row_10.AddCell(new PdfPCell(new Paragraph("Reference", FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_10.AddCell(new PdfPCell(new Paragraph("Amount", FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });

            doc.Add(table_row_10);

            PdfPTable table_row_11 = new PdfPTable(7);
            table_row_11.WidthPercentage = 100f;
            table_row_11.SetWidths(new float[] { 12, 20, 28, 15, 15, 10, 20 });

            table_row_11.AddCell(new PdfPCell(new Paragraph("Date", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 35, VerticalAlignment = Element.ALIGN_MIDDLE });
            table_row_11.AddCell(new PdfPCell(new Paragraph("Particulars", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 35, VerticalAlignment = Element.ALIGN_MIDDLE });
            table_row_11.AddCell(new PdfPCell(new Paragraph("ORS/JEV/RCI/RADAI No.", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 35, VerticalAlignment = Element.ALIGN_MIDDLE });
            table_row_11.AddCell(new PdfPCell(new Paragraph("Obligation", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 35, VerticalAlignment = Element.ALIGN_MIDDLE });
            table_row_11.AddCell(new PdfPCell(new Paragraph("Payment", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 35, VerticalAlignment = Element.ALIGN_MIDDLE });
            table_row_11.AddCell(new PdfPCell(new Paragraph("Not Yet Due", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 35, VerticalAlignment = Element.ALIGN_MIDDLE });
            table_row_11.AddCell(new PdfPCell(new Paragraph("Due and \n Demandable", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 35, VerticalAlignment = Element.ALIGN_MIDDLE });

            doc.Add(table_row_11);

            PdfPTable table_row_12 = new PdfPTable(7);
            table_row_12.WidthPercentage = 100f;
            table_row_12.SetWidths(new float[] { 12, 20, 28, 15, 15, 10, 20 });

            table_row_12.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 100, Border = 13 });
            table_row_12.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 100, Border = 13 });
            table_row_12.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 100 });
            table_row_12.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 100 });
            table_row_12.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 100 });
            table_row_12.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 100 });
            table_row_12.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 100 });

            doc.Add(table_row_12);

            PdfPTable table_row_13 = new PdfPTable(7);
            table_row_13.WidthPercentage = 100f;
            table_row_13.SetWidths(new float[] { 12, 20, 28, 15, 15, 10, 20 });


            table_row_13.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 12 });
            table_row_13.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 12 });
            table_row_13.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_13.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_13.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_13.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_13.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });

            doc.Add(table_row_13);


            PdfPTable table_row_14 = new PdfPTable(7);
            table_row_14.WidthPercentage = 100f;
            table_row_14.SetWidths(new float[] { 12, 20, 28, 15, 15, 10, 20 });

            table_row_14.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 14 });
            table_row_14.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Border = 14 });
            table_row_14.AddCell(new PdfPCell(new Paragraph("Totals", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_14.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_14.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_14.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_14.AddCell(new PdfPCell(new Paragraph("\n", FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER });

            doc.Add(table_row_14);


            doc.Close();

            var fileStream = new FileStream(Server.MapPath("~/rpt_ors/" + filename),
                                     FileMode.Open,
                                     FileAccess.Read
                                   );
            var fsResult = new FileStreamResult(fileStream, "application/pdf");

            return fsResult;
        }

        
    }
}