using System.Collections;
using System.Collections.Generic;
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
    
    
    

    void Start()
    {
        cameraStartPos = mainCamera.transform.position;
    }



    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.transform.tag == "Celestial" && selected != hit.transform.gameObject && hit.transform.gameObject.layer != 4)
                {
                    selected = hit.transform.gameObject;
                    Debug.Log("Celestial Hit");
                    moveToCelestial=true;
                    chaseCam = false;
                }
            }
        }
        if (moveToCelestial)
        {
            var targetRotation = Quaternion.LookRotation(selected.transform.position - transform.position);
            var step = speed * Time.deltaTime;
            float distance = Vector3.Distance(mainCamera.transform.position, new Vector3(selected.transform.position.x, selected.transform.position.y+40, selected.transform.position.z-40));
            if (distance != 0)
            {
                mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z - 40), step);
                //mainCamera.transform.LookAt(selected.transform);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
                Debug.Log("shake");
            }
            else
            {
                chaseCam = true;
                
            }
            if (chaseCam)
            {
                mainCamera.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y + 40, selected.transform.position.z-40);
                //mainCamera.transform.LookAt(selected.transform);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            }
        }

        if (Input.GetKey("r"))
        {
            reset = true;
        }
        if (reset)
        {
            moveToCelestial = false;
            var step = speed * Time.deltaTime;
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, cameraStartPos, step);
        }
        if (reset && mainCamera.transform.position == cameraStartPos)
        {
            reset = false;
        }

    }
}
