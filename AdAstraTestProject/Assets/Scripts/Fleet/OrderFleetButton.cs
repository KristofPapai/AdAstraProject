using System.Collections;
using System.Collections.Generic;
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
            }
            else
            {
                GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().NumOfPMC += int.Parse(NumOfShips.text);
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
