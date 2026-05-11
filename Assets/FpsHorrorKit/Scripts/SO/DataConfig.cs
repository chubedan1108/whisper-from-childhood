using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

[CreateAssetMenu(fileName = "LevelConfig", menuName = "CHUBEDAN/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public LevelID levelID;
    public bool mustFollowerOrder = true;
    public List<DoorLevelEntry> doorLevelEntry;

    public List<Reaction> GetOnWrongReaction (DoorID id)
    {
        foreach(var door in doorLevelEntry)
        {
            if (door.doorID == id)
            {
                return door.onWrong;
            }
        }
        return null;
    }

    public List<Reaction> GetOnSucessReaction (DoorID id)
    {
        foreach (var door in doorLevelEntry)
        {
            if (door.doorID == id)
            {
                return door.onSucess;
            }
        }
        return null;
    }

    public List<DoorData> GetReactDoor (List<Reaction> react)
    {
        List<DoorData> data = new();
        foreach(var value in react)
        {
            if (value.type == ReactionType.ChangeDoorState)
            {
                data.Add(new DoorData(value.doorID, value.doorState));
            }
        }
        return data;
    }
}