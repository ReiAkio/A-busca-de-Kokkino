using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrap : TrapActivation
{
    //
    // Resumo:
    //     Executa trapWorking() a cada frame.
    private void Update()
    {
        trapWorking();
    }

    protected override void temporaryAction()
    {
        print("Acao temporaria");
    }

    protected override void permanentAction()
    {
        print("Acao PERMANENTE");
    }
}
