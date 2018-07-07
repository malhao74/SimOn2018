using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SimOn
{
    /// <summary>
    /// Methods to help the FireBaseRequest class
    /// </summary>
    internal static class FireBaseRequestHelper
    {
        private const string USER_AGENT = "firebase-net/1.0";

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
                Console.WriteLine(ex.Message);
                throw new System.Net.WebException("Problems with the JSON content.");
            }
        }

        public static Task<HttpResponseMessage> RequestHelper(HttpMethod method, Uri uri, string json = null)
        {
            HttpClient cliente = null;
            HttpRequestMessage msg = null;
            Task<HttpResponseMessage> httpResponseMessage = null;
            try
            {
                cliente = new HttpClient();
                msg = new HttpRequestMessage(method, uri);

                msg.Headers.Add("user-agent", USER_AGENT);
                if (json != null)
                {
                    msg.Content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                }
                httpResponseMessage = cliente.SendAsync(msg);
                httpResponseMessage.Wait();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new System.Net.WebException("Problems with the HTTP request.");
            }
            finally
            {
                if (cliente != null)
                {
                    cliente.Dispose();
                }
                if (msg != null)
                {
                    msg.Dispose();
                }
            }
            return httpResponseMessage;
        }
    }
}
