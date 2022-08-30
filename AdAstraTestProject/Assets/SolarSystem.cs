using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    readonly float G = 100f; //gravitációs konstans
    GameObject[] celestials;



    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");

        InitialVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Gravity();
    }

    void Gravity() 
    {
        foreach (GameObject AcelObj in celestials)
        {
            foreach (GameObject BcelObj in celestials)
            {
                if (!AcelObj.Equals(BcelObj))
                {
                    //a celestial bodyk súlyának lekérdezése
                    float m1 = AcelObj.GetComponent<Rigidbody>().mass;
                    float m2 = BcelObj.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(AcelObj.transform.position, BcelObj.transform.position);

                    AcelObj.GetComponent<Rigidbody>().AddForce(((BcelObj.transform.position-AcelObj.transform.position).normalized) * (G*(m1*m2)/(r*r)));
                }
            }
        }
    }

    void InitialVelocity()
    {
        foreach (GameObject AcelObj in celestials)
        {
            foreach (GameObject BcelObj in celestials)
            {
                float m2 = BcelObj.GetComponent<Rigidbody>().mass;
                float r = Vector3.Distance(AcelObj.transform.position, BcelObj.transform.position);
                AcelObj.transform.LookAt(BcelObj.transform);

                AcelObj.GetComponent<Rigidbody>().velocity += AcelObj.transform.right * Mathf.Sqrt((G * m2) / r);
            }
        }
    }
}
