using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager
{
    public static void GoToMenu(MenuName name)
    {
        switch (name)
        {
            case MenuName.Main:
                SceneManager.LoadScene("SampleScene");
                break;

            case MenuName.Pause:
                Object.Instantiate(Resources.Load("Pause"));
                break;

            case MenuName.VoltarMainMenu:
                SceneManager.LoadScene("Inicio");
                break;
        }
    }
}
