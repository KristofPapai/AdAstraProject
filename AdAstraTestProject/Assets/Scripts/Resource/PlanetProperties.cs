using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetProperties : MonoBehaviour
{
    public double GenUniEuros;
    public double GenInfluence;
    public double GenTech;
    public bool IsMotherPlanet = false;

    public List<string> RareMaterials = new List<string>() { "iron", "nickel", "iridium", "palladium", "platinum", "gold", "magnesium" }; // "iron", "nickel", "iridium", "palladium", "platinum", "gold", "magnesium"
    //public List<string> PlanetRareMaterials = new List<string>();
    public Dictionary<string, double> PlanetRareMaterials = new Dictionary<string, double>();


    public GameObject FloatingTextPrefab;

    void Start()
    {

        
        GenUniEuros = 0;
        GenInfluence = 0;
        GenTech = 0;

        int materialCount = Random.Range(1, 5);
        bool whileClose = false;
        int counter = 0;
        do
        {
            
            int tempIndex = Random.Range(0, RareMaterials.Count);
            if (!PlanetRareMaterials.Keys.Contains(RareMaterials[tempIndex]))
            {
                PlanetRareMaterials.Add(RareMaterials[tempIndex],Random.Range(10000,50000));
                counter++;
            }

        } while (counter != materialCount);
        if (IsMotherPlanet == true)
        {
            GenUniEuros = 20;
            GenInfluence = 5;
            GenTech = 2;
        }

    }

    void Update()
    {
        //MaterialListing();
    }

}
