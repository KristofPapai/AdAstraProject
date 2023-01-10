using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;
using Screen = UnityEngine.Screen;

public class BuildingMaster : MonoBehaviour
{


    public List<string> UpgradeBuildings = new List<string>() {"FOB","Outpost","Permament Base Facility" }; //UniEuros + Tech cost
                                                                                                            //Lvl1: 1000+100
                                                                                                            //Lvl2: 3000+200
                                                                                                            //Lvl3: 5000+300

    public List<string> lvl1Buildings = new List<string>() { "dsad","Rover Hangar 200", "Research Quarters", "Operation Deck" }; //making a small ammount of money and tech
    public List<string> lvl2Buildings = new List<string>() { "Apartments", "Surface Mining", "Warehouses" }; //mining of rare materials and stockpileing it also more income
    public List<string> lvl3Buildings = new List<string>() { "Core Mining", "Starport", "BioDome" }; //shipping routes and cargo ports

    public List<string> AbleToBuild = new List<string>();

    public List<string> BuiltUpgradeBuildings = new List<string>();
    public List<string> BuiltGroundBuildings = new List<string>();

    public GameObject buttonPrefab;
    public GameObject canvas;
    public GameObject panlePrefab;

    public TMP_Dropdown dropdown;

    void Start()
    {
        dropdown = GameObject.Find("BuildingsDropDown").GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });

    }
    void Update()
    {

    }

    private void OnGUI()
    {
        canvas = GameObject.FindGameObjectWithTag("HUD");
        if (Vector3.Distance(GameObject.Find("Cam").transform.position, this.transform.position) < 70)
        {
            
            if (this.gameObject.GetComponent<PlanetProperties>().IsMotherPlanet == false)
            {
                BuildingBuilder();


            }
        
        }
    }

    public void BuildingBuilder()
    {
        if (BuiltUpgradeBuildings.Count == 0)
        {
            if (GUI.Button(new Rect(((Screen.width / 2) + 100), (Screen.height / 2) - 100, 300, 25), "Upgrade to TECH I"))
            {
                
                if (enoughResource_UE_TECH(1000,100))
                {
                    BuiltUpgradeBuildings.Add("FOB");
                    UpgradeBuildings.Remove("FOB");
                    AbleToBuild.AddRange(lvl1Buildings);
                    dropdown.ClearOptions();
                    dropdown.AddOptions(AbleToBuild);
                    Debug.Log("Built FOB");
                    GenerateMIT(20,0,5);
                    
                }
                else
                {
                    //not enough resource
                }

            }
        }
        else if (BuiltUpgradeBuildings.Contains("FOB") && BuiltUpgradeBuildings.Count == 1)
        {
            if (GUI.Button(new Rect(((Screen.width / 2) + 100), (Screen.height / 2) - 100, 300, 25), "Upgrade to TECH II"))
            {
                if (enoughResource_UE_TECH(3000, 200))
                {
                    BuiltUpgradeBuildings.Add("Outpost");
                    UpgradeBuildings.Remove("FOB");
                    Debug.Log("Built Outpost");
                    AbleToBuild.AddRange(lvl2Buildings);
                    dropdown.ClearOptions();
                    dropdown.AddOptions(AbleToBuild);
                    GenerateMIT(120, 50, 10);
                }
                else
                {

                }


            }

            
        }
        else if (BuiltUpgradeBuildings.Contains("Outpost") && BuiltUpgradeBuildings.Contains("FOB") && BuiltUpgradeBuildings.Count == 2)
        {
            if (GUI.Button(new Rect(((Screen.width / 2) + 100), (Screen.height / 2) - 100, 300, 25), "Upgrade to TECH III"))
            {
                if (enoughResource_UE_TECH(5000, 300))
                {
                    BuiltUpgradeBuildings.Add("Permament Base Facility");
                    Debug.Log("Built Permament Base Facility");
                    AbleToBuild.AddRange(lvl3Buildings);
                    dropdown.ClearOptions();
                    dropdown.AddOptions(AbleToBuild);
                    GenerateMIT(200, 150, 20);

                    
                }

            }
        }
        else if (BuiltUpgradeBuildings.Contains("Permament Base Facility"))
        {

        }

    }


    void DropdownValueChanged(TMP_Dropdown change)
    {
        Debug.Log(change.value);

        //sokszor irja ki mert meghivódik töbször ez a szar
        //miután kivettuk az elemet 

        if (AbleToBuild[change.value] != "")
        {

        }


        //string temp_base_name = AbleToBuild[change.value];
        //AbleToBuild.Remove(temp_base_name);
        //dropdown.ClearOptions();
        //dropdown.AddOptions(AbleToBuild);
    }

    public void OnClick()
    {
    
    }

    public void ShowBuiltBuilings()
    {
        
    
    }


    public void GenerateMIT(double UE,double Influence,double Tech)
    {
        this.gameObject.GetComponent<PlanetProperties>().GenUniEuros += UE;
        this.gameObject.GetComponent<PlanetProperties>().GenInfluence += Influence;
        this.gameObject.GetComponent<PlanetProperties>().GenTech += Tech;

    }

    public bool enoughResource_UE_TECH(double UE, double Tech)
    {
        GameObject reasurceMasterScript = GameObject.Find("ScriptMaster");
        double currentUE = reasurceMasterScript.GetComponent<ResourceMaster>().OutUniEuros;
        double currentTech = reasurceMasterScript.GetComponent<ResourceMaster>().OutTech;
        if (currentUE - UE >= 0 && currentTech - Tech >= 0)
        {
            reasurceMasterScript.GetComponent<ResourceMaster>().AddUniEuros(-UE);
            reasurceMasterScript.GetComponent<ResourceMaster>().AddTech(-Tech);
            return true;
        }
        else
        {
            return false;
        }

    }



}
