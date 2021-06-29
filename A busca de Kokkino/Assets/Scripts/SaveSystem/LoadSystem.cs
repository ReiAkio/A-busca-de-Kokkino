using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSystem : MonoBehaviour
{
    public static String playerTag = "Player";
    public static String checkpointTag = "Checkpoint";
    public static String itemTag = "Item";
    private void Start()
    {
        PlayerData reloadedData = SaveSystem.loadData();
        GameObject player = GameObject.FindWithTag(playerTag);
        Vector3 position = new Vector3(reloadedData.checkPointPosition[0], reloadedData.checkPointPosition[1], reloadedData.checkPointPosition[2]);
        player.transform.position = position;
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag(checkpointTag);
        foreach (GameObject checkpoint in checkpoints)
        {
            if (checkpoint.transform.position.Equals(reloadedData.checkPointPosition))
                checkpoint.SetActive(false);
        }

        GameObject[] items = GameObject.FindGameObjectsWithTag(itemTag);
        foreach (GameObject item in items)
        {
            for (int i = 0; i < reloadedData.itemPosition.Count; i++)
            {
                Vector3 itemPos = new Vector3((reloadedData.itemPosition[i])[0], (reloadedData.itemPosition[i])[1],
                    (reloadedData.itemPosition[i])[2]);
                if (item.transform.position.Equals(itemPos))
                    item.SetActive(false);
            }
        }
    }
}