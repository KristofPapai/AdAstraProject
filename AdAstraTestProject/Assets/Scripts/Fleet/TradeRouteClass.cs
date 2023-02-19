using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class TradeRouteClass
{

    public int TradeRouteID;
    public string TradeRouteName;
    public string HomePlanet;
    public string TargetPlanet;
    List<TransportClass> Transports = new List<TransportClass>();
    List<PmcClass> PMCs = new List<PmcClass>();

    public TradeRouteClass()
    {
    }


    public int Cargocapacity()
    {
        int Capacity = 0;
        foreach (TransportClass transport in Transports)
        {
            Capacity += transport.CargoCapacity;
        }
        return Capacity;
    }

    public double FleetPower()
    {
        double Firepower = 0;
        foreach (TransportClass transport in Transports)
        {
            Firepower += transport.Firepower;
        }
        foreach (PmcClass pmc in PMCs)
        {
            Firepower += pmc.Firepower;
        }
        return Firepower;
    }

    public double FleetArmor()
    {
        double Armor = 0;
        foreach (TransportClass transport in Transports)
        {
            Armor += transport.Armor;
        }
        foreach (PmcClass pmc in PMCs)
        {
            Armor += pmc.Armor;
        }
        return Armor;
    }
}
