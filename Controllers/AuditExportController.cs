using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditExportController : ControllerBase
    {
        public IEnumerable<Audit> AuditData()
        {
            Audit[] audit =
            {
                new Audit()
                {
                    RefundEmployee = "Jack",
                    RefundMachine = "TP17POD25",
                    TransactionNumber = "005821369/001",
                    RefundTime = "2020/10/25",
                    SessionTime = "2020/10/05",
                    OverTime = "",
                    Money = 250,
                    RefundMethod = "SWAP",
                    Remarks = "溢收",
                    Authorizer = "Mike",
                    Examiner = "Mary"
                },

                new Audit()
                {
                    RefundEmployee = "Danny",
                    RefundMachine = "TP17POD26",
                    TransactionNumber = "005821369/002",
                    RefundTime = "2020/09/10",
                    SessionTime = "2020/10/05",
                    OverTime = "2020/10/10",
                    Money = 300,
                    RefundMethod = "SWAP",
                    Remarks = "短收",
                    Authorizer = "Kelly",
                    Examiner = "Nice"
                },

                new Audit()
                {
                    RefundEmployee = "Arthur",
                    RefundMachine = "TP17POD27",
                    TransactionNumber = "005821369/003",
                    RefundTime = "2020/08/12",
                    SessionTime = "2020/08/20",
                    OverTime = "",
                    Money = 1000,
                    RefundMethod = "SWAP",
                    Remarks = "",
                    Authorizer = "",
                    Examiner = "Nico"
                },

                new Audit()
                {
                    RefundEmployee = "Harry",
                    RefundMachine = "TP17POD28",
                    TransactionNumber = "005821369/004",
                    RefundTime = "2020/06/13",
                    SessionTime = "2020/07/30",
                    OverTime = "2020/08/15",
                    Money = 1500,
                    RefundMethod = "SWAP",
                    Remarks = "代收",
                    Authorizer = "Terry",
                    Examiner = "Jimmy"
                },

                new Audit()
                {
                    RefundEmployee = "Yoko",
                    RefundMachine = "TP17POD29",
                    TransactionNumber = "005821369/005",
                    RefundTime = "2020/08/16",
                    SessionTime = "2020/08/29",
                    OverTime = "",
                    Money = 2000,
                    RefundMethod = "SWAP",
                    Remarks = "委代收",
                    Authorizer = "Fancy",
                    Examiner = "Ray"
                },

                new Audit()
                {
                    RefundEmployee = "Peter",
                    RefundMachine = "TP17POD30",
                    TransactionNumber = "005821369/006",
                    RefundTime = "2020/10/25",
                    SessionTime = "2020/11/10",
                    OverTime = "",
                    Money = 3000,
                    RefundMethod = "SWAP",
                    Remarks = "",
                    Authorizer = "Gero",
                    Examiner = "Bill"
                },

            };

            return audit;
        }

        public IEnumerable<AuditDetail> AuditDetail()
        {
           List<AuditDetail> auditDetail = new List<AuditDetail>
           {
               new AuditDetail()
               {
                   RefundQty = 10,
                   ExaminQty = 2,
                   TicketTypeActual = 3,
                   TicketTypeVirtual = 5,
                   ShortTicketActual = 3,
                   ShortTicketVirtual = 6,
                   OverTimeTicketActual = 10,
                   OverTimeTicketVirtual = 7
               },

               new AuditDetail()
               {
                   RefundQty = 1,
                   ExaminQty = 2,
                   TicketTypeActual = 3,
                   TicketTypeVirtual = 5,
                   ShortTicketActual = 6,
                   ShortTicketVirtual = 12,
                   OverTimeTicketActual = 20,
                   OverTimeTicketVirtual = 8
               }
           };

            return auditDetail;            
        }

        [HttpGet]
        public ActionResult Get()
        {
            List<Audit> result = AuditData().ToList();
            DateTime date = DateTime.Now;
            
            string auditDate = string.Format("{0}/{1}/{2}", date.Year, date.Month, date.Day);

            //建立Excel
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(); //建立活頁簿
            ISheet sheet = hssfworkbook.CreateSheet("稽核報表"); //建立sheet

            //設定標題樣式
            ICellStyle headerStyle = hssfworkbook.CreateCellStyle();
            IFont headerfont = hssfworkbook.CreateFont();
            headerStyle.Alignment = HorizontalAlignment.Center; //水平置中
            headerStyle.VerticalAlignment = VerticalAlignment.Center; //垂直置中
            headerfont.FontName = "微軟正黑體";
            headerfont.FontHeightInPoints = 20;
            headerfont.Boldweight = (short)FontBoldWeight.Bold;
            headerStyle.SetFont(headerfont);

            //新增標題列
            sheet.CreateRow(0); //需先用CreateRow建立,才可通过GetRow取得該欄位
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 10)); //合併1~10列及A~K欄儲存格
            sheet.GetRow(0).CreateCell(0).SetCellValue("稽核報表");
            sheet.GetRow(0).GetCell(0).CellStyle = headerStyle; //套用樣式

            //設定日期樣式
            ICellStyle dateStyle = hssfworkbook.CreateCellStyle();
            IFont datefont = hssfworkbook.CreateFont();
            dateStyle.Alignment = HorizontalAlignment.Right; //水平靠右           
            datefont.FontName = "微軟正黑體";
            datefont.FontHeightInPoints = 20;
            datefont.Boldweight = (short)FontBoldWeight.Bold;
            dateStyle.SetFont(datefont);

            //新增稽核日期
            sheet.CreateRow(1);
            sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, 10)); //合併1~10列及A~K欄儲存格
            sheet.GetRow(1).CreateCell(0).SetCellValue(auditDate);
            sheet.GetRow(0).GetCell(0).CellStyle = dateStyle; //套用樣式

            sheet.CreateRow(2).CreateCell(0).SetCellValue("退款員工");
            sheet.GetRow(2).CreateCell(1).SetCellValue("退款機台");
            sheet.GetRow(2).CreateCell(2).SetCellValue("交易序號");
            sheet.GetRow(2).CreateCell(3).SetCellValue("退款時間");
            sheet.GetRow(2).CreateCell(4).SetCellValue("場次時間");
            sheet.GetRow(2).CreateCell(5).SetCellValue("超出時間");
            sheet.GetRow(2).CreateCell(6).SetCellValue("金額");
            sheet.GetRow(2).CreateCell(7).SetCellValue("退票方式");
            sheet.GetRow(2).CreateCell(8).SetCellValue("備註");
            sheet.GetRow(2).CreateCell(9).SetCellValue("授權者");
            sheet.GetRow(2).CreateCell(10).SetCellValue("檢查者");

            //填入資料
            int rowIndex = 3;
            for (int row = 0; row < result.Count(); row++)
            {
                sheet.CreateRow(rowIndex).CreateCell(0).SetCellValue(result[row].RefundEmployee);
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue(result[row].RefundMachine);
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue(result[row].TransactionNumber);
                sheet.GetRow(rowIndex).CreateCell(3).SetCellValue(result[row].RefundTime);
                sheet.GetRow(rowIndex).CreateCell(4).SetCellValue(result[row].SessionTime);
                sheet.GetRow(rowIndex).CreateCell(5).SetCellValue(result[row].OverTime);
                sheet.GetRow(rowIndex).CreateCell(6).SetCellValue(result[row].Money);
                sheet.GetRow(rowIndex).CreateCell(7).SetCellValue(result[row].RefundMethod);
                sheet.GetRow(rowIndex).CreateCell(8).SetCellValue(result[row].Remarks);
                sheet.GetRow(rowIndex).CreateCell(9).SetCellValue(result[row].Authorizer);
                sheet.GetRow(rowIndex).CreateCell(10).SetCellValue(result[row].Examiner);
                rowIndex++;
            }

            sheet.CreateRow(10).CreateCell(3).SetCellValue("數量");
            sheet.AddMergedRegion(new CellRangeAddress(10, 10, 3, 4));

            sheet.GetRow(10).CreateCell(5).SetCellValue("票種");
            sheet.AddMergedRegion(new CellRangeAddress(10, 10, 5, 6));

            sheet.GetRow(10).CreateCell(7).SetCellValue("缺少");
            sheet.AddMergedRegion(new CellRangeAddress(10, 10, 7, 8));

            sheet.GetRow(10).CreateCell(9).SetCellValue("超出場次時間退票");
            sheet.AddMergedRegion(new CellRangeAddress(10, 10, 9, 10));

            sheet.CreateRow(11).CreateCell(3).SetCellValue("退票張數");
            sheet.GetRow(11).CreateCell(4).SetCellValue("檢核張數");
            sheet.GetRow(11).CreateCell(5).SetCellValue("實體票");
            sheet.GetRow(11).CreateCell(6).SetCellValue("虛擬票");
            sheet.GetRow(11).CreateCell(7).SetCellValue("實體票");
            sheet.GetRow(11).CreateCell(8).SetCellValue("虛擬票");
            sheet.GetRow(11).CreateCell(9).SetCellValue("實體票");
            sheet.GetRow(11).CreateCell(10).SetCellValue("虛擬票");

            List<AuditDetail> auditDetail = AuditDetail().ToList();

            int rowDetailIndex = 12;
            foreach (var item in auditDetail)
            {
                sheet.CreateRow(rowDetailIndex).CreateCell(3).SetCellValue(item.RefundQty);
                sheet.GetRow(rowDetailIndex).CreateCell(4).SetCellValue(item.ExaminQty);
                sheet.GetRow(rowDetailIndex).CreateCell(5).SetCellValue(item.TicketTypeActual);
                sheet.GetRow(rowDetailIndex).CreateCell(6).SetCellValue(item.TicketTypeVirtual);
                sheet.GetRow(rowDetailIndex).CreateCell(7).SetCellValue(item.ShortTicketActual);
                sheet.GetRow(rowDetailIndex).CreateCell(8).SetCellValue(item.ShortTicketVirtual);
                sheet.GetRow(rowDetailIndex).CreateCell(9).SetCellValue(item.OverTimeTicketActual);
                sheet.GetRow(rowDetailIndex).CreateCell(10).SetCellValue(item.OverTimeTicketVirtual);

                rowDetailIndex++;
            }

            sheet.CreateRow(15).CreateCell(9).SetCellValue("審核人員：");
            sheet.GetRow(15).CreateCell(10).SetCellValue("Ryan");

            sheet.CreateRow(17).CreateCell(9).SetCellValue("日期：");
            sheet.GetRow(17).CreateCell(10).SetCellValue(auditDate);

            var excelDatas = new MemoryStream();
            hssfworkbook.Write(excelDatas);

            return File(excelDatas.ToArray(), "application/vnd.ms-excel", string.Format($"稽核報表.xls"));
        }
    }
}
