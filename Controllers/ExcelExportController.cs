using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace core.Controllers
{
    [Route("[controller]")]
    [ApiController]  
    public class ExcelExportController : ControllerBase
    {
        public interface IAbstractDataExport
        {
            HttpResponseMessage Export(List<PersonInforDetail> exportData, string fileName, string sheetName);
        }

        public abstract class AbstractDataExport : IAbstractDataExport
        {
            protected string _sheetName;
            protected string _fileName;
            protected List<String> _headers;
            protected List<String> _type;
            protected IWorkbook _workbook;
            protected ISheet _sheet;
            private const string DefaultSheetName = "Sheet1";
            public abstract void WriteData(List<PersonInforDetail> exportData);

            [HttpGet]
            public HttpResponseMessage Export(List<PersonInforDetail> exportData, string fileName, string sheetName = DefaultSheetName)
            {
                _fileName = fileName;
                _sheetName = sheetName;

                _workbook = new XSSFWorkbook(); //Creating New Excel object
                _sheet = _workbook.CreateSheet(_sheetName);

                var headerStyle = _workbook.CreateCellStyle(); //Formatting
                var headerFont = _workbook.CreateFont();
                headerFont.IsBold = true;
                headerStyle.SetFont(headerFont);

                WriteData(exportData); //your list object to NPOI excel conversion happens here

                //Header
                var header = _sheet.CreateRow(0);
                for (var i = 0; i < _headers.Count; i++)
                {
                    var cell = header.CreateCell(i);
                    cell.SetCellValue(_headers[i]);
                    cell.CellStyle = headerStyle;
                    // It's heavy, it slows down your Excel if you have large data                
                    //_sheet.AutoSizeColumn(i);
                }

                using (var memoryStream = new MemoryStream()) //creating memoryStream
                {
                    _workbook.Write(memoryStream);
                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(memoryStream.ToArray())
                    };

                    response.Content.Headers.ContentType = new MediaTypeHeaderValue
                           ("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    response.Content.Headers.ContentDisposition =
                           new ContentDispositionHeaderValue("attachment")
                           {
                               FileName = $"{_fileName}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx"
                           };

                    return response;
                }
            }
        }
        

        public class AbstractDataExportBridge : AbstractDataExport
        {
            public AbstractDataExportBridge()
            {
                _headers = new List<string>();
                _type = new List<string>();
            }

            public override void WriteData(List<PersonInforDetail> exportData)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(PersonInforDetail));

                DataTable table = new DataTable();

                foreach (PropertyDescriptor prop in properties)
                {
                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    _type.Add(type.Name);
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                                      prop.PropertyType);
                    string name = Regex.Replace(prop.Name, "([A-Z])", " $1").Trim();
                    //name by caps for header
                    _headers.Add(name);
                }

                foreach (var item in exportData)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    table.Rows.Add(row);
                }

                IRow sheetRow = null;

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    sheetRow = _sheet.CreateRow(i + 1);

                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        ICell Row1 = sheetRow.CreateCell(j);
                        string cellvalue = Convert.ToString(table.Rows[i][j]);

                        // TODO: move it to switch case

                        if (string.IsNullOrWhiteSpace(cellvalue))
                        {
                            Row1.SetCellValue(string.Empty);
                        }
                        else if (_type[j].ToLower() == "string")
                        {
                            Row1.SetCellValue(cellvalue);
                        }
                        else if (_type[j].ToLower() == "int32")
                        {
                            Row1.SetCellValue(Convert.ToInt32(table.Rows[i][j]));
                        }
                        else if (_type[j].ToLower() == "double")
                        {
                            Row1.SetCellValue(Convert.ToDouble(table.Rows[i][j]));
                        }
                        else if (_type[j].ToLower() == "datetime")
                        {
                            Row1.SetCellValue(Convert.ToDateTime
                                 (table.Rows[i][j]).ToString("dd/MM/yyyy hh:mm:ss"));
                        }
                        else
                        {
                            Row1.SetCellValue(string.Empty);
                        }
                    }
                }
            }
        }

    }
}
