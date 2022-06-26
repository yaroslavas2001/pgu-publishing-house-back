﻿using PublishingHouse.Interfaces.Extensions.Pagination;

namespace PublishingHouse.Interfaces.Model.Author;

public class AuthorGetModel : IPaginationRequest
{
	public string Search { get; set; } = string.Empty;

	public Page Page { get; set; } = new Page();
}