using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public PlayerPosition pp;
    private CheckPointManager cm;
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Verifica se o player colidiu com o objeto checkpoint
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);                                   //Desativa o checkpoint
            cm.checkPointactivated.Add(this.gameObject.transform.position);     //Adiciona a posicão do checkpoint ao vetor de checkpoints salvos
            cm.lastCheckpointPos = transform.position;                          //Atualiza a posicão do ultimo checkpoint salvo para o novo
            SavePlayer(cm);
        }
    }
    void Start()
    {
        //Instancia o checkpointmanager
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckPointManager>();
    }

    public void SavePlayer(CheckPointManager cm) // Salvar a posição quando chamado
    {
        Debug.Log("Salvei");
        SaveSystem.SavePlayer(cm);

    }

    public void LoadPlayer() // Fazer o load da posição quando requisitado
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];
        pp.transform.position = position;

    }
}
