using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Client.Songs;

public class SongResponse
{
    public Guid SongId { get; set; }
    public required string Genre { get; set; }
    public required List<string> Tags { get; set; }
    public DateTime Created { get; set; }
    public DateTime ModifiedAt { get; set; }
}
