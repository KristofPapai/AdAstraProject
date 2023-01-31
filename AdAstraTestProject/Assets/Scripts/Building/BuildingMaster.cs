using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;
using UnityEngine.UIElements;

using Screen = UnityEngine.Screen;

public class BuildingMaster : MonoBehaviour
{


    public List<string> UpgradeBuildings = new List<string>() {"FOB","Outpost","Permament Base Facility" }; //UniEuros + Tech cost
                                                                                                            //Lvl1: 1000+100
                                                                                                            //Lvl2: 3000+200
                                                                                                            //Lvl3: 5000+300

    public List<string> lvl1Buildings = new List<string>() { "","Rover Hangar 200", "Research Quarters", "Operation Deck" }; //making a small ammount of money and tech
    public List<string> lvl2Buildings = new List<string>() { "Apartments", "Surface Mining", "Warehouses" }; //mining of rare materials and stockpileing it also more income
    public List<string> lvl3Buildings = new List<string>() { "Core Mining", "Starport", "BioDome" }; //shipping routes and cargo ports

    public List<string> AbleToBuild = new List<string>();
    private GameObject reasurceMasterScript;
    public List<string> BuiltUpgradeBuildings = new List<string>();
    public List<string> BuiltGroundBuildings = new List<string>();

    public GameObject buttonPrefab;
    public GameObject canvas;
    public GameObject panlePrefab;

    public TMP_Dropdown dropdown;

    void Start()
    {
        dropdown = GameObject.Find("BuildingsDropDown").GetComponent<TMP_Dropdown>();
        reasurceMasterScript = GameObject.Find("ScriptMaster");


    }
    void Update()
    {

    }

    public void OnGUI()
    {

        //canvas = GameObject.FindGameObjectWithTag("HUD");
        //if (Vector3.Distance(GameObject.Find("Cam").transform.position, this.transform.position) < 70)
        //{
            
        //    if (this.gameObject.GetComponent<PlanetProperties>().IsMotherPlanet == false)
        //    {
        //        BuildingBuilder();


        //    }
        
        //}
    }


    void ListenerCall()
    {
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown, AbleToBuild);
        });
    }


    public void BuildingBuilder()
    {
        if (BuiltUpgradeBuildings.Count == 0)
        {

            if (enoughResource_UE_TECH(1000,100))
            {
                BuiltUpgradeBuildings.Add("FOB");
                UpgradeBuildings.Remove("FOB");
                AbleToBuild.AddRange(lvl1Buildings);
                dropdown.ClearOptions();
                dropdown.AddOptions(AbleToBuild);
                Debug.Log("Built FOB");
                Debug.Log(AbleToBuild.Count);
                GenerateMIT(20,0,5);
                GameObject.Find("OutCurrentOperationLevel").GetComponent<TMP_Text>().text = "CURRENT OPERATION\n/// OPERATION LVL I.";
                GameObject.Find("TextReqUniEuros").GetComponent<TMP_Text>().text = "UNIEUROS /// 3000";
                GameObject.Find("TextReqTech").GetComponent<TMP_Text>().text = "UNIEUROS /// 200";
                ListenerCall();
                
            }
            else
            {

            }
        }
        else if (BuiltUpgradeBuildings.Contains("FOB") && BuiltUpgradeBuildings.Count == 1)
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
                GameObject.Find("OutCurrentOperationLevel").GetComponent<TMP_Text>().text = "CURRENT OPERATION\n/// OPERATION LVL II.";
                GameObject.Find("TextReqUniEuros").GetComponent<TMP_Text>().text = "UNIEUROS /// 5000";
                GameObject.Find("TextReqTech").GetComponent<TMP_Text>().text = "UNIEUROS /// 300";
                ListenerCall();
            }
        }
        else if (BuiltUpgradeBuildings.Contains("Outpost") && BuiltUpgradeBuildings.Contains("FOB") && BuiltUpgradeBuildings.Count == 2)
        {

            if (enoughResource_UE_TECH(5000, 300))
            {
                BuiltUpgradeBuildings.Add("Permament Base Facility");
                Debug.Log("Built Permament Base Facility");
                AbleToBuild.AddRange(lvl3Buildings);
                dropdown.ClearOptions();
                dropdown.AddOptions(AbleToBuild);
                GenerateMIT(200, 150, 20);
                GameObject.Find("OutCurrentOperationLevel").GetComponent<TMP_Text>().text = "CURRENT OPERATION\n/// OPERATION LVL III.";
                GameObject.Find("TextReqUniEuros").GetComponent<TMP_Text>().text = "UNIEUROS /// NONE";
                GameObject.Find("TextReqTech").GetComponent<TMP_Text>().text = "UNIEUROS /// NONE";
                GameObject.Find("TextMainButton").GetComponent<TMP_Text>().text = "MAX OPERATION LVL";
                ListenerCall();
            }
            
        }
        else if (BuiltUpgradeBuildings.Contains("Permament Base Facility"))
        {

        }

    }


    void DropdownValueChanged(TMP_Dropdown change,List<string> temp)
    {
        Debug.Log(temp.Count);
        if (AbleToBuild[change.value] == "")
        {
            Debug.Log("Empty Selection");
        }
        else
        {
            //ha megvan a pénz rá
            if (enoughResource_UE_TECH(200,50))
            {
                BuiltGroundBuildings.Add(AbleToBuild[change.value]);
                AbleToBuild.RemoveAt(change.value);
                dropdown.ClearOptions();
                dropdown.AddOptions(AbleToBuild);
                dropdown.value = 0;
                Debug.Log("gb: " + BuiltGroundBuildings.Count);
            }
            else
            {
                Debug.Log("Not enough UE_TECH");
            }
        }
    }


    public void GenerateMIT(double UE,double Influence,double Tech)
    {
        this.gameObject.GetComponent<PlanetProperties>().GenUniEuros += UE;
        this.gameObject.GetComponent<PlanetProperties>().GenInfluence += Influence;
        this.gameObject.GetComponent<PlanetProperties>().GenTech += Tech;

    }

    public bool enoughResource_UE_TECH(double UE, double Tech)
    {
        double currentUE = double.Parse(reasurceMasterScript.GetComponent<ResourceMaster>().UniEuros.text);
        double currentTech = double.Parse(reasurceMasterScript.GetComponent<ResourceMaster>().Tech.text);
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
