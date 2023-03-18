using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class StockPileMaster : MonoBehaviour
{


    private GameObject MotherPlanet;
    public TMP_Text[] Stocks;

    public void Start()
    {

    }


    public void FixedUpdate()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Celestial");
        foreach (GameObject planet in planets)
        {
            if (planet.GetComponent<PlanetProperties>().IsMotherPlanet == true)
            {
                MotherPlanet = planet;
            }
        }
        
        for (int i = 0; i < Stocks.Length; i++)
        {
            Debug.Log(i);
            Stocks[i].text = MotherPlanet.GetComponent<BuildingMaster>().returnAmountByIndex(i).ToString();
        }
    }


    

}
