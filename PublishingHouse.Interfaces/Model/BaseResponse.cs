namespace PublishingHouse.Interfaces.Model;

public class BaseResponse<T> : BaseResponse
{
	public BaseResponse(T data)
	{
		Data = data;
	}

	public BaseResponse(Exception ex) : base(ex)
	{
	}

	public T Data { get; set; }
}

public class BaseResponse
{
	public BaseResponse()
	{
	}

	public BaseResponse(string errorMessage, string stackTrace = "")
	{
		ErrorMessage = errorMessage;
		StackTrace = stackTrace;
	}

	public BaseResponse(Exception ex)
	{
		ErrorMessage = ex.Message;
		StackTrace = ex.StackTrace ?? "";
	}

	public bool IsSuccess => string.IsNullOrWhiteSpace(ErrorMessage);

	public string ErrorMessage { get; set; } = null!;

	public string StackTrace { get; set; } = null!;
}