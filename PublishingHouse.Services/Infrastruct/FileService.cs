using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Enums;
using PublishingHouse.Interfaces.Extensions;
using PublishingHouse.Interfaces.Model;
using PublishingHouse.Interfaces.Model.Files;

namespace PublishingHouse.Services.Infrastruct;

public class FileService : IFileService
{
	private const string BaseDir = "\\wwwroot\\files";
	private readonly DataContext _db;

	public FileService(DataContext db)
	{
		_db = db;
		if (BaseDir.IsValidPath() && !Directory.Exists(BaseDir))
			Directory.CreateDirectory(BaseDir);
	}

	public async Task<string> AddFileAsync(AddFileModel? model)
	{
		var filePath = Guid.NewGuid().ToString().Replace("-", "")[..8];
		var directory = filePath.ConvertToServerPath(BaseDir);
		if (!Directory.Exists(directory))
			Directory.CreateDirectory(directory);

		var path = Path.Combine(directory, model?.Name);

		if (File.Exists(path))
			throw new Exception($"File {path} is already exists");

		var fileBytes = Convert.FromBase64String(model.FileBase64);
		await File.WriteAllBytesAsync(path, fileBytes);
		try
		{
			var file = new Data.Models.File
			{
				IsVisibleForReviewers = model.IsVisibleForReviewers,
				Name = path,
				Type = model.FileType,
				PublicationId = model.PublicationId,
				Url = path.ConvertServerPathToUri()
			};

			await _db.Files.AddAsync(file);
			await _db.SaveChangesAsync();
			return file.Url;
		}
		catch (Exception)
		{
			if (File.Exists(path))
				File.Delete(path);
			throw;
		}
	}

	public async Task<IReadOnlyCollection<PublicationFileModel>> GetPublicationFilesAsync(long publicationId,
		bool isReviewer)
	{
		if (await _db.Publications.AllAsync(x => x.Id != publicationId))
			throw new PublicationHouseException($"Publication id = {publicationId} is not exists!", EnumErrorCode.EntityIsNotFound);

		var query = _db.Files.AsQueryable();
		if (isReviewer)
			query = query.Where(x => x.IsVisibleForReviewers == true);

		var files = await query
			.Where(x => x.PublicationId == publicationId && x.IsVisibleForReviewers)
			.Select(x => new PublicationFileModel
			{
				Name = x.Name,
				Type = x.Type,
				Url = x.Url,
				ReviewId = x.ReviewId
			}).ToArrayAsync();

		return files;
	}
}