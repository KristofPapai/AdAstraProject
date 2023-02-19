using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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


    }

    public void FixedUpdate()
    {
        if (FleetInfoPanel.active)
        {
            UpdateFleetInfo();
        }
        if (TradeRouteInfoPanel.active)
        {
            UpdateFleetInfoTradeRoute();
        }
    }
}
