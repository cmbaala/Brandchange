using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace HRMS.CommonUtility
{
    public class PdfGeneration
    {
        private static Font catFont = new Font(Font.FontFamily.TIMES_ROMAN, 8,
            Font.BOLD);
        private static Font subBFont = new Font(Font.FontFamily.TIMES_ROMAN, 8,
                Font.BOLD);
        private static Font subFont = new Font(Font.FontFamily.TIMES_ROMAN, 8,
                Font.NORMAL);

        private static Font smallBold = new Font(Font.FontFamily.TIMES_ROMAN, 8,Font.BOLD, BaseColor.WHITE);
        private static BaseColor myColor = WebColors.GetRGBColor("#0062A1");
        private static Font subFontheader = new Font(Font.FontFamily.TIMES_ROMAN, 6, Font.NORMAL, WebColors.GetRGBColor("#0062A1"));
        private static Font subFontheaderheading = new Font(Font.FontFamily.TIMES_ROMAN, 7, Font.BOLD, WebColors.GetRGBColor("#0062A1"));
        private static int footerRows = 1;

        private static String[] earningsHeading = new String[] { "Basic Pay", "HRA", "Telephone Allowance", "Dearness Allowance", "Conveyance", "Loyality Incentives", "Performance Incentives", "Others" };
        private static String[] earningsValues = new String[] { "3750.00", "1875.00", "0.00", "0.00", "800.00", "0.00", "6400.00", "0.00" };
        private static String[] deductionsHeading = new String[] { "Provident Fund", "ESI Deductions", "TDS", "Others", "Professional Tax", "Loss of Pay" };
        private static String[] deductionsValues = new String[] { "450.00", "225.0", "0.00", "0.00", "0.00" };
        private static String[] additionalInfo = new String[] { "Additional site Allowance", "Arrears of last month salary", "EMI deduction" };

        public static void generatePdf(string fileName, Pay pay, string month, int year)
        {

            ////reset response
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/pdf";

            ////define pdf filename
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            using (MemoryStream ms = new MemoryStream())

            {
            Document document = new Document(PageSize.A4, 36, 36, 54, 54);
            PdfWriter.GetInstance(document, ms);
            //document.setPageSize();
            // document.setPageSize(new Rectangle(500,800));
            document.Open();


            addMetaData(document);

            addTitlePage(document, month, year);

            addempdetails(document, pay.getEmployee());
            document.Add(new Paragraph("\n"));
            addemppaydetails(document, pay);

            //addTestTable(document);
            //addContent(document);
            //createTable(document);

            document.Close();
            HttpContext.Current.Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);

            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();

            }


        
        }
        public static void addMetaData(Document document)
        {
            document.AddTitle("Kaspon Employee Payslip");
            document.AddSubject("Payslip of Kaspon Employees");
            document.AddKeywords("Payslip, Earnings, Deductions");
            document.AddAuthor("Kaspon Techworks (P) LTD");
            document.AddCreator("Balasubramaniyan M");

        }

         public static void addTitlePage(Document document,String month,int year) {

        PdfPTable toptable = new PdfPTable(2);

        Paragraph address = new Paragraph();
        Image image = null;

        try {
            image = Image.GetInstance(HttpContext.Current.Server.MapPath("~/images/logo.png"));
            //image.setAlignment(Image.MIDDLE);
            image.ScaleAbsolute(100, 30);
            image.Alignment=Image.ALIGN_LEFT;
        } catch (Exception e) {
            
        } 

        address.Add(new Paragraph("Kaspon Techworks Pvt Limited\n", subFontheaderheading));

        address.Add(new Paragraph("#123, Devloped Plots Estate\n", subFontheader));

        address.Add(new Paragraph("Off OMR,Perungudi,Chennai-96\n", subFontheader));

        address.Add(new Paragraph("Tel : +91-44 42125741,42125914\n", subFontheader));

        address.Add(new Paragraph("http://www.kaspontech.com\n", subFontheader));
        address.Add(new Paragraph(""));

        PdfPCell addresCell = new PdfPCell();
        addresCell.AddElement(image);
        addresCell.AddElement(address);
        addresCell.Border = 0;
        //addresCell.SetBorder(0);
        toptable.AddCell(addresCell);


        PdfPCell lCell = new PdfPCell(new Paragraph(""));
        lCell.Border = 0;
       // lCell.setBorder(0);
        toptable.AddCell(lCell);
        addEmptyCell(toptable, 2,1);

        lCell = new PdfPCell(new Phrase("Payslip for the month of "+month+" "+year, smallBold));

        lCell.HorizontalAlignment = Element.ALIGN_CENTER;
      //  lCell.setHorizontalAlignment(Element.ALIGN_CENTER);
        lCell.Colspan = 2;
        lCell.Top = 2;
        lCell.Border = 0;
        lCell.BackgroundColor = myColor;
       //lCell.setColspan(2);
        //lCell.setTop(2);



        //lCell.setBackgroundColor(myColor);
      // lCell.setBorder(0);

        toptable.AddCell(lCell);
        document.Add(toptable);

    }
     public static void addEmptyCell(PdfPTable table, int n, int f)
         {
             for (int i = 0; i < n; i++)
             {
                 PdfPCell t = new PdfPCell(new Paragraph("  ", subFont));
                 if (f == 1)
                     t.Border=0;
                 if (f == 2)
                 {
                     t.BackgroundColor = BaseColor.GRAY;
                     t.BorderColor = myColor;
                     //t.setBackgroundColor(BaseColor.GRAY);
                 }

                 table.AddCell(t);
             }
         }

        public static void addempdetails(Document document,Employee employee)  {

        PdfPTable empPTable = new PdfPTable(4);

        //first row
        PdfPCell emnameCell = new PdfPCell(new Paragraph("Employee Name ", subFont));
        emnameCell.PaddingTop=10;
        PdfPCell emnamecellValue = new PdfPCell(new Paragraph(": "+employee.getEmployeename(), subFont));
          emnamecellValue.PaddingTop=10;
        PdfPCell emlocationCell = new PdfPCell(new Paragraph("Location ", subFont));
          emlocationCell.PaddingTop=10;
        PdfPCell emloactionCellValue = new PdfPCell(new Paragraph(": Chennai", subFont));
          emloactionCellValue.PaddingTop=10;
        emnameCell.Border=0;
        emnamecellValue.Border = 0;
        emlocationCell.Border = 0;
        emloactionCellValue.Border = 0;
        empPTable.AddCell(emnameCell);
        empPTable.AddCell(emnamecellValue);
        empPTable.AddCell(emlocationCell);
        empPTable.AddCell(emloactionCellValue);
        //second row
        PdfPCell empIDCell = new PdfPCell(new Paragraph("Employee Id ", subFont));

        PdfPCell empIdCellValue = new PdfPCell(new Paragraph(": "+employee.getEmployee_id().ToUpper(), subFont));

        PdfPCell empPanNumber = new PdfPCell(new Paragraph("Pan Number ", subFont));

        PdfPCell empPanCellValue = new PdfPCell(new Paragraph(": "+employee.getPan_number(), subFont));
        empIDCell.Border = 0;
        empIdCellValue.Border = 0;
        empPanNumber.Border = 0;
        empPanCellValue.Border = 0;

        empPTable.AddCell(empIDCell);
        empPTable.AddCell(empIdCellValue);
        empPTable.AddCell(empPanNumber);
        empPTable.AddCell(empPanCellValue);

        //third row
        PdfPCell emdesignationCell = new PdfPCell(new Paragraph("Designation ", subFont));

        PdfPCell emdesignationcellValue = new PdfPCell(new Paragraph(": "+employee.getDesignation(), subFont));
        PdfPCell empdojCell = new PdfPCell(new Paragraph("D.O.J ", subFont));
        /* Calendar cal = Calendar.getInstance();
            */
           
          PdfPCell empdojCellValue;
         if(employee.getDoj()!=null){
             /*cal.setTime(employee.getDoj());
        
             int year = cal.get(Calendar.YEAR);
             int month = cal.get(Calendar.MONTH);
             int day = cal.get(Calendar.DAY_OF_MONTH);
         
                */
         empdojCellValue = new PdfPCell(new Paragraph(": "+employee.getDoj().ToString("dd/MM/yyyy"), subFont));
         }else{
          empdojCellValue = new PdfPCell(new Paragraph(": ", subFont));
         }
         emdesignationCell.Border = 0;
         emdesignationcellValue.Border = 0;
            
         empdojCell.Border = 0;
         empdojCellValue.Border = 0;
        empPTable.AddCell(emdesignationCell);
        empPTable.AddCell(emdesignationcellValue);
        empPTable.AddCell(empdojCell);
        empPTable.AddCell(empdojCellValue);

        document.Add(empPTable);



    }

        public static void addemppaydetails(Document document,Pay pay)  {



        PdfPTable attendencePTable = new PdfPTable(4);
        PdfPCell attendenceheding = new PdfPCell(new Paragraph("Attendence Details ", smallBold));
        attendenceheding.Border=7;
        attendenceheding.BorderColor = myColor;
        attendenceheding.BackgroundColor=myColor;
        PdfPCell totaldaysCell = new PdfPCell(new Paragraph("Total Days", subFont));
        totaldaysCell.Border = 7;
        totaldaysCell.BorderColor = myColor;
        PdfPCell totaldaysCellValue = new PdfPCell(new Paragraph(pay.getTotalDays().ToString(), subFont));
        totaldaysCellValue.Border = 3;
        totaldaysCellValue.BorderColor = myColor;
        PdfPCell workeddaysCell = new PdfPCell(new Paragraph("No.of Working Days", subFont));
        workeddaysCell.Border = 7;
        workeddaysCell.BorderColor = myColor;
        PdfPCell workeddaysCellValue = new PdfPCell(new Paragraph(pay.getWorkingDays().ToString(), subFont));
        workeddaysCellValue.Border = 3;
        workeddaysCellValue.BorderColor = myColor;
        // First Row
        attendencePTable.AddCell(attendenceheding);
        //addEmptyCell(attendencePTable, 3,1);
         PdfPCell t = new PdfPCell(new Paragraph("  ",subFont));
         t.BackgroundColor=myColor;
         t.Border=3;
         t.BorderColor = myColor;
         attendencePTable.AddCell(t);
         t = new PdfPCell(new Paragraph("  ",subFont));
         t.Border=3;
         t.BorderColor = myColor;
         t.BackgroundColor=myColor;

         attendencePTable.AddCell(t);
          t = new PdfPCell(new Paragraph("  ",subFont));
         t.Border=(11);
         t.BorderColor = myColor;
          t.BackgroundColor=(myColor);
         attendencePTable.AddCell(t);

        //Second Row
        attendencePTable.AddCell(totaldaysCell);
        attendencePTable.AddCell(totaldaysCellValue);

         t = new PdfPCell(new Paragraph("  ",subFont));
         t.Border=(3);
         t.BorderColor = myColor;
         attendencePTable.AddCell(t);
          t = new PdfPCell(new Paragraph("  ",subFont));
          t.BorderColor = myColor;
           t.Border=(11);
          attendencePTable.AddCell(t);
      //  addEmptyCell(attendencePTable, 2,1);
        //third Row
        attendencePTable.AddCell(workeddaysCell);
        attendencePTable.AddCell(workeddaysCellValue);
        t = new PdfPCell(new Paragraph("  ",subFont));
        t.BorderColor = myColor;
         t.Border=(3);
         attendencePTable.AddCell(t);
          t = new PdfPCell(new Paragraph("  ",subFont));
           t.Border=(11);
           t.BorderColor = myColor;
          attendencePTable.AddCell(t);
        addEmptyCell(attendencePTable, 2,0);


        document.Add(attendencePTable);
        document.Add(new Paragraph("\n"));

        PdfPTable earningsPTable = addEarnings(document,pay.getEarnings());
            
        //document.add(new Paragraph("\n"));
        PdfPTable deductionPTable = addDeuctions(document,pay.getDeduction());

        PdfPTable payDetailsPTable = new PdfPTable(2);

        PdfPCell c1 = new PdfPCell(earningsPTable);
        c1.Border = (0);
        payDetailsPTable.AddCell(c1);
        c1.BorderColor = myColor;


         c1 = new PdfPCell(deductionPTable);

        c1.Border = (0);
        c1.BorderColor = myColor;
        payDetailsPTable.AddCell(c1);
        document.Add(payDetailsPTable);

        document.Add(new Paragraph("\n"));
       document.Add(totalEarnings(document,pay.getTotalEarnings(),pay.getTotalDeduction(),pay.getNetincome()));



    }

        //public static float[] earningsArray(Earnings earnings)
        //{
        //    float[] arr = new float[8];
        //  //  arr[0] = earnings.getBasic();
        //  //  arr[1] = earnings.getHra();
        //  //  arr[2] = earnings.getTelAllowance();
        //   // arr[3] = earnings.getDearanceAllowance();
        //  //  arr[4] = earnings.getConveyance();
        //  //  arr[5] = earnings.getLoyalityIncentives();
        //   // arr[6] = earnings.getPerformanceIncentives();
        //   // arr[7] = earnings.getOthers();



        //    return arr;
        //}
        //{"Provident Fund", "ESI Deductions", "TDS", "Gratuity", "Professional Tax"};
        //public static float[] deductionArray(Deductions deductions)
        //{
        //    float[] arr = new float[6];
        //    //arr[0] = deductions.getPf();
        //    //arr[1] = deductions.getEsi();
        //    //arr[2] = deductions.getTds();
        //    //arr[3] = deductions.getGratuity();
        //    //arr[4] = deductions.getPt();
        //    //arr[5] = deductions.getLossofpay();




        //    return arr;
        //}

    //    public static PdfPTable addEarnings(Document document,Earnings earnings)  {


    //    PdfPTable earningsTable = new PdfPTable(2);
    //    float[] earningsValues=earningsArray(earnings);
    //    PdfPCell c1 = new PdfPCell(new Phrase("Earnings", smallBold));
    //    // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
    //    c1.Border=(7);
    //    c1.BackgroundColor=(myColor);
    //    earningsTable.AddCell(c1);

    //    c1 = new PdfPCell(new Phrase("Amount", smallBold));
    //    // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
    //     c1.BackgroundColor=(myColor);
    //      c1.Border=(3);
    //    earningsTable.AddCell(c1);
      
    //    for (int i = 0; i < 8; i++) {
    //        c1 = new PdfPCell(new Phrase(earningsHeading[i], subFont));
    //        //c1.setHorizontalAlignment(Element.ALIGN_CENTER);
    //        c1.Border=(7);
    //        earningsTable.AddCell(c1);

    //        c1 = new PdfPCell(new Phrase(earningsValues[i].ToString(), subFont));
    //        c1.Border=(3);
    //        // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
    //        earningsTable.AddCell(c1);
    //    }
    //    //  document.add(earningsTable);



    //    return earningsTable;
    //}

        public static PdfPTable addEarnings(Document document, string earnings)
        {


            PdfPTable earningsTable = new PdfPTable(2);
           // float[] earningsValues = earningsArray(earnings);

            PdfPCell c1 = new PdfPCell(new Phrase("Earnings", smallBold));
            // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
            c1.Border = (7);
            c1.BorderColor = myColor;
            c1.BackgroundColor = (myColor);
            earningsTable.AddCell(c1);

            c1 = new PdfPCell(new Phrase("Amount", smallBold));
            // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
            c1.BackgroundColor = (myColor);
            c1.BorderColor = myColor;
            c1.Border = (3);
            earningsTable.AddCell(c1);

            string[] earningsarray = Regex.Split(earnings, "---");
            //  
            foreach (string value in earningsarray)
            {

                string[] temp = Regex.Split(value, "--");

                c1 = new PdfPCell(new Phrase(temp[0], subFont));
                //c1.setHorizontalAlignment(Element.ALIGN_CENTER);
                c1.Border = (7);
                c1.BorderColor = myColor;
                earningsTable.AddCell(c1);

                c1 = new PdfPCell(new Phrase(temp[1], subFont));
                c1.Border = (3);
                c1.BorderColor = myColor;
                // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
                earningsTable.AddCell(c1);


            }
            CommonUtility.earningslength = earningsarray.Length;
       
            //for (int i = 0; i < 8; i++)
            //{
            //    c1 = new PdfPCell(new Phrase(earningsHeading[i], subFont));
            //    //c1.setHorizontalAlignment(Element.ALIGN_CENTER);
            //    c1.Border = (7);
            //    earningsTable.AddCell(c1);

            //    c1 = new PdfPCell(new Phrase(earningsValues[i].ToString(), subFont));
            //    c1.Border = (3);
            //    // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
            //    earningsTable.AddCell(c1);
            //}
            //  document.add(earningsTable);



            return earningsTable;
        }

        public static PdfPTable addDeuctions(Document document, string deductions)
        {


            PdfPTable deductionTable = new PdfPTable(2);
          //  float[] deductionsValues = deductionArray(deductions);
            PdfPCell c1 = new PdfPCell(new Phrase("Deductions", smallBold));
            // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
            c1.Border = (3);
            c1.BorderColor = myColor;
            c1.BackgroundColor = (myColor);
            deductionTable.AddCell(c1);

            c1 = new PdfPCell(new Phrase("Amount", smallBold));
            // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
            c1.BackgroundColor = (myColor);
            c1.BorderColor = myColor;
            c1.Border = (11);
            deductionTable.AddCell(c1);
            // System.out.print(deductionsValues.length);
            //System.out.print(deductionsHeading.length);

            string[] deductionsarray = Regex.Split(deductions, "---");
            //  
            foreach (string value in deductionsarray)
            {
                string[] temp = Regex.Split(value, "--");

                c1 = new PdfPCell(new Phrase(temp[0], subFont));
                //c1.setHorizontalAlignment(Element.ALIGN_CENTER);
                c1.Border = (3);
                c1.BorderColor = myColor;
                deductionTable.AddCell(c1);

                c1 = new PdfPCell(new Phrase(temp[1], subFont));
                c1.Border = (11);
                c1.BorderColor = myColor;
                // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
                deductionTable.AddCell(c1);

            }

            //for (int i = 0; i < 6; i++)
            //{
            //    c1 = new PdfPCell(new Phrase(deductionsHeading[i], subFont));
            //    //c1.setHorizontalAlignment(Element.ALIGN_CENTER);
            //    c1.Border = (3);
            //    deductionTable.AddCell(c1);

            //    c1 = new PdfPCell(new Phrase(deductionsValues[i].ToString(), subFont));
            //    c1.Border = (11);
            //    // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
            //    deductionTable.AddCell(c1);
            //}
            for (int i = 0; i < (CommonUtility.earningslength-deductionsarray.Length); i++)
            {
                c1 = new PdfPCell(new Phrase(" \t", subFont));
                //c1.setHorizontalAlignment(Element.ALIGN_CENTER);
                c1.Border = (3);
                c1.BorderColor = myColor;
                deductionTable.AddCell(c1);

                c1 = new PdfPCell(new Phrase("\t", subFont));
                c1.Border = (11);
                c1.BorderColor = myColor;
                // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
                deductionTable.AddCell(c1);
            }

            //addEmptyCell(deductionTable, 6,1);
            //  document.add(deductionTable);



            return deductionTable;
        }

    //    public static PdfPTable addDeuctions(Document document,Deductions deductions) {


    //    PdfPTable deductionTable = new PdfPTable(2);
    //    float[] deductionsValues=deductionArray(deductions);
    //    PdfPCell c1 = new PdfPCell(new Phrase("Deductions", smallBold));
    //    // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
    //      c1.Border=(3);
    //       c1.BackgroundColor=(myColor);
    //    deductionTable.AddCell(c1);

    //    c1 = new PdfPCell(new Phrase("Amount", smallBold));
    //    // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
    //     c1.BackgroundColor=(myColor);
    //      c1.Border=(11);
    //    deductionTable.AddCell(c1);
    //   // System.out.print(deductionsValues.length);
    //    //System.out.print(deductionsHeading.length);
    //    for (int i = 0; i < 6; i++) {
    //        c1 = new PdfPCell(new Phrase(deductionsHeading[i], subFont));
    //        //c1.setHorizontalAlignment(Element.ALIGN_CENTER);
    //         c1.Border=(3);
    //        deductionTable.AddCell(c1);

    //        c1 = new PdfPCell(new Phrase(deductionsValues[i].ToString(), subFont));
    //         c1.Border=(11);
    //        // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
    //        deductionTable.AddCell(c1);
    //    }
    //    for (int i = 0; i < 2; i++) {
    //        c1 = new PdfPCell(new Phrase(" \t", subFont));
    //        //c1.setHorizontalAlignment(Element.ALIGN_CENTER);
    //         c1.Border=(3);
    //        deductionTable.AddCell(c1);

    //        c1 = new PdfPCell(new Phrase("\t", subFont));
    //         c1.Border=(11);
    //        // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
    //        deductionTable.AddCell(c1);
    //    }

    //    //addEmptyCell(deductionTable, 6,1);
    //    //  document.add(deductionTable);



    //    return deductionTable;
    //}

        public static PdfPTable totalEarnings(Document document, double totalearnings, double totaldeductions, double netpay)
        {


        PdfPTable earningsTable = new PdfPTable(4);

        PdfPCell c1 = new PdfPCell(new Phrase("Total Earnings", subBFont));
        c1.HorizontalAlignment=(Element.ALIGN_CENTER);
        c1.BackgroundColor=(BaseColor.GRAY);
        c1.BorderColor = myColor;
        earningsTable.AddCell(c1);

        c1 = new PdfPCell(new Phrase(totalearnings.ToString(), subFont));
        //  c1.setHorizontalAlignment(Element.ALIGN_CENTER);
        c1.BackgroundColor=(BaseColor.GRAY);
        c1.BorderColor = myColor;
        earningsTable.AddCell(c1);
        c1 = new PdfPCell(new Phrase("Total Dedctions", subBFont));
        c1.HorizontalAlignment=(Element.ALIGN_CENTER);
        c1.BackgroundColor=(BaseColor.GRAY);
        c1.BorderColor = myColor;
        earningsTable.AddCell(c1);

        c1 = new PdfPCell(new Phrase(totaldeductions.ToString(), subFont));
        // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
       c1.BackgroundColor=(BaseColor.GRAY);
       c1.BorderColor = myColor;
        earningsTable.AddCell(c1);
        addEmptyCell(earningsTable, 2,2);
        c1 = new PdfPCell(new Phrase("Net Amount", subBFont));
        c1.BackgroundColor=(BaseColor.GRAY);
        c1.BorderColor = myColor;
        c1.HorizontalAlignment=(Element.ALIGN_CENTER);
        earningsTable.AddCell(c1);

        c1 = new PdfPCell(new Phrase(netpay.ToString(), subFont));
        c1.BackgroundColor=(BaseColor.GRAY);
        c1.BorderColor = myColor;
        // c1.setHorizontalAlignment(Element.ALIGN_CENTER);
        earningsTable.AddCell(c1);

        addEmptyCell(earningsTable, 32,1);

        c1 = new PdfPCell(new Phrase("This is a computer generated salary slip,Does not require a signature.", subFont));
        c1.Colspan=(4);
        c1.Border=(0);
        c1.BorderColor = myColor;
        c1.HorizontalAlignment=(Element.ALIGN_CENTER);
        earningsTable.AddCell(c1);
        c1 = new PdfPCell(new Phrase("Payslip amount are shown in INR.", subFont));
        c1.Colspan=(4);
        c1.BorderColor = myColor;
        c1.Border=(0);
        c1.HorizontalAlignment=(Element.ALIGN_CENTER);
        earningsTable.AddCell(c1);

        // document.add(earningsTable);
        //  document.add(earningsTable);



        return earningsTable;
    }

    }
}