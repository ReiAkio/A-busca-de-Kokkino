using UnityEngine;

public class FaderController : MonoBehaviour
{
    private GameObject Player;
    void Start()
    {
        //Cria a inst�ncia do protagonista
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica uma vez por frame se o jgoador morreu e inicia a animac�o (da transic�o de tela na morte)
        if (Player.GetComponent<PlayerController2>().IsDead)
        {
            //Fader controler est� associado a uma imagem (filha do canvas), esta imagem escurece e esclarece
            this.gameObject.GetComponent<Animator>().SetTrigger("HasDied");
            reloadScene();
        }
    }

    void reloadScene()
    {

    }
}
