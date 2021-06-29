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
    public LinkedList<float[]> itemPosition = new LinkedList<float[]>();
    
    /// <summary>
    /// Preenche os dados do PlayerData para posterior salvamento no arquivo.
    /// </summary>
    /// <param name="checkpointPosition"></param> posição do checkpoint que colidiu com o player.
    /// <param name="inventory"></param> referência do inventário do player.
    public PlayerData(GameObject checkpoint, InventoryObject inventory) // Posição do último Checkpoint (lastCheckpointPos)
    {
        int positionLenght = (int)checkpoint.transform.position.magnitude; // Calcula o tamanho do vetor de posição
        checkPointPosition = new float[positionLenght]; // Instancia o vetor no tamanho adequado
        
        for (int index = 0; index < positionLenght; index++) // Para cada posição do vetor (x, y, z), iguala ao do checkpoint
        {
            // Associa a posição do checkpoint ao playerData
            checkPointPosition[index] = checkpoint.transform.position[index];
        }
        
        for (int i = 0; i < inventory.Container.Count; i++) // Para cada item no inventário
        {
            float[] posicaoDoItemX = new float[3]; // Instancia uma nova posição de item
            for (int j = 0; j < inventory.Container[i].itemPosition.Length; j++) // Para cada posição no vetor posição do item
            {
                // Associa a posição do item ao vetor
                posicaoDoItemX[j] = inventory.Container[i].itemPosition[j];
            }
            itemPosition.AddLast(posicaoDoItemX); // Adiciona a posição do item à lista de itens coletados
        }
        
    }
}