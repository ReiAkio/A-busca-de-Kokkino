using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public string playerTag;            //String referente � tag que cujo objeto ser� identificado ao colidir com o trigger
    public bool activationStatus;       //Armazena o estado de ativac�o do trigger

    //
    // Resumo:
    //     Ajusta os par�metros iniciais da classe.
    private void Awake()
    {
        activationStatus = false;
    }

    //
    // Resumo:
    //     Quando o objeto referido entra no gatilho, altera o status de ativac�o
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
            activationStatus = true;
    }

    //
    // Resumo:
    //     Quando o objeto referido sai no gatilho, altera o status de ativac�o
    private void OnTriggerExit2D(Collider2D collision)
    {
        activationStatus = false;
    }

}
