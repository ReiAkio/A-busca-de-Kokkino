using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public InventoryObject playerInventory;
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Verifica se o player colidiu com o objeto checkpoint
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);                                           //Desativa o checkpoint
            saveActualPlayerInfo();                                                               //Salva o jogo
        }
    }

    public void saveActualPlayerInfo() // Salvar a posi��o quando chamado
    {
        PlayerData actualData = new PlayerData(gameObject, playerInventory);
        SaveSystem.saveData(actualData);
    }
}
