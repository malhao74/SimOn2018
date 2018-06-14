using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;

namespace SimOn
{
    public enum DataSource { Excel, XML, FireBase}
    public static class DataLayerExcel
    {
        #region Definicao de variaveis
        public static string folhaExcel = AppContext.BaseDirectory.ToString() + @"\L201803C.xls";
        public static string workSheet = "LIGEIROS";
        #endregion

        internal static List<Marca> GetMarcasExcel()
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
        internal static List<MarcaModelo> GetModelosExcel(Marca marca)
        {
            var excel = new ExcelQueryFactory(folhaExcel);
            //excel.AddMapping("descricaoMarca", "MARCA");
            //excel.AddMapping("descricaoModelo", "MODELO");
            var modelosLinq = from c in excel.Worksheet<MarcaModelo>(workSheet)
                              where c.descricaoMarca == marca.descricaoMarca.Trim()
                              select c;


            List<MarcaModelo> modelos = modelosLinq.ToList();
            //Because LinqToExcel doesn't support group by functionality.
            var modelUnicos = modelos.Distinct();
            modelos = modelUnicos.ToList();

            //viaturas..Aggregate(viatura => )
            return modelos;
        }
        internal static List<MarcaModeloVersao> GetVersoesExcel(MarcaModelo modelo)
        {
            var excel = new ExcelQueryFactory(folhaExcel);
            //excel.AddMapping("descricaoMarca", "MARCA");
            //excel.AddMapping("descricaoModelo", "MODELO");
            var versoesLinq = from c in excel.Worksheet<MarcaModeloVersao>(workSheet)
                              where c.descricaoMarca == modelo.descricaoMarca.Trim() && c.descricaoModelo == modelo.descricaoModelo.Trim()
                              select c;


            List<MarcaModeloVersao> versoes = versoesLinq.ToList();
            //Because LinqToExcel doesn't support group by functionality.
            var versoesUnicos = versoes.Distinct();
            versoes = versoesUnicos.ToList();

            //viaturas..Aggregate(viatura => )
            return versoes;
        }
        internal static Viatura GetViaturaExcel(MarcaModeloVersao versao)
        {
            var excel = new ExcelQueryFactory(folhaExcel);
            var viatura = from c in excel.Worksheet<Viatura>(workSheet)
                          where c.descricaoMarca == versao.descricaoMarca.Trim() && c.descricaoModelo == versao.descricaoModelo.Trim() && c.descricaoVersao == versao.descricaoVersao.Trim()
                          select c;
            return viatura.FirstOrDefault();
        }
    }
}
