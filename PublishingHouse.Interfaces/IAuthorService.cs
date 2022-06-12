using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Author;

namespace PublishingHouse.Interfaces;

public interface IAuthorService
{
	Task<BaseResponse<AuthorShortResponse>> Add(AuthorAddRequest request);
	Task<BaseResponse<List<AuthorShortResponse>>> Get(AuthorGetRequest request);
	Task<BaseResponse> Update(AuthorUpdateRequest request);
}

public class AuthorUpdateRequest
{
}