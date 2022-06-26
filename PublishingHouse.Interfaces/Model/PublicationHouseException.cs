using PublishingHouse.Interfaces.Enums;
using PublishingHouse.Interfaces.Extensions;

namespace PublishingHouse.Interfaces.Model;

public class PublicationHouseException : Exception
{
	public PublicationHouseException(string? message = null, EnumErrorCode errorCode = EnumErrorCode.Unknown) : base(message ?? errorCode.GetDescription())
	{
		ErrorCode = errorCode;
	}

	public PublicationHouseException(EnumErrorCode errorCode) : base(errorCode.GetDescription())
	{
		ErrorCode = errorCode;
	}

	public EnumErrorCode ErrorCode { get; set; }
}