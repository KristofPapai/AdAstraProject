using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildTimer : MonoBehaviour
{
    
    [SerializeField] private float _duration;
    private float _timer = 0f;
    private void Start()
    {
        _duration = float.Parse(this.name.Split(',')[6]);
    }
    private void FixedUpdate()
    {
        //double percentage = ((float)Math.Round(-_timer/10.0))*10;
        int percentage = (int)Math.Round((double)(100 * _timer) / _duration);
        GameObject.Find(this.name + "/TextQueueProgress").GetComponent<TMP_Text>().text = "progress: "+percentage+"%";
        _timer += Time.deltaTime;
        if (_timer >= _duration)
        {
            Debug.Log("megepült");
            _timer = 0f;
            buildBuilding();
            Destroy(this.gameObject);
        }
    }

    public void buildBuilding()
    {
        string currentPlanetName = GameObject.Find("CelestialName").GetComponent<TMP_Text>().text;
        GameObject currentPlanet = GameObject.Find(currentPlanetName);

        string[] splitter = this.gameObject.name.Split(',');
        double price = double.Parse(splitter[2]);
        double techprice = double.Parse(splitter[3]);
        //currentPlanet.GetComponent<BuildingMaster>().enoughResource_UE_TECH(price, techprice);
        if (currentPlanet.GetComponent<BuildingMaster>().enoughResource_UE_TECH(price, techprice))
        {
            currentPlanet.GetComponent<BuildingMaster>().AbleToBuild.Remove(this.gameObject.name);
            currentPlanet.GetComponent<BuildingMaster>().BuiltGroundBuildings.Add(this.name);
            currentPlanet.GetComponent<BuildingMaster>().GenerateMIT(double.Parse(splitter[4]), 0, double.Parse(splitter[5]));
            if (this.name == "Warehouses,500,500,0,0")
            {
                isStockpile();
            }
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("not enough MIT");
        }
    }

    public void isStockpile()
    {
        string currentPlanetName = GameObject.Find("CelestialName").GetComponent<TMP_Text>().text;
        GameObject currentPlanet = GameObject.Find(currentPlanetName);
        GameObject.Find("TextStockpile").GetComponent<TMP_Text>().text = "local stockpile /// available";
        foreach (string item in currentPlanet.GetComponent<PlanetProperties>().PlanetRareMaterials)
        {
            currentPlanet.GetComponent<BuildingMaster>().stockpile.Add(item, 0);

        }
        GameObject.Find("ScriptMaster").GetComponent<CameraMoveOnClick>().stockpileListing(currentPlanet);
    }
}
