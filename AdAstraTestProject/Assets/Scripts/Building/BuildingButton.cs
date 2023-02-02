using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    public void buildBuilding() 
    {
        string currentPlanetName = GameObject.Find("CelestialName").GetComponent<TMP_Text>().text;
        GameObject currentPlanet = GameObject.Find(currentPlanetName);
        currentPlanet.GetComponent<BuildingMaster>().AbleToBuild.Remove(this.gameObject.name);
        Destroy(this.gameObject);
    }
}
