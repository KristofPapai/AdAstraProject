using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DestroyQueueItem : MonoBehaviour
{
    private Building curentBuildingData;
    public void destroyQueuItem()
    {
        curentBuildingData = this.GetComponent<StoreClassObject>().saveBuilding;
        GameObject.Find("ScriptMaster").GetComponent<ResourceMaster>().AddUniEuros(curentBuildingData.UniEurosPrice);
        GameObject.Find("ScriptMaster").GetComponent<ResourceMaster>().AddTech(curentBuildingData.TechPrice);
        string currentPlanetName = GameObject.Find(this.name + "/TextBuildingPlanet").GetComponent<TMP_Text>().text;
        GameObject currentPlanet = GameObject.Find(currentPlanetName);
        //string[] splitter = this.name.Split(",");
        //string toAbletoBuild = "";
        //for (int i = 1; i < splitter.Length; i++)
        //{
        //    toAbletoBuild += splitter[i] + ",";
        //}
        //toAbletoBuild = toAbletoBuild.Substring(0, toAbletoBuild.Length - 1);
        currentPlanet.GetComponent<BuildingMaster>().classAbleToBuild.Add(curentBuildingData);
        GameObject.Find("AvailableOpBuildings").GetComponent<ShowBuildingPopup>().refresh();
        Destroy(this.gameObject);
    }
}
