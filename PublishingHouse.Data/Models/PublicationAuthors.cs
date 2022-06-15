﻿namespace PublishingHouse.Data.Models;

public class PublicationAuthors
{
	public long PublicationId { get; set; }

	public long AuthorId { get; set; }

	public Publication Publication { get; set; }

	public Author Author { get; set; }
}