using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZeroAmounts : MonoBehaviour
{
    public TMP_Text mothball;
    public TMP_Text recrew;
    public TMP_Text scrap;



    public void OnClick()
    {
        mothball.text = "0";
        recrew.text = "0";
        scrap.text = "0";
    }
}
