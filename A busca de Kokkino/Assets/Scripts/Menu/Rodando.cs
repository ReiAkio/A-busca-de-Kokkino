using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rodando : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuManager.GoToMenu(MenuName.Pause);
        }
    }
}
