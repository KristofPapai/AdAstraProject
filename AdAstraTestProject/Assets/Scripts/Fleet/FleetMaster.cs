using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using UnityEngine;
//using static UnityEditor.Progress;

public class FleetMaster : MonoBehaviour
{
    public int NumOfTransport;
    public int NumOfPMC;
    public int UpkeepTransport = 5;
    public int UpkeepPMC = 15;
    public int UpkeepTransportMoth = 1;
    public int UpkeepPMCMoth = 5;
    public int NumMothTransport;
    public int NummothPMC;
    public double FullUpkeep = 0;
    private GameObject MotherPlanet;
    public List<TradeRouteClass> TradeRoutes = new List<TradeRouteClass>();
    public List<PmcClass> PMCships = new List<PmcClass>();
    public List<TransportClass> Transportships = new List<TransportClass>();


    public GameObject FleetInfoPanel;
    public TMP_Text HomePlanet;
    public TMP_Text TransportSize;
    public TMP_Text PMCSize;
    public TMP_Text Upkeep;
    public TMP_Text TransportMoth;
    public TMP_Text PMCMoth;

    public GameObject TradeRouteInfoPanel;
    public TMP_Text HomePlanet2;
    public TMP_Text TransportSize2;
    public TMP_Text PMCSize2;
    public TMP_Text Upkeep2;
    public TMP_Text TransportMoth2;
    public TMP_Text PMCMoth2;


    public GameObject TradeCardVeriticalLayout;
    public GameObject ButtonPrefab;

    public GameObject ModifyTradeRoutePage;
    public GameObject CreateTradeRoutePage;

    public void UpdateTradeInfo()
    {
        while (TradeCardVeriticalLayout.transform.childCount > 0)
        {
            DestroyImmediate(TradeCardVeriticalLayout.transform.GetChild(0).gameObject);
        }
        foreach (TradeRouteClass trades in TradeRoutes)
        {
            GameObject button = Instantiate(ButtonPrefab, TradeCardVeriticalLayout.transform);
            button.transform.SetParent(TradeCardVeriticalLayout.transform);
            button.GetComponent<PrefabModifyClick>().ModifyTradeRoute = ModifyTradeRoutePage;
            button.GetComponent<PrefabModifyClick>().CreateTradeRoute = CreateTradeRoutePage;
            button.GetComponent<PrefabModifyClick>().StoredRoute = trades;
            button.name = trades.TradeRouteName;
            //GameObject.Find(item.Name + "/TextBuildingName").GetComponent<TMP_Text>().text = item.Name;
            GameObject.Find(button.name + "/OutTradeRouteName").GetComponent<TMP_Text>().text = trades.TradeRouteName;
            GameObject.Find(button.name + "/FrameTargetPlanet/OutTargetPlanet").GetComponent<TMP_Text>().text = trades.TargetPlanet;
            GameObject.Find(button.name + "/FrameOutHomePlanet/OutHomePlanet").GetComponent<TMP_Text>().text = trades.HomePlanet;
            GameObject.Find(button.name + "/FrameOutActiveTransport/OutActiveTransports").GetComponent<TMP_Text>().text = trades.Transports.Count.ToString();
            GameObject.Find(button.name + "/FrameOutPMCs/OutActivePMCS").GetComponent<TMP_Text>().text = trades.PMCs.Count.ToString();
            GameObject.Find(button.name + "/FrameOutCapacity/OutTransportCapacity").GetComponent<TMP_Text>().text = trades.Cargocapacity().ToString();
            string materialBuilder = "| ";
            foreach (string item in trades.TransportedMaterials)
            {
                materialBuilder += item + " |";
            }
            GameObject.Find(button.name + "/OutTransportedReasurces/OutReasurces").GetComponent<TMP_Text>().text = materialBuilder;

        }
    }


    public void UpdateFleetInfo()
    {
        if (MotherPlanet == null)
        {
            GameObject[] planets = GameObject.FindGameObjectsWithTag("Celestial");
            foreach (GameObject planet in planets)
            {
                if (planet.GetComponent<PlanetProperties>().IsMotherPlanet == true)
                {
                    MotherPlanet = planet;
                }

            }
        }
        HomePlanet.text = MotherPlanet.name;
        int transportMothed = 0;
        int pmcMothed = 0;
        int transportStandby = 0;
        int pmcStandby = 0;
        foreach (TransportClass transport in Transportships)
        {
            if (transport.Status == "mothball")
            {
                transportMothed++;
            }
            else
            {
                transportStandby++;
            }
        }
        foreach (PmcClass pmc in PMCships)
        {
            if (pmc.Status == "mothball")
            {
                pmcMothed++;
            }
            else
            {
                pmcStandby++;
            }
        }
        TransportSize.text = transportStandby.ToString();
        PMCSize.text = pmcStandby.ToString();
        TransportMoth.text = transportMothed.ToString();
        PMCMoth.text = pmcMothed.ToString();
        Upkeep.text = ((pmcStandby * UpkeepPMC) + (transportStandby * UpkeepTransport) + (transportMothed*UpkeepTransportMoth)+(pmcMothed*UpkeepPMCMoth)).ToString() + " UE";
        FullUpkeep = (pmcStandby * UpkeepPMC) + (transportStandby * UpkeepTransport) + (transportMothed * UpkeepTransportMoth) + (pmcMothed * UpkeepPMCMoth);

    }

    public void UpdateFleetInfoTradeRoute()
    {
        if (MotherPlanet == null)
        {
            GameObject[] planets = GameObject.FindGameObjectsWithTag("Celestial");
            foreach (GameObject planet in planets)
            {
                if (planet.GetComponent<PlanetProperties>().IsMotherPlanet == true)
                {
                    MotherPlanet = planet;
                }

            }
        }
        HomePlanet2.text = MotherPlanet.name;
        int transportMothed = 0;
        int pmcMothed = 0;
        int transportStandby = 0;
        int pmcStandby = 0;
        foreach (TransportClass transport in Transportships)
        {
            if (transport.Status == "mothball")
            {
                transportMothed++;
            }
            else
            {
                transportStandby++;
            }
        }
        foreach (PmcClass pmc in PMCships)
        {
            if (pmc.Status == "mothball")
            {
                pmcMothed++;
            }
            else
            {
                pmcStandby++;
            }
        }
        TransportSize2.text = transportStandby.ToString();
        PMCSize2.text = pmcStandby.ToString();
        TransportMoth2.text = transportMothed.ToString();
        PMCMoth2.text = pmcMothed.ToString();
        Upkeep2.text = ((pmcStandby * UpkeepPMC) + (transportStandby * UpkeepTransport) + (transportMothed * UpkeepTransportMoth) + (pmcMothed * UpkeepPMCMoth)).ToString() + " UE";
        FullUpkeep = (pmcStandby * UpkeepPMC) + (transportStandby * UpkeepTransport) + (transportMothed * UpkeepTransportMoth) + (pmcMothed * UpkeepPMCMoth);
    }

    public TMP_Dropdown Dropdown;


    

    public GameObject PrefabTradePlanetButton;
    public GameObject PrefabTradeMaterialButton;
    public GameObject TradePlanetVertical;
    public GameObject TradeMaterialVertical;
    public void UpdateTradeScroll()
    {
        while (TradePlanetVertical.transform.childCount > 0)
        {
            DestroyImmediate(TradePlanetVertical.transform.GetChild(0).gameObject);
        }
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Celestial");
        List<string> planetNames = new List<string>();

        foreach (GameObject planet in planets)
        {
            if (planet.GetComponent<BuildingMaster>().classBuiltGroundBuildings.Any(x => x.Name == "Starport"))
            {
                planetNames.Add(planet.name);
                GameObject tempButton = Instantiate(PrefabTradePlanetButton, TradePlanetVertical.transform);
                tempButton.name = planet.name;
                tempButton.transform.SetParent(TradePlanetVertical.transform);
                tempButton.GetComponentInChildren<TMP_Text>().text = planet.name;
            }
        }
    }

    public TMP_Text NumOfTradeShips;
    public TMP_Text NumOfPmcShips;
    public TMP_Text CargoCapacity;
    public TMP_Text DeltaVReq;
    public TMP_Text Cost;
    public TMP_Text ModifyTransportNum;
    public TMP_Text ModifyCargoCapacity;
    public int CurrentCargoCapPerShip = 20; //ez majd scalelhet
    public void UpdateTradeProps()
    {
        CargoCapacity.text = (int.Parse(NumOfTradeShips.text) * CurrentCargoCapPerShip).ToString() + " Unit";
        ModifyCargoCapacity.text = (int.Parse(ModifyTransportNum.text) * CurrentCargoCapPerShip).ToString() + " Unit";
    }


    public void FixedUpdate()
    {
        if (FleetInfoPanel.active)
        {
            UpdateFleetInfo();
        }
        if (TradeRouteInfoPanel.active)
        {
            UpdateTradeProps();
            UpdateFleetInfoTradeRoute();
            //updateMaterialDorpdown();
        }
    }
}
