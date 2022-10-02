namespace mono_financialbot_backend_cs_datalayer.Utils.Responses
{
   public class ResponseBase<T> where T : class
    {
        #region props to the shared response model across the server
        /// <summary>
        /// proprety associated to the response code of the request
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// property associated to the success of an request operation
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        ///  property associated wether the request was found or not.
        /// </summary>
        public bool IsNotFound { get; set; }
        /// <summary>
        /// property associated to a non-technical type message of the request.
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// body of the response regarding its class
        /// </summary>
        public T? Result { get; set; }

        #endregion
    }
}