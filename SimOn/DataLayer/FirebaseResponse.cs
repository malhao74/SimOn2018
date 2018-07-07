using System.Net.Http;

namespace SimOn
{
    /// <summary>
    /// Class to handle firebase responses
    /// </summary>
    internal class FirebaseResponse
    {
        #region Properties
        public bool Success { get; set; }
        public string JSONContent { get; set; }
        public string ErrorMessage { get; set; }
        public HttpResponseMessage HttpResponse { get; set; }
        #endregion

        public FirebaseResponse()
        { }

        public FirebaseResponse(bool success, string errorMessage, HttpResponseMessage httpResponse = null, string jsonContent = null)
        {
            this.Success = success;
            this.ErrorMessage = errorMessage;
            this.HttpResponse = httpResponse;
            this.JSONContent = jsonContent;
        }
    }
}
