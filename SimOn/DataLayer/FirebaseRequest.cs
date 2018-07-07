using System;
using System.Net.Http;
using System.Globalization;

namespace SimOn
{
    /// <summary>
    /// Class to handle firebase requests
    /// </summary>
    class FirebaseRequest
    {
        #region Fields
        private const string JSON_SUFFIX = ".json";

        private readonly HttpMethod Method;
        private readonly string JSON;
        private readonly string Uri;
        #endregion

        public FirebaseRequest(HttpMethod method, string uri, string jsonString = null)
        {
            this.Method = method;
            this.JSON = jsonString;

            if (uri.Replace("/", string.Empty).EndsWith("firebaseio.com",StringComparison.Ordinal))
            {
                this.Uri = uri + "/" + JSON_SUFFIX;
            }
            else
            {
                this.Uri = uri + JSON_SUFFIX;
            }
        }

        public FirebaseResponse Execute()
        {
            Uri requestURI;
            if (FireBaseRequestHelper.ValidadeURI(this.Uri))
            { requestURI = new Uri(this.Uri); }
            else
            { return new FirebaseResponse(false, "Proided Firebase path is not valid HTTP/S URL."); }

            string json = null;
            if (this.JSON != null)
            {
                if(!FireBaseRequestHelper.TryParseJSON(this.JSON, out json))
                { return new FirebaseResponse(false, string.Format(CultureInfo.CurrentCulture,"Invalid JSON: {0}", json)); }

            }

            var response = FireBaseRequestHelper.RequestHelper(this.Method, requestURI, json);
            var result = response.Result;
            var firebaseResponse = new FirebaseResponse()
            {
                HttpResponse = result,
                ErrorMessage = result.StatusCode.ToString() + " : " + result.ReasonPhrase,
                Success = response.Result.IsSuccessStatusCode
            };

            if (this.Method.Equals(HttpMethod.Get))
            {
                var content = result.Content.ReadAsStringAsync();
                content.Wait();
                firebaseResponse.JSONContent = content.Result;
            }

            return firebaseResponse;
        }
    }
}
