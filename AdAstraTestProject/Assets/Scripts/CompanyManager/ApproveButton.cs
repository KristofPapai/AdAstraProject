using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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

    
    FleetMaster FleetMaster;

    public void OnClick()
    {
        FleetMaster = GameObject.Find("ScriptMaster").GetComponent<FleetMaster>();
        if (EnougUE(double.Parse(Cost.text.Split(' ')[0]))
            && int.Parse(CurrentFleetSize.text) - (int.Parse(Scrap.text)+int.Parse(Mothball.text))>= 0
            && int.Parse(CurrentMothBallSize.text) - int.Parse(Recrew.text) >=-0)
        {

            FleetMaster.NumOfTransport -= (int.Parse(Scrap.text) + int.Parse(Mothball.text));
            FleetMaster.NumMothTransport -= int.Parse(Recrew.text);
            FleetMaster.NumMothTransport += int.Parse(Mothball.text);
            FleetMaster.NumOfTransport += int.Parse(Recrew.text);


            int x = 0;
            foreach (TransportClass transport in FleetMaster.Transportships)
            {
                if (x < int.Parse(Recrew.text) && transport.Status == "mothball")
                {
                    transport.Status = "standby";
                    x++;
                }
            }

            int i = 0;
            foreach (TransportClass transport in FleetMaster.Transportships)
            {
                if (i < int.Parse(Mothball.text)&& transport.Status != "mothball")
                {
                    transport.Status = "mothball";
                    i++;
                }
            }
            int removeCount = int.Parse(Scrap.text);
            int listLength = FleetMaster.Transportships.Count - removeCount;
            FleetMaster.Transportships.RemoveRange(listLength, removeCount);



        }
        else
        {
            StartCoroutine(Warning());
        }
    }

    public void OnClickPMC()
    {
        FleetMaster = GameObject.Find("ScriptMaster").GetComponent<FleetMaster>();
        if (EnougUE(double.Parse(Cost.text.Split(' ')[0]))
            && int.Parse(CurrentFleetSize.text) - (int.Parse(Scrap.text) + int.Parse(Mothball.text)) >= 0
            && int.Parse(CurrentMothBallSize.text) - int.Parse(Recrew.text) >= -0)
        {

            int x = 0;
            foreach (PmcClass pmc in FleetMaster.PMCships)
            {
                if (x < int.Parse(Recrew.text) && pmc.Status == "mothball")
                {
                    pmc.Status = "standby";
                    x++;
                }
            }

            int i = 0;
            foreach (PmcClass pmc in FleetMaster.PMCships)
            {
                if (i < int.Parse(Mothball.text) && pmc.Status != "mothball")
                {
                    pmc.Status = "mothball";
                    i++;
                }
            }
            int removeCount = int.Parse(Scrap.text);
            int listLength = FleetMaster.PMCships.Count - removeCount;
            FleetMaster.PMCships.RemoveRange(listLength, removeCount);
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
