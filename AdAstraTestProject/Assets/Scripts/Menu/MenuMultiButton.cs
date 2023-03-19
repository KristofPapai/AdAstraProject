using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMultiButton : MonoBehaviour
{
    public void NewGameButton()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;

    }

    public void QuiteGame()
    {
        Application.Quit();
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
