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

        if (connectedBodyToJoint) {            // � executado quando Distance Joint 2D est� conectado a outro objeto

            hookLineRender.enabled = true;                                                          // Habilita a exibic�o do Line Renderer
            hookLineRender.SetPosition(1, connectedBodyToJoint.transform.position);                 // Define a posic�o final do Distance Joint 2D
            hookLineRender.SetPosition(0, playerPosition.transform.position);                       // Define a posic�o inicial do Distance Joint 2D

        }
        else {                                 // � executado quando Distance Joint 2D N�O est� conectado a outro objeto --> NULL REFERENCE EXEPTION

            hookLineRender.enabled = false;                                                         // Desabilita a exibic�o do Line Renderer

        }       

    }



}
