using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAppear_DissapearTrap : TrapActivation
{
    // Classe utilizada para objetos que possuem Renderer e Collider2D do tipo Sprite e Polygon, respectivamente.

    public float timeUntillActivation;      // Tempo (em segundos) antes do objeto alterar seu estado inicial
    public float timeWhileActivated;        // Tempo (em segundos) antes do objeto retornar ao seu estado inicial
    public bool initialState;               // Estado inicial do objeto (true = ativado / false = desativado)

    //
    // Resumo:
    //     Ajusta as configurac?es iniciais do objeto ao ser instanciado
    private void Awake()
    {
        this.GetComponent<SpriteRenderer>().enabled = initialState;        // Designa o estado inicial ao SpriteRenderer
        this.GetComponent<PolygonCollider2D>().enabled = initialState;      // Designa o estado inicial ao PolygonCollider2D
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
    //     Implementac?o da ac?o tempor?ria do objeto.
    protected override void temporaryAction()
    {
        StartCoroutine(ExecuteForSomeTime());
    }

    //
    // Resumo:
    //     Implementac?o da ac?o permanente do objeto.
    protected override void permanentAction()
    {
        // Sem implementac?o - sem ac?o permanente
    }

    //
    // Resumo:
    //     Altera por um certo tempo pr? definido o estado do objeto, e depois
    //     o retorna ao seu estado original depois de outro tempo pr? definido.
    private IEnumerator ExecuteForSomeTime()
    {
        yield return new WaitForSeconds(timeUntillActivation);                 // Aguarda um tempo antes da mudanca de estado
        this.GetComponent<SpriteRenderer>().enabled = !initialState;          // Altera o estado inicial do SpriteRenderer
        this.GetComponent<PolygonCollider2D>().enabled = !initialState;        // Altera o estado inicia do PolygonCollider2D

        yield return new WaitForSeconds(timeWhileActivated);                   // Aguarda um tempo antes de retornar ao estado inicial
        this.GetComponent<SpriteRenderer>().enabled = initialState;           // Retorna ao estado inicial o SpriteRenderer
        this.GetComponent<PolygonCollider2D>().enabled = initialState;         // Retorna ao estado inicial o PolygonCollider2D
    }

    //
    // Resumo:
    //     Verifica se existe um componente (especifico do player) na colis?o e marca como morto o player se existir.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController2 player = collision.GetComponent<PlayerController2>();
        if (player != null)
            player.IsDead = true;
    }
}
