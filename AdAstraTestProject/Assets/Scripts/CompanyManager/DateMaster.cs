using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateMaster : MonoBehaviour
{
    DateTime currentDate;
    void Start()
    {
        currentDate = DateTime.Parse("2100.01.01");
    }


    public float timeRate = 1f;
    private float nextDay = 0.0f;
    public GameObject outDate;
    void Update()
    {
        if (Time.time > nextDay)
        {
            nextDay = Time.time + timeRate;
            currentDate = currentDate.AddDays(1);
            outDate.GetComponent<TMP_Text>().text = currentDate.ToString("yyyy.MM.dd");

        }
    }
}
