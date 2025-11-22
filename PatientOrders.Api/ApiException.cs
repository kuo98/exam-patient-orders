namespace PatientOrders.Api;

public class ApiException : Exception
{
    private const string DefaultDisplayMessage = "Error";

    public ApiException(Exception e) : base(e.Message, e)
    {
    }

    protected ApiException()
    {
    }

    protected ApiException(string developerMessage) : base(developerMessage)
    {
    }

    public int HttpStatusCode { get; protected init; } = 500;
    public string DisplayMessage { get; protected init; } = DefaultDisplayMessage;
    public string Code { get; protected init; } = "InternalServerError";
    public object? Details { get; protected init; }
}

public class BadRequestException : ApiException
{
    public BadRequestException(string? displayMessage = null, object? details = null)
    {
        Code = "BadRequest";
        HttpStatusCode = 400;
        DisplayMessage = displayMessage ?? "Bad request";
        Details = details;
    }
}
