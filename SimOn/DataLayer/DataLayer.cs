using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimOn
{
    public enum DataSource { Excel, XML, FireBase }

    /// <summary>
    /// Disponibiliza um layer entre o interface do utlizador e os dados
    /// </summary>
    class DataLayer
    {
        #region Metodos publicos
        public static List<Marca> GetMarcas(DataSource dataSource)
        {
            switch (dataSource)
            {
                case DataSource.Excel:
                    return DataLayerExcel.GetMarcas();
                case DataSource.XML:
                    return DataLayerXml.GetMarcas();
                case DataSource.FireBase:
                    return DataLayerFireBase.GetMarcas(); ;
                default:
                    return null;
            }
        }

        public static List<MarcaModelo> GetModelos(DataSource dataSource, Marca marca)
        {
            switch (dataSource)
            {
                case DataSource.Excel:
                    return DataLayerExcel.GetModelos(marca);
                case DataSource.XML:
                    return DataLayerXml.GetModelos(marca);
                case DataSource.FireBase:
                    return DataLayerFireBase.GetModelos(marca);
                default:
                    return null;
            }
        }

        public static List<MarcaModeloVersao> GetVersoes(DataSource dataSource, MarcaModelo modelo)
        {
            switch (dataSource)
            {
                case DataSource.Excel:
                    return DataLayerExcel.GetVersoes(modelo);
                case DataSource.XML:
                    return DataLayerXml.GetVersoes(modelo);
                case DataSource.FireBase:
                    return DataLayerFireBase.GetVersoes(modelo);
                default:
                    return null;
            }
        }

        public static Viatura GetViatura(DataSource dataSource, MarcaModeloVersao versao)
        {
            switch (dataSource)
            {
                case DataSource.Excel:
                    return DataLayerExcel.GetViatura(versao);
                case DataSource.XML:
                    return DataLayerXml.GetViatura(versao);
                case DataSource.FireBase:
                    return DataLayerFireBase.GetViatura(versao);
                default:
                    return null;
            }
        }

        #endregion
    }
}
