using System;
using UnityEngine;

public class LoadSystem : MonoBehaviour
{
    public static String playerTag = "Player";
    public static String checkpointTag = "Checkpoint";
    private void Start()
    {
        Reload();
    }

    public static void Reload()
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
    }
}