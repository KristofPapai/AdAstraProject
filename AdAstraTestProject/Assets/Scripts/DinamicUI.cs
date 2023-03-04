using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DinamicUI : MonoBehaviour
{

    public TextMeshProUGUI output;

    public GameObject selected;
    public void HandleInput(int val)
    {
        string PlanetName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text;
        selected = GameObject.Find(PlanetName);

        //selected.GetComponent<BuildingMaster>().AbleToBuild.RemoveAt(val);

    }

}
