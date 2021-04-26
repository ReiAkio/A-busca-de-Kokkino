using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrapActivation : MonoBehaviour
{
    public GameObject activationObjectRelated;              //Objeto que ir� servir de gatilho para a ac�o deste
    private bool activeStatus;                              //Status de ativac�o desta armadilha
    private bool triggerStatus;                             //Status se j� foi ativado ao menos uma vez essa armadilha

    //
    // Resumo:
    //     Inicia os par�metros iniciais para o bom funcionamento do sistema
    void Start()
    {
        triggerStatus = false;                                                                      //Por padr�o inicializa em falso o gatilho
        activeStatus = activationObjectRelated.GetComponent<triggerObject>().activationStatus;      //Atribui o status de ativac�o do outro objeto � vari�vel
    }

    //
    // Resumo:
    //     Executa as principais funcoes para o funcionamento do sistema.
    //     Criada para ser executada na func�o Update().
    protected void trapWorking()
    {
        statusUpdate();
        triggerVerify();
        action();
    }

    //
    // Resumo:
    //     Executa as ac�es da armadilha em quest�o, tanto a tempor�ria (enquanto o gatilho estiver pressionado)
    //     quanto a ac�o permanente (executada sempre ap�s o gatilho ser acionado ao menos 1 vez).
    private void action()
    {
        if (activeStatus)
        {
            temporaryAction();
        }

        if (triggerStatus)
        {
            permanentAction();
        }
    }

    //
    // Resumo:
    //     Funcao que ser� sobreescrita (override) por uma classe filha mais espec�fica, executando a ac�o permanente.
    protected abstract void permanentAction();

    //
    // Resumo:
    //     Funcao que ser� sobreescrita (override) por uma classe filha mais espec�fica, executando a ac�o tempor�ria.
    protected abstract void temporaryAction();

    //
    // Resumo:
    //     Verifica se a armadilha foi ativada ao menos 1 vez.
    private void triggerVerify()
    {
        if (activeStatus)
            triggerStatus = true;
    }

    //
    // Resumo:
    //     Atualiza o status de ativac�o instant�nea da armadilha.
    private void statusUpdate()
    {
        activeStatus = activationObjectRelated.GetComponent<triggerObject>().activationStatus;
        triggerVerify();
    }

}
