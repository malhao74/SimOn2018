using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimOn
{
    public enum DataSource { Excel, Xml, FireBase }

    /// <summary>
    /// Provides a layer between the UI and the multiple data layers
    /// </summary>
    internal static class DataLayer
    {
        public static List<Brand> FetchBrands(DataSource dataSource)
        {
            switch (dataSource)
            {
                case DataSource.Excel:
                    return DataLayerExcel.FetchBrands();
                case DataSource.Xml:
                    return DataLayerXml.FetchBrands();
                case DataSource.FireBase:
                    return DataLayerFireBase.FetchBrands(); ;
                default:
                    return null;
            }
        }

        public static List<Model> FetchModels(DataSource dataSource, Brand marca)
        {
            switch (dataSource)
            {
                case DataSource.Excel:
                    return DataLayerExcel.FetchModels(marca);
                case DataSource.Xml:
                    return DataLayerXml.FetchModels(marca);
                case DataSource.FireBase:
                    return DataLayerFireBase.FetchModels(marca);
                default:
                    return null;
            }
        }

        public static List<Version> FetchVersions(DataSource dataSource, Model modelo)
        {
            switch (dataSource)
            {
                case DataSource.Excel:
                    return DataLayerExcel.FetchVersions(modelo);
                case DataSource.Xml:
                    return DataLayerXml.FetchVersions(modelo);
                case DataSource.FireBase:
                    return DataLayerFireBase.FetchVersions(modelo);
                default:
                    return null;
            }
        }

        public static Car FetchCar(DataSource dataSource, Version versao)
        {
            switch (dataSource)
            {
                case DataSource.Excel:
                    return DataLayerExcel.FetchCar(versao);
                case DataSource.Xml:
                    return DataLayerXml.FetchCar(versao);
                case DataSource.FireBase:
                    return DataLayerFireBase.FetchCar(versao);
                default:
                    return null;
            }
        }
    }
}
