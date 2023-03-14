using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModifyTradeRoute : MonoBehaviour
{

    public TMP_Text ModifyTransport;
    public TMP_Text ModifyPMC;
    public TMP_Text AvailableTransport;
    public TMP_Text AvailablePMC;

    public GameObject CreateTradePanel;
    public GameObject ModifyTradePanel;

    public void OnClickModify()
    {

        FleetMaster fleetMaster = GameObject.Find("ScriptMaster").GetComponent<FleetMaster>();
        TradeRouteClass saved = this.GetComponentInParent<SelectedTradeSave>().SelectedRoute;
        for (int i = 0; i < fleetMaster.TradeRoutes.Count; i++)
        {
            if (fleetMaster.TradeRoutes[i].TradeRouteID == saved.TradeRouteID)
            {
                if (Validation(saved.Transports.Count, saved.PMCs.Count) == true)
                {
                    foreach (TransportClass transport in fleetMaster.TradeRoutes[i].Transports)
                    {
                        fleetMaster.Transportships.Add(transport);
                    }
                    fleetMaster.TradeRoutes[i].Transports.Clear();
                    int shipCounter = 0;
                    foreach (TransportClass transport in fleetMaster.Transportships)
                    {
                        if (shipCounter < int.Parse(ModifyTransport.text) && transport.Status == "standby")
                        {
                            fleetMaster.TradeRoutes[i].Transports.Add(transport);
                            shipCounter++;
                        }
                    }

                    foreach (PmcClass pmc in fleetMaster.TradeRoutes[i].PMCs)
                    {
                        fleetMaster.PMCships.Add(pmc);
                    }
                    fleetMaster.TradeRoutes[i].PMCs.Clear();
                    shipCounter = 0;
                    foreach (PmcClass pmc in fleetMaster.PMCships)
                    {
                        if (shipCounter < int.Parse(ModifyPMC.text) && pmc.Status == "standby")
                        {
                            fleetMaster.TradeRoutes[i].PMCs.Add(pmc);
                            shipCounter++;
                        }
                    }

                    foreach (TransportClass transport in fleetMaster.TradeRoutes[i].Transports)
                    {
                        fleetMaster.Transportships.Remove(transport);
                    }
                    foreach (PmcClass pmc in fleetMaster.TradeRoutes[i].PMCs)
                    {
                        fleetMaster.PMCships.Remove(pmc);
                    }
                }
            }
        }
        fleetMaster.UpdateTradeInfo();
        ModifyTradePanel.SetActive(false);
        CreateTradePanel.SetActive(true);
       
    }


    public bool Validation(int ActualRouteTransport, int ActualRoutePMC)
    {
        if (int.Parse(AvailableTransport.text)+ActualRouteTransport - int.Parse(ModifyTransport.text) >= 0 && int.Parse(AvailablePMC.text)+ActualRoutePMC - int.Parse(ModifyPMC.text) >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }



    public void OnclickDelete()
    {
        FleetMaster fleetMaster = GameObject.Find("ScriptMaster").GetComponent<FleetMaster>();
        TradeRouteClass saved = this.GetComponentInParent<SelectedTradeSave>().SelectedRoute;
        fleetMaster.TradeRoutes.Remove(saved);
        ModifyTradePanel.SetActive(false);
        CreateTradePanel.SetActive(true);
        fleetMaster.UpdateTradeInfo();

    }
}
