using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildTimer : MonoBehaviour
{
    
    [SerializeField] private float _duration;
    private float _timer = 0f;
    private string currentPlanetName;
    private GameObject currentPlanet;
    private Building curentBuildingData;

    private void Start()
    {
        curentBuildingData = this.GetComponent<StoreClassObject>().saveBuilding;
        _duration = curentBuildingData.BuildingTime;
        currentPlanetName = GameObject.Find("CelestialName").GetComponent<TMP_Text>().text;
        currentPlanet = GameObject.Find(currentPlanetName);
    }
    private void FixedUpdate()
    {
        int percentage = (int)Math.Round((double)(100 * _timer) / _duration);
        GameObject.Find(this.name + "/TextQueueProgress").GetComponent<TMP_Text>().text = "progress: "+percentage+"%";
        _timer += Time.deltaTime;
        if (_timer >= _duration)
        {
            _timer = 0f;
            buildBuilding();
            if (GameObject.Find("ScriptMaster").GetComponent<CameraMoveOnClick>().selected != null && currentPlanet == GameObject.Find("ScriptMaster").GetComponent<CameraMoveOnClick>().selected)
            {
                GameObject.Find("ScriptMaster").GetComponent<CameraMoveOnClick>().buildingListing(currentPlanet);
            }
            Destroy(this.gameObject);
        }
    }

    public void buildBuilding()
    {
        double price = curentBuildingData.UniEurosPrice;
        double techprice = curentBuildingData.TechPrice;
        if (currentPlanet.GetComponent<BuildingMaster>().enoughResource_UE_TECH(price, techprice))
        {
            currentPlanet.GetComponent<BuildingMaster>().classAbleToBuild.Remove(curentBuildingData);
            currentPlanet.GetComponent<BuildingMaster>().GenerateMIT(curentBuildingData.UniEurosGenerate, curentBuildingData.InfluenceGenerate, curentBuildingData.TechGenerate);
            currentPlanet.GetComponent<BuildingMaster>().classBuiltGroundBuildings.Add(curentBuildingData);
            if (this.name == "queue item,Warehouses")
            {
                isStockpile();
                if (GameObject.Find("ScriptMaster").GetComponent<CameraMoveOnClick>().selected != null && GameObject.Find("ScriptMaster").GetComponent<CameraMoveOnClick>().selected == currentPlanet)
                {
                    GameObject.Find("ScriptMaster").GetComponent<CameraMoveOnClick>().stockpileListing(currentPlanet);
                }
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

        //GameObject.Find("TextStockpile").GetComponent<TMP_Text>().text = "local stockpile /// available";
        foreach (string item in currentPlanet.GetComponent<PlanetProperties>().PlanetRareMaterials.Keys)
        {
            currentPlanet.GetComponent<BuildingMaster>().stockpile.Add(item, 0);
        }
        //GameObject.Find("ScriptMaster").GetComponent<CameraMoveOnClick>().stockpileListing(currentPlanet);
    }


}
