using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseFilterFactory
{
    public static INoiseFilter CreateNoiseFilter(NoiseSettings settigns)
    {
        switch (settigns.filterType)
        {
            case NoiseSettings.FilterType.Simple:
                return new SimpleNoiseFilter(settigns.simpleNoiseSettings);
                break;
            case NoiseSettings.FilterType.Rigid:
                return new RigidNoiseFilter(settigns.rigidNoiseSettings);
                break;
            default:
                break;
        }
        return null;
    }
}
