using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public float time;
    void Start()
    {
        Time.timeScale = 0;
    }

    public void HandleResumeButtonOnClickEvent()
    {
        StartCoroutine(help(time));
    }

    public void HandleVoltarMainMenuButtonOnClickEvent()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.VoltarMainMenu);
        Rodando.foi = true;
    }

    IEnumerator help(float time)
    {
        Time.timeScale = 1;
        //gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        Rodando.foi = true;
        yield break;
    }
}
