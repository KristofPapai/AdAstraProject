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
    public GameObject ChaseCamera;
    public float rotationSpeed;
    public float dampAnt;
    public Button hudbutton;
    private List<GameObject> planets = new List<GameObject>();
    public Camera mainCamera;
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
        string[] solType = new string[] { "WD", "WR", "RG", "BD", "NE" };
        string[] latinNames = new string[] { "Yolninus",
                                            "Sophoria",
                                            "Lucronoe",
                                            "Dulides",
                                            "Choanope",
                                            "Ilea",
                                            "Llezistea",
                                            "Sulutov",
                                            "Made A75",
                                            "Llarth W",
                                            "Komehines",
                                            "Tholnenides",
                                            "Sungade",
                                            "Ulvillon",
                                            "Geutania",
                                            "Vavis",
                                            "Namaria",
                                            "Vineturn",
                                            "Garth U7G3",
                                            "Leron 7F9",
                                            "Licrenope",
                                            "Mecraeturn",
                                            "Gilichi",
                                            "Cuzarvis",
                                            "Piohines",
                                            "Iarus",
                                            "Grebicarro",
                                            "Zutheter",
                                            "Trypso 12RZ",
                                            "Neron I50"};
        string inSol = solType[Random.Range(0, solType.Length)];
        List<string> choosenNames = new List<string>();
        for (int i = 0; i < numberOfPlanets; i++)
        {

            float newRotation = Random.Range(0, 360);
            float rangeDecider = Random.Range(60, 120);
            float newX = lastX + rangeDecider;
            lastX = newX;
            float orbitrotation = Random.Range(-5, 5);
            var parentPlanet = Instantiate(parentPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)); //inside the sun lehet itt van probléma mivel nem a küzéppont körül forog normálisan a bolygó a (0,0,0) pontal lesz valami
            parentPlanet.GetComponent<RotateMaster>().dampAnt = dampningUpdate;
            dampningUpdate -= 0.5f;
            var childPlanet = Instantiate(prefab, new Vector3(newX, 0, 0), Quaternion.identity);

            bool goodName = false;
            do
            {
                string name = latinNames[Random.Range(0, latinNames.Length)];
                if (!choosenNames.Contains(name))
                {
                    choosenNames.Add(name);
                    childPlanet.name = name;
                    goodName = true;
                }

            } while (goodName != true);
            
            int typeOfPlanet = Random.Range(0, 4);
            float sizeOfPlanet;
            //ha 0-nál több holdat akar generálni akkor majd figyelni kell a oldak távolságára

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

            float tempX = childPlanet.transform.localPosition.x + 30;
            //var ChaserCam = Instantiate(ChaseCamera, new Vector3(tempX, 10, 0), Quaternion.Euler(25,0,0));
            //ChaserCam.transform.parent = childPlanet.transform;
            childPlanet.transform.parent = parentPlanet.transform;
            parentPlanet.transform.rotation = Quaternion.Euler(0, 0, orbitrotation);
            parentPlanet.name = "parent " + childPlanet.name;
            planets.Add(childPlanet);

        }
    }


    private Vector2 scrollViewVector = Vector2.zero;
    public Rect dropDownRect = new Rect(125, 50, 125, 300);
    public static string[] list = { "List of Planets in the system" };

    int indexNumber;
    bool show = false;

    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect((dropDownRect.x - 100), dropDownRect.y, dropDownRect.width, 25), ""))
    //    {
    //        if (!show)
    //        {
    //            show = true;
    //        }
    //        else
    //        {
    //            show = false;
    //        }
    //    }
    //    if (show)
    //    {
    //        scrollViewVector = GUI.BeginScrollView(new Rect((dropDownRect.x - 100), (dropDownRect.y + 25), dropDownRect.width, dropDownRect.height), scrollViewVector, new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (list.Length * 25))));

    //        GUI.Box(new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (planets.Count * 25))), "");

    //        for (int index = 0; index < planets.Count; index++)
    //        {

    //            if (GUI.Button(new Rect(0, (index * 25), dropDownRect.height, 25), ""))
    //            {
    //                show = false;
    //                indexNumber = index;
    //            }

    //            GUI.Label(new Rect(5, (index * 25), dropDownRect.height, 25), planets[index].name);

    //        }

            

    //        GUI.EndScrollView();
    //    }
    //    else
    //    {

    //        GUI.Label(new Rect((dropDownRect.x - 95), dropDownRect.y, 300, 25), planets[indexNumber].name);

    //        CamMoveToListTarget(planets[indexNumber].name);
    //    }
    //}

    //private bool chaseCam = false;
    //public float speed = 100.0f;
    //public void CamMoveToListTarget(string planetName)
    //{

    //    GameObject selected = GameObject.Find(planetName);

    //    var step = speed * Time.deltaTime;
    //    float distance = Vector3.Distance(mainCamera.transform.position, new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40));
    //    if (distance != 0)
    //    {
    //        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40), step);
    //        //mainCamera.transform.LookAt(selected.transform);
    //        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    //        Debug.Log("shake");
            
    //    }
    //    else
    //    {
    //        chaseCam = true;

    //    }
    //    if (chaseCam)
    //    {
    //        mainCamera.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40);
    //        //mainCamera.transform.LookAt(selected.transform);
    //        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    //    }

    //}
}
