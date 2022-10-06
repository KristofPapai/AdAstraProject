using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{

    void  Update()
    {
        transform.LookAt(this.transform.parent);
    }
}
