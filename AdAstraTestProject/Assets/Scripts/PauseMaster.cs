using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMaster : MonoBehaviour
{

    public GameObject PauseMenu;
    private void Update()
    {
        
        if (Input.GetKeyDown("escape"))
        {
            if (PauseMenu.active == true)
            {
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
            }

        }
    }
}
