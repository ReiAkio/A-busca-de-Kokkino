using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Note Object", menuName = "Inventory System/Items/Note")]
public class NoteObject : ItemObject
{
    public string text;
    private void Awake()
    {
        type = ItemType.Note;
    }
}
