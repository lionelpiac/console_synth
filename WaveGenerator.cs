using System;

public class WaveGenerator
{
    private const int SampleRate = 44100;
    private const double TwoPi = 2 * Math.PI;

    public static float[] GenerateSineWave(double frequency, double duration)
    {
        int numSamples = (int)(SampleRate * duration);
        float[] samples = new float[numSamples];

        for (int i = 0; i < numSamples; i++)
        {
            double t = i / (double)SampleRate;
            samples[i] = (float)Math.Sin(TwoPi * frequency * t);
        }

        return samples;
    }
}
