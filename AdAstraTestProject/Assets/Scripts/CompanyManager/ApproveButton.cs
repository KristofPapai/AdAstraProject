using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ApproveButton : MonoBehaviour
{
    public TMP_Text Mothball;
    public TMP_Text Recrew;
    public TMP_Text Scrap;
    public TMP_Text Cost;

    public TMP_Text CurrentFleetSize;
    public TMP_Text CurrentMothBallSize;

    public GameObject NotEnougUEText;


    public void OnClick()
    {
        if (EnougUE(double.Parse(Cost.text.Split(' ')[0]))
            && int.Parse(CurrentFleetSize.text) - (int.Parse(Scrap.text)+int.Parse(Mothball.text))>= 0
            && int.Parse(CurrentMothBallSize.text) - int.Parse(Recrew.text) >=-0)
        {

            GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().NumOfTransport -= (int.Parse(Scrap.text) + int.Parse(Mothball.text));
            GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().NumMothTransport -= int.Parse(Recrew.text);
            GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().NumMothTransport += int.Parse(Mothball.text);
            GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().NumOfTransport += int.Parse(Recrew.text);
        }
        else
        {
            StartCoroutine(Warning());
        }
    }

    public void OnClickPMC()
    {
        if (EnougUE(double.Parse(Cost.text.Split(' ')[0]))
            && int.Parse(CurrentFleetSize.text) - (int.Parse(Scrap.text) + int.Parse(Mothball.text)) >= 0
            && int.Parse(CurrentMothBallSize.text) - int.Parse(Recrew.text) >= -0)
        {

            GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().NumOfPMC -= (int.Parse(Scrap.text) + int.Parse(Mothball.text));
            GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().NummothPMC -= int.Parse(Recrew.text);
            GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().NummothPMC += int.Parse(Mothball.text);
            GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().NumOfPMC += int.Parse(Recrew.text);
        }
        else
        {
            StartCoroutine(Warning());
        }
    }

    public bool EnougUE(double UEAmount)
    {
        if (GameObject.Find("ScriptMaster").GetComponent<ResourceMaster>().OutUniEuros - UEAmount >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    IEnumerator Warning()
    {
        NotEnougUEText.SetActive(true);
        yield return new WaitForSeconds(0.5F);
        NotEnougUEText.SetActive(false);
        yield return new WaitForSeconds(0.5F);
        NotEnougUEText.SetActive(true);
        yield return new WaitForSeconds(0.5F);
        NotEnougUEText.SetActive(false);
        yield return new WaitForSeconds(0.5F);
        NotEnougUEText.SetActive(true);
        yield return new WaitForSeconds(0.5F);
        NotEnougUEText.SetActive(false);

    }

}
