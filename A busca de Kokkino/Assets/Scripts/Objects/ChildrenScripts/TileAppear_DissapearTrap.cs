using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileAppear_DissapearTrap : TrapActivation
{
    // Classe utilizada para objetos que possuem Renderer e Collider2D do tipo Tilemap.

    public float timeUntillActivation;      // Tempo (em segundos) antes do objeto alterar seu estado inicial
    public float timeWhileActivated;        // Tempo (em segundos) antes do objeto retornar ao seu estado inicial
    public bool initialState;               // Estado inicial do objeto (true = ativado / false = desativado)

    //
    // Resumo:
    //     Ajusta as configurac�es iniciais do objeto ao ser instanciado
    private void Awake()
    {        
        this.GetComponent<TilemapRenderer>().enabled = initialState;        // Designa o estado inicial ao TilemapRenderer
        this.GetComponent<TilemapCollider2D>().enabled = initialState;      // Designa o estado inicial ao TilemapCollider2D
    }

    //
    // Resumo:
    //     Executa trapWorking() a cada frame.
    private void Update()
    {
        trapWorking();
    }

    //
    // Resumo:
    //     Implementac�o da ac�o tempor�ria do objeto.
    protected override void temporaryAction()
    {
        StartCoroutine(ExecuteForSomeTime());
    }

    //
    // Resumo:
    //     Implementac�o da ac�o permanente do objeto.
    protected override void permanentAction()
    {
        // Sem implementac�o - sem ac�o permanente
    }

    //
    // Resumo:
    //     Altera por um certo tempo pr� definido o estado do objeto, e depois
    //     o retorna ao seu estado original depois de outro tempo pr� definido.
    private IEnumerator ExecuteForSomeTime()
    {
        yield return new WaitForSeconds(timeUntillActivation);                 // Aguarda um tempo antes da mudanca de estado
        this.GetComponent<TilemapRenderer>().enabled = !initialState;          // Altera o estado inicial do TilemapRenderer
        this.GetComponent<TilemapCollider2D>().enabled = !initialState;        // Altera o estado inicia do TilemapCollider2D

        yield return new WaitForSeconds(timeWhileActivated);                   // Aguarda um tempo antes de retornar ao estado inicial
        this.GetComponent<TilemapRenderer>().enabled = initialState;           // Retorna ao estado inicial o TilemapRenderer
        this.GetComponent<TilemapCollider2D>().enabled = initialState;         // Retorna ao estado inicial o TilemapCollider2D
    }
}
