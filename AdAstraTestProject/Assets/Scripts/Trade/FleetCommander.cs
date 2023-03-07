using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetCommander : MonoBehaviour
{
    public TradeRouteClass SpecificTrade;
    private bool InTransit = false;
    private bool OnHomePlanet = true;
    private bool LoadedUp = false;
    GameObject HomePlanet;
    GameObject TargetPlanet;

    private void Start()
    {
        HomePlanet = GameObject.Find(SpecificTrade.HomePlanet);
        TargetPlanet = GameObject.Find(SpecificTrade.TargetPlanet);
    }

    //ez alapján határozzuk meg mennyi tick lesz a travel time
    public float CalculateDistance()
    {
        float distance = Vector3.Distance(HomePlanet.transform.position, TargetPlanet.transform.position);
        return distance;
    }

    public void FixedUpdate()
    {
        if (OnHomePlanet == true)
        {
            OnHomePlanet = false;
            InTransit = true;
            StartCoroutine(Transit());
        }
        if (InTransit == false && OnHomePlanet == false)
        {
            InTransit = true;
            StartCoroutine(CargoLoading());
        }
        if (LoadedUp == true && OnHomePlanet == false)
        {
            LoadedUp = false;
            StartCoroutine(TransitToHome());
            
        }

    }

    IEnumerator Transit()
    {
        float distance = CalculateDistance();
        Debug.Log("Transit The fleet is in transit with the distance to cover : "+distance);
        distance = 10;
        yield return new WaitForSeconds(distance);
        InTransit = false;
        Debug.Log("Vége a transitnak");


    }

    IEnumerator TransitToHome()
    {
        float distance = CalculateDistance();
        Debug.Log("HomeTransit The fleet is in transit with the distance to cover : " + distance);
        distance = 10;
        yield return new WaitForSeconds(distance);
        InTransit = false;
        OnHomePlanet = true;
        LoadedUp = false;
        HomePlanet.GetComponent<BuildingMaster>().AddHomeStockpile(SpecificTrade.TransportedMaterials[0],SpecificTrade.LoadedCargo);
        Debug.Log("Deload");

    }

    IEnumerator CargoLoading()
    {
        float LoadUpTime = (float)SpecificTrade.Cargocapacity()/2;
        double LoadedMaterial = 0;
        if (TargetPlanet.GetComponent<BuildingMaster>().stockpile != null)
        {
            LoadedMaterial = TargetPlanet.GetComponent<BuildingMaster>().DeductStockpile(SpecificTrade.TransportedMaterials[0], SpecificTrade.Cargocapacity());
        }
        SpecificTrade.LoadedCargo = (int)LoadedMaterial;
        yield return new WaitForSeconds(LoadUpTime);
        LoadedUp = true;
        InTransit = true;
        Debug.Log("Sikeres Load");
        Debug.Log(LoadedMaterial);
    }

}
