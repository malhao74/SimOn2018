using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using LinqToExcel.Attributes;

namespace SimOn
{
    /// <summary>
    /// Incapsulates the car information extracted from excel file with LinqToExcel.
    /// </summary>
    internal class Viatura : MarcaModeloVersao
    {
        #region Variables declaration
        [ExcelColumn("PNOVO")]
        public double precoNovo { get; set; }
        #endregion

        #region Metodos publicos.
        public Viatura() { }

        #endregion
    }
}
