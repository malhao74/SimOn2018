using System.Net.Http;

namespace SimOn
{
    class FirebaseResponse
    {
        #region Declaracao de variaveis
        public bool Success;
        public string JSONContent;
        public string ErrorMessage;
        public HttpResponseMessage HttpResponse;
        #endregion

        #region Metodos publicos
        public FirebaseResponse()
        { }

        public FirebaseResponse(bool success, string errorMessage, HttpResponseMessage httpResponse = null, string jsonContent = null)
        {
            this.Success = success;
            this.ErrorMessage = errorMessage;
            this.HttpResponse = httpResponse;
            this.JSONContent = jsonContent;
        }
        #endregion
    }
}
