using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key", menuName = "Inventory System/Key")]
public class KeyObject : ScriptableObject
{
    public String description;
    public Sprite sprite;
    public bool wasCollected = false;
}
