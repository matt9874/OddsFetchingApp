using ApiClients.JSON;

namespace ApiClients.JsonClientHelpers
{
    internal static class ExceptionReconstituter
    {
        internal static System.Exception ReconstituteException(ApiClients.Exception ex)
        {
            var data = ex.Data;

            // API-NG exception -- it must have "data" element to tell us which exception
            var exceptionName = data.Property("exceptionname").Value.ToString();
            var exceptionData = data.Property(exceptionName).Value.ToString();
            return JsonConvert.Deserialize<ApiNgException>(exceptionData);
        }
    }
}
