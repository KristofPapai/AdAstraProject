using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    readonly float G = 100f; //gravitációs konstans
    GameObject[] celestials;
    public GameObject myPrefab;


    // Start is called before the first frame update
    void Start()
    {
        //myPrefab.GetComponent<Rigidbody>().mass = 30;
        //Instantiate(myPrefab, new Vector3(1560, 0, 0), Quaternion.identity);
        float numberOfPlanets = Random.Range(3, 8);
        for (int i = 0; i < numberOfPlanets; i++)
        {
            //type of the planet
            float typeOfPlanet = Random.Range(0, 4);
            switch (typeOfPlanet)
            {
                case 1:
                    myPrefab.GetComponent<Rigidbody>().mass = Random.RandomRange(0.031f, 0.071f);
                    float c1 = Random.Range(0.5f, 1.5f);
                    Vector3 scaleChanger1 = new Vector3(c1,c1,c1);
                    myPrefab.transform.localScale = scaleChanger1;
                    float x1 = Random.Range(40, 300);
                    Instantiate(myPrefab, new Vector3(x1, 0, 0), Quaternion.identity);
                    break;
                case 2:
                    myPrefab.GetComponent<Rigidbody>().mass = Random.RandomRange(8, 20);
                    float c2 = Random.Range(4, 10);
                    Vector3 scaleChanger2 = new Vector3(c2, c2, c2);
                    myPrefab.transform.localScale = scaleChanger2;
                    float x2 = Random.Range(40, 300);
                    Instantiate(myPrefab, new Vector3(x2, 0, 0), Quaternion.identity);
                    break;
                case 3:
                    myPrefab.GetComponent<Rigidbody>().mass = Random.RandomRange(0.01f, 0.02f);
                    float c3 = Random.Range(0.1f, 0.5f);
                    Vector3 scaleChanger3 = new Vector3(c3, c3, c3);
                    myPrefab.transform.localScale = scaleChanger3;
                    float x3 = Random.Range(40, 300);
                    Instantiate(myPrefab, new Vector3(x3, 0, 0), Quaternion.identity);
                    break;
                default:
                    break;
            }
        }



        //ez el?tt kell a generálást elvégezni
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
