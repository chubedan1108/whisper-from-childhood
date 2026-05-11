
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DoorUIConfig", menuName = "Game/DoorUIConfig")]
public class DoorUIConfig : ScriptableObject
{
     public string interactText = "Door Open/Close [E]";
     public string doorLockedText = "The door is locked, you need a Key to open";
     public string useKeyText = "Use Key [E]";
     public string lockDoor = "Lock the door [Q]";
}
