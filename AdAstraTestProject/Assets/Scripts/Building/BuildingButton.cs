using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    public void buildBuilding() 
    {
        string currentPlanetName = GameObject.Find("CelestialName").GetComponent<TMP_Text>().text;
        GameObject currentPlanet = GameObject.Find(currentPlanetName);
        currentPlanet.GetComponent<BuildingMaster>().AbleToBuild.Remove(this.gameObject.name);
        string[] splitter = this.gameObject.name.Split(',');
        double price = double.Parse(splitter[1]);
        double techprice = double.Parse(splitter[2]);
        currentPlanet.GetComponent<BuildingMaster>().GenerateMIT(double.Parse(splitter[3]), 0, double.Parse(splitter[4]));
        //currentPlanet.GetComponent<BuildingMaster>().enoughResource_UE_TECH(price, techprice);
        if (currentPlanet.GetComponent<BuildingMaster>().enoughResource_UE_TECH(price, techprice))
        {
            currentPlanet.GetComponent<BuildingMaster>().BuiltGroundBuildings.Add(this.name);
            if (this.name == "Warehouses,500,500,0,0")
            {
                isStockpile();
            }
            pushToQueue();
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("not enough MIT");
        }
    }

    public void isStockpile()
    {
        string currentPlanetName = GameObject.Find("CelestialName").GetComponent<TMP_Text>().text;
        GameObject currentPlanet = GameObject.Find(currentPlanetName);
        GameObject.Find("TextStockpile").GetComponent<TMP_Text>().text = "local stockpile /// available";
        foreach (string item in currentPlanet.GetComponent<PlanetProperties>().PlanetRareMaterials)
        {
            currentPlanet.GetComponent<BuildingMaster>().stockpile.Add(item, 0);
            
        }
        GameObject.Find("ScriptMaster").GetComponent<CameraMoveOnClick>().stockpileListing(currentPlanet);
    }

    public GameObject queueVerticalLayout;
    public GameObject queueItemPrefab;
    public void pushToQueue()
    {
        queueVerticalLayout = GameObject.Find("QueueVerticalLayout");
        
        GameObject queueitem = Instantiate(queueItemPrefab, queueVerticalLayout.transform);
        queueitem.transform.SetParent(queueVerticalLayout.transform);
        queueitem.transform.name = "queue item,"+this.name;
        GameObject.Find(queueitem.name + "/TextQueueBuildingName").GetComponent<TMP_Text>().text = this.name.Split(',')[0];
        GameObject.Find(queueitem.name + "/TextQueueProgress").GetComponent<TMP_Text>().text = "progress --%";
        string currentPlanetName = GameObject.Find("CelestialName").GetComponent<TMP_Text>().text;
        GameObject.Find(queueitem.name + "/TextBuildingPlanet").GetComponent<TMP_Text>().text = currentPlanetName;
;

    }
}
