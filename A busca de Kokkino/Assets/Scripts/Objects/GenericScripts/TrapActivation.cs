using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrapActivation : MonoBehaviour
{
    public GameObject activationObjectRelated;              //Objeto que irá servir de gatilho para a acão deste
    private bool activeStatus;                              //Status de ativacão desta armadilha
    private bool triggerStatus;                             //Status se já foi ativado ao menos uma vez essa armadilha

    //
    // Resumo:
    //     Inicia os parâmetros iniciais para o bom funcionamento do sistema
    void Start()
    {
        triggerStatus = false;                                                                      //Por padrão inicializa em falso o gatilho
        activeStatus = activationObjectRelated.GetComponent<triggerObject>().activationStatus;      //Atribui o status de ativacão do outro objeto à variável
    }

    //
    // Resumo:
    //     Executa as principais funcoes para o funcionamento do sistema.
    //     Criada para ser executada na funcão Update().
    protected void trapWorking()
    {
        statusUpdate();
        triggerVerify();
        action();
    }

    //
    // Resumo:
    //     Executa as acões da armadilha em questão, tanto a temporária (enquanto o gatilho estiver pressionado)
    //     quanto a acão permanente (executada sempre após o gatilho ser acionado ao menos 1 vez).
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
    //     Funcao que será sobreescrita (override) por uma classe filha mais específica, executando a acão permanente.
    protected abstract void permanentAction();

    //
    // Resumo:
    //     Funcao que será sobreescrita (override) por uma classe filha mais específica, executando a acão temporária.
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
    //     Atualiza o status de ativacão instantânea da armadilha.
    private void statusUpdate()
    {
        activeStatus = activationObjectRelated.GetComponent<triggerObject>().activationStatus;
        triggerVerify();
    }

}
