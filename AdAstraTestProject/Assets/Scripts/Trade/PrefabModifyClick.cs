using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabModifyClick : MonoBehaviour
{
    private GameObject CreateTradeRoute;
    private GameObject ModifyTradeRoute;
    public TradeRouteClass StoredRoute;

    private void Start()
    {
        CreateTradeRoute = GameObject.Find("TextCreateANewTradeRoute");
        ModifyTradeRoute = GameObject.Find("ModifyTradeRoute");
    }

    public void OnClick()
    {
        if (CreateTradeRoute.active)
        {
            CreateTradeRoute.SetActive(false);
            ModifyTradeRoute.SetActive(true);

        }
        //CreateTradeRoute.SetActive(false);
    }
}
