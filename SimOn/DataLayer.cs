using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;

namespace SimOn
{
    /// <summary>
    /// Provides a layer between the bussiness logic and data location/interface.
    /// </summary>
    class DataLayer
    {
        #region Variable definition.
        //public static string folhaExcel = @"D:\users\f17069c\source\repos\SimOn\SimOn\L201803C.xls";
        public static string folhaExcel = AppContext.BaseDirectory.ToString()+@"\L201803C.xls";
        public static string workSheet = "LIGEIROS";
        #endregion

        #region public methodes
        public static List<Marca> getMarcas()
        {


            //List<string> marcas = new List<string>();
            // Definição de critérios de pequisa.
            //Func<Viatura, bool> predicateBase = (viatura => carta.ProcessamentoId == parametros.processamentoId && (carta.tipoRegisto == parametros.tipoRegisto || parametros.tipoRegisto == ""));
            //Func<Viatura, bool> predicateProduto = (carta => true);
            //Func<Viatura, bool> predicateTipoIdentificador = (item => true);

            FirebaseLayer firebase = new FirebaseLayer();

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
        public static List<MarcaModelo> getModelos (Marca marca)
        {
            //List<string> marcas = new List<string>();
            // Definição de critérios de pequisa.
            //Func<Viatura, bool> predicateBase = (viatura => carta.ProcessamentoId == parametros.processamentoId && (carta.tipoRegisto == parametros.tipoRegisto || parametros.tipoRegisto == ""));
            //Func<Viatura, bool> predicateProduto = (carta => true);
            //Func<Viatura, bool> predicateTipoIdentificador = (item => true);

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
        public static List<MarcaModeloVersao> getVersoes(MarcaModelo modelo)
        {
            //List<string> marcas = new List<string>();
            // Definição de critérios de pequisa.
            //Func<Viatura, bool> predicateBase = (viatura => carta.ProcessamentoId == parametros.processamentoId && (carta.tipoRegisto == parametros.tipoRegisto || parametros.tipoRegisto == ""));
            //Func<Viatura, bool> predicateProduto = (carta => true);
            //Func<Viatura, bool> predicateTipoIdentificador = (item => true);

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
        public static Viatura getViatura(MarcaModeloVersao versao)
        {
            var excel = new ExcelQueryFactory(folhaExcel);
            var viatura = from c in excel.Worksheet<Viatura>(workSheet)
                              where c.descricaoMarca == versao.descricaoMarca.Trim() && c.descricaoModelo == versao.descricaoModelo.Trim() && c.descricaoVersao == versao.descricaoVersao.Trim()
                          select c;
            return viatura.FirstOrDefault();
        }
        #endregion
    }
}
