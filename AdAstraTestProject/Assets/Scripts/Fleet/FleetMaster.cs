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
    public int NumMothTransport;
    public int NummothPMC;
    private GameObject MotherPlanet;


    public GameObject FleetInfoPanel;
    public TMP_Text HomePlanet;
    public TMP_Text TransportSize;
    public TMP_Text PMCSize;
    public TMP_Text Upkeep;
    public TMP_Text TransportMoth;
    public TMP_Text PMCMoth;


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
        TransportSize.text = NumOfTransport.ToString();
        PMCSize.text = NumOfPMC.ToString();
        Upkeep.text = ((NumOfPMC * UpkeepPMC) + (NumOfTransport * UpkeepTransport)).ToString() + " UE";
        TransportMoth.text = NumMothTransport.ToString();
        PMCMoth.text = NummothPMC.ToString();

    }

    public void FixedUpdate()
    {
        if (FleetInfoPanel.active)
        {
            UpdateFleetInfo();
        }
    }
}
