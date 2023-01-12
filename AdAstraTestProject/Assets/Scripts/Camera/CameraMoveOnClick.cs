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
    private bool chaseCam = false;
    private bool reset = false;


    private bool camResetMode = false;
    private bool GUIControl = false;
    private bool startCam = true;

    private Vector3 scrollViewVector = Vector3.zero;
    public Rect dropDownRect = new Rect(125, 50, 125, 300);

    public TMP_Dropdown dropdown;
   

    int indexNumber;
    bool show = false;


    public void CameraReset()
    {
        mainCamera.transform.position = cameraStartPos;
    }
    
    public void CameraControll()
    {
        if (Input.GetMouseButtonDown(0))
        {
            camResetMode = false;
            GUIControl = false;
            startCam = false;
            moveToCelestial = true;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.SphereCast(ray,30, out hit, 10000f) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.transform.tag == "Celestial" && selected != hit.transform.gameObject && hit.transform.gameObject.layer != 4)
                {
                    selected = hit.transform.gameObject;
                    Debug.Log("Celestial Hit");
                    moveToCelestial = true;
                    chaseCam = false;
                    List<string> availablebuildings = selected.GetComponent<BuildingMaster>().AbleToBuild;

                    PopulateDropdown(dropdown, availablebuildings);
                }
            }
        }
        if (moveToCelestial)
        {
            var targetRotation = Quaternion.LookRotation(selected.transform.position - transform.position);
            var step = speed * Time.deltaTime;
            float distance = Vector3.Distance(mainCamera.transform.position, new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40));
            if (distance != 0)
            {
                mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40), step);
                //Debug.Log("shake");
            }
            else
            {
                chaseCam = true;

            }
            if (chaseCam)
            {
                mainCamera.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40);
                for (var i = ScrollBuilding.transform.childCount - 1; i >= 0; i--)
                {
                    Object.Destroy(ScrollBuilding.transform.GetChild(i).gameObject);
                }
                

            }
        }
        else
        {
            GUIControl = true;
        }
        if (Input.GetKey("r"))
        {
            CameraReset();
            moveToCelestial = false;
            camResetMode = true;
            GUIControl = true;
        }


    }


    public GameObject buttonPrefab;
    public GameObject canvas;
    public GameObject verticalLayout;

    public GameObject ScrollBuilding;

    void Start()
    {
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

            //buttonStack += 30;
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
        

        PopulateDropdown(dropdown,availablebuildings);




        float distance = Vector3.Distance(mainCamera.transform.position, new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40));
        var step = speed * Time.deltaTime;
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40), step);
        distance = Vector3.Distance(mainCamera.transform.position, new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40));

        if (distance < 10)
        {
            camResetMode = true;
            mainCamera.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40);
            this.gameObject.transform.LookAt(GameObject.Find("Cam").transform);
        }
    }

    void PopulateDropdown(TMP_Dropdown dropdown, List<string> options)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(options);

    }

    void Update()
    {
        CameraControll();
    }


}
