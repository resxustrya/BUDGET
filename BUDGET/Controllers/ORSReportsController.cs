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
    [Authorize]
    [OutputCache(Duration = 0)]
    public class ORSReportsController : Controller
    {
        ORSReporting rpt_ors = new ORSReporting();
        BudgetDB db = new BudgetDB();

        // GET: ORSReports
        public ActionResult PrintOrs(String id)
        {

            Int32 ID = Convert.ToInt32(id);
            var ors = db.ors.Where(p => p.ID == ID).FirstOrDefault();

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

            var ors_uacs = (from uacs_list in db.ors_expense_codes
                            join expensecodes in db.uacs on uacs_list.uacs equals expensecodes.Title
                            where uacs_list.ors_obligation == ors.ID
                            select new
                            {
                                uacs = expensecodes.Code,
                                amount = uacs_list.amount
                            }).ToList();

            foreach (var u in ors_uacs)
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


            PdfPTable table_row_8 = new PdfPTable(4);
            float[] w_table_row_8 = { 5, 20, 5, 20 };
            table_row_8.WidthPercentage = 100f;
            table_row_8.SetWidths(w_table_row_8);


            table_row_8.AddCell(new PdfPCell(new Paragraph("A.", new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))));
            table_row_8.AddCell(new PdfPCell(new Paragraph("Certified:", new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))));
            table_row_8.AddCell(new PdfPCell(new Paragraph("B.", new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))));
            table_row_8.AddCell(new PdfPCell(new Paragraph("Certified:", new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))));



            table_row_8.AddCell(new PdfPCell(new Paragraph("")) { FixedHeight = 50f, Border = 13 });
            table_row_8.AddCell(new PdfPCell(new Paragraph("Charges to appropriation/ allotment necessary, lawful and under my direct supervision; and supporting documents valid, proper and legal \n", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { FixedHeight = 50f, Border = 13 });
            table_row_8.AddCell(new PdfPCell(new Paragraph("")) { FixedHeight = 50f, Border = 13 });
            table_row_8.AddCell(new PdfPCell(new Paragraph("Allotment available and obligated for the purpose/adjustment necessary as indicated above \n", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { FixedHeight = 50f, Border = 13 });


            //SIGNATURE 1
            table_row_8.AddCell(new PdfPCell(new Paragraph("Signature :", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { Border = 14 });

            table_row_8.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { Border = 14 });

            //SIGNATURE 2
            table_row_8.AddCell(new PdfPCell(new Paragraph("Signature :", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { Border = 14 });

            table_row_8.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { Border = 14 });


            //var ors_requesting_head = db.ors_head_request.Where(p => p.Name == ors.head_requesting_office).FirstOrDefault();

            var ors_fsh = (from list in db.ors
                           join ors_master in db.orsmaster on list.ors_id equals ors_master.ID
                           join fsh in db.fsh on ors_master.allotments.ToString() equals fsh.allotment
                           where fsh.Code == ors.FundSource && fsh.allotment == ors_master.allotments.ToString()
                           && list.ID.ToString() == id
                           select new
                           {
                               ors_head = fsh.ors_head
                           }).FirstOrDefault();

            var ors_head = db.ors_head_request.Where(p => p.ID.ToString() == ors_fsh.ors_head.ToString()).FirstOrDefault();

            table_row_8.AddCell(new PdfPCell(new Paragraph("Printed Name :", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))));
            //HEAD REQUESTING OFFICE / AUTHORIZED REPRESENTATIVE
            table_row_8.AddCell(new PdfPCell(new Paragraph(ors_head != null ? ors_head.Name : "", new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_8.AddCell(new PdfPCell(new Paragraph("Printed Name", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))));
            table_row_8.AddCell(new PdfPCell(new Paragraph("LEONORA A. ANIEL", new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });


            table_row_8.AddCell(new PdfPCell(new Paragraph("Position :", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))));
            //HEAD REQUESTING OFFICE / AUTHORIZED REPRESENTATIVE
            table_row_8.AddCell(new PdfPCell(new Paragraph(ors_head.Position, new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_8.AddCell(new PdfPCell(new Paragraph("Position", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))));
            table_row_8.AddCell(new PdfPCell(new Paragraph("BUDGET OFFICER III", new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });


            table_row_8.AddCell(new PdfPCell(new Paragraph("\n")) { FixedHeight = 30f });
            table_row_8.AddCell(new PdfPCell(new Paragraph("\n")) { FixedHeight = 30f });
            table_row_8.AddCell(new PdfPCell(new Paragraph("\n")) { FixedHeight = 30f });
            table_row_8.AddCell(new PdfPCell(new Paragraph("\n")) { FixedHeight = 30f });


            //HEAD REQUESTING OFFICE / AUTHORIZED REPRESENTATIVE
            table_row_8.AddCell(new PdfPCell(new Paragraph("")));
            table_row_8.AddCell(new PdfPCell(new Paragraph("Head Requesting Office / Authorized Representative", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_8.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))));
            table_row_8.AddCell(new PdfPCell(new Paragraph("Head, Budget Unit / Authorized Representative", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_CENTER });

            table_row_8.AddCell(new PdfPCell(new Paragraph("Date :", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });
            table_row_8.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
            table_row_8.AddCell(new PdfPCell(new Paragraph("Date", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });
            table_row_8.AddCell(new PdfPCell(new Paragraph(DateTime.Now.ToString("MM/dd/yyyy"), new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });


            doc.Add(table_row_8);

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

        [HttpGet]
        public ActionResult ORS_PAGE()
        {
            var orslist = (from list in db.orsmaster where list.Year == GlobalData.Year select list).ToList();
            return PartialView(orslist);
        }

        [HttpPost]
        public ActionResult ORS_PAGE(FormCollection collection)
        {

            string[] page = null;
            string pages = collection.Get("page");


            page = pages.Split('-');
            page[0] = page[0].Trim();
            page[1] = page[1].Trim();


            Int32 page1 = Convert.ToInt32(page[0]);
            Int32 page2 = Convert.ToInt32(page[1]);

            Int32 ors_id = Convert.ToInt32(collection.Get("ors"));


            var ors_list = (from list in db.ors where list.Row >= page1 && list.Row <= page2 && list.ors_id == ors_id orderby list.Row ascending select list).ToList();


            String filename = "pages.pdf";
            Document doc = new Document(PageSize.A4);
            try
            {
                // System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/rpt_ors/" + filename));
            }
            catch
            { }

            var output = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/rpt_ors/" + filename), FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, output);
            doc.Open();
            foreach (ORS ors in ors_list)
            {

                
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
                table3.AddCell(new PdfPCell(new Paragraph(ors.Date.ToString("yyyy-MM-dd") + "-" + ors.Row.ToString(), column3_font)) { Border = 2, Padding = 6f, HorizontalAlignment = Element.ALIGN_CENTER, PaddingRight = 5 });

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

                var ors_uacs = (from uacs_list in db.ors_expense_codes
                                join expensecodes in db.uacs on uacs_list.uacs equals expensecodes.Title
                                where uacs_list.ors_obligation == ors.ID
                                select new
                                {
                                    uacs = expensecodes.Code,
                                    amount = uacs_list.amount
                                }).ToList();
                foreach (var u in ors_uacs)
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


                PdfPTable table_row_8 = new PdfPTable(4);
                float[] w_table_row_8 = { 5, 20, 5, 20 };
                table_row_8.WidthPercentage = 100f;
                table_row_8.SetWidths(w_table_row_8);


                table_row_8.AddCell(new PdfPCell(new Paragraph("A.", new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))));
                table_row_8.AddCell(new PdfPCell(new Paragraph("Certified:", new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))));
                table_row_8.AddCell(new PdfPCell(new Paragraph("B.", new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))));
                table_row_8.AddCell(new PdfPCell(new Paragraph("Certified:", new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))));



                table_row_8.AddCell(new PdfPCell(new Paragraph("")) { FixedHeight = 50f, Border = 13 });
                table_row_8.AddCell(new PdfPCell(new Paragraph("Charges to appropriation/ allotment necessary, lawful and under my direct supervision; and supporting documents valid, proper and legal \n", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { FixedHeight = 50f, Border = 13 });
                table_row_8.AddCell(new PdfPCell(new Paragraph("")) { FixedHeight = 50f, Border = 13 });
                table_row_8.AddCell(new PdfPCell(new Paragraph("Allotment available and obligated for the purpose/adjustment necessary as indicated above \n", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { FixedHeight = 50f, Border = 13 });


                //SIGNATURE 1
                table_row_8.AddCell(new PdfPCell(new Paragraph("Signature :", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { Border = 14 });

                table_row_8.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { Border = 14 });

                //SIGNATURE 2
                table_row_8.AddCell(new PdfPCell(new Paragraph("Signature :", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { Border = 14 });

                table_row_8.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { Border = 14 });


                //var ors_requesting_head = db.ors_head_request.Where(p => p.Name == ors.head_requesting_office).FirstOrDefault();

                var ors_fsh = (from list in db.ors
                               join ors_master in db.orsmaster on list.ors_id equals ors_master.ID
                               join fsh in db.fsh on ors_master.allotments.ToString() equals fsh.allotment
                               where fsh.Code == ors.FundSource && fsh.allotment == ors_master.allotments.ToString()
                               && list.ID == ors.ID
                               select new
                               {
                                   ors_head = fsh.ors_head
                               }).FirstOrDefault();

                var ors_head = db.ors_head_request.Where(p => p.ID.ToString() == ors_fsh.ors_head.ToString()).FirstOrDefault();

                table_row_8.AddCell(new PdfPCell(new Paragraph("Printed Name :", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))));
                //HEAD REQUESTING OFFICE / AUTHORIZED REPRESENTATIVE
                table_row_8.AddCell(new PdfPCell(new Paragraph(ors_head.Name, new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
                table_row_8.AddCell(new PdfPCell(new Paragraph("Printed Name", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))));
                table_row_8.AddCell(new PdfPCell(new Paragraph("LEONORA A. ANIEL", new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });


                table_row_8.AddCell(new PdfPCell(new Paragraph("Position :", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))));
                //HEAD REQUESTING OFFICE / AUTHORIZED REPRESENTATIVE
                table_row_8.AddCell(new PdfPCell(new Paragraph(ors_head.Position, new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
                table_row_8.AddCell(new PdfPCell(new Paragraph("Position", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))));
                table_row_8.AddCell(new PdfPCell(new Paragraph("BUDGET OFFICER III", new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });


                table_row_8.AddCell(new PdfPCell(new Paragraph("\n")) { FixedHeight = 30f });
                table_row_8.AddCell(new PdfPCell(new Paragraph("\n")) { FixedHeight = 30f });
                table_row_8.AddCell(new PdfPCell(new Paragraph("\n")) { FixedHeight = 30f });
                table_row_8.AddCell(new PdfPCell(new Paragraph("\n")) { FixedHeight = 30f });


                //HEAD REQUESTING OFFICE / AUTHORIZED REPRESENTATIVE
                table_row_8.AddCell(new PdfPCell(new Paragraph("")));
                table_row_8.AddCell(new PdfPCell(new Paragraph("Head Requesting Office / Authorized Representative", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
                table_row_8.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))));
                table_row_8.AddCell(new PdfPCell(new Paragraph("Head, Budget Unit / Authorized Representative", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });

                table_row_8.AddCell(new PdfPCell(new Paragraph("Date :", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });
                table_row_8.AddCell(new PdfPCell(new Paragraph("", new Font(Font.FontFamily.HELVETICA, 6f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });
                table_row_8.AddCell(new PdfPCell(new Paragraph("Date", new Font(Font.FontFamily.HELVETICA, 6f, Font.NORMAL))) { HorizontalAlignment = Element.ALIGN_LEFT });
                table_row_8.AddCell(new PdfPCell(new Paragraph(DateTime.Now.ToString("MM/dd/yyyy"), new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_CENTER });



                doc.Add(table_row_8);

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

                doc.NewPage();

            }
            try
            {
                doc.Close();
            }
            catch
            {
                doc = null;
                output = null;
                writer = null;
            }
            

            var fileStream = new FileStream(Server.MapPath("~/rpt_ors/" + filename),
                                        FileMode.Open,
                                        FileAccess.Read
                                    );
            var fsResult = new FileStreamResult(fileStream, "application/pdf");

            return fsResult;

        }
    }
}