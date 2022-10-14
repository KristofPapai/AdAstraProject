using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter: INoiseFilter
{
    Noise noise = new Noise();
    NoiseSettings.RigidNoiseSetting settings;

    public RigidNoiseFilter(NoiseSettings.RigidNoiseSetting settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = 1-Mathf.Abs(noise.Evaluate(point * frequency + settings.centre));
            v *= v;
            v *= weight;
            weight = v;
            noiseValue += Mathf.Clamp01(v * settings.weightMulitplier);
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }
        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
        return noiseValue * settings.strength;
    }
}
