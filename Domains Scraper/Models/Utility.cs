using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Domains_Scraper.Models
{
    public static class Utility
    {
        public static string ConnectionString = "Data Source=system.db;Version=3;";
        public static string SimpleDateFormat = "dd/MM/yyyy HH:mm:ss";

        public static void SaveCookies(CookieContainer cookieContainer, string url)
        {
            try
            {
                var cookies = new List<Cookie>();
                foreach (Cookie cookie in cookieContainer.GetCookies(new Uri(url)))
                    cookies.Add(new Cookie { Name = cookie.Name, Value = cookie.Value });
                File.WriteAllText("ses", JsonConvert.SerializeObject(cookies));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static string BetweenStrings(string text, string start, string end)
        {
            var p1 = text.IndexOf(start, StringComparison.Ordinal) + start.Length;
            var p2 = text.IndexOf(end, p1, StringComparison.Ordinal);
            if (end == "") return (text.Substring(p1));
            else return text.Substring(p1, p2 - p1);
        }

        public static CookieContainer LoadCookies(string url)
        {
            var cookieContainer = new CookieContainer();
            try
            {
                var myCookies = JsonConvert.DeserializeObject<List<Cookie>>(File.ReadAllText("ses"));
                foreach (var myCookie in myCookies)
                    cookieContainer.Add(new Uri(url), new Cookie(myCookie.Name, myCookie.Value));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return cookieContainer;
        }

    }
    public static class Save
    {
        public static async void SaveToExcel<T2>(this List<T2> objects, string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelPkg = new ExcelPackage(new FileInfo(path));

            var sheet = excelPkg.Workbook.Worksheets.Add("output");
            sheet.Protection.IsProtected = false;
            sheet.Protection.AllowSelectLockedCells = false;
            sheet.Row(1).Height = 20;
            sheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Row(1).Style.Font.Bold = true;
            sheet.Row(1).Style.Font.Size = 8;
            var col = 1;
            var colums = new Dictionary<string, string>();
            foreach (var propertyInfo in typeof(T2).GetProperties())
            {
                var propertyInfoName = propertyInfo.Name;
                var coulumName = propertyInfoName;
                for (int i = 1; i < propertyInfoName.Length - 1; i++)
                {
                    if (char.IsUpper(propertyInfoName[i]) && !char.IsUpper(propertyInfoName[i + 1]))
                        coulumName = coulumName.Replace(propertyInfoName[i] + "", " " + propertyInfoName[i]);
                }
                colums.Add(coulumName, propertyInfoName);
                sheet.Cells[1, col].Value = coulumName;
                col++;
            }

            var colNbr = typeof(T2).GetProperties().Count();
            var columnLetter = ExcelCellAddress.GetColumnLetter(colNbr);
            var range = sheet.Cells[$"A1:{columnLetter}{objects.Count + 1}"];
            var tab = sheet.Tables.Add(range, "");
            tab.TableStyle = TableStyles.Medium2;
            sheet.Cells.Style.Font.Size = 12;
            var row = 2;
            foreach (var obj in objects)
            {
                for (int i = 1; i <= sheet.Dimension.End.Column; i++)
                {
                    var colName = (string)sheet.Cells[1, i].Value;
                    var prop = colums[colName];
                    var value = (obj.GetType().GetProperty(prop))?.GetValue(obj, null);
                    sheet.Cells[row, i].Value = value;
                }
                row++;
            }
            for (int i = 1; i <= sheet.Dimension.End.Column; i++)
                sheet.Column(i).AutoFit();
            await excelPkg.SaveAsync();
        }

        public static async void Bilel<T2>(this List<T2> objects, string path)
        {
            Console.WriteLine(@"hi");
        }
    }
}
