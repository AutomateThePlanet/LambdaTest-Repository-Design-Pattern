using System.Collections.Generic;

namespace RepositoryDesignPatternTests.Models;
public class Playlist
{
    public Playlist() => PlaylistTrack = new HashSet<PlaylistTrack>();

    public long PlaylistId { get; set; }
    public string Name { get; set; }

    public ICollection<PlaylistTrack> PlaylistTrack { get; set; }
}
