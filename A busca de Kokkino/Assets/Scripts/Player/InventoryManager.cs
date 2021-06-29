using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject inventory;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject[] itens = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in itens)
        {
            if (collision.gameObject.Equals(item))
            {
                inventory.addItem(item.GetComponent<InventorySlot>().item);
                item.SetActive(false);
            }
        }
    }
}
