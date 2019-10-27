using System;
using System.Xml.Serialization;
using CSCore;
using CSCore.SoundIn;
using CSCore.Streams;

namespace Core
{
    public static class APILayer
    {
        public static string GetSome()
        {
            return "U call method from Core";
        }
    }

    public class AudioControler : IDisposable
    {
        const int mixerSampleRate = 44100; //44.1kHz

        private Mixer.SimpleMixer mixer;

        public IWaveSource MixAudioAndVoice(IWaveSource audio, out VolumeSource vol1, IWaveSource voice, out VolumeSource vol2)
        {
            mixer = new Mixer.SimpleMixer(2, mixerSampleRate) //output: stereo, 44,1kHz
            {
                FillWithZeros = true,
                DivideResult = false //you may play around with this
            };

            mixer.AddSource(audio
                    .ChangeSampleRate(mixerSampleRate)
                    .ToStereo()
                    .AppendSource(x => new VolumeSource(x.ToSampleSource()), out vol1));

                mixer.AddSource(voice
                    .ChangeSampleRate(mixerSampleRate)
                    .ToStereo()
                    .AppendSource(x => new VolumeSource(x.ToSampleSource()), out vol2));

                return mixer.ToWaveSource();
        }


        public void Dispose()
        {
            mixer.Dispose();
        }
    }
}
