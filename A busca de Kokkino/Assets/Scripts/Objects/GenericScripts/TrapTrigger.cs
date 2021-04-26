using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public string playerTag;            //String referente à tag que cujo objeto será identificado ao colidir com o trigger
    public bool activationStatus;       //Armazena o estado de ativacão do trigger

    //
    // Resumo:
    //     Ajusta os parâmetros iniciais da classe.
    private void Awake()
    {
        activationStatus = false;
    }

    //
    // Resumo:
    //     Quando o objeto referido entra no gatilho, altera o status de ativacão
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
            activationStatus = true;
    }

    //
    // Resumo:
    //     Quando o objeto referido sai no gatilho, altera o status de ativacão
    private void OnTriggerExit2D(Collider2D collision)
    {
        activationStatus = false;
    }

}
