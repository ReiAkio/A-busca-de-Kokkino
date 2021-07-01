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

    public void HandleNewGameButtonOnClickEvent()
    {
        Vector3 initialPosition = new Vector3(-20.37655f, 2f, 0f);
        PlayerData defaultSave = new PlayerData(initialPosition);
        SaveSystem.saveData(defaultSave);
        SceneManager.LoadScene(LoadScene);
    }

    public void HandleQuitButtonOnClickEvent()
    {
        Application.Quit();
    }

}
