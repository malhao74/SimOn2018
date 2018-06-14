using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SimOn
{
    class UtilityHelper
    {
        private const string USER_AGENT = "firebase-net/1.0";

        #region Metodos publicos
        public static bool ValidadeURI(string url)
        {

            if (System.Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri locUrl) == false)
            { return false; }
            if (!(locUrl.IsAbsoluteUri && (locUrl.Scheme == "http"|| locUrl.Scheme == "https")) || !locUrl.IsAbsoluteUri)
            { return false; }
            return true;
        }

        public static bool TryParseJSON(string inJSON, out string output)
        {
            try
            {
                JToken parsedJSON = JToken.Parse(inJSON);
                output = parsedJSON.ToString();
                return true;
            }
            catch (Exception ex)
            {
                output = ex.Message;
                return false;
            }
        }

        public static Task<HttpResponseMessage> RequestHelper(HttpMethod method, Uri uri, string json = null)
        {
            var cliente = new HttpClient();
            var msg = new HttpRequestMessage(method, uri);
            msg.Headers.Add("user-agent", USER_AGENT);
            if (json != null)
            {
                msg.Content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            }
            return cliente.SendAsync(msg);
        }
        #endregion
    }
}
