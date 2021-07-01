using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public InventoryObject inventory;
    public int collectedLimit = 3;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag.Equals("Player") && inventory.itemCollectedQuantity == collectedLimit)
        {
            Application.Quit();
            Debug.Log("Fim");
        }
    }
}
