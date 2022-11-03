using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


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
   

    int indexNumber;
    bool show = false;


    private void OnGUI()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Celestial");
        if (startCam == false)
        {


            if (GUI.Button(new Rect((dropDownRect.x - 100), dropDownRect.y, dropDownRect.width, 25), ""))
            {
                if (!show)
                {
                    show = true;
                }
                else
                {
                    show = false;
                }
            }
            if (show)
            {
                scrollViewVector = GUI.BeginScrollView(new Rect((dropDownRect.x - 100), (dropDownRect.y + 25), dropDownRect.width, dropDownRect.height), scrollViewVector, new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (planets.Length * 25))));
                GUI.Box(new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (planets.Length * 25))), "");
                for (int index = 0; index < planets.Length; index++)
                {
                    if (GUI.Button(new Rect(0, (index * 25), dropDownRect.height, 25), ""))
                    {
                        show = false;
                        indexNumber = index;
                    }
                    GUI.Label(new Rect(5, (index * 25), dropDownRect.height, 25), planets[index].name);
                }
                GUI.EndScrollView();
            }
            else
            {
                GUI.Label(new Rect((dropDownRect.x - 95), dropDownRect.y, 300, 25), planets[indexNumber].name);
                //Debug.Log(planets[indexNumber].name);

                if (GUIControl)
                {
                    selected = GameObject.Find(planets[indexNumber].name);

                    float distance = Vector3.Distance(mainCamera.transform.position, new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40));
                    if (camResetMode == false)
                    {
                        //selected = GameObject.Find(planets[indexNumber].name);
                        var step = speed * Time.deltaTime;
                        //mainCamera.transform.LookAt(selected.transform);
                        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40), step);
                        distance = Vector3.Distance(mainCamera.transform.position, new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40));

                    }
                    if (distance < 10)
                    {
                        camResetMode = true;
                        mainCamera.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40);
                        this.gameObject.transform.LookAt(GameObject.Find("Cam").transform);
                    }
                }
            }
        }
    }

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
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.SphereCast(ray,30, out hit, 1000f) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.transform.tag == "Celestial" && selected != hit.transform.gameObject && hit.transform.gameObject.layer != 4)
                {
                    selected = hit.transform.gameObject;
                    //Debug.Log("Celestial Hit");
                    moveToCelestial = true;
                    chaseCam = false;
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

    void Start()
    {
        cameraStartPos = mainCamera.transform.position;
    }



    void Update()
    {
        CameraControll();
    }


}
