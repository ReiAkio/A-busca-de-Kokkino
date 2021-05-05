using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void addItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++){
            if (Container[i].item == _item)
            {
                Container[i].addAmount(_amount);
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }
    }

    public void removeItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++){
            if (Container[i].item == _item)
            {
                Container[i].removeAmount(_amount);

                if (Container[i].amount <= 0)
                {
                    Container.Remove(Container[i]);
                }

                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            Debug.Log("The item you are trying to remove doesn't exists");
        }
    }

}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;

    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void addAmount(int value)
    {
        amount += value;
    }

    public void removeAmount(int value)
    {
        amount -= value;
    }

}
