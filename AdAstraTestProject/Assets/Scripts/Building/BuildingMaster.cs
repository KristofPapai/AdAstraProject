using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.UIElements;

using Screen = UnityEngine.Screen;

public class BuildingMaster : MonoBehaviour
{

    //felépítése a épületek elnevezésének név/szukséges unieuros/tech/fix érkezés? unieuros/tech/építésiid?
    public File txtBuildings = new File();

    public List<string> UpgradeBuildings = new List<string>() { "FOB", "Outpost", "Permament Base Facility" };
    public List<string> lvl1Buildings = new List<string>() { "Rover Hangar,200,50,10,0,10", "Research Quarters,100,200,10,50,120", "Operation Deck,300,20,10,10,120" }; //making a small ammount of money and tech
    public List<string> lvl2Buildings = new List<string>() { "Apartments,100,200,0,0,200", "Surface Mining,500,500,100,0,10", "Warehouses,500,500,0,0,10" }; //mining of rare materials and stockpileing it also more income
    public List<string> lvl3Buildings = new List<string>() { "Core Mining,2500,700,0,0,300", "Starport,3000,1000,100,100,300", "BioDome,1000,1000,30,30,300" }; //shipping routes and cargo ports
    private List<string> textSaveing = new List<string>() {"current operations\n/// none","unieuros /// 1000","tech /// 100"}; //oplevel,buttons,
    public List<string> AbleToBuild = new List<string>();
    public Dictionary<string, double> stockpile = new Dictionary<string, double>();


    private GameObject reasurceMasterScript;
    public List<string> BuiltUpgradeBuildings = new List<string>();
    public List<string> BuiltGroundBuildings = new List<string>();
    public List<string> Miningbuildings = new List<string>();
    public GameObject popup;
    public bool upgraded = false;



    //class update
    public List<string> AllBuildings = new List<string>{
        "Rover Hangar,1,200,0,50,10,0,0,10",
        "Research Quarters,1,100,0,200,10,0,50,120",
        "Operation Deck,1,300,0,20,10,0,10,120",
        "Apartments,2,100,0,200,0,0,200",
        "Surface Mining,2,500,,0,500,100,0,0,10",
        "Warehouses,2,500,0,500,0,0,0,10",
        "Core Mining,3,2500,700,0,0,300",
        "Starport,3000,3,1000,0,100,100,0,300",
        "BioDome,3,1000,0,1000,30,0,30,300"
    };

    public List<Building> buildings = new List<Building>();
    public List<Building> classAbleToBuild = new List<Building>();



    void Start()
    {
        reasurceMasterScript = GameObject.Find("ScriptMaster");
        popup = GameObject.Find("PopUpBuilding");
        foreach (string item in AllBuildings)
        {
            string[] splitter = item.Split(',');
            Building tempBuilding = new Building();
            tempBuilding.Name = splitter[0];
            tempBuilding.Level = int.Parse(splitter[1]);
            tempBuilding.UniEurosPrice = double.Parse(splitter[2]);
            tempBuilding.InfluencePrice = double.Parse(splitter[3]);
            tempBuilding.TechPrice = double.Parse(splitter[4]);
            tempBuilding.UniEurosGenerate = double.Parse(splitter[5]);
            tempBuilding.InfluenceGenerate = double.Parse(splitter[6]);
            tempBuilding.TechGenerate = double.Parse(splitter[7]);
            tempBuilding.BuildingTime = double.Parse(splitter[8]);
            buildings.Add(tempBuilding);
        }

    }

    private void FixedUpdate()
    {
        MiningOperations();
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

                Debug.Log("Built FOB");
                Debug.Log(AbleToBuild.Count);
                GenerateMIT(20,0,5);

                textSaveing[0] = "current operation\n/// operation lvl I.";
                textSaveing[1] = "unieuros /// 3000";
                textSaveing[2] = "tech /// 200";

                GameObject.Find("OutCurrentOperationLevel").GetComponent<TMP_Text>().text = "current operation\n/// operation lvl I.";
                GameObject.Find("TextReqUniEuros").GetComponent<TMP_Text>().text = "unieuros /// 3000";
                GameObject.Find("TextReqTech").GetComponent<TMP_Text>().text = "tech /// 200";
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

                GenerateMIT(120, 50, 10);
                textSaveing[0] = "current operation\n/// operation lvl II.";
                textSaveing[1] = "unieuros /// 5000";
                textSaveing[2] = "tech /// 300";
                GameObject.Find("OutCurrentOperationLevel").GetComponent<TMP_Text>().text = "current operation\n/// operation lvl II.";
                GameObject.Find("TextReqUniEuros").GetComponent<TMP_Text>().text = "unieuros /// 5000";
                GameObject.Find("TextReqTech").GetComponent<TMP_Text>().text = "tech /// 300";
            }
        }
        else if (BuiltUpgradeBuildings.Contains("Outpost") && BuiltUpgradeBuildings.Contains("FOB") && BuiltUpgradeBuildings.Count == 2)
        {

            if (enoughResource_UE_TECH(5000, 300))
            {
                BuiltUpgradeBuildings.Add("Permament Base Facility");
                Debug.Log("Built Permament Base Facility");
                AbleToBuild.AddRange(lvl3Buildings);

                GenerateMIT(200, 150, 20);
                textSaveing[0] = "current operation\n/// operation lvl III.";
                textSaveing[1] = "unieuros /// none";
                textSaveing[2] = "tech /// none";
                upgraded = true;
                GameObject.Find("OutCurrentOperationLevel").GetComponent<TMP_Text>().text = "current operation\n/// operation lvl III.";
                GameObject.Find("TextReqUniEuros").GetComponent<TMP_Text>().text = "unieuros /// none";
                GameObject.Find("TextReqTech").GetComponent<TMP_Text>().text = "tech /// none";
                GameObject.Find("TextMainButton").GetComponent<TMP_Text>().text = "max operation lvl";
            }
            
        }
        else if (BuiltUpgradeBuildings.Contains("Permament Base Facility"))
        {

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
        if (BuiltGroundBuildings.Contains("Warehouses,500,500,0,0"))
        {
            GameObject.Find("TextStockpile").GetComponent<TMP_Text>().text = "local stockpile /// available";
        }
        else
        {
            GameObject.Find("TextStockpile").GetComponent<TMP_Text>().text = "local stockpile /// none";
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

    public bool haveminingOps = false;

    public void MiningOperations()
    {
        

        if (BuiltGroundBuildings.Contains("Warehouses,500,500,0,0,10") && BuiltGroundBuildings.Contains("Surface Mining,500,500,100,0,10") && haveminingOps == false)
        {
            foreach (string item in this.GetComponent<PlanetProperties>().PlanetRareMaterials)
            {
                this.GetComponent<BuildingMaster>().AbleToBuild.Add(item + "mine,500,50,100,0,300");
            }
            haveminingOps = true;
        }

    }
}
