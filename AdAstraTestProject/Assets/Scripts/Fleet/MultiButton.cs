using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MultiButton : MonoBehaviour
{
    public TMP_Text NumToChange;
    public TMP_Text Cost;
    public TMP_Text Type;


    public void MothOnClick()
    {

        int NumberOfShips = int.Parse(NumToChange.text);
        //Decrease
        if (this.GetComponentInChildren<TMP_Text>().text == "<")
        {
            if (NumberOfShips != 0)
            {
                NumberOfShips--;
                NumToChange.text = NumberOfShips.ToString();
                int outCost = 0;
                //Cost.text = (NumberOfShips * 500).ToString() + " UE";
                if (Type.text == "Transport fleet Manager")
                {
                    if (this.GetComponentInParent<Transform>().parent.name == "MothballPanel")
                    {
                        outCost = (int.Parse(Cost.text.Split(' ')[0]) - 5);
                        Cost.text = outCost.ToString() + " UE";
                    }
                    if (this.GetComponentInParent<Transform>().parent.name == "recrewpanel")
                    {
                        outCost = int.Parse(Cost.text.Split(' ')[0]) -10;
                        Cost.text = outCost.ToString() + " UE";
                    }
                    else
                    {
                        //scrapship
                        outCost += 0;
                    }
                }
                else
                {
                    if (this.GetComponentInParent<Transform>().parent.name == "MothballPanel")
                    {
                        outCost = (int.Parse(Cost.text.Split(' ')[0]) - 20);
                        Cost.text = outCost.ToString() + " UE";
                    }
                    if (this.GetComponentInParent<Transform>().parent.name == "recrewpanel")
                    {
                        outCost = int.Parse(Cost.text.Split(' ')[0]) - 50;
                        Cost.text = outCost.ToString() + " UE";
                    }
                    else
                    {
                        //scrapship
                        outCost += 0;
                    }
                }
            }
        }
        if (this.GetComponentInChildren<TMP_Text>().text == ">") 
        {
            NumberOfShips++;
            NumToChange.text = NumberOfShips.ToString();
            int outCost = 0;
            if (Type.text == "Transport fleet Manager")
            {
                if (this.GetComponentInParent<Transform>().parent.name == "MothballPanel")
                {
                    outCost = int.Parse(Cost.text.Split(' ')[0]) + 5;
                    Cost.text = outCost.ToString() + " UE";
                }
                if (this.GetComponentInParent<Transform>().parent.name == "recrewpanel")
                {
                    outCost = int.Parse(Cost.text.Split(' ')[0]) + 10;
                    Cost.text = outCost.ToString() + " UE";
                }
                else
                {
                    //scrapship
                    outCost += 0;
                }
            }
            else
            {
                if (this.GetComponentInParent<Transform>().parent.name == "MothballPanel")
                {
                    outCost = int.Parse(Cost.text.Split(' ')[0]) + 20;
                    Cost.text = outCost.ToString() + " UE";
                }
                if (this.GetComponentInParent<Transform>().parent.name == "recrewpanel")
                {
                    outCost = int.Parse(Cost.text.Split(' ')[0]) + 50;
                    Cost.text = outCost.ToString() + " UE";
                }
                else
                {
                    //scrapship
                    outCost += 0;
                }
            }
        }

    }



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
