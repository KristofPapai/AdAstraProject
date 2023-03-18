using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PriceMaster : MonoBehaviour
{

    public TMP_Text[] Buys;
    public TMP_Text[] Sells;

    public void Start()
    {
        for (int i = 0; i < Buys.Length; i++)
        {
            double randomDouble = Random.Range(50, 100);
            Buys[i].text = (double.Parse(Buys[i].text) + randomDouble).ToString("#.00");
            randomDouble = Random.Range(50, 100);
            Sells[i].text = (double.Parse(Sells[i].text) + randomDouble).ToString("#.00"); 
        }
    }


    public void PriceManipulatorBuySell()
    {
        for (int i = 0; i < Buys.Length; i++)
        {
            double randomDouble = Random.Range(-0.5f, 0.5f);
            if ((double.Parse(Buys[i].text) + randomDouble) >= 0)
            {
                Buys[i].text = (double.Parse(Buys[i].text) + randomDouble).ToString("#.00");
            }
            randomDouble = Random.Range(-0.5f, 0.5f);
            if ((double.Parse(Sells[i].text) + randomDouble) >= 0)
            {
                Sells[i].text = (double.Parse(Sells[i].text) + randomDouble).ToString("#.00");
            }
        }
    }

    private float _timer = 0f;
    public void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1f)
        {
            _timer = 0f;
            PriceManipulatorBuySell();

        }
    }


}
