using RepositoryDesignPatternTests.Models;

namespace RepositoryDesignPatternTests.Data.Repositories;
public class AlbumRepository : HttpRepository<Album, List<Album>>
{
    public AlbumRepository(string baseUrl)
        : base(baseUrl, "albums")
    {
    }

    // Add methods specific to albums if needed
}
