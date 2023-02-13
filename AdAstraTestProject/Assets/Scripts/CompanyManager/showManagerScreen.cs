using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showManagerScreen : MonoBehaviour
{
    public GameObject CompanyManagerPanel;
    public GameObject CelestialPropsPanel;

    public void OnClick()
    {
        if (CompanyManagerPanel.active)
        {
            CompanyManagerPanel.SetActive(false);
        }
        else
        {
            refresh();
        }
    }

    public void refresh()
    {
        CompanyManagerPanel.SetActive(true);
        GameObject.Find("ScriptMaster").GetComponent<CameraMoveOnClick>().CameraReset();
    }
}
