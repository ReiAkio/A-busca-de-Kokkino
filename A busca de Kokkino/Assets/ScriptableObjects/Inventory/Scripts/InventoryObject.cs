using System;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public int itemCollectedQuantity = 0;
}
