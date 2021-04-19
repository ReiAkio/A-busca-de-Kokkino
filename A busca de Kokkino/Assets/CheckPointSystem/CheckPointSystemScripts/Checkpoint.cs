using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckPointManager cm;
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Verifica se o player colidiu com o objeto checkpoint
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);                                   //Desativa o checkpoint
            cm.checkPointactivated.Add(this.gameObject.transform.position);     //Adiciona a posicão do checkpoint ao vetor de checkpoints salvos
            cm.lastCheckpointPos = transform.position;                          //Atualiza a posicão do ultimo checkpoint salvo para o novo
        }
    }
    void Start()
    {
        //Instancia o checkpointmanager
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckPointManager>();
    }

}
