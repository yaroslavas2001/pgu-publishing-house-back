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

	public BaseResponse(string stackTrace, string errorMessage = "")
	{
		StackTrace = stackTrace;
		ErrorMessage = errorMessage;
	}

	public BaseResponse(Exception ex, string errorMessage = "")
	{
		ErrorMessage = $"Custom message: {errorMessage}\n Exception: {ex.Message}";

		StackTrace = ex.StackTrace ?? "";
	}

	public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);

	public string ErrorMessage { get; set; }

	public string StackTrace { get; set; }
}