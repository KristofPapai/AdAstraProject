using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTradeRoutes : MonoBehaviour
{
    private FleetMaster FleetMaster;
    private bool DrawOrHide = false;
    public LineRenderer Line;

    private void Start()
    {
        FleetMaster = GameObject.Find("ScriptMaster").GetComponent<FleetMaster>();
    }

    public void OnClick()
    {
        if (DrawOrHide == false)
        {
            DrawOrHide = true;
        }
        else
        {
            DrawOrHide = false;
        }
        
    }


    public void FixedUpdate()
    {

        GameObject[] AllTradeGameObjects = GameObject.FindGameObjectsWithTag("LineElement");

        if (DrawOrHide == true)
        {
            for (int i = 0; i < AllTradeGameObjects.Length; i++)
            {
                AllTradeGameObjects[i].GetComponent<FleetCommander>().Line.enabled = true;
                AllTradeGameObjects[i].GetComponent<FleetCommander>().DrawLine();
            }
        }
        else
        {
            for (int i = 0; i < AllTradeGameObjects.Length; i++)
            {
                AllTradeGameObjects[i].GetComponent<FleetCommander>().Line.enabled = false;
            }
        }
    }
}
