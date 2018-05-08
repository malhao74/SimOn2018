using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel.Attributes;

namespace SimOn
{
    internal class MarcaModeloVersao : MarcaModelo
    {
        [ExcelColumn("VERSAO")]
        public string descricaoVersao { get; set; }

        public MarcaModeloVersao() { }

    }
}
