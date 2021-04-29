using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 10f;               // Velocidade do proj�til
    public Rigidbody2D projectileRb;        // Rigidbody2D do proj�til para alterar as propriedades
    public string hitBoxTag;                // Tag da hitbox da cena para economizar mem�ria, destruindo objetos que sairem da cena

    //
    // Resumo:
    //     Garante uma velocidade inicial ao proj�til.
    void Start()
    {
        projectileRb.velocity = transform.right * speed;
    }

    //
    // Resumo:
    //     Verifica se existe um componente (especifico do player) na colis�o, marca como morto o player se existir
    //     e se o objeto da colis�o for a hitbox da cena o objeto � destru�do.
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
