using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OperationUpgrade : MonoBehaviour
{

    public GameObject popup;

    public void UpgradeOperationLevel()
    {

        string currentPlanetName = GameObject.Find("CelestialName").GetComponent<TMP_Text>().text;
        GameObject currentPlanet = GameObject.Find(currentPlanetName);
        currentPlanet.GetComponent<BuildingMaster>().BuildingBuilder();
        if (popup.active)
        {
            popup.SetActive(false);
            GameObject.Find("AvailableOpBuildings").GetComponent<ShowBuildingPopup>().refresh();
        }

    }
    
}
