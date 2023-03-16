using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OrbitMaster : MonoBehaviour
{

    public GameObject prefab;
    public GameObject parentPrefab;
    public GameObject GasGiantPrefab;
    public GameObject ChaseCamera;
    public float rotationSpeed;
    public float dampAnt;
    public Button hudbutton;
    private List<GameObject> planets = new List<GameObject>();
    public Camera mainCamera;
    public Color color;
    public Material MotherPlanet;
    public Material[] PlanetMaterials;
    //public Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
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
        bool haveMotherPlanet = false;
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
            int typeOfPlanet = Random.Range(0, 4);
            var childPlanet = GasGiantPrefab;
            if (typeOfPlanet == 1)
            {
                //gas Giant
                childPlanet = Instantiate(GasGiantPrefab, new Vector3(newX, 0, 0), Quaternion.identity);

            }
            else
            {
                childPlanet = Instantiate(prefab, new Vector3(newX, 0, 0), Quaternion.identity);
            }
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
            
            //int typeOfPlanet = Random.Range(0, 4);
            float sizeOfPlanet;
            //ha 0-nál több holdat akar generálni akkor majd figyelni kell a oldak távolságára

            switch (typeOfPlanet)
            {
                case 0:
                    sizeOfPlanet = Random.Range(4, 7);
                    childPlanet.transform.localScale = new Vector3(sizeOfPlanet, sizeOfPlanet, sizeOfPlanet);
                    childPlanet.GetComponent<Renderer>().material = PlanetMaterials[Random.RandomRange(0, PlanetMaterials.Length)];
                    if (!haveMotherPlanet)
                    {
                        childPlanet.GetComponent<PlanetProperties>().IsMotherPlanet = true;
                        haveMotherPlanet=true;
                        childPlanet.GetComponent<Renderer>().material = MotherPlanet;
                    }
                    //Debug.Log("rocky planet");
                    break;
                case 1:
                    sizeOfPlanet = Random.Range(20, 30);
                    childPlanet.transform.localScale = new Vector3(sizeOfPlanet, sizeOfPlanet, sizeOfPlanet);
                    childPlanet.GetComponent<Renderer>().material = PlanetMaterials[3];

                    //Debug.Log("Gas giant");
                    if (!haveMotherPlanet)
                    {
                        childPlanet.GetComponent<PlanetProperties>().IsMotherPlanet = true;
                        haveMotherPlanet = true;
                        childPlanet.GetComponent<Renderer>().material = MotherPlanet;
                    }
                    break;
                case 3:
                    sizeOfPlanet = Random.Range(2, 5);
                    childPlanet.transform.localScale = new Vector3(sizeOfPlanet, sizeOfPlanet, sizeOfPlanet);
                    childPlanet.GetComponent<Renderer>().material = PlanetMaterials[Random.RandomRange(0, PlanetMaterials.Length)];

                    //Debug.Log("dwarf planet");
                    if (!haveMotherPlanet)
                    {
                        childPlanet.GetComponent<PlanetProperties>().IsMotherPlanet = true;
                        haveMotherPlanet = true;
                        childPlanet.GetComponent<Renderer>().material = MotherPlanet;

                    }
                    break;
                default:
                    break;
            }

            float tempX = childPlanet.transform.localPosition.x + 30;
            float randomRotation = Random.Range(0, 361);
            childPlanet.transform.parent = parentPlanet.transform;
            parentPlanet.transform.rotation = Quaternion.Euler(0, randomRotation, orbitrotation);
            parentPlanet.name = "parent " + childPlanet.name;
            planets.Add(childPlanet);

        }
    }


    private Vector2 scrollViewVector = Vector2.zero;
    public Rect dropDownRect = new Rect(125, 50, 125, 300);
    public static string[] list = { "List of Planets in the system" };
}
