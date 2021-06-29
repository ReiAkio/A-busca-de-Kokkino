using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void addItem(ItemObject _item, int _amount)
    {
        // bool hasItem = false;
        // for (int i = 0; i < Container.Count; i++){
        //     if (Container[i].item == _item)
        //     {
        //         Container[i].addAmount(_amount);
        //         hasItem = true;
        //         break;
        //     }
        // }
        //
        // if (!hasItem)
        // {
        //     Container.Add(new InventorySlot(_item, _amount));
        // }
    }

    // public void removeItem(ItemObject _item, int _amount)
    // {
    //     bool hasItem = false;
    //     for (int i = 0; i < Container.Count; i++){
    //         if (Container[i].item == _item)
    //         {
    //             Container[i].removeAmount(_amount);
    //
    //             if (Container[i].itemPosition <= 0)
    //             {
    //                 Container.Remove(Container[i]);
    //             }
    //
    //             hasItem = true;
    //             break;
    //         }
    //     }
    //
    //     if (!hasItem)
    //     {
    //         Debug.Log("The item you are trying to remove doesn't exists");
    //     }
    // }

}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public float[] itemPosition;

    public InventorySlot(ItemObject _item, int itemPosition)
    {
        item = _item;
        itemPosition = itemPosition;
    }

    // public void addAmount(int value)
    // {
    //     itemPosition += value;
    // }
    //
    // public void removeAmount(int value)
    // {
    //     itemPosition -= value;
    // }

}
