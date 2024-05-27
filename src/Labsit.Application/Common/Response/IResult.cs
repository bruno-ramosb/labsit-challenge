using System.Net;

namespace Labsit.Application.Common.Response
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
        List<string> Notifications { get; }    
        HttpStatusCode StatusCode { get; }
    }
    public interface IResult<out T> : IResult
    {
        T Data { get; }
    }
}
