using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimOn
{
    class DataLayerFireBase
    {
        #region Definicao de variaveis
        private static readonly string firebaseLink = "https://simon-4288d.firebaseio.com";
        #endregion

        #region Metodos internos
        internal static List<Marca> GetMarcas()
        {

            FirebaseResponse getResponse = GetResponse("Marcas");
            Console.WriteLine(getResponse.Success);
            if (getResponse.Success == false)
            {
                return null;
            }

            string resposta = getResponse.JSONContent;

            List<Marca> marcas = JsonConvert.DeserializeObject<List<Marca>>(resposta);
            marcas.Remove(null);

            return marcas.ToList();
        }
        internal static List<MarcaModelo> GetModelos(Marca marca)
        {
            FirebaseResponse getResponse = GetResponse("Modelos");
            Console.WriteLine(getResponse.Success);
            if (getResponse.Success == false)
            {
                return null;
            }

            string resposta = getResponse.JSONContent;

            List<MarcaModelo> todosModelos = JsonConvert.DeserializeObject<List<MarcaModelo>>(resposta);
            todosModelos.Remove(null);

            List<MarcaModelo> modelosMarca = todosModelos.Where(x => x.Marca == marca.IdMarca).ToList();
            return modelosMarca.ToList();
        }
        internal static List<MarcaModeloVersao> GetVersoes(MarcaModelo modelo)
        {
            FirebaseResponse getResponse = GetResponse("Versoes");
            Console.WriteLine(getResponse.Success);
            if (getResponse.Success == false)
            {
                return null;
            }

            string resposta = getResponse.JSONContent;

            List<MarcaModeloVersao> todasVersoes = JsonConvert.DeserializeObject<List<MarcaModeloVersao>>(resposta);
            todasVersoes.Remove(null);

            List<MarcaModeloVersao> versoesModelos = todasVersoes.Where(x => x.Modelo == modelo.IdModelo).ToList();
            return versoesModelos.ToList();
        }
        internal static Viatura GetViatura(MarcaModeloVersao versao)
        {
            FirebaseResponse getResponse = GetResponse("Versoes");
            Console.WriteLine(getResponse.Success);
            if (getResponse.Success == false)
            {
                return null;
            }

            string resposta = getResponse.JSONContent;

            List<Viatura> viaturas = JsonConvert.DeserializeObject<List<Viatura>>(resposta);
            viaturas.Remove(null);

            Viatura viatura = viaturas.Where(x => x.IdVersao == versao.IdVersao).ToList().FirstOrDefault();
            return viatura;
        }
        internal static FirebaseResponse GetResponse(string node)
        {
            FirebaseDB firebaseDB = new FirebaseDB(firebaseLink);
            FirebaseDB firebaseMarcas = firebaseDB.Node(node);
            FirebaseResponse getResponse = firebaseMarcas.Get();

            return getResponse;
        }
        #endregion

    }
}
