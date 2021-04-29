using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : TrapActivation
{
    public Transform firePoint;             // Ponto de "spawn" dos projéteis.
    public GameObject projectile;           // GameObject do projétil pré-fabricado.
    public float shootingDelay = 2f;        // Tempo de espera entre os tiros.
    public bool permanentShooting;          // Se executa acão permanente (true = atirar para  sempre depois de ativo uma vez).

    private bool shootTrigger = true;       // Controle de "spawn" dos projéteis, para instanciar um de cada vez na chamada da corrotina.

    //
    // Resumo:
    //     Executa trapWorking() a cada frame.
    private void Update()
    {
        trapWorking();
    }

    //
    // Resumo:
    //     Enquanto o trigger da armadilha estiver ativo, atira projéteis em intervalos.
    protected override void temporaryAction()
    {
        if (!permanentShooting && shootTrigger)
        {
                StartCoroutine(shootingWithDelay(shootingDelay));
        }
    }


    //
    // Resumo:
    //     Enquanto o trigger da armadilha sor ativado, atira projéteis em intervalos até o final da fase.
    protected override void permanentAction()
    {
        if (permanentShooting && shootTrigger)
        {
            StartCoroutine(shootingWithDelay(shootingDelay));
        }
    }


    //
    // Resumo:
    //     Cria o projétil no ponto definido pelo firePoint.
    private void shoot()
    {
        Instantiate(projectile, firePoint.position, firePoint.rotation);    // Instancia o projétil na posicão do firePoint, com a rotacão do firePoint.
    }

    //
    // Resumo:
    //     Executa a funcão shoot() em um intervalo de tempo pré-definido.
    // Parâmetros:
    //   delay:
    //     Tempo (float) que será esperado entre os tiros de projéteis.
    IEnumerator shootingWithDelay(float delay)
    {
        shootTrigger = false;                       // Desabilita novas ativacões da corrotina (para evitar tiros consecutivos).
        shoot();                                    // Instancia um projétil.
        yield return new WaitForSeconds(delay);     // Espera um tempo pré-determinado.
        shootTrigger = true;                        // Habilita novas ativacões da corrotina.
    }

}
