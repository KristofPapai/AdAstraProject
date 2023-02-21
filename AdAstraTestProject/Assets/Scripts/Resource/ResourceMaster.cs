using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceMaster : MonoBehaviour
{

    public class Resources
    {

        private double uniEuros;
        private double influence;
        private double tech;

        public double UniEuros { get => uniEuros; set => uniEuros = value; }
        public double Influence { get => influence; set => influence = value; }
        public double Tech { get => tech; set => tech = value; }

        public Resources()
        {
        
        }

        public Resources(double uniEuros, double influence, double tech)
        {
            UniEuros = uniEuros;
            Influence = influence;
            Tech = tech;
        }


        public void AddUniEuros(double ammount)
        {
            UniEuros += ammount;
        }
        public void AddInfluence(double ammount)
        {
            Influence += ammount;
        }
        public void AddTech(double ammount)
        {
            Tech += ammount;
        }


        public void MakeNewTech(double costUniEuro, double costTech)
        {
            if (UniEuros - costUniEuro >= 0 && Tech - costTech >= 0)
            {
                UniEuros -= costUniEuro;
                Tech -= costTech;
            }
        }

        public void MakeDecision(double costUniEuro, double costInfluence)
        {
            if (UniEuros - costUniEuro >= 0 && Influence - costInfluence >= 0)
            {
                UniEuros -= costUniEuro;
                Influence -= costInfluence;
            }
        }

        public void BuildStructure(double costUniEuro, double costInfluence, double costTech)
        {
            if (UniEuros - costUniEuro >= 0 && Influence - costInfluence >= 0 && Tech - costTech >= 0)
            {
                UniEuros -= costUniEuro;
                Influence -= costInfluence;
                Tech -= costTech;
            }
        }
    }
    
    static Resources resourceMaster = new Resources(30000,30000,30000);
    public Camera mainCamera;

    [SerializeField] private float _duration = 5f;
    private float _timer = 0f;
    GameObject[] planets;



    public TextMeshProUGUI UniEuros;
    public TextMeshProUGUI Influence;
    public TextMeshProUGUI Tech;

    void Start()
    {
        
    }



    private void OnGUI()
    {
        UniEuros.text = resourceMaster.UniEuros.ToString();
        Influence.text = resourceMaster.Influence.ToString();
        Tech.text = resourceMaster.Tech.ToString();
        
    }


    public double OutUniEuros = resourceMaster.UniEuros;
    public double OutInfluence = resourceMaster.Influence;
    public double OutTech = resourceMaster.Tech;

    
    
    


    public TextMeshProUGUI genuni;
    public TextMeshProUGUI gentech;
    public TextMeshProUGUI geninf;
    public TMP_Text fleetUpkeep;


    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _duration)
        {
            double generatedunieuros = 0;
            double generatedtech = 0;
            double generatedinfluence = 0;

            planets = GameObject.FindGameObjectsWithTag("Celestial");
            _timer = 0f;
            generatedunieuros -= this.GetComponent<FleetMaster>().FullUpkeep;
            foreach (GameObject planet in planets)
            {
                //resourceMaster.AddUniEuros(planet.GetComponent<PlanetProperties>().GenUniEuros);
                //resourceMaster.AddInfluence(planet.GetComponent<PlanetProperties>().GenInfluence);
                //resourceMaster.AddTech(planet.GetComponent<PlanetProperties>().GenTech);
                generatedunieuros += planet.GetComponent<PlanetProperties>().GenUniEuros;
                generatedtech += planet.GetComponent<PlanetProperties>().GenTech;
                generatedinfluence += planet.GetComponent<PlanetProperties>().GenInfluence;
            }
            OutUniEuros = resourceMaster.UniEuros;
            OutInfluence = resourceMaster.Influence;
            OutTech = resourceMaster.Tech;
            genuni.text = "" + generatedunieuros;
            gentech.text = "" + generatedtech;
            geninf.text = "" + generatedinfluence;
            resourceMaster.AddUniEuros(generatedunieuros);
            resourceMaster.AddInfluence(generatedtech);
            resourceMaster.AddTech(generatedinfluence);

        }
    }

    public void AddUniEuros(double amount)
    {

        resourceMaster.AddUniEuros(amount);

    }

    public void AddTech(double amount)
    {

        resourceMaster.AddTech(amount);

    }

    public void AddInfluence(double amount)
    {

        resourceMaster.AddInfluence(amount);

    }
}
