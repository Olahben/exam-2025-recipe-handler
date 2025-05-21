using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Client.Songs;

public class AddSongResponse
{
    public Guid SongId { get; set; }
}

public class AddSongRequest
{
    public required string Genre { get; set; }
    public required List<string> Tags { get; set; }
}
