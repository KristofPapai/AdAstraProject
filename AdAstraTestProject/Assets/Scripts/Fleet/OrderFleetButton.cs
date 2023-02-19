using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class OrderFleetButton : MonoBehaviour
{

    public TMP_Text Amount;
    public GameObject Panel;
    public TMP_Text NumOfShips;
    public TMP_Text TypeOfShip;
    public GameObject Lore;
    public void OnClick()
    {
        double ActualAmount = double.Parse(Amount.text.Split(' ')[0]);
        if (EnougUE(ActualAmount))
        {
            GameObject.Find("ScriptMaster").GetComponent<ResourceMaster>().AddUniEuros(-ActualAmount);
            if (TypeOfShip.text == "Order transport ships")
            {
                GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().NumOfTransport += int.Parse(NumOfShips.text);
                for (int i = 0; i < int.Parse(NumOfShips.text); i++)
                {
                    TransportClass tempTransport = new TransportClass();
                    if (GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().Transportships.Count == 0)
                    {
                        tempTransport.Shipumber = 1;
                    }
                    else
                    {
                        tempTransport.Shipumber = GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().Transportships.Last().Shipumber + 1;
                    }
                    tempTransport.Firepower = 1;
                    tempTransport.Armor = 10;
                    tempTransport.CargoCapacity = 20;
                    tempTransport.Status = "standby";
                    GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().Transportships.Add(tempTransport);
                }
            }
            else
            {
                GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().NumOfPMC += int.Parse(NumOfShips.text);
                for (int i = 0; i < int.Parse(NumOfShips.text); i++)
                {
                    PmcClass tempPMC = new PmcClass();
                    if (GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().PMCships.Count == 0)
                    {
                        tempPMC.Shipumber = 1;
                    }
                    else
                    {
                        tempPMC.Shipumber = GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().PMCships.Last().Shipumber + 1;
                    }
                    tempPMC.Firepower = 10;
                    tempPMC.Armor = 20;
                    tempPMC.CargoCapacity = 0;
                    tempPMC.Status = "standby";
                    GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().PMCships.Add(tempPMC);
                }
            }
            Panel.SetActive(false);
            Lore.SetActive(true);
        }
        
    }

    public bool EnougUE(double UEAmount)
    {
        if (GameObject.Find("ScriptMaster").GetComponent<ResourceMaster>().OutUniEuros - UEAmount >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}
