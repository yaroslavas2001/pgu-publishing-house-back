using PublishingHouse.Interfaces.Model;

namespace PublishingHouse.Services.Extensions;

public static class QueryableExtensions
{
	public static IQueryable<T> Page<T>(this IQueryable<T> query, PaginationRequest request)
	{
		if (request.Skip is > 0) 
			query = query.Skip(request.Skip.Value);

		if (request.Take is > 0)
			query = query.Take(request.Take.Value);

		return query;
	}
}