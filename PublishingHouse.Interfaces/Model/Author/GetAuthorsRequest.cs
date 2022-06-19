﻿using PublishingHouse.Interfaces.Exstensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Author;

public class GetAuthorsRequest:IPaginationRequest
{
	public Page Page { get; set; }
	
	public long? AuthorId { get; set; } = null;
}