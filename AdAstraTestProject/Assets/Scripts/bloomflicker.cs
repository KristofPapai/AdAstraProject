using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class bloomflicker : MonoBehaviour
{
    public float min = 2f;
    public float max = 5f;
    public float speed = 0.5f;

    public PostProcessVolume volume;
    Bloom bloom;

    private void Start()
    {
        volume.profile.TryGetSettings(out bloom);
    }
    void Update()
    {
        bloom.intensity.value = Mathf.Lerp(min, max, Mathf.PingPong(Time.time, speed));
    }
}
