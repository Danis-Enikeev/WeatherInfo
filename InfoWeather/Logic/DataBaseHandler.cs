using InfoWeather.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InfoWeather.Logic
{

    public static class DataBaseHandler
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static List<WeatherEntry> GetData(int year, int month)
        {
            using (WeatherDBModelContainer db = new WeatherDBModelContainer())
            {
                var query = from st in db.WeatherEntries
                            where st.Date.Year == year && st.Date.Month == month
                            orderby st.Date, st.Time
                            select st;
                List<WeatherEntry> data = query.ToList();
                return data;
            }

        }

        public static int[] GetYears()
        {
            using (WeatherDBModelContainer db = new WeatherDBModelContainer())
            {
                var query = (from st in db.WeatherEntries
                             orderby st.Date.Year
                             select st.Date.Year).Distinct();
                int[] years = query.ToArray();
                return years;
            }
        }
        public static int[] GetMonths(int year)
        {
            using (WeatherDBModelContainer db = new WeatherDBModelContainer())
            {
                var query = (from st in db.WeatherEntries
                             where st.Date.Year == year
                             orderby st.Date.Month
                             select st.Date.Month).Distinct();
                int[] months = query.ToArray();
                return months;
            }
        }

        public static void FilesToDB(string filePath)
        {
            XSSFWorkbook xssfwb;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                xssfwb = new XSSFWorkbook(file);
            }
            using (WeatherDBModelContainer db = new WeatherDBModelContainer())
            {
                //Parallel.For(0, xssfwb.NumberOfSheets,
                //i =>
                for (int i = 0; i < xssfwb.NumberOfSheets; i++)
                {
                    ISheet sheet = xssfwb.GetSheetAt(i);
                    for (int row = 4; row <= sheet.LastRowNum; row++)
                    {
                        if (sheet.GetRow(row) != null)
                        {
                            try
                            {
                                db.WeatherEntries.Add(new WeatherEntry
                                {
                                    Date = DateTime.Parse(sheet.GetRow(row).GetCell(0).StringCellValue).Date,
                                    Time = DateTime.Parse(sheet.GetRow(row).GetCell(1).StringCellValue).TimeOfDay,
                                    Temp = sheet.GetRow(row).GetCell(2).NumericCellValue,
                                    Humidity = sheet.GetRow(row).GetCell(3).NumericCellValue,
                                    Td = sheet.GetRow(row).GetCell(4).NumericCellValue,
                                    Pressure = (short)sheet.GetRow(row).GetCell(5).NumericCellValue,
                                    WindDir = (sheet.GetRow(row).GetCell(6).StringCellValue == null) ? null : sheet.GetRow(row).GetCell(6).StringCellValue,
                                    WindSpd = (sheet.GetRow(row).GetCell(7).CellType.ToString() == "String") ? (short?)null : (short)sheet.GetRow(row).GetCell(7).NumericCellValue,
                                    Cloudness = (sheet.GetRow(row).GetCell(8).CellType.ToString() == "String") ? (short?)null : (short)sheet.GetRow(row).GetCell(8).NumericCellValue,
                                    h = (sheet.GetRow(row).GetCell(9).CellType.ToString() == "String") ? (short?)null : (short)sheet.GetRow(row).GetCell(9).NumericCellValue,
                                    VV = (sheet.GetRow(row).GetCell(10).CellType.ToString() == "String") ? (short?)null : (short)sheet.GetRow(row).GetCell(10).NumericCellValue,
                                    WeatherPhen = (sheet.GetRow(row).GetCell(11) == null) ? null : sheet.GetRow(row).GetCell(11).StringCellValue
                                });
                            }
                            catch (Exception ex)
                            {
                                Logger.Error(ex, string.Format("Failed to insert file {0} sheet {1} row {2} into the database", filePath, i, row));
                            }

                        }
                    }
                }
                db.SaveChanges();
            }
        }
    }
}