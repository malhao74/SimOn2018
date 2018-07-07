using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel.Attributes;
using Newtonsoft.Json;

namespace SimOn
{
    /// <summary>
    /// Encapsulates the version information of a specific car
    /// </summary>
    internal class Version : Model
    {
        [ExcelColumn("VERSION")] // For excel usage
        [JsonProperty("descricaoVersao")] // For Firebase usage
        public string VersionDescription { get; set; }

        public int VersionId { get; set; } // For FireBase usage
        public int Model { get; set; }

        public Version() { }
    }
}
