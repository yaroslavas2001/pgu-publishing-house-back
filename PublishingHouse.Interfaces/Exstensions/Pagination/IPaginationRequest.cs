﻿namespace PublishingHouse.Interfaces.Exstensions.Pagination;

/// <summary>
///     Интерфейс запроса постранички
/// </summary>
public interface IPaginationRequest
{
	Page Page { get; set; }
}