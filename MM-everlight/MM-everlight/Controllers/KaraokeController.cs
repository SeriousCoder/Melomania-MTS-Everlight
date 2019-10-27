using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using MM_everlight.Models;

namespace MM_everlight.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/Karaoke")]
    [ApiController]
    public class MusicController : ControllerBase
    {
       // GET: api/Music
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("res")]
        public async Task<IActionResult> DownloadRes()
        {
            string filename = "Nirvana Smells Like Teen Spirit Inst-your-track.mp3";
            //string filename = "me.jpg";

            if (!System.IO.File.Exists(filename))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var fileBytes = System.IO.File.ReadAllBytes(filename);

            var fileMemStream = new MemoryStream(fileBytes);


            return File(fileMemStream, "application/octet-stream", filename);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("trackInst")]
        public async Task<IActionResult> GetTrack()
        {
            string filename = "Nirvana Smells Like Teen Spirit Inst.mp3";
            //string filename = "me.jpg";

            if (!System.IO.File.Exists(filename))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var fileBytes = System.IO.File.ReadAllBytes(filename);

            var fileMemStream = new MemoryStream(fileBytes);


            return File(fileMemStream, "application/octet-stream", filename);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("text")]
        public async Task<IActionResult> GetText()
        {
            string filename = "lyric.txt";
            //string filename = "me.jpg";

            if (!System.IO.File.Exists(filename))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var fileBytes = System.IO.File.ReadAllBytes(filename);

            var fileMemStream = new MemoryStream(fileBytes);


            return File(fileMemStream, "application/octet-stream", filename);
        }

        //// GET: api/Music/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Karaoke
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public HttpResponseMessage MakeCoolTrack([FromForm] KaraokeElems value)
        {
            string name = value.TrackName;
            // Getting Image
            var image = value.VoiceFile;

            try
            {
                // Saving Image on Server
                if (image.Length > 0)
                {
                    using (var fileStream = new FileStream(image.FileName.Substring(0, image.FileName.Length - 4) + "-your-track.mp3", FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        //// PUT: api/Music/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
