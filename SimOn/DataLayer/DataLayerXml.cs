using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SimOn
{
    /// <summary>
    /// Fetch vehicle information from a xml file  
    /// </summary>
    internal static class DataLayerXml
    {
        private const string ficheiroXml = "Dados/l201803C.xml";

        internal static List<Brand> FetchBrands()
        {
            bool perdicate(Brand x) => true;
            List<Brand> marcas = new List<Brand>(FetchCars(perdicate));
            var marcasDistincts = marcas.Distinct(new DataExtractorElementComparer());
            List<Brand> marcasRetorno = marcasDistincts.ToList();
            return marcasRetorno; 
        }

        internal static List<Model> FetchModels(Brand marca)
        {
            bool perdicate(Car x) => x.BrandDescription == marca.BrandDescription;
            List<Model> modelos = new List<Model>(FetchCars(perdicate));
            return modelos.Distinct().ToList();
        }

        internal static List<Version> FetchVersions(Model modelo)
        {
            bool perdicate(Car x) => x.BrandDescription == modelo.BrandDescription &&
                                                 x.ModelDescription == modelo.ModelDescription;
            List<Version> versoes = FetchCars(perdicate).Cast<Version>().ToList();
            return versoes;
        }

        internal static Car FetchCar(Version versao)
        {
            bool perdicate(Car x) => x.BrandDescription == versao.BrandDescription &&
                                                 x.ModelDescription == versao.ModelDescription &&
                                                 x.VersionDescription == versao.VersionDescription;
            List<Car> viaturas = FetchCars(perdicate);
            return viaturas.FirstOrDefault();
        }

        internal static List<Car> FetchCars(Func<Car,bool> perdicate)
        {
            System.Xml.Linq.XElement eurotax = System.Xml.Linq.XElement.Load(ficheiroXml);
            List<Car> viaturas = eurotax.Elements("NewsItemRow").Select( x =>
                                    new Car { BrandDescription = (string)x.Element("BRAND"),
                                                  ModelDescription = (string)x.Element("MODEL"),
                                                  VersionDescription = (string)x.Element("VERSION"),
                                                  Price = Convert.ToDouble((string)x.Element("PRICE"), CultureInfo.CurrentCulture)
                                    }).Where(perdicate).ToList();
            return viaturas;
        }
    }

    class DataExtractorElementComparer : IEqualityComparer<Brand>
    {

        public bool Equals(Brand x, Brand y)
        {
            if ( x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            return x.BrandDescription == y.BrandDescription;
        }

        public int GetHashCode(Brand obj)
        {
            return obj == null ? 0 : obj.BrandDescription.GetHashCode();
        }
    }
}
