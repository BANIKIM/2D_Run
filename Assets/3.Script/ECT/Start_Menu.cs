using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu : MonoBehaviour
{
    public void StartGame()
    {
        PlayerPrefs.SetInt("Sro", 0);
        SceneManager.LoadScene(1);
    }

    public void Go_Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Go_Help()
    {
        SceneManager.LoadScene("HELP");
    }
}
