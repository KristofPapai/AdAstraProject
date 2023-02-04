using System.Collections;
using System.Collections.Generic;
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
    GameObject selected;
    public float speed = 100.0f;
    private bool moveToCelestial = false;
    public Rect dropDownRect = new Rect(125, 50, 125, 300);
    public TMP_Dropdown dropdown;
    public bool chaseCam = false;
    public GameObject CelestialPropPanel;




    public void CameraReset()
    {
        mainCamera.transform.position = cameraStartPos;
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
                    List<string> availablebuildings = selected.GetComponent<BuildingMaster>().AbleToBuild;
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
            moveToCelestial = false;
            CelestialPropPanel.SetActive(false);

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
        float h = canvas.GetComponent<RectTransform>().rect.height;
        float w = canvas.GetComponent<RectTransform>().rect.width;
        foreach (GameObject planet in planets)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(verticalLayout.transform);
            button.transform.localPosition = new Vector3((-60)-(w/3), (h/2) - buttonStack, 0);
            button.transform.localScale = new Vector3(1f,1f,1f);
            button.transform.rotation = canvas.transform.rotation;
            button.GetComponent<Button>().onClick.AddListener(OnClick);
            button.GetComponentInChildren<TMP_Text>().text = planet.name;

        }

    }

    void OnClick()
    {
        string PlanetName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text;
        selected = GameObject.Find(PlanetName);
        List<string> availablebuildings = selected.GetComponent<BuildingMaster>().AbleToBuild;
        mainCamera.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40);
        moveToCelestial = true;
        CelestialPropPanel.SetActive(true);
        SideGuiMaster(selected);
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
        List<string> selectedRareMaterials = Selected.GetComponent<PlanetProperties>().PlanetRareMaterials;
        selected.GetComponent<BuildingMaster>().clickToPlanet();
        string builder = "";
        foreach (string material in selectedRareMaterials)
        {
            builder += material + "\n";
        }
        ListPlanetMaterials.text = builder.ToLower();
    }
}
