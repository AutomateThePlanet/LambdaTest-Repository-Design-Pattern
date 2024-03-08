using System.Collections.Generic;

namespace RepositoryDesignPatternTests.Models;
public class Genre
{
    public Genre() => Tracks = new HashSet<Track>();

    public long GenreId { get; set; }
    public string Name { get; set; }

    public ICollection<Track> Tracks { get; set; }
}
