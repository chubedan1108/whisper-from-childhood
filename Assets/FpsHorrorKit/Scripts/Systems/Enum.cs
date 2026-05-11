using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum DoorState
{
    Open,
    Close,
    Lock
}

public enum DoorID
{
    D01, D02, D03, D04, D05, D06, D07, D08, D09, D10, D11, D12, D13, D14, D15,
}

public enum LevelID
{
    Lv01, Lv02, Lv03, Lv04, Lv05,
}

public enum ReactionType
{
    ChangeDoorState,
    Audio,
    Light,
}

[Serializable]
public struct DoorAngle
{
    public float startRotation; //Open
    public float endRotation; // Close
    public float rotationSpeed;
    public float defaulStartRotation; //Mac dinh khi start
}
    [Serializable]
    public struct DoorData
    {
        public DoorID id;
        public DoorState doorState;
        public DoorData(DoorID doorID, DoorState doorState) : this()
        {
            this.id = doorID;
            this.doorState = doorState;
        }
    }

    [Serializable]
    public struct Reaction
    {
        public ReactionType type;
        public DoorID doorID;
        public DoorState doorState;

    }

    [Serializable]
    public struct DoorLevelEntry
    {
        public DoorID doorID;
        public DoorState targetState;
        public List<Reaction> onWrong;
        public List<Reaction> onSucess;
    }


