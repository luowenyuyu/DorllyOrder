using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.IO;
using System.Web;

namespace project.Presentation.Op
{
    public static class OrderPrint
    {
        //public static string Path = "E:\\Project\\DorllyOrder\\DOWeb\\pdf\\缴费通知单";
        //public static string Path = "D:\\GitHub\\DorllyAsp2\\DorllyOrder\\DOWeb\\pdf\\缴费通知单";
        public static string Path = HttpRuntime.AppDomainAppPath + "pdf\\缴费通知单";
        /// <summary>
        /// HttpRuntime.AppDomainAppPath + "pdf\\未缴费通知单"
        /// </summary>
        public static string UnpayPath = HttpRuntime.AppDomainAppPath + "pdf\\未缴费通知单";
        static BaseFont bf = BaseFont.CreateFont(@"c:\Windows\fonts\SURSONG.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

        static Font font16 = new Font(bf, 16, Font.NORMAL);
        static Font font10 = new Font(bf, 10, Font.NORMAL);
        static Font font9 = new Font(bf, 9, Font.NORMAL);
        static Font font9_Bold = new Font(bf, 9, Font.BOLD);
        static Font font8 = new Font(bf, 8, Font.NORMAL);

        public static void Print(Document doc, string OrderRP)
        {
            try
            {
                Data obj = new Data();
                //int count = int.Parse(obj.PopulateDataSet("select count(1) as Cnt from Op_OrderDetail where RefRP='" + OrderRP + "'").Tables[0].Rows[0]["Cnt"].ToString());
                int count = int.Parse(obj.PopulateDataSet("select count(1) as Cnt from Op_OrderDetail a " +
                "left join Op_ContractRMRentList b on a.RefNo=b.RowPointer " +
                "left join Op_ContractRMRentList_Readout c on c.RefRentRP=b.RowPointer " +
                "left join Op_Readout d on d.RowPointer=c.RefReadoutRP " +
                "where a.RefRP='" + OrderRP + "'").Tables[0].Rows[0]["Cnt"].ToString());

                int page = int.Parse(System.Math.Ceiling(count / 8.0).ToString());

                for (int i = 1; i <= page; i++)
                {
                    DataTable dt = obj.ExecSelect("Op_OrderDetail a " +
                        "left join Mstr_Service b on a.ODSRVNo=b.SRVNo " +
                        "left join Mstr_ServiceProvider c on c.SPNo=a.ODContractSPNo " +
                        "left join Op_OrderHeader d on d.RowPointer=a.RefRP " +
                        "left join Mstr_Customer e on e.CustNo=d.CustNo " +


                        "left join Op_ContractRMRentList f on a.RefNo=f.RowPointer " +
                        "left join Op_ContractRMRentList_Readout g on g.RefRentRP=f.RowPointer " +
                        "left join Op_Readout h on h.RowPointer=g.RefReadoutRP "
                        ,

                        "a.*,b.SRVName,c.SPName,c.SPBank,c.SPBankAccount,c.SPBankTitle,d.OrderTime,e.CustName,d.OrderType," +

                        "h.LastReadout as RLastReadout,h.Readout as RReadout,isnull(h.Readings,0)*isnull(h.MeteRate,0) as RODQTY," +
                        "isnull(h.Readings,0)*isnull(h.MeteRate,0)*a.ODUnitPrice as RODARAmount,h.MeteRate as RMeteRate",

                        " and a.RefRP=" + "'" + OrderRP + "'", i, 8, "a.ResourceNo,a.ODCreateDate");

                    if (i > 1) doc.NewPage();

                    if (dt.Rows[0]["OrderType"].ToString() == "04" || dt.Rows[0]["OrderType"].ToString() == "09")
                    {
                        PrintHeader_PT(doc, dt.Rows[0]);
                        PrintBody_PT(doc, dt, i, page);
                        PrintFooter_PT(doc, OrderRP, dt.Rows[0], i, page);
                    }
                    else
                    {
                        PrintHeader(doc, dt.Rows[0]);
                        PrintBody(doc, dt, i, page);
                        PrintFooter(doc, OrderRP, dt.Rows[0], i, page);
                    }
                }
            }
            catch { }
        }


        #region
        private static void PrintHeader(Document doc, DataRow dr)
        {
            PdfPTable Tit = new PdfPTable(1);
            Tit.DefaultCell.Padding = 3;
            float[] wid = { 1 };
            Tit.SetWidths(wid);
            Tit.WidthPercentage = 100;
            PdfPCell cell1 = new PdfPCell(new Paragraph(DateTime.Parse(dr["OrderTime"].ToString()).ToString("yyyy年MM月 ") + "缴费通知单", font16));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell1.Border = Rectangle.NO_BORDER;
            cell1.FixedHeight = 60;
            Tit.AddCell(cell1);
            doc.Add(Tit);

            PdfPTable PT1 = new PdfPTable(1);
            PT1.DefaultCell.Padding = 0;
            float[] hw8 = { 1 };
            PT1.SetWidths(hw8);
            PT1.WidthPercentage = 100;
            PT1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PdfPCell cell11 = new PdfPCell(new Paragraph("客户：" + dr["CustName"].ToString(), font9_Bold));
            cell11.HorizontalAlignment = Element.ALIGN_LEFT;
            cell11.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell11.Border = Rectangle.NO_BORDER;
            cell11.FixedHeight = 20;
            PT1.AddCell(cell11);
            doc.Add(PT1);
        }
        private static void PrintBody(Document doc, DataTable dt, int page, int pages)
        {
            PdfPTable PT1 = new PdfPTable(4);
            PT1.DefaultCell.Padding = 3;
            float[] hw1 = { 2, 10, 8, 8 };
            PT1.SetWidths(hw1);
            PT1.WidthPercentage = 100;
            PT1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell cell1 = new PdfPCell(new Paragraph("序号", font9_Bold));
            cell1.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.FixedHeight = 18;
            PdfPCell cell2 = new PdfPCell(new Paragraph("资源编号", font9_Bold));
            cell2.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell3 = new PdfPCell(new Paragraph("费用项", font9_Bold));
            cell3.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell4 = new PdfPCell(new Paragraph("费用金额", font9_Bold));
            cell4.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;

            PT1.AddCell(cell1);
            PT1.AddCell(cell2);
            PT1.AddCell(cell3);
            PT1.AddCell(cell4);

            doc.Add(PT1);
            int cnt = 0;
            int row = (page - 1) * 10;
            foreach (DataRow dr in dt.Rows)
            {
                PdfPTable PT2 = new PdfPTable(4);
                PT2.DefaultCell.Padding = 3;
                float[] hw2 = { 2, 10, 8, 8 };
                PT2.SetWidths(hw2);
                PT2.WidthPercentage = 100;

                PdfPCell cell21 = new PdfPCell(new Paragraph((row + 1).ToString(), font9));
                cell21.Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell21.HorizontalAlignment = Element.ALIGN_CENTER;
                cell21.FixedHeight = 18;

                PdfPCell cell22 = new PdfPCell(new Paragraph(dr["ResourceNo"].ToString(), font9));
                cell22.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell22.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell23 = new PdfPCell(new Paragraph(dr["SRVName"].ToString(), font9));
                cell23.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell23.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell24 = new PdfPCell(new Paragraph(decimal.Parse(dr["ODARAmount"].ToString()).ToString("0.##"), font9));
                cell24.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell24.HorizontalAlignment = Element.ALIGN_CENTER;

                PT2.AddCell(cell21);
                PT2.AddCell(cell22);
                PT2.AddCell(cell23);
                PT2.AddCell(cell24);

                doc.Add(PT2);
                row++;
                cnt++;
            }
            int maxcnt = 7;
            if (dt.Rows.Count > 0 && pages == page)
            {
                Data obj = new Data();
                DataTable dt1 = obj.PopulateDataSet("select sum(ODARAmount) as ODARAmount from Op_OrderDetail where RefRP='" + dt.Rows[0]["RefRP"].ToString() + "'").Tables[0];
                decimal ODARAmount = decimal.Parse(dt1.Rows[0]["ODARAmount"].ToString());

                PdfPTable PT3 = new PdfPTable(4);
                PT3.DefaultCell.Padding = 3;
                float[] hw3 = { 2, 10, 8, 8 };
                PT3.SetWidths(hw3);
                PT3.WidthPercentage = 100;

                PdfPCell cell31 = new PdfPCell(new Paragraph("合计：  ", font9));
                cell31.Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell31.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell31.FixedHeight = 18;
                cell31.Colspan = 3;

                PdfPCell cell34 = new PdfPCell(new Paragraph(ODARAmount.ToString("0.##"), font9));
                cell34.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell34.HorizontalAlignment = Element.ALIGN_CENTER;

                PT3.AddCell(cell31);
                PT3.AddCell(cell34);
                doc.Add(PT3);

                maxcnt++;
            }

            while (cnt < maxcnt)
            {
                PdfPTable PT2 = new PdfPTable(1);
                PT2.DefaultCell.Padding = 3;
                float[] hw2 = { 1 };
                PT2.SetWidths(hw2);
                PT2.WidthPercentage = 100;

                PdfPCell cel = new PdfPCell(new Paragraph());
                cel.FixedHeight = 18;
                cel.Border = Rectangle.NO_BORDER;

                PT2.AddCell(cel);
                doc.Add(PT2);

                cnt++;
            }
        }
        private static void PrintFooter(Document doc, string OrderRP, DataRow dr, int page, int pages)
        {
            //Data obj = new Data();
            //DataTable dt = obj.PopulateDataSet("select sum(ODARAmount) as ODARAmount from Op_OrderDetail where RefRP='" + OrderRP + "'").Tables[0];
            //decimal ODARAmount = decimal.Parse(dt.Rows[0]["ODARAmount"].ToString());

            //PdfPTable PT1 = new PdfPTable(2);
            //PT1.DefaultCell.Padding = 0;
            //float[] hw1 = { 3, 20 };
            //PT1.SetWidths(hw1);
            //PT1.WidthPercentage = 100;
            //PdfPCell cell11 = new PdfPCell(new Paragraph("合计金额：", font9));
            //cell11.Border = Rectangle.NO_BORDER;
            //cell11.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell11.VerticalAlignment = Element.ALIGN_MIDDLE;
            //cell11.FixedHeight = 15;
            //PT1.AddCell(cell11);
            //PdfPCell cell12 = new PdfPCell(new Paragraph(ODARAmount.ToString("0.##"), font9));
            //cell12.Border = Rectangle.NO_BORDER;
            //cell12.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell12.VerticalAlignment = Element.ALIGN_MIDDLE;
            //cell12.FixedHeight = 15;
            //PT1.AddCell(cell12);

            PdfPTable PT2 = new PdfPTable(2);
            PT2.DefaultCell.Padding = 0;
            float[] hw2 = { 3, 20 };
            PT2.SetWidths(hw2);
            PT2.WidthPercentage = 100;
            PdfPCell cell21 = new PdfPCell(new Paragraph("服务商：", font9));
            cell21.Border = Rectangle.NO_BORDER;
            cell21.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell21.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell21.FixedHeight = 15;
            PT2.AddCell(cell21);
            PdfPCell cell22 = new PdfPCell(new Paragraph(dr["SPName"].ToString(), font9));
            cell22.Border = Rectangle.NO_BORDER;
            cell22.HorizontalAlignment = Element.ALIGN_LEFT;
            cell22.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell22.FixedHeight = 15;
            PT2.AddCell(cell22);

            PdfPTable PT3 = new PdfPTable(2);
            PT3.DefaultCell.Padding = 0;
            float[] hw3 = { 3, 20 };
            PT3.SetWidths(hw3);
            PT3.WidthPercentage = 100;
            PdfPCell cell31 = new PdfPCell(new Paragraph("开户行：", font9));
            cell31.Border = Rectangle.NO_BORDER;
            cell31.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell31.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell31.FixedHeight = 15;
            PT3.AddCell(cell31);
            PdfPCell cell32 = new PdfPCell(new Paragraph(dr["SPBank"].ToString(), font9));
            cell32.Border = Rectangle.NO_BORDER;
            cell32.HorizontalAlignment = Element.ALIGN_LEFT;
            cell32.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell32.FixedHeight = 15;
            PT3.AddCell(cell32);

            PdfPTable PT4 = new PdfPTable(2);
            PT4.DefaultCell.Padding = 0;
            float[] hw4 = { 3, 20 };
            PT4.SetWidths(hw4);
            PT4.WidthPercentage = 100;
            PdfPCell cell41 = new PdfPCell(new Paragraph("账户：", font9));
            cell41.Border = Rectangle.NO_BORDER;
            cell41.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell41.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell41.FixedHeight = 15;
            PT4.AddCell(cell41);
            PdfPCell cell42 = new PdfPCell(new Paragraph(dr["SPBankAccount"].ToString(), font9));
            cell42.Border = Rectangle.NO_BORDER;
            cell42.HorizontalAlignment = Element.ALIGN_LEFT;
            cell42.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell42.FixedHeight = 15;
            PT4.AddCell(cell42);

            PdfPTable PT5 = new PdfPTable(2);
            PT5.DefaultCell.Padding = 0;
            float[] hw5 = { 3, 20 };
            PT5.SetWidths(hw5);
            PT5.WidthPercentage = 100;
            PdfPCell cell51 = new PdfPCell(new Paragraph("收款人：", font9));
            cell51.Border = Rectangle.NO_BORDER;
            cell51.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell51.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell51.FixedHeight = 15;
            PT5.AddCell(cell51);
            PdfPCell cell52 = new PdfPCell(new Paragraph(dr["SPBankTitle"].ToString(), font9));
            cell52.Border = Rectangle.NO_BORDER;
            cell52.HorizontalAlignment = Element.ALIGN_LEFT;
            cell52.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell52.FixedHeight = 15;
            PT5.AddCell(cell52);

            PdfPTable PT6 = new PdfPTable(2);
            PT6.DefaultCell.Padding = 0;
            float[] hw6 = { 3, 20 };
            PT6.SetWidths(hw6);
            PT6.WidthPercentage = 100;
            PdfPCell cell61 = new PdfPCell(new Paragraph("备注：", font9));
            cell61.Border = Rectangle.NO_BORDER;
            cell61.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell61.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell61.FixedHeight = 36;
            PT6.AddCell(cell61);
            PdfPCell cell62 = new PdfPCell(new Paragraph("请贵司持此通知单于当月 5 日前将上述款项存入我司指定账户，节假日顺延。要求严格按照合同签订主体做付款处理，对于非合同主体打款入账的，财务按正常流程发未缴款通知单，对于超期仍未缴交的按合同约定标准计算并收取违约金。  如有疑问，请及时前来我司财务部核对确认。", font9));
            cell62.Border = Rectangle.NO_BORDER;
            cell62.HorizontalAlignment = Element.ALIGN_LEFT;
            cell62.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell61.FixedHeight = 36;
            PT6.AddCell(cell62);

            PdfPTable PT7 = new PdfPTable(2);
            PT7.DefaultCell.Padding = 0;
            float[] hw7 = { 3, 20 };
            PT7.SetWidths(hw7);
            PT7.WidthPercentage = 100;
            PdfPCell cell71 = new PdfPCell(new Paragraph("联系电话：", font9));
            cell71.Border = Rectangle.NO_BORDER;
            cell71.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell71.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell71.FixedHeight = 15;
            PT7.AddCell(cell71);
            PdfPCell cell72 = new PdfPCell(new Paragraph("0755-83117262", font9));
            cell72.Border = Rectangle.NO_BORDER;
            cell72.HorizontalAlignment = Element.ALIGN_LEFT;
            cell72.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell72.FixedHeight = 15;
            PT7.AddCell(cell72);

            PdfPTable PT9 = new PdfPTable(1);
            PT9.DefaultCell.Padding = 0;
            float[] hw9 = { 1 };
            PT9.SetWidths(hw9);
            PT9.WidthPercentage = 100;
            PdfPCell cell91 = new PdfPCell(new Paragraph(page.ToString() + " / " + pages.ToString(), font9));
            cell91.Border = Rectangle.NO_BORDER;
            cell91.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell91.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell91.FixedHeight = 15;
            PT9.AddCell(cell91);

            //doc.Add(PT1);
            doc.Add(PT2);
            doc.Add(PT3);
            doc.Add(PT4);
            doc.Add(PT5);
            doc.Add(PT6);
            doc.Add(PT7);
            doc.Add(PT9);
        }
        #endregion

        #region
        private static void PrintHeader_PT(Document doc, DataRow dr)
        {
            PdfPTable Tit = new PdfPTable(1);
            Tit.DefaultCell.Padding = 3;
            float[] wid = { 1 };
            Tit.SetWidths(wid);
            Tit.WidthPercentage = 100;
            PdfPCell cell1 = new PdfPCell(new Paragraph(DateTime.Parse(dr["OrderTime"].ToString()).ToString("yyyy年MM月 ") + "缴费通知单", font16));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell1.Border = Rectangle.NO_BORDER;
            cell1.FixedHeight = 60;
            Tit.AddCell(cell1);
            doc.Add(Tit);

            PdfPTable PT1 = new PdfPTable(1);
            PT1.DefaultCell.Padding = 0;
            float[] hw8 = { 1 };
            PT1.SetWidths(hw8);
            PT1.WidthPercentage = 100;
            PT1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PdfPCell cell11 = new PdfPCell(new Paragraph("客户：" + dr["CustName"].ToString(), font9_Bold));
            cell11.HorizontalAlignment = Element.ALIGN_LEFT;
            cell11.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell11.Border = Rectangle.NO_BORDER;
            cell11.FixedHeight = 20;
            PT1.AddCell(cell11);
            doc.Add(PT1);
        }
        private static void PrintBody_PT(Document doc, DataTable dt, int page, int pages)
        {
            PdfPTable PT1 = new PdfPTable(9);
            PT1.DefaultCell.Padding = 3;
            float[] hw1 = { 2, 8, 5, 4, 4, 4, 2, 4, 4 };
            PT1.SetWidths(hw1);
            PT1.WidthPercentage = 100;
            PT1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell cell1 = new PdfPCell(new Paragraph("序号", font9_Bold));
            cell1.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.FixedHeight = 18;
            PdfPCell cell2 = new PdfPCell(new Paragraph("资源编号", font9_Bold));
            cell2.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell3 = new PdfPCell(new Paragraph("费用项", font9_Bold));
            cell3.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell4 = new PdfPCell(new Paragraph("上期读数", font9_Bold));
            cell4.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell5 = new PdfPCell(new Paragraph("本期读数", font9_Bold));
            cell5.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell5.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell6 = new PdfPCell(new Paragraph("数量", font9_Bold));
            cell6.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell6.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell7 = new PdfPCell(new Paragraph("倍率", font9_Bold));
            cell7.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell7.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell8 = new PdfPCell(new Paragraph("单价", font9_Bold));
            cell8.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell8.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell9 = new PdfPCell(new Paragraph("费用金额", font9_Bold));
            cell9.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell9.HorizontalAlignment = Element.ALIGN_CENTER;

            PT1.AddCell(cell1);
            PT1.AddCell(cell2);
            PT1.AddCell(cell3);
            PT1.AddCell(cell4);
            PT1.AddCell(cell5);
            PT1.AddCell(cell6);
            PT1.AddCell(cell7);
            PT1.AddCell(cell8);
            PT1.AddCell(cell9);

            doc.Add(PT1);
            int cnt = 0;
            int row = (page - 1) * 10;
            foreach (DataRow dr in dt.Rows)
            {
                PdfPTable PT2 = new PdfPTable(9);
                PT2.DefaultCell.Padding = 3;
                float[] hw2 = { 2, 8, 5, 4, 4, 4, 2, 4, 4 };
                PT2.SetWidths(hw2);
                PT2.WidthPercentage = 100;

                PdfPCell cell21 = new PdfPCell(new Paragraph((row + 1).ToString(), font9));
                cell21.Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell21.HorizontalAlignment = Element.ALIGN_CENTER;
                cell21.FixedHeight = 18;

                PdfPCell cell22 = new PdfPCell(new Paragraph(dr["ResourceNo"].ToString(), font9));
                cell22.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell22.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell23 = new PdfPCell(new Paragraph(dr["SRVName"].ToString(), font9));
                cell23.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell23.HorizontalAlignment = Element.ALIGN_CENTER;

                string LastReadout = "";
                string Readout = "";
                string MeteRate = "";

                string ODQTY = decimal.Parse(dr["ODQTY"].ToString()).ToString("0.##");
                string ODARAmount = decimal.Parse(dr["ODARAmount"].ToString()).ToString("0.##");
                //水费、电费、公摊电费、超额电费 dr["ODSRVNo"].ToString() == "GDDF-56" ||
                if (dr["ODSRVNo"].ToString() == "DF-56" || dr["ODSRVNo"].ToString() == "SF-55" || dr["ODSRVNo"].ToString() == "CEDF-62")
                {
                    if (dr["RLastReadout"].ToString() != "")
                        LastReadout = decimal.Parse(dr["RLastReadout"].ToString()).ToString("0.##");
                    if (dr["RReadout"].ToString() != "")
                        Readout = decimal.Parse(dr["RReadout"].ToString()).ToString("0.##");
                    if (dr["RMeteRate"].ToString() != "")
                        MeteRate = decimal.Parse(dr["RMeteRate"].ToString()).ToString("0.##");

                    if (dr["RODQTY"].ToString() != "")
                        ODQTY = decimal.Parse(dr["RODQTY"].ToString()).ToString("0.##");
                    if (dr["RODARAmount"].ToString() != "")
                        ODARAmount = decimal.Parse(dr["RODARAmount"].ToString()).ToString("0.##");
                }

                PdfPCell cell24 = new PdfPCell(new Paragraph(LastReadout, font9));
                cell24.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell24.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell25 = new PdfPCell(new Paragraph(Readout, font9));
                cell25.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell25.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell26 = new PdfPCell(new Paragraph(ODQTY, font9));
                cell26.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell26.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell27 = new PdfPCell(new Paragraph(MeteRate, font9));
                cell27.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell27.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell28 = new PdfPCell(new Paragraph(decimal.Parse(dr["ODUnitPrice"].ToString()).ToString("0.###"), font9));
                cell28.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell28.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell29 = new PdfPCell(new Paragraph(ODARAmount, font9));
                cell29.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell29.HorizontalAlignment = Element.ALIGN_CENTER;

                PT2.AddCell(cell21);
                PT2.AddCell(cell22);
                PT2.AddCell(cell23);
                PT2.AddCell(cell24);
                PT2.AddCell(cell25);
                PT2.AddCell(cell26);
                PT2.AddCell(cell27);
                PT2.AddCell(cell28);
                PT2.AddCell(cell29);

                doc.Add(PT2);
                row++;
                cnt++;
            }
            int maxcnt = 7;
            if (dt.Rows.Count > 0 && pages == page)
            {
                Data obj = new Data();
                DataTable dt1 = obj.PopulateDataSet("select sum(ODARAmount) as ODARAmount from Op_OrderDetail where RefRP='" + dt.Rows[0]["RefRP"].ToString() + "'").Tables[0];
                decimal ODARAmount = decimal.Parse(dt1.Rows[0]["ODARAmount"].ToString());

                PdfPTable PT3 = new PdfPTable(9);
                PT3.DefaultCell.Padding = 3;
                float[] hw3 = { 2, 8, 5, 4, 4, 4, 2, 4, 4 };
                PT3.SetWidths(hw3);
                PT3.WidthPercentage = 100;

                PdfPCell cell31 = new PdfPCell(new Paragraph("合计：  ", font9));
                cell31.Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell31.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell31.FixedHeight = 18;
                cell31.Colspan = 8;

                PdfPCell cell34 = new PdfPCell(new Paragraph(ODARAmount.ToString("0.##"), font9));
                cell34.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell34.HorizontalAlignment = Element.ALIGN_CENTER;

                PT3.AddCell(cell31);
                PT3.AddCell(cell34);
                doc.Add(PT3);

                maxcnt++;
            }

            while (cnt < maxcnt)
            {
                PdfPTable PT2 = new PdfPTable(1);
                PT2.DefaultCell.Padding = 3;
                float[] hw2 = { 1 };
                PT2.SetWidths(hw2);
                PT2.WidthPercentage = 100;

                PdfPCell cel = new PdfPCell(new Paragraph());
                cel.FixedHeight = 18;
                cel.Border = Rectangle.NO_BORDER;

                PT2.AddCell(cel);
                doc.Add(PT2);

                cnt++;
            }
        }
        private static void PrintFooter_PT(Document doc, string OrderRP, DataRow dr, int page, int pages)
        {
            //Data obj = new Data();
            //DataTable dt = obj.PopulateDataSet("select sum(ODARAmount) as ODARAmount from Op_OrderDetail where RefRP='" + OrderRP + "'").Tables[0];
            //decimal ODARAmount = decimal.Parse(dt.Rows[0]["ODARAmount"].ToString());

            //PdfPTable PT1 = new PdfPTable(2);
            //PT1.DefaultCell.Padding = 0;
            //float[] hw1 = { 3, 20 };
            //PT1.SetWidths(hw1);
            //PT1.WidthPercentage = 100;
            //PdfPCell cell11 = new PdfPCell(new Paragraph("合计金额：", font9));
            //cell11.Border = Rectangle.NO_BORDER;
            //cell11.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell11.VerticalAlignment = Element.ALIGN_MIDDLE;
            //cell11.FixedHeight = 15;
            //PT1.AddCell(cell11);
            //PdfPCell cell12 = new PdfPCell(new Paragraph(ODARAmount.ToString("0.##"), font9));
            //cell12.Border = Rectangle.NO_BORDER;
            //cell12.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell12.VerticalAlignment = Element.ALIGN_MIDDLE;
            //cell12.FixedHeight = 15;
            //PT1.AddCell(cell12);

            PdfPTable PT2 = new PdfPTable(2);
            PT2.DefaultCell.Padding = 0;
            float[] hw2 = { 3, 20 };
            PT2.SetWidths(hw2);
            PT2.WidthPercentage = 100;
            PdfPCell cell21 = new PdfPCell(new Paragraph("服务商：", font9));
            cell21.Border = Rectangle.NO_BORDER;
            cell21.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell21.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell21.FixedHeight = 15;
            PT2.AddCell(cell21);
            PdfPCell cell22 = new PdfPCell(new Paragraph(dr["SPName"].ToString(), font9));
            cell22.Border = Rectangle.NO_BORDER;
            cell22.HorizontalAlignment = Element.ALIGN_LEFT;
            cell22.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell22.FixedHeight = 15;
            PT2.AddCell(cell22);

            PdfPTable PT3 = new PdfPTable(2);
            PT3.DefaultCell.Padding = 0;
            float[] hw3 = { 3, 20 };
            PT3.SetWidths(hw3);
            PT3.WidthPercentage = 100;
            PdfPCell cell31 = new PdfPCell(new Paragraph("开户行：", font9));
            cell31.Border = Rectangle.NO_BORDER;
            cell31.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell31.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell31.FixedHeight = 15;
            PT3.AddCell(cell31);
            PdfPCell cell32 = new PdfPCell(new Paragraph(dr["SPBank"].ToString(), font9));
            cell32.Border = Rectangle.NO_BORDER;
            cell32.HorizontalAlignment = Element.ALIGN_LEFT;
            cell32.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell32.FixedHeight = 15;
            PT3.AddCell(cell32);

            PdfPTable PT4 = new PdfPTable(2);
            PT4.DefaultCell.Padding = 0;
            float[] hw4 = { 3, 20 };
            PT4.SetWidths(hw4);
            PT4.WidthPercentage = 100;
            PdfPCell cell41 = new PdfPCell(new Paragraph("账户：", font9));
            cell41.Border = Rectangle.NO_BORDER;
            cell41.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell41.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell41.FixedHeight = 15;
            PT4.AddCell(cell41);
            PdfPCell cell42 = new PdfPCell(new Paragraph(dr["SPBankAccount"].ToString(), font9));
            cell42.Border = Rectangle.NO_BORDER;
            cell42.HorizontalAlignment = Element.ALIGN_LEFT;
            cell42.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell42.FixedHeight = 15;
            PT4.AddCell(cell42);

            PdfPTable PT5 = new PdfPTable(2);
            PT5.DefaultCell.Padding = 0;
            float[] hw5 = { 3, 20 };
            PT5.SetWidths(hw5);
            PT5.WidthPercentage = 100;
            PdfPCell cell51 = new PdfPCell(new Paragraph("收款人：", font9));
            cell51.Border = Rectangle.NO_BORDER;
            cell51.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell51.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell51.FixedHeight = 15;
            PT5.AddCell(cell51);
            PdfPCell cell52 = new PdfPCell(new Paragraph(dr["SPBankTitle"].ToString(), font9));
            cell52.Border = Rectangle.NO_BORDER;
            cell52.HorizontalAlignment = Element.ALIGN_LEFT;
            cell52.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell52.FixedHeight = 15;
            PT5.AddCell(cell52);

            PdfPTable PT6 = new PdfPTable(2);
            PT6.DefaultCell.Padding = 0;
            float[] hw6 = { 3, 20 };
            PT6.SetWidths(hw6);
            PT6.WidthPercentage = 100;
            PdfPCell cell61 = new PdfPCell(new Paragraph("备注：", font9));
            cell61.Border = Rectangle.NO_BORDER;
            cell61.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell61.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell61.FixedHeight = 36;
            PT6.AddCell(cell61);
            PdfPCell cell62 = new PdfPCell(new Paragraph("请贵司持此通知单于当月 5 日前将上述款项存入我司指定账户，节假日顺延。要求严格按照合同签订主体做付款处理，对于非合同主体打款入账的，财务按正常流程发未缴款通知单，对于超期仍未缴交的按合同约定标准计算并收取违约金。  如有疑问，请及时前来我司财务部核对确认。", font9));
            cell62.Border = Rectangle.NO_BORDER;
            cell62.HorizontalAlignment = Element.ALIGN_LEFT;
            cell62.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell61.FixedHeight = 36;
            PT6.AddCell(cell62);

            PdfPTable PT7 = new PdfPTable(2);
            PT7.DefaultCell.Padding = 0;
            float[] hw7 = { 3, 20 };
            PT7.SetWidths(hw7);
            PT7.WidthPercentage = 100;
            PdfPCell cell71 = new PdfPCell(new Paragraph("联系电话：", font9));
            cell71.Border = Rectangle.NO_BORDER;
            cell71.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell71.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell71.FixedHeight = 15;
            PT7.AddCell(cell71);
            PdfPCell cell72 = new PdfPCell(new Paragraph("0755-83117262", font9));
            cell72.Border = Rectangle.NO_BORDER;
            cell72.HorizontalAlignment = Element.ALIGN_LEFT;
            cell72.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell72.FixedHeight = 15;
            PT7.AddCell(cell72);

            PdfPTable PT9 = new PdfPTable(1);
            PT9.DefaultCell.Padding = 0;
            float[] hw9 = { 1 };
            PT9.SetWidths(hw9);
            PT9.WidthPercentage = 100;
            PdfPCell cell91 = new PdfPCell(new Paragraph(page.ToString() + " / " + pages.ToString(), font9));
            cell91.Border = Rectangle.NO_BORDER;
            cell91.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell91.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell91.FixedHeight = 15;
            PT9.AddCell(cell91);

            //doc.Add(PT1);
            doc.Add(PT2);
            doc.Add(PT3);
            doc.Add(PT4);
            doc.Add(PT5);
            doc.Add(PT6);
            doc.Add(PT7);
            doc.Add(PT9);
        }
        #endregion

        #region 未缴费通知单打印

        public static void UnpayPrint(Document doc, string OrderRP)
        {
            try
            {
                Data obj = new Data();
                //int count = int.Parse(obj.PopulateDataSet("select count(1) as Cnt from Op_OrderDetail where RefRP='" + OrderRP + "'").Tables[0].Rows[0]["Cnt"].ToString());
                int count = int.Parse(obj.PopulateDataSet("select count(1) as Cnt from Op_OrderDetail a " +
                "left join Op_ContractRMRentList b on a.RefNo=b.RowPointer " +
                "left join Op_ContractRMRentList_Readout c on c.RefRentRP=b.RowPointer " +
                "left join Op_Readout d on d.RowPointer=c.RefReadoutRP " +
                "where a.RefRP='" + OrderRP + "' and ISNULL(a.ODARAmount,0)!=ISNULL(a.ODPaidAmount,0)").Tables[0].Rows[0]["Cnt"].ToString());

                int page = int.Parse(System.Math.Ceiling(count / 8.0).ToString());

                for (int i = 1; i <= page; i++)
                {
                    DataTable dt = obj.ExecSelect("Op_OrderDetail a " +
                        "left join Mstr_Service b on a.ODSRVNo=b.SRVNo " +
                        "left join Mstr_ServiceProvider c on c.SPNo=a.ODContractSPNo " +
                        "left join Op_OrderHeader d on d.RowPointer=a.RefRP " +
                        "left join Mstr_Customer e on e.CustNo=d.CustNo " +


                        "left join Op_ContractRMRentList f on a.RefNo=f.RowPointer " +
                        "left join Op_ContractRMRentList_Readout g on g.RefRentRP=f.RowPointer " +
                        "left join Op_Readout h on h.RowPointer=g.RefReadoutRP "
                        ,

                        "a.*,b.SRVName,c.SPName,c.SPBank,c.SPBankAccount,c.SPBankTitle,d.OrderTime,e.CustName,d.OrderType," +

                        "h.LastReadout as RLastReadout,h.Readout as RReadout,isnull(h.Readings,0)*isnull(h.MeteRate,0) as RODQTY," +
                        "isnull(h.Readings,0)*isnull(h.MeteRate,0)*a.ODUnitPrice as RODARAmount,h.MeteRate as RMeteRate,ISNULL(a.ODARAmount,0)-ISNULL(a.ODPaidAmount,0) as ODUnpayAmount",

                        " and a.RefRP=" + "'" + OrderRP + "' and ISNULL(a.ODARAmount,0)!=ISNULL(a.ODPaidAmount,0)", i, 8, "a.ResourceNo,a.ODCreateDate");

                    if (i > 1) doc.NewPage();

                    if (dt.Rows[0]["OrderType"].ToString() == "04" || dt.Rows[0]["OrderType"].ToString() == "09")
                    {
                        UnpayPrintHeader_PT(doc, dt.Rows[0]);
                        UnpayPrintBody_PT(doc, dt, i, page);
                        PrintFooter_PT(doc, OrderRP, dt.Rows[0], i, page);
                    }
                    else
                    {
                        UnpayPrintHeader(doc, dt.Rows[0]);
                        UnpayPrintBody(doc, dt, i, page);
                        PrintFooter(doc, OrderRP, dt.Rows[0], i, page);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        #region 头部打印

        private static void UnpayPrintHeader(Document doc, DataRow dr)
        {
            PdfPTable Tit = new PdfPTable(1);
            Tit.DefaultCell.Padding = 3;
            float[] wid = { 1 };
            Tit.SetWidths(wid);
            Tit.WidthPercentage = 100;
            PdfPCell cell1 = new PdfPCell(new Paragraph(DateTime.Parse(dr["OrderTime"].ToString()).ToString("yyyy年MM月 ") + "未缴费通知单", font16));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell1.Border = Rectangle.NO_BORDER;
            cell1.FixedHeight = 60;
            Tit.AddCell(cell1);
            doc.Add(Tit);

            PdfPTable PT1 = new PdfPTable(1);
            PT1.DefaultCell.Padding = 0;
            float[] hw8 = { 1 };
            PT1.SetWidths(hw8);
            PT1.WidthPercentage = 100;
            PT1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PdfPCell cell11 = new PdfPCell(new Paragraph("客户：" + dr["CustName"].ToString(), font9_Bold));
            cell11.HorizontalAlignment = Element.ALIGN_LEFT;
            cell11.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell11.Border = Rectangle.NO_BORDER;
            cell11.FixedHeight = 20;
            PT1.AddCell(cell11);
            doc.Add(PT1);
        }

        private static void UnpayPrintHeader_PT(Document doc, DataRow dr)
        {
            PdfPTable Tit = new PdfPTable(1);
            Tit.DefaultCell.Padding = 3;
            float[] wid = { 1 };
            Tit.SetWidths(wid);
            Tit.WidthPercentage = 100;
            PdfPCell cell1 = new PdfPCell(new Paragraph(DateTime.Parse(dr["OrderTime"].ToString()).ToString("yyyy年MM月 ") + "未缴费通知单", font16));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell1.Border = Rectangle.NO_BORDER;
            cell1.FixedHeight = 60;
            Tit.AddCell(cell1);
            doc.Add(Tit);

            PdfPTable PT1 = new PdfPTable(1);
            PT1.DefaultCell.Padding = 0;
            float[] hw8 = { 1 };
            PT1.SetWidths(hw8);
            PT1.WidthPercentage = 100;
            PT1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PdfPCell cell11 = new PdfPCell(new Paragraph("客户：" + dr["CustName"].ToString(), font9_Bold));
            cell11.HorizontalAlignment = Element.ALIGN_LEFT;
            cell11.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell11.Border = Rectangle.NO_BORDER;
            cell11.FixedHeight = 20;
            PT1.AddCell(cell11);
            doc.Add(PT1);
        }

        #endregion

        #region 数据表格打印

        private static void UnpayPrintBody(Document doc, DataTable dt, int page, int pages)
        {
            PdfPTable PT1 = new PdfPTable(5);
            PT1.DefaultCell.Padding = 3;
            float[] hw1 = { 2, 14, 10, 10, 10 };
            PT1.SetWidths(hw1);
            PT1.WidthPercentage = 100;
            PT1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell cell1 = new PdfPCell(new Paragraph("序号", font9_Bold));
            cell1.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.FixedHeight = 18;
            PdfPCell cell2 = new PdfPCell(new Paragraph("资源编号", font9_Bold));
            cell2.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell3 = new PdfPCell(new Paragraph("费用项", font9_Bold));
            cell3.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell4 = new PdfPCell(new Paragraph("应收金额", font9_Bold));
            cell4.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell5 = new PdfPCell(new Paragraph("未缴金额", font9_Bold));
            cell5.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell5.HorizontalAlignment = Element.ALIGN_CENTER;

            PT1.AddCell(cell1);
            PT1.AddCell(cell2);
            PT1.AddCell(cell3);
            PT1.AddCell(cell4);
            PT1.AddCell(cell5);

            doc.Add(PT1);
            int cnt = 0;
            int row = (page - 1) * 10;
            foreach (DataRow dr in dt.Rows)
            {
                PdfPTable PT2 = new PdfPTable(5);
                PT2.DefaultCell.Padding = 3;
                float[] hw2 = { 2, 14, 10, 10, 10 };
                PT2.SetWidths(hw2);
                PT2.WidthPercentage = 100;

                PdfPCell cell21 = new PdfPCell(new Paragraph((row + 1).ToString(), font9));
                cell21.Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell21.HorizontalAlignment = Element.ALIGN_CENTER;
                cell21.FixedHeight = 18;

                PdfPCell cell22 = new PdfPCell(new Paragraph(dr["ResourceNo"].ToString(), font9));
                cell22.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell22.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell23 = new PdfPCell(new Paragraph(dr["SRVName"].ToString(), font9));
                cell23.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell23.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell24 = new PdfPCell(new Paragraph(decimal.Parse(dr["ODARAmount"].ToString()).ToString("0.##"), font9));
                cell24.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell24.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell25 = new PdfPCell(new Paragraph(decimal.Parse(dr["ODUnpayAmount"].ToString()).ToString("0.##"), font9));
                cell25.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell25.HorizontalAlignment = Element.ALIGN_CENTER;

                PT2.AddCell(cell21);
                PT2.AddCell(cell22);
                PT2.AddCell(cell23);
                PT2.AddCell(cell24);
                PT2.AddCell(cell25);
                doc.Add(PT2);
                row++;
                cnt++;
            }
            int maxcnt = 7;
            if (dt.Rows.Count > 0 && pages == page)
            {
                Data obj = new Data();
                DataTable dt1 = obj.PopulateDataSet("select SUM(ISNULL(ODARAmount,0)-ISNULL(ODPaidAmount,0)) as ODARAmount from Op_OrderDetail where RefRP='" + dt.Rows[0]["RefRP"].ToString()
                    + "' and ISNULL(ODARAmount,0)!=ISNULL(ODPaidAmount,0)").Tables[0];
                decimal ODARAmount = decimal.Parse(dt1.Rows[0]["ODARAmount"].ToString());

                PdfPTable PT3 = new PdfPTable(5);
                PT3.DefaultCell.Padding = 3;
                float[] hw3 = { 2, 14, 10, 10, 10 };
                PT3.SetWidths(hw3);
                PT3.WidthPercentage = 100;

                PdfPCell cell31 = new PdfPCell(new Paragraph("合计：  ", font9));
                cell31.Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell31.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell31.FixedHeight = 18;
                cell31.Colspan = 4;

                PdfPCell cell34 = new PdfPCell(new Paragraph(ODARAmount.ToString("0.##"), font9));
                cell34.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell34.HorizontalAlignment = Element.ALIGN_CENTER;

                PT3.AddCell(cell31);
                PT3.AddCell(cell34);
                doc.Add(PT3);

                maxcnt++;
            }

            while (cnt < maxcnt)
            {
                PdfPTable PT2 = new PdfPTable(1);
                PT2.DefaultCell.Padding = 3;
                float[] hw2 = { 1 };
                PT2.SetWidths(hw2);
                PT2.WidthPercentage = 100;

                PdfPCell cel = new PdfPCell(new Paragraph());
                cel.FixedHeight = 18;
                cel.Border = Rectangle.NO_BORDER;

                PT2.AddCell(cel);
                doc.Add(PT2);

                cnt++;
            }
        }

        private static void UnpayPrintBody_PT(Document doc, DataTable dt, int page, int pages)
        {
            PdfPTable PT1 = new PdfPTable(5);
            PT1.DefaultCell.Padding = 3;
            float[] hw1 = { 2, 14, 10, 10, 10 };
            PT1.SetWidths(hw1);
            PT1.WidthPercentage = 100;
            PT1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell cell1 = new PdfPCell(new Paragraph("序号", font9_Bold));
            cell1.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.FixedHeight = 18;
            PdfPCell cell2 = new PdfPCell(new Paragraph("资源编号", font9_Bold));
            cell2.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell3 = new PdfPCell(new Paragraph("费用项", font9_Bold));
            cell3.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell4 = new PdfPCell(new Paragraph("应收金额", font9_Bold));
            cell4.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell5 = new PdfPCell(new Paragraph("未缴金额", font9_Bold));
            cell5.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
            cell5.HorizontalAlignment = Element.ALIGN_CENTER;

            PT1.AddCell(cell1);
            PT1.AddCell(cell2);
            PT1.AddCell(cell3);
            PT1.AddCell(cell4);
            PT1.AddCell(cell5);

            doc.Add(PT1);
            int cnt = 0;
            int row = (page - 1) * 10;
            foreach (DataRow dr in dt.Rows)
            {
                PdfPTable PT2 = new PdfPTable(5);
                PT2.DefaultCell.Padding = 3;
                float[] hw2 = { 2, 14, 10, 10, 10 };
                PT2.SetWidths(hw2);
                PT2.WidthPercentage = 100;

                PdfPCell cell21 = new PdfPCell(new Paragraph((row + 1).ToString(), font9));
                cell21.Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell21.HorizontalAlignment = Element.ALIGN_CENTER;
                cell21.FixedHeight = 18;

                PdfPCell cell22 = new PdfPCell(new Paragraph(dr["ResourceNo"].ToString(), font9));
                cell22.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell22.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell23 = new PdfPCell(new Paragraph(dr["SRVName"].ToString(), font9));
                cell23.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell23.HorizontalAlignment = Element.ALIGN_CENTER;

                string ODARAmount = decimal.Parse(dr["ODARAmount"].ToString()).ToString("0.##");
                string UnpayAmount = (decimal.Parse(dr["ODARAmount"].ToString()) - decimal.Parse(string.IsNullOrEmpty(dr["ODPaidAmount"].ToString()) ? "0" : dr["ODPaidAmount"].ToString())).ToString("0.##");
                //水费、电费、公摊电费、超额电费 dr["ODSRVNo"].ToString() == "GDDF-56" ||
                if (dr["ODSRVNo"].ToString() == "DF-56" || dr["ODSRVNo"].ToString() == "SF-55" || dr["ODSRVNo"].ToString() == "CEDF-62")
                {
                    if (dr["RODARAmount"].ToString() != "")
                    {
                        ODARAmount = decimal.Parse(dr["RODARAmount"].ToString()).ToString("0.##");
                        UnpayAmount = (decimal.Parse(dr["RODARAmount"].ToString()) - decimal.Parse(dr["ODPaidAmount"].ToString())).ToString("0.##");
                    }

                }

                PdfPCell cell24 = new PdfPCell(new Paragraph(ODARAmount, font9));
                cell24.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell24.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell25 = new PdfPCell(new Paragraph(UnpayAmount, font9));
                cell25.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell25.HorizontalAlignment = Element.ALIGN_CENTER;

                PT2.AddCell(cell21);
                PT2.AddCell(cell22);
                PT2.AddCell(cell23);
                PT2.AddCell(cell24);
                PT2.AddCell(cell25);

                doc.Add(PT2);
                row++;
                cnt++;
            }
            int maxcnt = 7;
            if (dt.Rows.Count > 0 && pages == page)
            {
                Data obj = new Data();
                DataTable dt1 = obj.PopulateDataSet("select SUM(ISNULL(ODARAmount,0)-ISNULL(ODPaidAmount,0)) as ODARAmount from Op_OrderDetail where RefRP='"
                    + dt.Rows[0]["RefRP"].ToString() + "' and ISNULL(ODARAmount,0)!=ISNULL(ODPaidAmount,0)").Tables[0];
                decimal ODARAmount = decimal.Parse(dt1.Rows[0]["ODARAmount"].ToString());

                PdfPTable PT3 = new PdfPTable(5);
                PT3.DefaultCell.Padding = 3;
                float[] hw3 = { 2, 14, 10, 10, 10 };
                PT3.SetWidths(hw3);
                PT3.WidthPercentage = 100;

                PdfPCell cell31 = new PdfPCell(new Paragraph("合计：  ", font9));
                cell31.Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell31.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell31.FixedHeight = 18;
                cell31.Colspan = 4;

                PdfPCell cell34 = new PdfPCell(new Paragraph(ODARAmount.ToString("0.##"), font9));
                cell34.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell34.HorizontalAlignment = Element.ALIGN_CENTER;

                PT3.AddCell(cell31);
                PT3.AddCell(cell34);
                doc.Add(PT3);

                maxcnt++;
            }

            while (cnt < maxcnt)
            {
                PdfPTable PT2 = new PdfPTable(1);
                PT2.DefaultCell.Padding = 3;
                float[] hw2 = { 1 };
                PT2.SetWidths(hw2);
                PT2.WidthPercentage = 100;

                PdfPCell cel = new PdfPCell(new Paragraph());
                cel.FixedHeight = 18;
                cel.Border = Rectangle.NO_BORDER;

                PT2.AddCell(cel);
                doc.Add(PT2);

                cnt++;
            }
        }

        #endregion

        #endregion
        public static bool PDFWatermark(string inputfilepath, string outputfilepath, string ModelPicName, float top, float left)
        {
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            try
            {
                pdfReader = new PdfReader(inputfilepath);
                int numberOfPages = pdfReader.NumberOfPages;
                iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);
                float width = psize.Width;
                float height = psize.Height;
                pdfStamper = new PdfStamper(pdfReader, new FileStream(outputfilepath, FileMode.Create));
                PdfContentByte waterMarkContent;
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ModelPicName);
                image.GrayFill = 20;//透明度，灰色填充
                //image.Rotation//旋转
                //image.RotationDegrees//旋转角度
                //水印的位置 
                if (left < 0)
                {
                    left = width - image.Width + left;
                }

                image.SetAbsolutePosition(left, (height - image.Height) - top);

                //每一页加水印,也可以设置某一页加水印 
                for (int i = 1; i <= numberOfPages; i++)
                {
                    waterMarkContent = pdfStamper.GetUnderContent(i);
                    waterMarkContent.AddImage(image);
                }
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.Trim();
                return false;
            }
            finally
            {
                if (pdfStamper != null)
                    pdfStamper.Close();

                if (pdfReader != null)
                    pdfReader.Close();
            }
        }

    }

}
