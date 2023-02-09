using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    public string Name;
    public int Level;
    public double UniEurosPrice;
    public double InfluencePrice;
    public double TechPrice;
    public double UniEurosGenerate;
    public double InfluenceGenerate;
    public double TechGenerate;
    public float BuildingTime;

    public Building(string name, int level, double uniEurosPrice, double influencePrice, double techPrice, double uniEurosGenerate, double influenceGenerate, double techGenerate, float buildingTime)
    {
        Name = name;
        Level = level;
        UniEurosPrice = uniEurosPrice;
        InfluencePrice = influencePrice;
        TechPrice = techPrice;
        UniEurosGenerate = uniEurosGenerate;
        InfluenceGenerate = influenceGenerate;
        TechGenerate = techGenerate;
        BuildingTime = buildingTime;
    }

    public Building()
    {
    }
}
