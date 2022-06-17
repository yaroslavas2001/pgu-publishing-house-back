using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Interfaces.Model;

namespace PublishingHouse.Controller;

[Route("/[controller]")]
[Produces("application/json")]
public class DocumentController : Microsoft.AspNetCore.Mvc.Controller
{
	[HttpPost]
	[Route($"{nameof(Add)}")]
	[ProducesResponseType(200, Type = typeof(BaseResponse))]
	[ProducesResponseType(400, Type = typeof(BaseResponse))]
	public async Task<BaseResponse> Add(IFormFile file)
	{
		try
		{
			return new BaseResponse();
		}
		catch (Exception e)
		{
			return new BaseResponse(e);
		}
	}
}