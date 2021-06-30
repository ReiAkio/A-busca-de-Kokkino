using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuStart : MonoBehaviour
{
    public string LoadScene;
    public void HandlePlayButtonOnClickEvent()
    {
        SceneManager.LoadScene(LoadScene);
    }

    public void HandleQuitButtonOnClickEvent()
    {
        Application.Quit();
    }

}
