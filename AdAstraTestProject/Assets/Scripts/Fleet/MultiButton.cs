using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiButton : MonoBehaviour
{
    public TMP_Text NumToChange;
    public TMP_Text Cost;
    public TMP_Text Type;
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
                //Cost.text = (NumberOfShips * 500).ToString() + " UE";
                if (Type.text == "Order transport ships")
                {
                    Cost.text = (NumberOfShips * 500).ToString() + " UE";
                }
                else
                {
                    Cost.text = (NumberOfShips * 1000).ToString() + " UE";
                }
            }
        }

        //Increase
        if (this.GetComponentInChildren<TMP_Text>().text == ">")
        {
            NumberOfShips++;
            NumToChange.text = NumberOfShips.ToString();
            if (Type.text == "Order transport ships")
            {
                Cost.text = (NumberOfShips * 500).ToString() + " UE";
            }
            else
            {
                Cost.text = (NumberOfShips * 1000).ToString() + " UE";
            }
        }
    }
}
