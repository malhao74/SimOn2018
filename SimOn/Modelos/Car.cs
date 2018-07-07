using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using LinqToExcel.Attributes;
using Newtonsoft.Json;

namespace SimOn
{
    /// <summary>
    /// Incapsulates the car information extracted from several data sources
    /// </summary>
    internal class Car : Version
    {
        [ExcelColumn("PRICE")] // For excel usage
        [JsonProperty("precoNovo")] // For Firebase usage
        public double Price { get; set; }

        public Car() { }
    }
}
