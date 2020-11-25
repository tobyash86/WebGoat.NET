using Newtonsoft.Json;

public class ApiResponse
{
    public int StatusCode { get; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; }

    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    private static string GetDefaultMessageForStatusCode(int statusCode)
    {
        switch (statusCode)
        {
            case 400:
                return "Bad Request.";
            case 401:
                return "Unauthorized: You do not have sufficient permissions to see this page.";
            case 403:
                return "Forbidden: Required permissions to view this page are not met.";
            case 404:
                return "Resource not found.";
            case 408:
                return "Request Timeout: Please retry accessing the resource later.";
            case 410:
                return "Gone: Resource has been removed and will not be available.";
            case int i when i >= 400 && i <= 499:
                return "Client Error: Please refer to the specified HTTP Status Code for more details.";
            case 500:
                return "Internal Server Error: An unhandled error occurred.";
            case 501:
                return "Not Implemented: This feature will be implemented soon.";
            case int i when i >= 500 && i <= 599:
                return "Server Error: Please refer to the specified HTTP Status Code for more details.";
            default:
                return null;
        }
    }
}