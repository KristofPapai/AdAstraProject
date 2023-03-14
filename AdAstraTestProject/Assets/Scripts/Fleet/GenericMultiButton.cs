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
}
