using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues")]
public class DialogueBase : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public CharacterProfile character;
        //public string myName;
        //public Sprite portrait;
        [TextArea(4, 8)]
        public string myText;

    }
    [Header("Coloque as info a baixo")]
    public Info[] dialogueInfo;

}