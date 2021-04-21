using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook_line_render : MonoBehaviour
{
    public LineRenderer hookLineRender;
    public DistanceJoint2D hookDistanceJoint;
    public Transform playerPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        positionAdjust();
    }

void positionAdjust()
    {
        Rigidbody2D connectedBodyToJoint = hookDistanceJoint.connectedBody; // Instancia o Rigidbody2D do objeto conectado ao Distance Joint 2D

        if (connectedBodyToJoint) {            // É executado quando Distance Joint 2D está conectado a outro objeto

            hookLineRender.enabled = true;                                                          // Habilita a exibicão do Line Renderer
            hookLineRender.SetPosition(1, connectedBodyToJoint.transform.position);                 // Define a posicão final do Distance Joint 2D
            hookLineRender.SetPosition(0, playerPosition.transform.position);                       // Define a posicão inicial do Distance Joint 2D

        }
        else {                                 // É executado quando Distance Joint 2D NÃO está conectado a outro objeto --> NULL REFERENCE EXEPTION

            hookLineRender.enabled = false;                                                         // Desabilita a exibicão do Line Renderer

        }       

    }



}
