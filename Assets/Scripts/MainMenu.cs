using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("You are winning.");
        SceneManager.LoadScene("ready_map");
    }
    public void ExitGame()
    {
        Debug.Log("Closedb aaaaaaaaaaaaaaaaa");
        Application.Quit();
    }
    public void LoadGame()
    {
        Debug.Log("LOADED");
    }
}
