using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : TrapActivation
{
    public Transform firePoint;             // Ponto de "spawn" dos proj�teis.
    public GameObject projectile;           // GameObject do proj�til pr�-fabricado.
    public float shootingDelay = 2f;        // Tempo de espera entre os tiros.
    public bool permanentShooting;          // Se executa ac�o permanente (true = atirar para  sempre depois de ativo uma vez).

    private bool shootTrigger = true;       // Controle de "spawn" dos proj�teis, para instanciar um de cada vez na chamada da corrotina.

    //
    // Resumo:
    //     Executa trapWorking() a cada frame.
    private void Update()
    {
        trapWorking();
    }

    //
    // Resumo:
    //     Enquanto o trigger da armadilha estiver ativo, atira proj�teis em intervalos.
    protected override void temporaryAction()
    {
        if (!permanentShooting && shootTrigger)
        {
                StartCoroutine(shootingWithDelay(shootingDelay));
        }
    }


    //
    // Resumo:
    //     Enquanto o trigger da armadilha sor ativado, atira proj�teis em intervalos at� o final da fase.
    protected override void permanentAction()
    {
        if (permanentShooting && shootTrigger)
        {
            StartCoroutine(shootingWithDelay(shootingDelay));
        }
    }


    //
    // Resumo:
    //     Cria o proj�til no ponto definido pelo firePoint.
    private void shoot()
    {
        Instantiate(projectile, firePoint.position, firePoint.rotation);    // Instancia o proj�til na posic�o do firePoint, com a rotac�o do firePoint.
    }

    //
    // Resumo:
    //     Executa a func�o shoot() em um intervalo de tempo pr�-definido.
    // Par�metros:
    //   delay:
    //     Tempo (float) que ser� esperado entre os tiros de proj�teis.
    IEnumerator shootingWithDelay(float delay)
    {
        shootTrigger = false;                       // Desabilita novas ativac�es da corrotina (para evitar tiros consecutivos).
        shoot();                                    // Instancia um proj�til.
        yield return new WaitForSeconds(delay);     // Espera um tempo pr�-determinado.
        shootTrigger = true;                        // Habilita novas ativac�es da corrotina.
    }

}
