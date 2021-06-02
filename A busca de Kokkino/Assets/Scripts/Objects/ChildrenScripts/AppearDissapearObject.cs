using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AppearDissapearObject : TrapActivation
{
    public float timeUntillActivation;      // Tempo (em segundos) antes do objeto alterar seu estado inicial
    public float timeWhileActivated;        // Tempo (em segundos) antes do objeto retornar ao seu estado inicial
    public bool initialState;               // Estado inicial do objeto (true = ativado / false = desativado)
    
    void Awake()
    {
        this.gameObject.SetActive(initialState);        // Designa o estado inicial ao GameObject
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
        this.gameObject.SetActive(!initialState);          // Altera o estado inicial do GameObject

        yield return new WaitForSeconds(timeWhileActivated);                   // Aguarda um tempo antes de retornar ao estado inicial
        this.gameObject.SetActive(!initialState);           // Retorna ao estado inicial o GameObject
    }
    
}
