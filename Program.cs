using NAudio.Wave;
using System;

class Program
{
    static void Main()
    {

        Console.WriteLine("Playing bass sequence. Press 'x' to stop.");
        Boolean playSequence = true;

        while (playSequence==true){ 
            PlaySequence();

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.X)
            {
                Console.WriteLine("\nStopping sequence");
                playSequence=false;
            }
            else
            {
                Console.WriteLine($"\nYou pressed '{keyInfo.KeyChar}', but 'x' was expected.");
            }
        }

        //Console.ReadKey(true);

        static void PlaySequence()
        {
              while (!Console.KeyAvailable)
        //while (Console.ReadKey().Key != ConsoleKey.X)

        {
            PlaySound(120, 1);  // Play a 440 Hz sound for 1 second
            PlaySound(120, 1);  // Play an 880 Hz sound for 1 second
            PlaySound(120, 1);  // Play a 660 Hz sound for 1 second
            PlaySound(130, 1.2);  // Play a 660 Hz sound for 1.2 second
        }

        }

        static void PlaySound(double frequency, double duration)
        {
            float[] soundWave = WaveGenerator.GenerateSineWave(frequency, duration);

            using (var waveOut = new WaveOutEvent())
            {
                waveOut.Init(new WaveGeneratorWaveProvider(soundWave));
                waveOut.Play();

                // Keep the program running while the audio plays
                while (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

    }
}
public class WaveGeneratorWaveProvider : IWaveProvider
{
    private readonly float[] samples;
    private int position;

    public WaveGeneratorWaveProvider(float[] samples)
    {
        this.samples = samples;
        this.position = 0;
    }

    public WaveFormat WaveFormat => WaveFormat.CreateIeeeFloatWaveFormat(SampleRate, 1);

    public int Read(byte[] buffer, int offset, int count)
    {
        int samplesToCopy = Math.Min(count / 4, (samples.Length - position));

        for (int i = 0; i < samplesToCopy; i++)
        {
            byte[] sampleBytes = BitConverter.GetBytes(samples[position]);
            Buffer.BlockCopy(sampleBytes, 0, buffer, offset + i * 4, 4);
            position++;
        }

        return samplesToCopy * 4;
    }

    private static int SampleRate => 48000;
}
