using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Exstensions;
using PublishingHouse.Interfaces.Model.Files;

namespace PublishingHouse.Services.Infrastruct;

public class FileService : IFileService
{
	private const string BaseDir = "/Files";
	private readonly DataContext _db;

	public FileService(DataContext db)
	{
		_db = db;
		if (BaseDir.IsValidPath() && !Directory.Exists(BaseDir))
			Directory.CreateDirectory(BaseDir);
	}

	public async Task<string> AddFileAsync(AddFileModel? model)
	{
		if (model?.Path == null || model.Path.IsValidPath())
			throw new FileLoadException($"Path {model?.Path} is not valid!");

		var path = model.Path.ConvertToServerPath(BaseDir);

		if (File.Exists(path))
			throw new Exception($"File {path} is already exists");

		var fileBytes = Convert.FromBase64String(model.FileBase64);
		await File.WriteAllBytesAsync(path, fileBytes);

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

	public async Task<IReadOnlyCollection<PublicationFileModel>> GetPublicationFilesAsync(long publicationId, bool isReviewer)
	{
		if (await _db.Publications.AllAsync(x => x.Id != publicationId))
			throw new Exception($"Publication id = {publicationId} is not exists!");

		var query = _db.Files.AsQueryable();
		if (isReviewer)
			query = query.Where(x => x.IsVisibleForReviewers == true);

		var files = await query
			.Where(x => x.PublicationId == publicationId && x.IsVisibleForReviewers)
			.Select(x => new PublicationFileModel
			{
				Name =  x.Name,
				Type =  x.Type,
				Url=x.Url,
				ReviewId =x.ReviewId
			}).ToArrayAsync();

		return files;
	}
}