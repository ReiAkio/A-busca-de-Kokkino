using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    //Scrip associado a um objeto dentro da cena
    private static CheckPointManager instance;
    public Vector2 lastCheckpointPos;               //Armazena a posic�o do ultimo checkpoint atingido
    public List<Vector2> checkPointactivated;       //Armazena uma lista com a posic�o de todos os checkpoints salvos (para previnir o ressalvamento)
    private GameObject player;

    void Awake() //Garante a exist�ncia de apenas um checkpointmanager (que mant�m suas propriedades ao recarregar cenas)
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lastCheckpointPos = player.GetComponent<Rigidbody2D>().transform.position;
    }

    private void Update()
    {
        //Seria poss�vel somente verificar os checkpoints uma vez a cada recarregamento de cena, precisa melhorar...
        correctCheckpoints(checkPointactivated);
    }

    private void correctCheckpoints(List<Vector2> checks)
    {
        //Compara a posic�o de cada checkpoint (recriados a cada recarregamento da cena) com os salvos anteriormente e desativa os j� salvos
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (Vector2 check in checks)
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                Checkpoint esteCheckpoint = checkpoints[i].GetComponent<Checkpoint>();
                if (check.Equals(esteCheckpoint.transform.position))
                {
                    esteCheckpoint.gameObject.SetActive(false);
                }
            }
        }
    }
}
