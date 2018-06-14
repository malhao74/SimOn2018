using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimOn
{
    class DataLayerXml
    {
        //System.Xml.Linq.XElement eurotax = System.Xml.Linq.XElement.Load("l201803C.xml");

        //var marcas = from mc in eurotax.Elements("MARCA")
        //             select mc;
        //var marcasUnicas = marcas.Distinct();

        //    return new List<Marca>();
        #region Variable definition.
        private static string ficheiroXml = "l201803C.xml";
        #endregion

        internal static List<Marca> GetMarcasExcel()
        {
            Func<Viatura, bool> perdicate = x => true;
            List<Marca> marcas = GetViaturas(perdicate).Cast<Marca>().ToList();
            return marcas.Distinct().ToList();
        }
        internal static List<MarcaModelo> GetModelos(Marca marca)
        {
            Func<Viatura, bool> perdicate = x => x.descricaoMarca == marca.descricaoMarca;
            List<MarcaModelo> modelos = GetViaturas(perdicate).Cast<MarcaModelo>().ToList();
            return modelos.Distinct().ToList();
        }
        internal static List<MarcaModeloVersao> GetVersoes(MarcaModelo modelo)
        {
            Func<Viatura, bool> perdicate = x => x.descricaoMarca == modelo.descricaoMarca &&
                                                 x.descricaoModelo == modelo.descricaoModelo;
            List<MarcaModeloVersao> versoes = GetViaturas(perdicate).Cast<MarcaModeloVersao>().ToList();
            return versoes;
        }
        internal static Viatura GetViatura(MarcaModeloVersao versao)
        {
            Func<Viatura, bool> perdicate = x => x.descricaoMarca == versao.descricaoMarca &&
                                                 x.descricaoModelo == versao.descricaoModelo &&
                                                 x.descricaoVersao == versao.descricaoVersao;
            List<Viatura> viaturas = GetViaturas(perdicate);
            return viaturas.FirstOrDefault();
        }

        internal static List<Viatura> GetViaturas(Func<Viatura,bool> perdicate)
        {
            System.Xml.Linq.XElement eurotax = System.Xml.Linq.XElement.Load(ficheiroXml);
            List<Viatura> viaturas = eurotax.Elements("NewsItemRow").Select( x =>
                                    new Viatura { descricaoMarca = (string)x.Element("MARCA"),
                                                  descricaoModelo = (string)x.Element("MODELO"),
                                                  descricaoVersao = (string)x.Element("VERSAO"),
                                                  precoNovo = Convert.ToDouble((string)x.Element("PNOVO"))
                                    }).Where(perdicate).ToList();
            return viaturas;
        }
    }
}
