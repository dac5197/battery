using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Views.Utils
{
    public class CalculateTextareaRows : ICalculateTextareaRows
    {
        public int CalculateRows(string value, int minRows = 3, int maxRows = 20)
        {
            int rows = minRows;
            
            if (value is not null)
            {
                rows = Math.Max(value.Split('\n').Length, value.Split('\r').Length);
            }

            rows = Math.Max(rows, minRows);
            rows = Math.Min(rows, maxRows);

            return rows;
        }
    }
}
