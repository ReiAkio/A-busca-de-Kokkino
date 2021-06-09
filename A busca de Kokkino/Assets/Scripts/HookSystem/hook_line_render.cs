using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook_line_render : MonoBehaviour
{
    public LineRenderer hookLineRender;               // Instancia para Line Renderer
    public DistanceJoint2D hookDistanceJoint;         // Instancia para Distance Joint 2D
    public Transform playerPosition;                  // Instancia para Transform do jogador

    void Update()
    {
        positionAdjust();
    }

    //
    // Resumo:
    //     Realiza o ajuste nas posicões dos vetores do Line Renderer.
private void positionAdjust()
    {
        Rigidbody2D connectedBodyToJoint = hookDistanceJoint.connectedBody;                             // Instancia o Rigidbody2D do objeto conectado ao Distance Joint 2D

        if (connectedBodyToJoint && hookDistanceJoint.isActiveAndEnabled){                              // É executado quando Distance Joint 2D está conectado a outro objeto e ativo
                hookLineRender.enabled = true;                                                          // Habilita a exibicão do Line Renderer
                hookLineRender.SetPosition(1, connectedBodyToJoint.transform.position);                 // Define a posicão final do Distance Joint 2D
                hookLineRender.SetPosition(0, playerPosition.transform.position);                       // Define a posicão inicial do Distance Joint 2D
        }
        
        else {                                                                                          // É executado quando Distance Joint 2D NÃO está conectado a outro objeto (NULL REFERENCE EXEPTION) ou inativo

            hookLineRender.enabled = false;                                                             // Desabilita a exibicão do Line Renderer

        }      
    }
}
