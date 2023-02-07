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

        GameObject.Find("AvailableOpBuildings").GetComponent<ShowBuildingPopup>().refresh();
        Destroy(this.gameObject);
    }
}
