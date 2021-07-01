using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;

// Os arquivos que deseja ser salvado
[Serializable]
public class PlayerData
{
    public float[] checkPointPosition;

    /// <summary>
    /// Preenche os dados do PlayerData para posterior salvamento no arquivo.
    /// </summary>
    /// <param name="checkpointPosition"></param> posição do checkpoint que colidiu com o player.
    /// <param name="inventory"></param> referência do inventário do player.
    public PlayerData(GameObject checkpoint) // Posição do último Checkpoint (lastCheckpointPos)
    {
        int positionLenght = 3; // Determina o tamanho do vetor de posição
        checkPointPosition = new float[positionLenght]; // Instancia o vetor no tamanho adequado
        checkPointPosition[0] = checkpoint.transform.position.x;
        checkPointPosition[1] = checkpoint.transform.position.y;
        checkPointPosition[2] = checkpoint.transform.position.z;
    }
    
    public PlayerData(Vector3 startPoint) // Posição do último Checkpoint (lastCheckpointPos)
    {
        Debug.Log("Resetou o save de maneira esperada");
        int positionLenght = 3; // Determina o tamanho do vetor de posição
        checkPointPosition = new float[positionLenght]; // Instancia o vetor no tamanho adequado
        checkPointPosition[0] = startPoint.x;
        checkPointPosition[1] = startPoint.y;
        checkPointPosition[2] = startPoint.z;
    }
}