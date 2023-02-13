using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{

    public Building currentButtonData;
    public void buildBuilding()
    {
        string currentPlanetName = GameObject.Find("CelestialName").GetComponent<TMP_Text>().text;
        GameObject currentPlanet = GameObject.Find(currentPlanetName);
        currentButtonData = this.GetComponent<StoreClassObject>().saveBuilding;
        if (currentPlanet.GetComponent<BuildingMaster>().enoughResource_UE_TECH(currentButtonData.UniEurosPrice, currentButtonData.TechPrice))
        {
            pushToQueue();
            currentPlanet.GetComponent<BuildingMaster>().classAbleToBuild.Remove(currentButtonData);
            Destroy(this.gameObject);
        }
        


        //string[] splitter = this.gameObject.name.Split(',');
        //double price = double.Parse(splitter[1]);
        //double techprice = double.Parse(splitter[2]);
        ////currentPlanet.GetComponent<BuildingMaster>().enoughResource_UE_TECH(price, techprice);
        //if (currentPlanet.GetComponent<BuildingMaster>().enoughResource_UE_TECH(price, techprice))
        //{
        //    pushToQueue();
        //    currentPlanet.GetComponent<BuildingMaster>().AbleToBuild.Remove(this.name);
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    Debug.Log("not enough MIT");
        //}
    }


    public GameObject queueVerticalLayout;
    public GameObject queueItemPrefab;
    public void pushToQueue()
    {
        queueVerticalLayout = GameObject.Find("QueueVerticalLayout");
        
        GameObject queueitem = Instantiate(queueItemPrefab, queueVerticalLayout.transform);
        queueitem.transform.SetParent(queueVerticalLayout.transform);
        queueitem.GetComponent<StoreClassObject>().saveBuilding = currentButtonData;
        queueitem.transform.name = "queue item,"+this.name;

        GameObject.Find(queueitem.name + "/TextQueueBuildingName").GetComponent<TMP_Text>().text = currentButtonData.Name;
        GameObject.Find(queueitem.name + "/TextQueueProgress").GetComponent<TMP_Text>().text = "progress --%";
        string currentPlanetName = GameObject.Find("CelestialName").GetComponent<TMP_Text>().text;
        GameObject.Find(queueitem.name + "/TextBuildingPlanet").GetComponent<TMP_Text>().text = currentPlanetName;


    }
}
