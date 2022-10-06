using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMaster : MonoBehaviour
{
    public float rotationSpeed;
    public float dampAnt;
    private void FixedUpdate()
    {
        transform.Rotate((Vector3.up) * (Time.deltaTime * dampAnt), Space.Self);

    }
}
