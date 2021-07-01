using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuStart : MonoBehaviour
{
    public string LoadScene;
    public KeyObject key_1;
    public KeyObject key_2;
    public KeyObject key_3;
    public InventoryObject invetory;
    public void HandlePlayButtonOnClickEvent()
    {
        SceneManager.LoadScene(LoadScene);
    }

    public void HandleNewGameButtonOnClickEvent()
    {
        key_1.wasCollected = false;
        key_2.wasCollected = false;
        key_3.wasCollected = false;
        invetory.itemCollectedQuantity = 0;
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
