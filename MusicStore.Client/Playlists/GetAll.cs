using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Client;

public class GetAllPlaylistsResponse
{
    public long Count { get; set; }
    public List<PlaylistResponse> Playlists { get; set; } = new();
}
