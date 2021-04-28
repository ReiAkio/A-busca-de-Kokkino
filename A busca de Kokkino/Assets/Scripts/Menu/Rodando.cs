using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rodando : MonoBehaviour
{
    public static bool foi = true;
    void Update()
    {
        if (foi)
        {
            AcionarMenu();
        }
        
    }

    public void AcionarMenu()
    {
        
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuManager.GoToMenu(MenuName.Pause);
                foi = false;
            }
        
    }
}
