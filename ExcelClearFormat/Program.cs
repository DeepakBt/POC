using Microsoft.Graph;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelClearFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            FileInfo file = new FileInfo(@"C:\Users\deepak\Desktop\System.Speech\Demo.xlsx");
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();
                excelWorksheet.Cells["A1:C36"].Clear();
                excelPackage.Save();
            }


            Console.WriteLine("End Clear");
            Console.ReadKey();
        }
    }
}
