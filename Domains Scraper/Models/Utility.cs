using Domains_Scraper.Entity_Framework_folder;
using Domains_Scraper.Services;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
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
    public static class SaveToExcel
    {
        public static async void Save<T2>(this List<T2> objects, string path)
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

        
    }
    public static class InsertAndUpdateExtenstion
    {
        public static async Task BulkInsert<T>(this LibraryContext dbContext, List<T> models, int batch = 1000) where T : class
        {
            var table = dbContext.Model.FindEntityType(typeof(T)).GetTableName();
            var fieldsSql = new StringBuilder($"insert into {table} (");
            var properties = new List<PropertyInfo>();
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.Name.Equals("Id")) continue;
                Type t = propertyInfo.PropertyType;
                if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    if (t.GetGenericArguments()[0] != typeof(int))
                        continue;
                }
                else if (Type.GetTypeCode(t) == TypeCode.Object)
                {
                    continue;
                }
                properties.Add(propertyInfo);
                fieldsSql.Append("`" + propertyInfo.Name + "`").Append(",");
            }
            fieldsSql.Remove(fieldsSql.Length - 1, 1);
            fieldsSql.Append(") values");

            var sql = new StringBuilder(fieldsSql.ToString());
            int inserted = 0;
            for (var i = 0; i < models.Count; i++)
            {
                var model = models[i];
                sql.Append("\n(");
                foreach (var propertyInfo in properties)
                {
                    string val = propertyInfo.GetValue(model)?.ToString() ?? "null";

                    if (propertyInfo.PropertyType == typeof(DateTime))
                    {
                        sql.Append($"'{((DateTime)propertyInfo.GetValue(model)):yyyy-MM-dd H:mm:ss}',");
                    }
                    else if (propertyInfo.PropertyType == typeof(string))
                    {
                        sql.Append($"'{MySqlHelper.EscapeString(val)}',");
                    }
                    else if (propertyInfo.PropertyType.IsEnum)
                    {
                        sql.Append((long)propertyInfo.GetValue(model, null)).Append(",");
                    }
                    else
                    {
                        sql.Append(MySqlHelper.EscapeString(val)).Append(",");
                    }
                }
                sql.Remove(sql.Length - 1, 1);
                sql.Append("),");
                inserted++;
                if ((inserted % batch == 0) || i == models.Count - 1)
                {
                    sql.Remove(sql.Length - 1, 1);
                    sql.Append(";");
                    Debug.WriteLine(sql.ToString());
                    Debug.WriteLine(Singleton.ConnectionString);

                    using (MySqlConnection connection = new MySqlConnection(Singleton.ConnectionString))
                    {
                        await connection.OpenAsync();
                        using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), connection))
                        {
                            var x = await cmd.ExecuteNonQueryAsync();
                            Console.WriteLine($"insert {x}");
                        }
                    }

                    sql = new StringBuilder(fieldsSql.ToString());
                    inserted = 0;
                }
            }
        }

        public static async Task BulkUpdate<T>(this LibraryContext dbContext, List<T> models, List<string> propertiesToCheck, int batch = 1000) where T : class
        {
            var table = dbContext.Model.FindEntityType(typeof(T)).GetTableName;
            var properties = typeof(T).GetProperties().Where(x => propertiesToCheck.Contains(x.Name));
            var propertyId = typeof(T).GetProperty("Id");
            var sql = new StringBuilder("");
            int inserted = 0;
            for (var i = 0; i < models.Count; i++)
            {
                var model = models[i];

                sql.Append($"update {table} set ");
                foreach (var propertyInfo in properties)
                {
                    sql.Append($"`{propertyInfo.Name}`").Append(" = ");
                    string val = propertyInfo.GetValue(model)?.ToString() ?? "null";

                    if (propertyInfo.PropertyType == typeof(DateTime))
                    {
                        sql.Append($"'{((DateTime)propertyInfo.GetValue(model)):yyyy-MM-dd H:mm:ss}',");
                    }
                    else if (propertyInfo.PropertyType == typeof(string))
                    {
                        sql.Append($"'{MySqlHelper.EscapeString(val)}',");
                    }
                    else
                    {
                        sql.Append(MySqlHelper.EscapeString(val)).Append(",");
                    }
                }
                sql.Remove(sql.Length - 1, 1);
                sql.Append($" where Id={propertyId.GetValue(model)};\n");

                inserted++;
                if ((inserted % batch == 0) || i == models.Count - 1)
                {
                    Console.WriteLine(sql);
                    Console.WriteLine(Singleton.ConnectionString);

                    using (MySqlConnection connection = new MySqlConnection(Singleton.ConnectionString))
                    {
                        await connection.OpenAsync();
                        using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), connection))
                        {
                            var x = await cmd.ExecuteNonQueryAsync();
                            Console.WriteLine($"insert {x}");
                        }
                    }

                    sql = new StringBuilder();
                    inserted = 0;
                }
            }
        }
    }
}
