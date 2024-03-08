using RepositoryDesignPatternTests.Models;

namespace RepositoryDesignPatternTests.Data.Repositories;
public class TrackRepository : HttpRepository<Track, List<Track>>
{
    public TrackRepository(string baseUrl)
        : base(baseUrl, "tracks" )
    {
    }

    // Add methods specific to tracks if needed
}
