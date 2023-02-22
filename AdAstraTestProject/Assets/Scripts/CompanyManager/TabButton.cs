using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabButton : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler,IPointerExitHandler
{

    public TabGroup tabGroup;
    public Image background;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
        if (this.name == "Trade Routes")
        {
            GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().UpdateTradeInfo();
            GameObject.Find("ScriptMaster").GetComponent<FleetMaster>().UpdateTradeDropdown();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
