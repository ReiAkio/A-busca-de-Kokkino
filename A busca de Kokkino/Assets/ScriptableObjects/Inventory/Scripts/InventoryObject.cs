using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void addItem(ItemObject _item)
    {
        InventorySlot slot = new InventorySlot(_item);
        Container.Add(slot);
    }
    
    public void removeItem(ItemObject _item)
    {
        InventorySlot toRemoveSlot = new InventorySlot(_item);
        foreach (InventorySlot slot in Container)
        {
            if (toRemoveSlot.item.Equals(slot.item) && toRemoveSlot.itemPosition.Equals(slot.itemPosition))
            {
                Container.Remove(slot);
            }
        }
    }

}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public float[] itemPosition;

    public InventorySlot(ItemObject _item)
    {
        item = _item;
        // itemPosition[0] = _item.inGameObject.transform.position.x;
        // itemPosition[1] = _item.inGameObject.transform.position.y;
        // itemPosition[2] = _item.inGameObject.transform.position.z;
    }
}
