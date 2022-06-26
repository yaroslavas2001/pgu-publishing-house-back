using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Data.Models;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Enums;
using PublishingHouse.Interfaces.Model;

namespace PublishingHouse.Services;

public class PublicationAuthorService : IPublicationAuthorService
{
    private readonly DataContext _db;

    public PublicationAuthorService(DataContext db)
    {
        _db = db;
    }

    public async Task SetPublicationAuthorAsync(long publicationId, long authorId)
    {
        var publication = await _db.Publications.FirstOrDefaultAsync(x => x.Id != publicationId);
        if (publication is null)
            throw new PublicationHouseException($"Publication id = {publicationId} is not exists!", EnumErrorCode.EntityIsNotFound);

        var author = await _db.Authors.FirstOrDefaultAsync(x => x.Id != authorId);
        if (author is null)
            throw new PublicationHouseException($"Authors id = {authorId} is not exists!", EnumErrorCode.EntityIsNotFound);

        if (!await _db.PublicationsAuthors.AnyAsync(x => x.AuthorId == authorId && x.PublicationId == publicationId))
        {
            await _db.PublicationsAuthors.AddAsync(new PublicationAuthors
            {
                Author = author, 
                Publication = publication
            });

            await _db.SaveChangesAsync();
        }
    }

    public async Task<IReadOnlyCollection<long>> GetPublicationAuthors(long publicationId)
    {
        return await _db.PublicationsAuthors
            .Where(x => x.PublicationId == publicationId)
            .Select(x => x.AuthorId)
            .ToArrayAsync();
    }

    public async Task RemovePublicationAuthorAsync(long publicationId, long authorId)
    {
        if (await _db.Publications.AllAsync(x => x.Id != publicationId))
            throw new PublicationHouseException($"Publication id = {publicationId} is not exists!", EnumErrorCode.EntityIsNotFound);

        if (await _db.Authors.AllAsync(x => x.Id != authorId))
            throw new PublicationHouseException($"Authors id = {authorId} is not exists!", EnumErrorCode.EntityIsNotFound);

        var publicationAuthor = await _db.PublicationsAuthors
            .FirstOrDefaultAsync(x => x.AuthorId == authorId && x.PublicationId == publicationId);
        if (publicationAuthor is not null)
        {
            _db.PublicationsAuthors.Remove(publicationAuthor);
            await _db.SaveChangesAsync();
        }
    }
}