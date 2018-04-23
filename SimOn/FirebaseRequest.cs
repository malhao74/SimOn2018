using System;
using System.Net.Http;

namespace SimOn
{
    class FirebaseRequest
    {
        private const string JSON_SUFFIX = ".json";

        private HttpMethod Method;
        private string JSON;
        private string Uri;

        public FirebaseRequest(HttpMethod method, string uri, string jsonString = null)
        {
            this.Method = method;
            this.JSON = jsonString;

            if (uri.Replace("/", string.Empty).EndsWith("firebaseio.com"))
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
            if (UtilityHelper.ValidadeURI(this.Uri))
            { requestURI = new Uri(this.Uri); }
            else
            { return new FirebaseResponse(false, "Proided Firebase path is not valid HTTP/S URL."); }

            string json = null;
            if (this.JSON != null)
            {
                if(!UtilityHelper.TryParseJSON(this.JSON, out json))
                { return new FirebaseResponse(false, string.Format("Invalid JSON: {0}", json)); }

            }

            var response = UtilityHelper.RequestHelper(this.Method, requestURI, json);
            response.Wait();
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
