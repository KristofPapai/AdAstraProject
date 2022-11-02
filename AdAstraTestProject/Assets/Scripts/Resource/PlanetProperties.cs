using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetProperties : MonoBehaviour
{
    public double GenUniEuros;
    public double GenInfluence;
    public double GenTech;

    List<string> RareMaterials = new List<string>() { "iron", "nickel", "iridium", "palladium", "platinum", "gold", "magnesium" }; // "iron", "nickel", "iridium", "palladium", "platinum", "gold", "magnesium"
    List<string> PlanetRareMaterials = new List<string>();


    public GameObject FloatingTextPrefab;

    void Start()
    {

        
        GenUniEuros = Random.Range(1, 11);
        GenInfluence = Random.Range(1, 11);
        GenTech = Random.Range(1, 11);

        int materialCount = Random.Range(1, 5);
        bool whileClose = false;
        int counter = 0;
        do
        {
            int tempIndex = Random.Range(0, RareMaterials.Count);
            if (!PlanetRareMaterials.Contains(RareMaterials[tempIndex]))
            {
                PlanetRareMaterials.Add(RareMaterials[tempIndex]);
                counter++;
            }

        } while (counter != materialCount);

    }

    void Update()
    {
        //MaterialListing();
    }


    public void OnGUI()
    {
        MaterialListing();
    }

    public void MaterialListing()
    {


        if (Vector3.Distance(GameObject.Find("Cam").transform.position, this.transform.position) < 70)
        {
            GUI.backgroundColor = Color.red;
            string builder = "";
            foreach (string material in PlanetRareMaterials)
            {
                builder += material + "\n";
            }

            GUI.Label(new Rect(((Screen.width)/2) - 300, (Screen.height/2) - 100, 300, 300), builder);
        }
        

    }
}
