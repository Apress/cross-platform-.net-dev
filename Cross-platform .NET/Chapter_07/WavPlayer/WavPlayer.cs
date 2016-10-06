//Filename: WavPlayer.cs
using System;
using Tao.OpenAl;

namespace Crossplatform.NET.Chapter7
{
    class WavPlayer : IDisposable
    {
        private int buffer;                             //Buffer to hold sound data.
        private int source;                             //The sound source
        private float pitch = 0.3F;			//The pitch
        private string fileName;                        //The file to play

        //Initialise the player
        public WavPlayer(string fileName)
        {
            this.fileName = fileName;
            Alut.alutInit();
            LoadALData();
        }

        public void Dispose()
        {
            //Do some house cleaning
            Al.alDeleteBuffers(1, ref this.buffer);
            Al.alDeleteSources(1, ref this.source);
            Alut.alutExit();
        }

        //Play the WAV file...
        public void Play()
        {       
            Al.alSourcePlay(this.source);
        }

        //Load the data for playing
        private void LoadALData()
        {
            byte[] data;
            int format, frequency, loop, size;
        
            //Load the wav data into a buffer.
            Al.alGenBuffers(1, out this.buffer);
            Alut.alutLoadWAVFile(this.fileName, out format, out data, out size, out frequency, out loop);
            Al.alBufferData(this.buffer, format, data, size, frequency);
            Alut.alutUnloadWAV(format, out data, size, frequency);

            //Bind the buffer with the source.
            Al.alGenSources(1, out this.source);
            Al.alSourcei(this.source, Al.AL_BUFFER, this.buffer);
            Al.alSourcef(this.source, Al.AL_PITCH, this.pitch);
        }

        //The program's entrypoint
        static void Main(string[] args)
        {
            //Play the wav file.
            using(WavPlayer player = new WavPlayer("ding.wav"))
            {
            	player.Play();
            }
            
	    Console.WriteLine("Press enter to exit...");
            Console.ReadLine();    
        }
    }
}
