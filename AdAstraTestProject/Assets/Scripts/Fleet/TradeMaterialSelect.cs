using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TradeMaterialSelect : MonoBehaviour
{
    public void OnClick()
    {
        GameObject outPlanet = GameObject.Find("OutSelectedMaterial");
        outPlanet.GetComponent<TMP_Text>().text = this.name;
    }
}
