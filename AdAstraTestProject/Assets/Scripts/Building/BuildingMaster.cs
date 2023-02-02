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


    public List<string> UpgradeBuildings = new List<string>() { "FOB", "Outpost", "Permament Base Facility" };
    public List<string> lvl1Buildings = new List<string>() { "Rover Hangar,200,50", "Research Quarters,100,200", "Operation Deck,300,20" }; //making a small ammount of money and tech
    public List<string> lvl2Buildings = new List<string>() { "Apartments,100,200", "Surface Mining,500,500", "Warehouses,500,500" }; //mining of rare materials and stockpileing it also more income
    public List<string> lvl3Buildings = new List<string>() { "Core Mining,2500,700", "Starport,3000,1000", "BioDome,1000,1000" }; //shipping routes and cargo ports
    private List<string> textSaveing = new List<string>() {"current operations\n/// none","unieuros /// 1000","tech /// 100"}; //oplevel,buttons,
    public List<string> AbleToBuild = new List<string>();
    private GameObject reasurceMasterScript;
    public List<string> BuiltUpgradeBuildings = new List<string>();
    public List<string> BuiltGroundBuildings = new List<string>();
    public GameObject popup;
    public bool upgraded = false;

    //public GameObject buttonPrefab;
    //public GameObject canvas;
    //public GameObject panlePrefab;

    public TMP_Dropdown dropdown;

    void Start()
    {
        dropdown = GameObject.Find("BuildingsDropDown").GetComponent<TMP_Dropdown>();
        reasurceMasterScript = GameObject.Find("ScriptMaster");
        popup = GameObject.Find("PopUpBuilding");


    }
    void Update()
    {
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

                textSaveing[0] = "current operation\n/// operation lvl I.";
                textSaveing[1] = "unieuros /// 3000";
                textSaveing[2] = "tech /// 200";

                GameObject.Find("OutCurrentOperationLevel").GetComponent<TMP_Text>().text = "current operation\n/// operation lvl I.";
                GameObject.Find("TextReqUniEuros").GetComponent<TMP_Text>().text = "unieuros /// 3000";
                GameObject.Find("TextReqTech").GetComponent<TMP_Text>().text = "tech /// 200";
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
                textSaveing[0] = "current operation\n/// operation lvl II.";
                textSaveing[1] = "unieuros /// 5000";
                textSaveing[2] = "tech /// 300";
                GameObject.Find("OutCurrentOperationLevel").GetComponent<TMP_Text>().text = "current operation\n/// operation lvl II.";
                GameObject.Find("TextReqUniEuros").GetComponent<TMP_Text>().text = "unieuros /// 5000";
                GameObject.Find("TextReqTech").GetComponent<TMP_Text>().text = "tech /// 300";
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
                textSaveing[0] = "current operation\n/// operation lvl III.";
                textSaveing[1] = "unieuros /// none";
                textSaveing[2] = "tech /// none";
                upgraded = true;
                GameObject.Find("OutCurrentOperationLevel").GetComponent<TMP_Text>().text = "current operation\n/// operation lvl III.";
                GameObject.Find("TextReqUniEuros").GetComponent<TMP_Text>().text = "unieuros /// none";
                GameObject.Find("TextReqTech").GetComponent<TMP_Text>().text = "tech /// none";
                GameObject.Find("TextMainButton").GetComponent<TMP_Text>().text = "max operation lvl";
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


    public void clickToPlanet()
    {
        ///GameObject.Find("PopUpBuilding").SetActive(false);
        GameObject.Find("OutCurrentOperationLevel").GetComponent<TMP_Text>().text = textSaveing[0].ToString();
        GameObject.Find("TextReqUniEuros").GetComponent<TMP_Text>().text = textSaveing[1].ToString();
        GameObject.Find("TextReqTech").GetComponent<TMP_Text>().text = textSaveing[2].ToString();
        if (upgraded)
        {
            GameObject.Find("TextMainButton").GetComponent<TMP_Text>().text = "max operation lvl";
        }
        else
        {
            GameObject.Find("TextMainButton").GetComponent<TMP_Text>().text = "upgrade operations";
        }
        
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
