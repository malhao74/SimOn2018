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
        #region Definicao de variaveis
        private static readonly string ficheiroXml = "Dados/l201803C.xml";
        #endregion

        internal static List<Marca> GetMarcasExcel()
        {
            Func<Viatura, bool> perdicate = x => true;
            List<Marca> marcas = GetViaturas(perdicate).Cast<Marca>().ToList();
            return marcas.Distinct().ToList();
        }
        internal static List<MarcaModelo> GetModelos(Marca marca)
        {
            Func<Viatura, bool> perdicate = x => x.DescricaoMarca == marca.DescricaoMarca;
            List<MarcaModelo> modelos = GetViaturas(perdicate).Cast<MarcaModelo>().ToList();
            return modelos.Distinct().ToList();
        }
        internal static List<MarcaModeloVersao> GetVersoes(MarcaModelo modelo)
        {
            Func<Viatura, bool> perdicate = x => x.DescricaoMarca == modelo.DescricaoMarca &&
                                                 x.DescricaoModelo == modelo.DescricaoModelo;
            List<MarcaModeloVersao> versoes = GetViaturas(perdicate).Cast<MarcaModeloVersao>().ToList();
            return versoes;
        }
        internal static Viatura GetViatura(MarcaModeloVersao versao)
        {
            Func<Viatura, bool> perdicate = x => x.DescricaoMarca == versao.DescricaoMarca &&
                                                 x.DescricaoModelo == versao.DescricaoModelo &&
                                                 x.DescricaoVersao == versao.DescricaoVersao;
            List<Viatura> viaturas = GetViaturas(perdicate);
            return viaturas.FirstOrDefault();
        }

        internal static List<Viatura> GetViaturas(Func<Viatura,bool> perdicate)
        {
            System.Xml.Linq.XElement eurotax = System.Xml.Linq.XElement.Load(ficheiroXml);
            List<Viatura> viaturas = eurotax.Elements("NewsItemRow").Select( x =>
                                    new Viatura { DescricaoMarca = (string)x.Element("MARCA"),
                                                  DescricaoModelo = (string)x.Element("MODELO"),
                                                  DescricaoVersao = (string)x.Element("VERSAO"),
                                                  PrecoNovo = Convert.ToDouble((string)x.Element("PNOVO"))
                                    }).Where(perdicate).ToList();
            return viaturas;
        }
    }
}
