﻿using PublishingHouse.Interfaces.Model.Files;

namespace PublishingHouse.Interfaces;

public interface IFileService
{
	Task<string> AddFileAsync(AddFileModel? model);
}