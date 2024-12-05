using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Utilitarios.HighCharts
{
    public class ChartColumn
    {
        public List<string> Categories { get; set; }
        public List<ChartColumnSeries> Series { get; set; }

        public ChartColumn()
        {
            Series = new List<ChartColumnSeries>();
            Categories = new List<string>();
        }

    }
    public class ChartColumnSeries
    {
        public string name { get; set; }
        public int y { get; set; }
        public List<object> data { get; set; }
        public ChartColumnSeries()
        {
            data = new List<object>();
        }
    }
}