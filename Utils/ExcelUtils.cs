using System;
using blazor_demo.Data;
using NPOI.SS.UserModel;

namespace blazor_demo.Utils
{
    public static class ExcelUtils
    {
        public static string GetCellValue(this IRow row, int index)
        {
            return row.GetCell(index)?.ToString() ?? string.Empty;
        }

        public static void SetCellValue(this IRow row, int index, string value)
        {
            ICell cell = row.GetCell(index);
            if(cell == null) cell = row.CreateCell(index);
            cell.SetCellValue(value);
        }
    }
}