namespace MusicStore.Infrastructure.Features.Songs.Models;

public class Song
{
    public Guid SongId { get; set; }
    public required string Genre { get; set; }
    public required List<string> Tags { get; set; }
    public DateTime Created { get; set; }
    public DateTime ModifiedAt { get; set; }
}
