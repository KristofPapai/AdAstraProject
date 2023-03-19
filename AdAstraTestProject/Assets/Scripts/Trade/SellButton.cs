using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellButton : MonoBehaviour
{
    public TMP_Text SellPrice;
    public TMP_Text amount;
    GameObject MotherPlanet;
   
    public void Onclick()
    {
        ResourceMaster master = GameObject.Find("ScriptMaster").GetComponent<ResourceMaster>();
        double ActSellPrice = double.Parse(SellPrice.text);
        double ActAmount = double.Parse(amount.text);
        master.AddUniEuros(ActAmount*ActSellPrice);
        double DeductPrice = ActAmount / 10;
        if (ActSellPrice-DeductPrice >= 0)
        {
            ActSellPrice -= DeductPrice;
        }
        SellPrice.text = ActSellPrice.ToString("#.00");
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Celestial");
        foreach (GameObject planet in planets)
        {
            if (planet.GetComponent<PlanetProperties>().IsMotherPlanet == true)
            {
                MotherPlanet = planet;
            }
        }
        string matName = this.name.Split(" ")[0].ToLower();
        MotherPlanet.GetComponent<BuildingMaster>().DeductHomeStockpile(matName,ActAmount);
        amount.text = "0";
    }

}
