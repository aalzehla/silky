namespace ITestApplication.Test.Dtos;

public class ResponseBase
{
    public ResponseBase(int code = 520, string message = "Unknown exception")
    {
        Code = code;

        Message = message;
    }

    /// <summary>
    /// status code
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// return description
    /// </summary>
    public string Message { get; set; }
}