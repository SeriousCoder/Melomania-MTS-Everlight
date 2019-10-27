using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MM_everlight.Models
{
    public class KaraokeElems
    {
        public string TrackName { get; set; }
        public IFormFile VoiceFile { get; set; }

    }
}
