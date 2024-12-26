namespace ConnectSphere.Application.Common.Models.General;

public class ResponseDto<T>
{
    public string Message { get; set; } = string.Empty;
    public T Data { get; set; }
    public bool Success { get; set; } = true;
    public List<string> Errors { get; set; } = [];

    public ResponseDto()
    {

    }

    public ResponseDto(T data)
    {
        Data = data;
    }

    public ResponseDto(string message, bool success)
    {
        Message = message;
        Success = success;
    }

    public ResponseDto(T data, string message) : this(data)
    {
        Message = message;
    }


    public ResponseDto(string message, List<string> errors)
    {
        Message = message;
        Errors = errors;
        Success = false;
    }

    public ResponseDto(T data, string message, List<string> errors) : this(message, errors)
    {
        Data = data;
        Success = false;
    }

    public ResponseDto(T data, string message, bool success, List<string> errors) : this(data, message, errors)
    {
        Success = success;

    }
}