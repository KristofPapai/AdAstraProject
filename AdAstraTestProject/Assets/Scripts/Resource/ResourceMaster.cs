using System.Collections;
using System.Collections.Generic;
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

    Resources resourceMaster = new Resources(30000,30000,30000);
    public Camera mainCamera;

    [SerializeField] private float _duration = 5f;
    private float _timer = 0f;
    GameObject[] planets;



    void Start()
    {

    }

    private void OnGUI()
    {
        GUI.backgroundColor = Color.cyan;
        GUI.Label(new Rect((Screen.width)-100, Screen.height-90, 300, 50),"UniEuros:" + resourceMaster.UniEuros.ToString());
        GUI.Label(new Rect((Screen.width) - 100, Screen.height - 70, 300, 50), "Influence:" + resourceMaster.Influence.ToString());
        GUI.Label(new Rect((Screen.width) - 100, Screen.height - 50, 300, 50), "Tech:" + resourceMaster.Tech.ToString());


    }


    public double OutUniEuros = 0;
    public double OutInfluence = 0;
    public double OutTech = 0;


    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _duration)
        {
            planets = GameObject.FindGameObjectsWithTag("Celestial");
            //Debug.Log(planets.Length);
            _timer = 0f;
            //Debug.Log("intervall");
            foreach (GameObject planet in planets)
            {
                resourceMaster.AddUniEuros(planet.GetComponent<PlanetProperties>().GenUniEuros);
                resourceMaster.AddInfluence(planet.GetComponent<PlanetProperties>().GenInfluence);
                resourceMaster.AddTech(planet.GetComponent<PlanetProperties>().GenTech);

                OutUniEuros = resourceMaster.UniEuros;
                OutInfluence = resourceMaster.Influence;
                OutTech = resourceMaster.Tech;
            }
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
