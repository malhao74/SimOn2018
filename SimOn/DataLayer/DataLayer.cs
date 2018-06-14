using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimOn
{
    /// <summary>
    /// Provides a layer between the bussiness logic and data location/interface.
    /// </summary>
    class DataLayer
    {
        // TODO: Implementar propriedade para poder mudar entre excel, xml e firebase.
        // TODO: Testar implementar o DataLayer do firebase.
        // TODO: Alimentar a base de dados do firebase. 

        #region Metodos publicos
        public static List<Marca> GetMarcas()
        {
            return DataLayerExcel.GetMarcasExcel();
        }


        public static List<MarcaModelo> GetModelos (Marca marca)
        {
            return DataLayerExcel.GetModelosExcel(marca);
        }


        public static List<MarcaModeloVersao> GetVersoes(MarcaModelo modelo)
        {
            return DataLayerExcel.GetVersoesExcel(modelo);
        }


        public static Viatura GetViatura(MarcaModeloVersao versao)
        {
            return DataLayerExcel.GetViaturaExcel(versao);
        }

        #endregion
    }
}
