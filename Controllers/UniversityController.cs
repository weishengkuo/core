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
    public class UniversityController : ControllerBase
    {
        public IEnumerable<University> PersonData()
        {
            University[] list =
            {
                new University()
                {
                    StudentID = "10028756",
                    StudentName = "王俊傑",
                    Subject = "資訊工程"
                },

                new University()
                {
                    StudentID = "10028757",
                    StudentName = "陳小美",
                    Subject = "應用外語系"
                },

                new University()
                {
                    StudentID = "10028758",
                    StudentName = "李培安",
                    Subject = "地球科學與地質學系"
                },

                new University()
                {
                    StudentID = "10028759",
                    StudentName = "蔣毅翔",
                    Subject = "機械工程"
                },

                new University()
                {
                    StudentID = "10028760",
                    StudentName = "吳明寰",
                    Subject = "通訊工程"
                },

                new University()
                {
                    StudentID = "10028761",
                    StudentName = "林曉君",
                    Subject = "材料工程"
                },

            };

            return list;
        }

        [HttpGet]
        public ActionResult Get()
        {
            List<University> result = PersonData().ToList();

            //建立Excel
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(); //建立活頁簿
            ISheet sheet = hssfworkbook.CreateSheet("sheet"); //建立sheet

            //設定樣式
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
            sheet.AddMergedRegion(new CellRangeAddress(0, 1, 0, 2)); //合併1~2列及A~C欄儲存格
            sheet.GetRow(0).CreateCell(0).SetCellValue("昕力大學");
            sheet.GetRow(0).GetCell(0).CellStyle = headerStyle; //套用樣式
            sheet.CreateRow(2).CreateCell(0).SetCellValue("學生編號");
            sheet.GetRow(2).CreateCell(1).SetCellValue("學生姓名");
            sheet.GetRow(2).CreateCell(2).SetCellValue("就讀科系");

            //填入資料
            int rowIndex = 3;
            for (int row = 0; row < result.Count(); row++)
            {
                sheet.CreateRow(rowIndex).CreateCell(0).SetCellValue(result[row].StudentID);
                sheet.GetRow(rowIndex).CreateCell(1).SetCellValue(result[row].StudentName);
                sheet.GetRow(rowIndex).CreateCell(2).SetCellValue(result[row].Subject);
                rowIndex++;
            }

            var excelDatas = new MemoryStream();
            hssfworkbook.Write(excelDatas);

            return File(excelDatas.ToArray(), "application/vnd.ms-excel", string.Format($"學生資料.xls"));
        }
    }
}
