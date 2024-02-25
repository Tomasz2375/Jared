using Jared.Domain.Enums;

namespace Jared.Domain.Abstractions;

public class Response<T>
{
    public const int SUCCESS_CODE = 200;
    public const int GENERAL_ERROR_CODE = 500;

    public Response()
        : this(null, SUCCESS_CODE, default)
    {
    }

    public Response(T data)
        : this(null, SUCCESS_CODE, data)
    {
    }

    public Response(ErrorModel error, T? data = default)
        : this(error, GENERAL_ERROR_CODE, data)
    {
    }

    public Response(ErrorCode code, ErrorModel? error = null)
        : this(error, (int)code, default)
    {
    }

    private Response(ErrorModel? error, int code, T? data)
    {
        Error = error;
        Code = code;
        Data = data;
    }

    public ErrorModel? Error { get; set; }
    public int Code { get; set; }
    public T? Data { get; set; }
}
