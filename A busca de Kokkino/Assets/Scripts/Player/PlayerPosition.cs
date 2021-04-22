using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPosition : MonoBehaviour
{
    //Script associado ao jogador para reposicioná-lo quando este morrer
    private CheckPointManager cm;
    private GameObject Player;
    void Start()
    {
        //Inicia a instância dos objetos
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckPointManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        //Move o personagem para a posicão do último checkpoint
        transform.position = cm.lastCheckpointPos;
    }
    void Update()
    {
        if (Player.GetComponent<PlayerController2>().IsDead)
        {
            //Se o jogador morrer, da um tempo antesa de reiniciar a cena (para melhor fluidez da cena)
            StartCoroutine(ExecuteAfterTime(0.5f));            
        }
    }
    
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
