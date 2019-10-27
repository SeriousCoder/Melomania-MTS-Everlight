using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Core;
using CSCore.Codecs;
using CSCore.MediaFoundation;
using CSCore.Streams;
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
        [Microsoft.AspNetCore.Mvc.Route("GetYouTrack")]
        public async Task<IActionResult> DownloadRes()
        {
            string filename = "Yours/Nirvana Smells Like Teen Spirit Inst-your-track.mp3";
            string filepath = "Assets/";

            if (!System.IO.File.Exists(filename))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (!System.IO.File.Exists(filepath + filename))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var fileBytes = System.IO.File.ReadAllBytes(filepath + filename);

            var fileMemStream = new MemoryStream(fileBytes);

            return File(fileMemStream, "application/octet-stream", filename);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("GetInst")]
        public async Task<IActionResult> GetTrack()
        {
            string filename = "Nirvana Smells Like Teen Spirit Inst.mp3";
            string filepath = "Assets/";

            if (!System.IO.File.Exists(filepath + filename))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var fileBytes = System.IO.File.ReadAllBytes(filepath + filename);

            var fileMemStream = new MemoryStream(fileBytes);


            return File(fileMemStream, "application/octet-stream", filename);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("GetLyric")]
        public async Task<IActionResult> GetText()
        {
            string filename = "lyric.txt";
            string filepath = "Assets/";

            if (!System.IO.File.Exists(filepath + filename))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var fileBytes = System.IO.File.ReadAllBytes(filepath + filename);

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
                var voiceFile = "Assets/Yours/" + image.FileName.Substring(0, image.FileName.Length - 4) +
                                "-your-voice.mp3";
                var instFile = "Assets/Nirvana Smells Like Teen Spirit Inst.mp3";

                var trackFile = "Assets/Yours/" + image.FileName.Substring(0, image.FileName.Length - 4) +
                                "-your-track.mp3";

                // Saving Image on Server
                if (image.Length > 0)
                {
                    using (var fileStream = new FileStream(voiceFile, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                }

                var voiceWaveSource = CodecFactory.Instance.GetCodec(voiceFile);
                var instWaveSource = CodecFactory.Instance.GetCodec(instFile);

                using (var cAurio = new AudioControler())
                {
                    VolumeSource vol1, vol2;
                    var yourTrack = cAurio.MixAudioAndVoice(instWaveSource, out vol1, voiceWaveSource, out vol2);

                    vol1.Volume = 0.5f;
                    vol2.Volume = 0.5f;

                    using (var encoder = MediaFoundationEncoder.CreateMP3Encoder(yourTrack.WaveFormat, trackFile))
                    {
                        byte[] buffer = new byte[yourTrack.WaveFormat.BytesPerSecond];
                        int read;
                        while ((read = yourTrack.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            encoder.Write(buffer, 0, read);

                            //Console.CursorLeft = 0;
                            //Console.Write("{0:P}/{1:P}", (double) yourTrack.Position / yourTrack.Length, 1);
                        }
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
