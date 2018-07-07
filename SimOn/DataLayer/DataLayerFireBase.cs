using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimOn
{
    /// <summary>
    /// Fetch vehicle information from a firebase database
    /// </summary>
    class DataLayerFireBase
    {
        private const string firebaseLink = "https://simon-4288d.firebaseio.com";

        internal static List<Brand> FetchBrands()
        {

            FirebaseResponse getResponse = GetResponse("Marcas");
            Console.WriteLine(getResponse.Success);
            if (getResponse.Success == false)
            {
                return null;
            }

            string resposta = getResponse.JSONContent;

            List<Brand> marcas = JsonConvert.DeserializeObject<List<Brand>>(resposta);
            marcas.Remove(null);

            return marcas.ToList();
        }

        internal static List<Model> FetchModels(Brand marca)
        {
            FirebaseResponse getResponse = GetResponse("Modelos");
            Console.WriteLine(getResponse.Success);
            if (getResponse.Success == false)
            {
                return null;
            }

            string resposta = getResponse.JSONContent;

            List<Model> todosModelos = JsonConvert.DeserializeObject<List<Model>>(resposta);
            todosModelos.Remove(null);

            List<Model> modelosMarca = todosModelos.Where(x => x.Brand == marca.BrandId).ToList();
            return modelosMarca.ToList();
        }

        internal static List<Version> FetchVersions(Model modelo)
        {
            FirebaseResponse getResponse = GetResponse("Versoes");
            Console.WriteLine(getResponse.Success);
            if (getResponse.Success == false)
            {
                return null;
            }

            string resposta = getResponse.JSONContent;

            List<Version> todasVersoes = JsonConvert.DeserializeObject<List<Version>>(resposta);
            todasVersoes.Remove(null);

            List<Version> versoesModelos = todasVersoes.Where(x => x.Model == modelo.ModelId).ToList();
            return versoesModelos.ToList();
        }

        internal static Car FetchCar(Version versao)
        {
            FirebaseResponse getResponse = GetResponse("Versoes");
            Console.WriteLine(getResponse.Success);
            if (getResponse.Success == false)
            {
                return null;
            }

            string resposta = getResponse.JSONContent;

            List<Car> viaturas = JsonConvert.DeserializeObject<List<Car>>(resposta);
            viaturas.Remove(null);

            Car viatura = viaturas.Where(x => x.VersionId == versao.VersionId).ToList().FirstOrDefault();
            return viatura;
        }

        internal static FirebaseResponse GetResponse(string node)
        {
            FirebaseDB firebaseDB = new FirebaseDB(firebaseLink);
            FirebaseDB firebaseMarcas = firebaseDB.Node(node);
            FirebaseResponse getResponse = firebaseMarcas.Get();

            return getResponse;
        }
    }
}
