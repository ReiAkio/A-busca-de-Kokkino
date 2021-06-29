using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSystem : MonoBehaviour
{
    public String playerTag;
    private void Start()
    {
        PlayerData reloadedData = SaveSystem.loadData();
        GameObject player = GameObject.FindWithTag("Player");
        //player.transform.position.x = reloadedData.checkPointPosition[0];
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject checkpoint in checkpoints)
        {
            if (checkpoint.transform.position.Equals(reloadedData.checkPointPosition))
                checkpoint.SetActive(false);
        }
    }
}
