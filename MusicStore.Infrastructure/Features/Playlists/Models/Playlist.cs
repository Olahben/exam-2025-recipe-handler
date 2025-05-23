using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Infrastructure.Features.Playlists.Models;

public class Playlist
{
    public required Guid PlaylistId { get; set; }
    public required string Name { get; set; }
    public List<Guid> SongIds { get; set; } = new();
    public required List<string> Occasions { get; set; }
    public DateTime Created { get; set; }
    public DateTime ModifiedAt { get; set; }
}
