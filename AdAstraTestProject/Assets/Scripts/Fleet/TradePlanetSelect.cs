using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradePlanetSelect : MonoBehaviour
{
    public GameObject PrefabTradeMaterial;
    public void OnClick()
    {

        GameObject outPlanet = GameObject.Find("OutSelectedPlanet");
        GameObject outMaterial = GameObject.Find("OutSelectedMaterial");
        outMaterial.GetComponent<TMP_Text>().text = "Select material";
        outPlanet.GetComponent<TMP_Text>().text = this.name;
        string PlanetName = this.name;
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Celestial");
        GameObject verticalG = GameObject.Find("MaterialVertical");
        while (verticalG.transform.childCount > 0)
        {
            DestroyImmediate(verticalG.transform.GetChild(0).gameObject);
        }
        foreach (GameObject planet in planets)
        {
            if (planet.name == PlanetName)
            {
                List<string> tempMatList = new List<string>();
                foreach (string material in planet.GetComponent<PlanetProperties>().PlanetRareMaterials.Keys)
                {
                    tempMatList.Add(material);
                    
                    GameObject tempButton = Instantiate(PrefabTradeMaterial,verticalG.transform);
                    tempButton.name = material;
                    tempButton.GetComponentInChildren<TMP_Text>().text = material;
                }
            }
        }
    }
}
