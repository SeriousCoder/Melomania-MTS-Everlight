using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSCore;
using CSCore.Codecs;
using Core;
using CSCore.SoundOut;
using CSCore.Streams;

namespace Tester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*Console.WriteLine(
                "Test mix two audio"
            );

            var CAudio = new AudioControler();

            IWaveSource audioWaveSource = null;

            do
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Title = "Select any file as audio",
                    Filter = CodecFactory.SupportedFilesFilterEn
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        audioWaveSource = CodecFactory.Instance.GetCodec(openFileDialog.FileName);
                    }
                    catch
                    {
                    }
                }

            } while (audioWaveSource == null);

            IWaveSource voiceWaveSource = null;

            do
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Title = "Select any file as audio",
                    Filter = CodecFactory.SupportedFilesFilterEn
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        voiceWaveSource = CodecFactory.Instance.GetCodec(openFileDialog.FileName);
                    }
                    catch
                    {
                    }
                }

            } while (voiceWaveSource == null);

            VolumeSource volumeSource1, volumeSource2;

            var soundOut = new WasapiOut() { Latency = 200 };
            /* soundOut.Initialize(audioWaveSource);

            soundOut.Play();

            Console.ReadKey();
            soundOut.Stop();
            */
            /*
            var mixedAudio = CAudio.MixAudioAndVoice(audioWaveSource, out volumeSource1, voiceWaveSource, out volumeSource2);

            
            soundOut.Initialize(mixedAudio);
            soundOut.Play();

            volumeSource1.Volume = 0.5f;
            volumeSource2.Volume = 0.5f;

            Console.ReadKey();

            soundOut.Dispose();
            CAudio.Dispose();*/


            var url = new System.Uri("C:\\tmp2\\Van Halen - Jump.mp3").AbsoluteUri;

            Console.WriteLine(url);
        }
    }
}
