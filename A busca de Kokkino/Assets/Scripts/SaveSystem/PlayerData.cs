using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;

// Os arquivos que deseja ser salvado
[Serializable]
public class PlayerData
{
    public float[] position;


    public PlayerData(CheckPointManager cm) // Posição do último Checkpoint (lastCheckpointPos)
    {
        cm.lastCheckpointPos = GameObject.Find("CheckPointManager").GetComponent<CheckPointManager>().lastCheckpointPos;
        position = new float[2];
        position[0] = cm.lastCheckpointPos[0];
        position[1] = cm.lastCheckpointPos[1];


    }
}