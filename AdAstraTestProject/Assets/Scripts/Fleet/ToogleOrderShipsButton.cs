using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToogleOrderShipsButton : MonoBehaviour
{
    public GameObject Panel;
    public TMP_Text NumOfShips;
    public GameObject Lore;
    public TMP_Text Amount;
    public void OnClick()
    {
        NumOfShips.text = "0";
        Amount.text = "0 UE";
        if (Panel.active == false)
        {
            Panel.SetActive(true);
            Lore.SetActive(false);
        }
        else
        {
            Panel.SetActive(false);
            Lore.SetActive(true);
        }
      
    }
}
