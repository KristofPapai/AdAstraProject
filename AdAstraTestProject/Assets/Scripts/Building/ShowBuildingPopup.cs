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
            PopUp.SetActive(true);
            string currentPlanetName = GameObject.Find("CelestialName").GetComponent<TMP_Text>().text;
            GameObject currentPlanet = GameObject.Find(currentPlanetName);
            List<string> availablebuildings = currentPlanet.GetComponent<BuildingMaster>().AbleToBuild;
            foreach (string building in availablebuildings)
            {
                
                GameObject button = Instantiate(buttonprefab,verticalLayout.transform);
                button.transform.SetParent(verticalLayout.transform);
                string[] splitted = building.Split(',');
                button.transform.name = building;
                GameObject.Find(building + "/TextBuildingName").GetComponent<TMP_Text>().text = splitted[0];
                GameObject.Find(building + "/TextReqUniEuros").GetComponent<TMP_Text>().text = "unieuros /// "+splitted[1];
                GameObject.Find(building + "/TextReqTech").GetComponent<TMP_Text>().text = "tecH /// " + splitted[2];

            }

        }
    }
   
}