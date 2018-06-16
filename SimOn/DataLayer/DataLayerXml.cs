using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimOn
{
    class DataLayerXml
    {
        #region Definicao de variaveis
        private static readonly string ficheiroXml = "Dados/l201803C.xml";
        #endregion

        #region Metodos internos
        internal static List<Marca> GetMarcas()
        {
            bool perdicate(Viatura x) => true;
            List<Marca> marcas = new List<Marca>(GetViaturas(perdicate).Cast<Marca>());
            return marcas.Distinct().ToList(); ;
        }

        internal static List<MarcaModelo> GetModelos(Marca marca)
        {
            bool perdicate(Viatura x) => x.DescricaoMarca == marca.DescricaoMarca;
            List<MarcaModelo> modelos = new List<MarcaModelo>(GetViaturas(perdicate).Cast<MarcaModelo>());
            return modelos.Distinct().ToList();
        }

        internal static List<MarcaModeloVersao> GetVersoes(MarcaModelo modelo)
        {
            bool perdicate(Viatura x) => x.DescricaoMarca == modelo.DescricaoMarca &&
                                                 x.DescricaoModelo == modelo.DescricaoModelo;
            List<MarcaModeloVersao> versoes = GetViaturas(perdicate).Cast<MarcaModeloVersao>().ToList();
            return versoes;
        }

        internal static Viatura GetViatura(MarcaModeloVersao versao)
        {
            bool perdicate(Viatura x) => x.DescricaoMarca == versao.DescricaoMarca &&
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
        #endregion
    }
}
