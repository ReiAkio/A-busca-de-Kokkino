using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Key,
    Note,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefabItem;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
}