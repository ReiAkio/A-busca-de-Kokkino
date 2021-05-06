using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class HookManager : MonoBehaviour
{   
    // Instancias
    public Transform playerPosition_hm;         // Instancia a posicão atual do jogador (utilizado para cálculos de distância)
    public DistanceJoint2D distanceJoint_hm;    // Instancia o Distance Joint 2D atribuido ao jogador

    // Variáveis uteis para a classe
    private GameObject[] anchorPoints;          // Array com todos os Anchorpoints da cena
    private GameObject lastAnchorInRange;       // Armazena o último Anchorpoint no alcance
    private Color AnchorColor;                  // Armazena a cor do anchorpoint

    [Header("Custom Properites")]
    public double anchorMinRange;               // Define qual a distância minima para o player conseguir se conectar ao Anchorpoint
    public KeyCode hookActivationKey;           // Define qual a tecla que quando pressionada conecta o player ao Anchorpoint
    public KeyCode releaseHookKey;              // Define qual a tecla que quando pressionada desconecta o player ao Anchorpoint
    public string anchorpointTag;               // Recebe qual a tag atribuida aos Anchorpoints da cena
    public Color selectedAnchorColor;           // Armazena a cor que será atribuida aos Anchorpoints dentro do alcance do player

    
    
    void Start()
    {
        anchorPoints = GameObject.FindGameObjectsWithTag(anchorpointTag);           // Identifica os Anchorpoints na cena atual
        AnchorColor = anchorPoints[0].GetComponent<SpriteRenderer>().color;         // Armazena  cor pré-definida dos anchorpoint ---> pode ser atualizado juntamente com inRangeHookTracker() para permitir váriadas cores de Anchorpoints
    }


    void Update()
    {
        inRangeHookTracker();
        connectToCloserInRangeHook();
        releaseConnectedHook();
    }

    //
    // Resumo:
    //    Retorna o Anchorpoint mais próximo do Player.
    public GameObject findAnchorInLowerRange()
    {
        int lowerIndex = 0;
        double lowerDistance = distancePlayerAnchor(anchorPoints[lowerIndex]);

        for (int i = 1; i < anchorPoints.Length; i++) {

            if (lowerDistance > distancePlayerAnchor(anchorPoints[i])) {

                lowerIndex = i;
                lowerDistance = distancePlayerAnchor(anchorPoints[i]);
            }
        }

        return anchorPoints[lowerIndex];
    }

    //
    // Resumo:
    //     Retorna a distância entre o Player e um Anchorpoint.
    //
    // Parâmetros:
    //   anchor:
    //     O objeto Anchorpoint cuja distancia será calculada até o Player.
    private double distancePlayerAnchor(GameObject anchor)
    {
        double xPlayer = playerPosition_hm.transform.position.x;
        double yPlayer = playerPosition_hm.transform.position.y;

        double xAnchor = anchor.transform.position.x;
        double yAnchor = anchor.transform.position.y;

        return Sqrt( (Pow( (xPlayer - xAnchor), 2 ) + Pow( (yPlayer - yAnchor), 2) ) );
    }

    //
    // Resumo:
    //     Retorna se existe algum corpo já conectado ao Distance Joint 2D.
    public bool hasConnectedBody()
    {
        if (distanceJoint_hm.connectedBody) // Verifica se existe um corpo conectado ao Distance Joint 2D
            return true;
        return false;
    }

    //
    // Resumo:
    //     Retorna se o corpo está no alcance pré definido.
    //
    // Parâmetros:
    //   anchor:
    //     O objeto Anchorpoint que será verificado se está na distancia definida.
    private bool isAnchoInRange(GameObject anchor)
    {
        if (distancePlayerAnchor(anchor) <= anchorMinRange) // Verifica se a distancia do objeto está dentro da definida como mínima para iteracões
            return true;
        return false;
    }

    //
    // Resumo:
    //     Realiza a verificacão do alcance e, se dentro do limite de interacão
    //     conecta o Distance Joint 2D ao corpo referido como dentro do limite.
    private void connectToCloserInRangeHook()
    {
        if (!hasConnectedBody()) // Acões se não estiver conectado previamente a um Anchorpoint
        {
            if (Input.GetKeyDown(hookActivationKey))
            {
                if (isAnchoInRange(findAnchorInLowerRange())) // Verifica se o anchor na menor distância esta dentro do range pré-definido
                {
                    distanceJoint_hm.enabled = true;
                    distanceJoint_hm.connectedBody = findAnchorInLowerRange().GetComponent<Rigidbody2D>(); // Atualiza a conexão do Distance Joint 2D
                }
                else distanceJoint_hm.enabled = false;
            }
        }
        else // Acões se estiver conectado previamente a um Anchorpoint
        {
            if (isAnchoInRange(findAnchorInLowerRange()) && !findAnchorInLowerRange().transform.position.Equals(lastAnchorInRange)) // Verifica se está tentando se conectar ao mesmo Anchorpoint
            {
                if (Input.GetKeyDown(hookActivationKey))
                {
                    releaseConnectedHook(); // Desconecta do antigo Anchorpoint
                    distanceJoint_hm.connectedBody = findAnchorInLowerRange().GetComponent<Rigidbody2D>(); // Atualiza a conexão do Distance Joint 2D
                }               

            }
        }
    }

    //
    // Resumo:
    //     Ao pressionar da tecla pré-definida, desconecta o Distance Joint 2D
    //     de qualquer Anchorpoint conectado.
    private void releaseConnectedHook()
    {
        if (hasConnectedBody())
        {
            if (Input.GetKeyDown(releaseHookKey)) {
                distanceJoint_hm.enabled = false;
                distanceJoint_hm.connectedBody = null;
            }
        }
        
    }

    //
    // Resumo:
    //     Altera a cor dos Anchorpoints os quais o jogador pode se conectar
    //     pressionando a tecla de interacão pré-definida.
    private void inRangeHookTracker()
    {
        GameObject atualAnchor = findAnchorInLowerRange();
        if (isAnchoInRange(atualAnchor)) // Verifica se o Anchorpoint mais próximo está dentro do alcance pré-definido
        {
            if (lastAnchorInRange) // Verifica a existência de um antigo Anchorpoint que foi conectado
            {
                if (!atualAnchor.transform.position.Equals(lastAnchorInRange.transform.position)) // Verifica se o Anchorpoint disponível não é o antigo (já alterado)
                {
                    lastAnchorInRange.GetComponent<SpriteRenderer>().color = AnchorColor;         // Restitui a cor normal do antigo Anchorpoint
                    atualAnchor.GetComponent<SpriteRenderer>().color = selectedAnchorColor;       // Atualiza a cor do Anchorpoint atual dentro do alcance
                    lastAnchorInRange = atualAnchor;                                              // Atualiza a definicão do ultimo Anchorpoint conectado para restituicão da cor normal
                }
            }
            else // Caso não haja nenhum antigo Anchorpoint
            {
                atualAnchor.GetComponent<SpriteRenderer>().color = selectedAnchorColor;           // Altera a cor do Anchorpoint dentro do alcance
                lastAnchorInRange = atualAnchor;                                                  // Atualiza a definicão do ultimo Anchorpoint conectado para restituicão da cor normal
            }
        }
        else // Caso o Anchorpoint mais próximo não esteja dentro do alcance pré-definido
        {
            atualAnchor.GetComponent<SpriteRenderer>().color = AnchorColor;                       // Restitui a cor normal do antigo Anchorpoint
            lastAnchorInRange = null;                                                             // Atualiza a definicão do ultimo Anchorpoint conectado para restituicão da cor normal
        }
        

        
    }

}
