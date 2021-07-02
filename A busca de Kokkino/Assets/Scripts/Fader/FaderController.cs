using UnityEngine;

public class FaderController : MonoBehaviour
{
    private GameObject Player;

    void Start()
    {
        //Cria a instância do protagonista
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame


    public void fader()
    {
        //Fader controler está associado a uma imagem (filha do canvas), esta imagem escurece e esclarece
        this.gameObject.GetComponent<Animator>().SetTrigger("HasDied");
    }
}
