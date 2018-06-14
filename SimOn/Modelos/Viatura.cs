﻿using System;
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
        #region Declaracao de variaveis
        [ExcelColumn("PNOVO")]
        public double PrecoNovo { get; set; }
        #endregion

        #region Metodos publicos
        public Viatura() { }

        #endregion
    }
}