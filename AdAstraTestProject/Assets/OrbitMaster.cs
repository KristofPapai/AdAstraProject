using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMaster : MonoBehaviour
{

    public GameObject prefab;
    public GameObject parentPrefab;
    //public Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            float newRotation = Random.Range(0, 360);
            float newX = Random.Range(50, 150);
            var parentPlanet = Instantiate(parentPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0,0,0)); //inside the sun
            var childPlanet = Instantiate(prefab, new Vector3(newX, 0, 0), Quaternion.identity);
            childPlanet.transform.parent = parentPlanet.transform;
            parentPlanet.transform.Rotate(0, newRotation, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
