using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueButton : MonoBehaviour
{
    [SerializeField] private float _duration;
    private float _timer = 0f;
    private void FixedUpdate()
    {
        
        _timer += Time.deltaTime;
        if (_timer >= _duration)
        {
            _timer = 0f;
        }
    }
}
