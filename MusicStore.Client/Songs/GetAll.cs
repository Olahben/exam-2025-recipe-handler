using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Client.Songs;

public class GetAllResponse
{
    public long Count { get; set; }
    public List<SongResponse> Songs { get; set; } = new();
}