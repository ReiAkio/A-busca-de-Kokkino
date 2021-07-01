using System;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    [Header("Inventory Configuration")] 
    public static int inventorySize = 3;
    public static Sprite emptySlotSprite;
    public static Camera playerCamera;
    
    [Header("Inventory")]
    public static InventorySlot[] inventory = new InventorySlot[inventorySize];

    public void startInventory()
    {
        playerCamera = GameObject.FindWithTag("PlayerCamera").GetComponent<Camera>();
        for (int i = 0; i < inventory.Length; i ++)
        {
            if (!inventory.Equals(null))
            {
                inventory[i].item = null;
                inventory[i].itemPosition = null;
            }
        }
        Debug.Log("Inventário: " + inventory);
    }

    public void addItem(GameObject gameItem)
    {
        // InventorySlot slot = new InventorySlot(_item);
        // inventory.Add(slot);
    }

    public void displayInventory()
    {
        
    }

}

[System.Serializable]
public class InventorySlot
{
    public KeyObject item;
    public float[] itemPosition;

    public InventorySlot(GameObject gameItem)
    {
        // item = gameItem.GetComponent<>();
        // itemPosition[0] = _item.position.transform.position.x;
        // itemPosition[1] = _item.position.transform.position.y;
        // itemPosition[2] = _item.position.transform.position.z;
    }
}
