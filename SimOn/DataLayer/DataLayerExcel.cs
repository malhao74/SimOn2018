using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;

namespace SimOn
{
    /// <summary>
    /// Fetch vehicle information from a excel file
    /// </summary>
    internal static class DataLayerExcel
    {
        private static readonly string excelSpreadSheet = AppContext.BaseDirectory.ToString() + @"Dados\L201803C.xls";
        private const string workSheet = "LIGEIROS";

        /// <summary>
        /// Fetch brands available in the excel file
        /// </summary>
        /// <returns></returns>
        internal static List<Brand> FetchBrands()
        {
            ExcelQueryFactory excel = null;
            List<Brand> marcas = null;
            try
            {
                excel = new ExcelQueryFactory(excelSpreadSheet);
                marcas = new List<Brand>(from c in excel.Worksheet<Brand>(workSheet)
                                 select c).ToList();
                marcas = new List<Brand>(marcas.Distinct());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new System.IO.IOException("Problems reading the excel file.");
            }
            finally
            {
                if (excel != null)
                {
                    excel.Dispose();
                }
            }
            return marcas;
        }

        /// <summary>
        /// Fetch models available on the excel file
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        internal static List<Model> FetchModels(Brand marca)
        {
            ExcelQueryFactory excel = null;
            List<Model> modelos = null;
            try
            {
                excel = new ExcelQueryFactory(excelSpreadSheet);
                modelos = new List<Model>( from c in excel.Worksheet<Model>(workSheet)
                                  where c.BrandDescription == marca.BrandDescription.Trim()
                                  select c).ToList();
                modelos = new List<Model>(modelos.Distinct());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new System.IO.IOException("Problems reading the excel file.");
            }
            finally
            {
                if (excel != null)
                {
                    excel.Dispose();
                }
            }
            return modelos;
        }

        /// <summary>
        /// Fetch versions available for a specific model
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        internal static List<Version> FetchVersions(Model modelo)
        {
            ExcelQueryFactory excel = null;
            List<Version> versoes = null;
            try
            {
                excel = new ExcelQueryFactory(excelSpreadSheet);
                versoes = new List<Version>(from c in excel.Worksheet<Version>(workSheet)
                                                where c.BrandDescription == modelo.BrandDescription.Trim() && c.ModelDescription == modelo.ModelDescription.Trim()
                                                select c).ToList();
                versoes = new List<Version>(versoes.Distinct());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new System.IO.IOException("Problems reading the excel file.");
            }
            finally
            {
                if (excel != null)
                {
                    excel.Dispose();
                }
            }
            return versoes;
        }

        /// <summary>
        /// Fetch vehicle information based on a specific version
        /// </summary>
        /// <param name="versao"></param>
        /// <returns></returns>
        internal static Car FetchCar(Version versao)
        {
            ExcelQueryFactory excel = null;
            Car viatura = null;
            try
            {
                excel = new ExcelQueryFactory(excelSpreadSheet);
                viatura = (Car)((from c in excel.Worksheet<Car>(workSheet)
                                                      where c.BrandDescription == versao.BrandDescription.Trim() && c.ModelDescription == versao.ModelDescription.Trim() 
                                                      && c.VersionDescription == versao.VersionDescription.Trim()
                                                      select c).FirstOrDefault());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new System.IO.IOException("Problems reading the excel file.");
            }
            finally
            {
                if (excel != null)
                {
                    excel.Dispose();
                }
            }
            return viatura;
        }
    }
}
