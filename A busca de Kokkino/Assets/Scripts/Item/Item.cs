using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject item;
    public InventoryObject playerInventory;
    public String playerTag;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Player em contato");
            gameObject.SetActive(false);
        }
    }
}
