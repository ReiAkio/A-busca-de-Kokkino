using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 10f;               // Velocidade do projétil
    public Rigidbody2D projectileRb;        // Rigidbody2D do projétil para alterar as propriedades
    public string hitBoxTag;                // Tag da hitbox da cena para economizar memória, destruindo objetos que sairem da cena

    //
    // Resumo:
    //     Garante uma velocidade inicial ao projétil.
    void Start()
    {
        projectileRb.velocity = transform.right * speed;
    }

    //
    // Resumo:
    //     Verifica se existe um componente (especifico do player) na colisão, marca como morto o player se existir
    //     e se o objeto da colisão for a hitbox da cena o objeto é destruído.
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerController2 player = hitInfo.GetComponent<PlayerController2>();
        if (player != null)
        {
            player.IsDead = true;
            Destroy(gameObject);
        }
        if (hitInfo.CompareTag(hitBoxTag))
            Destroy(gameObject);
    }

}
