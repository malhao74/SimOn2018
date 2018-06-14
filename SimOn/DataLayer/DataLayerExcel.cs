using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;

namespace SimOn
{
    public static class DataLayerExcel
    {
        #region Declaracao de variaveis
        public static string folhaExcel = AppContext.BaseDirectory.ToString() + @"Dados\L201803C.xls";
        public static string workSheet = "LIGEIROS";
        #endregion

        #region Metodos internos
        internal static List<Marca> GetMarcas()
        {
            var excel = new ExcelQueryFactory(folhaExcel);
            //excel.AddMapping("precoNovo", "PNOVO");
            var marcasLinq = from c in excel.Worksheet<Marca>(workSheet)
                             select c;


            List<Marca> marcas = marcasLinq.ToList();
            //Because LinqToExcel doesn't support group by functionality.
            var marcasUnicas = marcas.Distinct();
            marcas = marcasUnicas.ToList();
            return marcas;
        }
        internal static List<MarcaModelo> GetModelos(Marca marca)
        {
            var excel = new ExcelQueryFactory(folhaExcel);
            //excel.AddMapping("descricaoMarca", "MARCA");
            //excel.AddMapping("descricaoModelo", "MODELO");
            var modelosLinq = from c in excel.Worksheet<MarcaModelo>(workSheet)
                              where c.DescricaoMarca == marca.DescricaoMarca.Trim()
                              select c;


            List<MarcaModelo> modelos = modelosLinq.ToList();
            //Because LinqToExcel doesn't support group by functionality.
            var modelUnicos = modelos.Distinct();
            modelos = modelUnicos.ToList();

            //viaturas..Aggregate(viatura => )
            return modelos;
        }
        internal static List<MarcaModeloVersao> GetVersoes(MarcaModelo modelo)
        {
            var excel = new ExcelQueryFactory(folhaExcel);
            //excel.AddMapping("descricaoMarca", "MARCA");
            //excel.AddMapping("descricaoModelo", "MODELO");
            var versoesLinq = from c in excel.Worksheet<MarcaModeloVersao>(workSheet)
                              where c.DescricaoMarca == modelo.DescricaoMarca.Trim() && c.DescricaoModelo == modelo.DescricaoModelo.Trim()
                              select c;


            List<MarcaModeloVersao> versoes = versoesLinq.ToList();
            //Because LinqToExcel doesn't support group by functionality.
            var versoesUnicos = versoes.Distinct();
            versoes = versoesUnicos.ToList();

            //viaturas..Aggregate(viatura => )
            return versoes;
        }
        internal static Viatura GetViatura(MarcaModeloVersao versao)
        {
            var excel = new ExcelQueryFactory(folhaExcel);
            var viatura = from c in excel.Worksheet<Viatura>(workSheet)
                          where c.DescricaoMarca == versao.DescricaoMarca.Trim() && c.DescricaoModelo == versao.DescricaoModelo.Trim() && c.DescricaoVersao == versao.DescricaoVersao.Trim()
                          select c;
            return viatura.FirstOrDefault();
        }
        #endregion
    }
}
