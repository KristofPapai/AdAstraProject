using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraMoveOnClick : MonoBehaviour
{
    public Camera mainCamera;
    Vector3 cameraStartPos;

    public GameObject selected;
    public float speed = 100.0f;
    private bool moveToCelestial = false;
    public Rect dropDownRect = new Rect(125, 50, 125, 300);
    public TMP_Dropdown dropdown;
    public bool chaseCam = false;
    public GameObject CelestialPropPanel;




    public void CameraReset()
    {
        mainCamera.transform.position = cameraStartPos;
        //mainCamera.transform.Rotate(+19.21f, 0f, 0f);
        moveToCelestial = false;
        CelestialPropPanel.SetActive(false);
    }
    
    public void CameraControll()
    {

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 30, out hit, 10000f) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.transform.tag == "Celestial" && selected != hit.transform.gameObject && hit.transform.gameObject.layer != 4)
                {
                    selected = hit.transform.gameObject;
                    //List<string> availablebuildings = selected.GetComponent<BuildingMaster>().AbleToBuild;
                    SideGuiMaster(selected);
                    moveToCelestial = true;
                    CelestialPropPanel.SetActive(true);
                }
            }
        }
        if (moveToCelestial)
        {
            mainCamera.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40);
        }
        if (Input.GetKey("r"))
        {
            CameraReset();
        }
    }


    public GameObject buttonPrefab;
    public GameObject canvas;
    public GameObject verticalLayout;

    public GameObject ScrollBuilding;

    void Start()
    {
        CelestialPropPanel.SetActive(false);
        cameraStartPos = mainCamera.transform.position;
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Celestial");
        float buttonStack = 15;
        //float h = canvas.GetComponent<RectTransform>().rect.height;
        //float w = canvas.GetComponent<RectTransform>().rect.width;
        foreach (GameObject planet in planets)
        {
            GameObject button = Instantiate(buttonPrefab, verticalLayout.transform);
            button.transform.SetParent(verticalLayout.transform);
            button.GetComponent<Button>().onClick.AddListener(OnClick);
            button.GetComponentInChildren<TMP_Text>().text = planet.name;

        }

    }
    public GameObject CompanyManagerScreen;
    void OnClick()
    {
        //mainCamera.transform.Rotate(-19.21f, 0f, 0f);
        string PlanetName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text;
        selected = GameObject.Find(PlanetName);
        //List<string> availablebuildings = selected.GetComponent<BuildingMaster>().AbleToBuild;
        mainCamera.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40);
        moveToCelestial = true;
        CelestialPropPanel.SetActive(true);
        SideGuiMaster(selected);
        if (CompanyManagerScreen.active)
        {
           CompanyManagerScreen.SetActive(false);
        }
        if (selected.GetComponent<PlanetProperties>().IsMotherPlanet == true)
        {
            CelestialPropPanel.SetActive(false);
        }
    }



    void Update()
    {
        CameraControll();
    }


    //Gameobjects for sidepanel
    public TMP_Text TextPlanetName;
    public TMP_Text ListPlanetMaterials;
    public TMP_Text TextOperationLevel;
    public TMP_Text TextLocalStockPile;
    public TMP_Text ListLocalStockPile;
    public GameObject popup;
    void SideGuiMaster(GameObject Selected)
    {
        GameObject.Find("AvailableOpBuildings").GetComponent<ShowBuildingPopup>().killchildren();
        if (popup.active)
        {
            popup.SetActive(false);
        }
        TextPlanetName.text = Selected.transform.name;
        Selected.GetComponent<BuildingMaster>().clickToPlanet();
        refreshPlanetMaterial(Selected);
        if (selected.GetComponent<BuildingMaster>().classBuiltGroundBuildings.Any(x => (x.Name == "Warehouses")))
        {
            stockpileListing(selected);
        }
        else
        {
            ListLocalStockPile.text = "///none///";
        }
        buildingListing(selected);

    }

    public void refreshPlanetMaterial(GameObject Selected)
    {
        Dictionary<string, double> selectedRareMaterials = Selected.GetComponent<PlanetProperties>().PlanetRareMaterials;
        string builder = "";
        foreach (var material in selectedRareMaterials)
        {
            builder += material.Key + " - " + material.Value + " unit" + "\n";
        }
        ListPlanetMaterials.text = builder.ToLower();
    }




    public void stockpileListing(GameObject selected) 
    {
        ListLocalStockPile.text = "";
        foreach (var item in selected.GetComponent<BuildingMaster>().stockpile)
        {
            ListLocalStockPile.text += item.Key + " = " + item.Value + " unit" + "\n";
        }
    }


    public TMP_Text outBuiltBuilding;
    public void buildingListing(GameObject Selected)
    {
        outBuiltBuilding.text = "";
        foreach (Building building in selected.GetComponent<BuildingMaster>().classBuiltGroundBuildings)
        {
            outBuiltBuilding.text += building.Name + "\n";
        }

    }
}
