using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DestroyQueueItem : MonoBehaviour
{
    public void destroyQueuItem()
    {
        GameObject.Find("ScriptMaster").GetComponent<ResourceMaster>().AddUniEuros(double.Parse(this.name.Split(',')[2]));
        GameObject.Find("ScriptMaster").GetComponent<ResourceMaster>().AddUniEuros(double.Parse(this.name.Split(',')[3]));
        string currentPlanetName = GameObject.Find(this.name + "/TextBuildingPlanet").GetComponent<TMP_Text>().text;
        GameObject currentPlanet = GameObject.Find(currentPlanetName);
        string[] splitter = this.name.Split(",");
        string toAbletoBuild = "";
        for (int i = 1; i < splitter.Length; i++)
        {
            toAbletoBuild += splitter[i] + ",";
        }
        toAbletoBuild = toAbletoBuild.Substring(0, toAbletoBuild.Length - 1);
        currentPlanet.GetComponent<BuildingMaster>().AbleToBuild.Add(toAbletoBuild);
        GameObject.Find("AvailableOpBuildings").GetComponent<ShowBuildingPopup>().refresh();
        Destroy(this.gameObject);
    }
}
