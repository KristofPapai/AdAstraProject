using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OrbitMaster : MonoBehaviour
{

    public GameObject prefab;
    public GameObject parentPrefab;
    public float rotationSpeed;
    public float dampAnt;
    public Button hudbutton;
    //public Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        SystemGeneration();
    }


    public void SystemGeneration()
    {
        float lastX = 50;
        float numberOfPlanets = Random.Range(5, 11);
        float maxOrbitRing = numberOfPlanets * 80;
        float dampningUpdate = 6;
        for (int i = 0; i < numberOfPlanets; i++)
        {
            
            float newRotation = Random.Range(0, 360);
            float rangeDecider = Random.Range(60, 120);
            float newX = lastX + rangeDecider;
            lastX = newX;
            var parentPlanet = Instantiate(parentPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)); //inside the sun
            parentPlanet.GetComponent<RotateMaster>().dampAnt = dampningUpdate;
            dampningUpdate -= 0.5f;
            var childPlanet = Instantiate(prefab, new Vector3(newX, 0, 0), Quaternion.identity);
            int typeOfPlanet = Random.Range(0, 4);
            float sizeOfPlanet;
            int moonDecider = Random.Range(0, 3); //ha 0-nál több holdat akar generálni akkor majd figyelni kell a oldak távolságára

            switch (typeOfPlanet)
            {
                case 0:
                    sizeOfPlanet = Random.Range(4, 7);
                    childPlanet.transform.localScale = new Vector3(sizeOfPlanet, sizeOfPlanet, sizeOfPlanet);
                    //Debug.Log("rocky planet");
                    break;
                case 1:
                    sizeOfPlanet = Random.Range(20, 30);
                    childPlanet.transform.localScale = new Vector3(sizeOfPlanet, sizeOfPlanet, sizeOfPlanet);
                    //Debug.Log("Gas giant");
                    break;
                case 3:
                    sizeOfPlanet = Random.Range(2, 5);
                    childPlanet.transform.localScale = new Vector3(sizeOfPlanet, sizeOfPlanet, sizeOfPlanet);
                    //Debug.Log("dwarf planet");
                    break;
                default:
                    break;
            }

            //childPlanet.transform.parent = parentPlanet.transform;

            //parentPlanet.transform.Rotate(0, newRotation, 0);
        }
    }

}
