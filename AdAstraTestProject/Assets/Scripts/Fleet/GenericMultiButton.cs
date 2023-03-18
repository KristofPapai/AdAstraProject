using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenericMultiButton : MonoBehaviour
{

    public TMP_Text NumToChange;

    public void OnClick()
    {

        int NumberOfShips = int.Parse(NumToChange.text);
        //Decrease
        if (this.GetComponentInChildren<TMP_Text>().text == "<")
        {
            if (NumberOfShips != 0)
            {
                NumberOfShips--;
                NumToChange.text = NumberOfShips.ToString();
            }
        }

        //Increase
        if (this.GetComponentInChildren<TMP_Text>().text == ">")
        {
            NumberOfShips++;
            NumToChange.text = NumberOfShips.ToString();
        }
    }

    public void OnClickWithNegative()
    {

        int NumberOfShips = int.Parse(NumToChange.text);
        //Decrease
        if (this.GetComponentInChildren<TMP_Text>().text == "<")
        {
                NumberOfShips--;
                NumToChange.text = NumberOfShips.ToString();
        }

        //Increase
        if (this.GetComponentInChildren<TMP_Text>().text == ">")
        {
            NumberOfShips++;
            NumToChange.text = NumberOfShips.ToString();
        }
    }



    public TMP_Text ValNumOfTransport;
    public TMP_Text ValNumOfPMC;
    public void OnClickWithValidationTransport()
    {

        int NumberOfShips = int.Parse(NumToChange.text);
        //Decrease
        if (this.GetComponentInChildren<TMP_Text>().text == "<")
        {
            if (NumberOfShips != 0)
            {
                NumberOfShips--;
                NumToChange.text = NumberOfShips.ToString();
            }
        }

        //Increase
        if (this.GetComponentInChildren<TMP_Text>().text == ">")
        {
            if (int.Parse(ValNumOfTransport.text) > int.Parse(NumToChange.text))
            {
                NumberOfShips++;
                NumToChange.text = NumberOfShips.ToString();
            }

        }
    }

    public void OnClickWithValidationPMC()
    {

        int NumberOfShips = int.Parse(NumToChange.text);
        //Decrease
        if (this.GetComponentInChildren<TMP_Text>().text == "<")
        {
            if (NumberOfShips != 0)
            {
                NumberOfShips--;
                NumToChange.text = NumberOfShips.ToString();
            }
        }

        //Increase
        if (this.GetComponentInChildren<TMP_Text>().text == ">")
        {
            if (int.Parse(ValNumOfPMC.text) > int.Parse(NumToChange.text))
            {
                NumberOfShips++;
                NumToChange.text = NumberOfShips.ToString();
            }

        }
    }

    GameObject MotherPlanet;

    public void ValidateAmount()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Celestial");
        foreach (GameObject planet in planets)
        {
            if (planet.GetComponent<PlanetProperties>().IsMotherPlanet == true)
            {
                MotherPlanet = planet;
            }
        }
        string material = transform.parent.name.Split(" ")[0].ToLower();
        int Amount = int.Parse(NumToChange.text);
        //MotherPlanet.GetComponent<BuildingMaster>().AddHomeStockpile(material, 10);

        if (this.GetComponentInChildren<TMP_Text>().text == "<")
        {
            if (Amount != 0)
            {
                Amount--;
                NumToChange.text = Amount.ToString();
            }
        }

        //Increase
        if (this.GetComponentInChildren<TMP_Text>().text == ">")
        {
            double CurrentAmount = MotherPlanet.GetComponent<BuildingMaster>().ReturnHomeStockpileAmount(material);
            if (CurrentAmount > int.Parse(NumToChange.text))
            {
                Amount++;
                NumToChange.text = Amount.ToString();
            }

        }
    }
}
