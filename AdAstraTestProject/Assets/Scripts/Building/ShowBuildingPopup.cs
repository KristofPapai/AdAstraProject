using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBuildingPopup : MonoBehaviour
{
    public GameObject PopUp;
    public void ButtonPressed() 
    {
        if (PopUp.active)
        {
            PopUp.SetActive(false);
        }
        else
        {
            PopUp.SetActive(true);
        }
    }
   
}
