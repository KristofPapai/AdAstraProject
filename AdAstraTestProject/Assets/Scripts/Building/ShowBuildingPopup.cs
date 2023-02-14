using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowBuildingPopup : MonoBehaviour
{
    public GameObject PopUp;
    public GameObject buttonprefab;
    public GameObject verticalLayout;
    public void ButtonPressed() 
    {
        if (PopUp.active)
        {
            PopUp.SetActive(false);

            while (verticalLayout.transform.childCount > 0)
            {
                DestroyImmediate(verticalLayout.transform.GetChild(0).gameObject);
            }
        }
        else
        {
            refresh();
            
        }
    }

    public void refresh()
    {
        killchildren();
        PopUp.SetActive(true);
        string currentPlanetName = GameObject.Find("CelestialName").GetComponent<TMP_Text>().text;
        GameObject currentPlanet = GameObject.Find(currentPlanetName);
        foreach (Building item in currentPlanet.GetComponent<BuildingMaster>().classAbleToBuild)
        {
            GameObject button = Instantiate(buttonprefab, verticalLayout.transform);
            button.transform.SetParent(verticalLayout.transform);
            button.transform.name = item.Name;
            button.GetComponent<StoreClassObject>().saveBuilding = item;
            GameObject.Find(item.Name + "/TextBuildingName").GetComponent<TMP_Text>().text = item.Name;
            GameObject.Find(item.Name + "/TextReqUniEuros").GetComponent<TMP_Text>().text = "unieuros /// " + item.UniEurosPrice;
            GameObject.Find(item.Name + "/TextReqTech").GetComponent<TMP_Text>().text = "tecH /// " + item.TechPrice;
            GameObject.Find(item.Name + "/TextBuildingTime").GetComponent<TMP_Text>().text = "Assembly /// " + item.BuildingTime+" days";
        }
    }

    public void killchildren()
    {
        while (verticalLayout.transform.childCount > 0)
        {
            DestroyImmediate(verticalLayout.transform.GetChild(0).gameObject);
        }
    }

    //public void MiningOperations()
    //{
    //    string currentPlanetName = GameObject.Find("CelestialName").GetComponent<TMP_Text>().text;
    //    GameObject currentPlanet = GameObject.Find(currentPlanetName);
    //    if (currentPlanet.GetComponent<BuildingMaster>().BuiltGroundBuildings.Contains("Warehouses,500,500,0,0") && currentPlanet.GetComponent<BuildingMaster>().BuiltGroundBuildings.Contains("Surface Mining,500,500,100,0,200"))
    //    {
    //        foreach (string item in currentPlanet.GetComponent<BuildingMaster>().stockpile.Keys)
    //        {

    //        }
    //    }

    //}

}
