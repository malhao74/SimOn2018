using System.Net.Http;

namespace SimOn
{
    class FirebaseResponse
    {
        public bool Success;
        public string JSONContent;
        public string ErrorMessage;
        public HttpResponseMessage HttpResponse;

        public FirebaseResponse()
        {

        }

        public FirebaseResponse(bool success, string errorMessage, HttpResponseMessage httpResponse = null, string jsonContent = null)
        {
            this.Success = success;
            this.ErrorMessage = errorMessage;
            this.HttpResponse = httpResponse;
            this.JSONContent = jsonContent;
        }
    }
}
