using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    public TabButton selectedTabButton;
    public List<GameObject> ObjectsToSwap;
    
    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTabButton != null || button != selectedTabButton)
        {
            button.background.color = tabHover;
        }
    }
    public void OnTabExit(TabButton button)
    {
        ResetTabs();
        
    }
    public void OnTabSelected(TabButton button)
    {
        ResetTabs();
        button.background.color = tabActive;
        selectedTabButton = button;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < ObjectsToSwap.Count; i++)
        {
            if (i == index)
            {
                ObjectsToSwap[i].SetActive(true);
            }
            else
            {
                ObjectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTabButton != null && button == selectedTabButton)
            {
                continue;
            }
            button.background.color = tabIdle;
        }
    }
}
