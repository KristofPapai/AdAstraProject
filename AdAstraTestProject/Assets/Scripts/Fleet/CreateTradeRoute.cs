using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateTradeRoute : MonoBehaviour
{
    public TMP_Text NumOfTransport;
    public TMP_Text NumOfPMC;
    public TMP_Text SelectedPlanet;
    public TMP_Text SelectedMaterial;

    public void OnClick()
    {
        FleetMaster fleetMaster = GameObject.Find("ScriptMaster").GetComponent<FleetMaster>();
        if (Validation() == true)
        {
            TradeRouteClass TradeSave = new TradeRouteClass();
            TradeSave.TradeRouteID = Random.Range(1, 1000);
            TradeSave.TradeRouteName = "Trade route No.: " + TradeSave.TradeRouteID;
            TradeSave.HomePlanet = fleetMaster.HomePlanet.text;
            TradeSave.TargetPlanet = SelectedPlanet.text;
            int shipCounter = 0;
            Debug.Log("Transports: "+NumOfTransport.text);
            foreach (TransportClass transport in fleetMaster.Transportships)
            {
                if (shipCounter < int.Parse(NumOfTransport.text) && transport.Status == "standby")
                {
                    TradeSave.Transports.Add(transport);
                    shipCounter++;
                }
            }
            shipCounter = 0;
            foreach (PmcClass pmc in fleetMaster.PMCships)
            {
                if (shipCounter < int.Parse(NumOfPMC.text) && pmc.Status == "standby")
                {
                    TradeSave.PMCs.Add(pmc);
                    shipCounter++;
                }
            }
            TradeSave.TransportedMaterials.Add(SelectedMaterial.text);
            fleetMaster.TradeRoutes.Add(TradeSave);
            foreach (TransportClass transport in TradeSave.Transports)
            {
                fleetMaster.Transportships.Remove(transport);
            }
            foreach (PmcClass pmc in TradeSave.PMCs)
            {
                fleetMaster.PMCships.Remove(pmc);
            }
            fleetMaster.UpdateTradeInfo();
        }
        
    }


    public TMP_Text TransportFleetSize;
    public TMP_Text PMCFleetSize;

    public bool Validation()
    {
        if ((int.Parse(TransportFleetSize.text) - int.Parse(NumOfTransport.text)) >= 0 &&
            (int.Parse(PMCFleetSize.text) - int.Parse(NumOfPMC.text)) >= 0 &&
            GameObject.Find("ScriptMaster").GetComponent<ResourceMaster>().OutUniEuros - 1000 >= 0
            )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
