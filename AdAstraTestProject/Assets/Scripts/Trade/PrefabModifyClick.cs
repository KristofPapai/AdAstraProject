using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrefabModifyClick : MonoBehaviour
{
    public GameObject CreateTradeRoute;
    public GameObject ModifyTradeRoute;
    public TradeRouteClass StoredRoute;

    private void Start()
    {
        //CreateTradeRoute = GameObject.Find("TextCreateANewTradeRoute");
        //ModifyTradeRoute = GameObject.Find("ModifyTradeRoute");
    }

    public void OnClick()
    {
        if (CreateTradeRoute.active)
        {
            CreateTradeRoute.SetActive(false);
            ModifyTradeRoute.SetActive(true);
            ModifyTradeRoute.GetComponent<SelectedTradeSave>().SelectedRoute = StoredRoute;
            RefreshInfo();
        }
    }


    public void RefreshInfo()
    {
        ModifyTradeRoute.GetComponent<TMP_Text>().text = "Modify Trade Route No.: " + StoredRoute.TradeRouteID;
        GameObject.Find("ModifyTradeRoute/PanelTransports/InputNumberPrefab/ChangeNumOfShips").GetComponent<TMP_Text>().text = StoredRoute.Transports.Count.ToString();
        GameObject.Find("ModifyTradeRoute/PanelPMC/InputNumberPrefab/ChangeNumOfShips").GetComponent<TMP_Text>().text = StoredRoute.PMCs.Count.ToString();



    }
}
