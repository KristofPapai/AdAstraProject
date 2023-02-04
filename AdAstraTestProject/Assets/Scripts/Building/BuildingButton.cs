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
        string[] splitter = this.gameObject.name.Split(',');
        double price = double.Parse(splitter[1]);
        double techprice = double.Parse(splitter[2]);
        //currentPlanet.GetComponent<BuildingMaster>().enoughResource_UE_TECH(price, techprice);
        if (currentPlanet.GetComponent<BuildingMaster>().enoughResource_UE_TECH(price, techprice))
        {
            Debug.Log("megepult");
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("not enough MIT");
        }
    }
}
