using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Client;

public class AddPlaylistRequest
{
    public required string Name { get; set; }
    public required List<Guid> SongIds { get; set; }
    public required List<string> Occasions { get; set; }
}

public class AddPlaylistResponse
{
    public Guid PlaylistId { get; set; }
}
