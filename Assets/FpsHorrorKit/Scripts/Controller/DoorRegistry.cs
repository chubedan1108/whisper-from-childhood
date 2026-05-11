using System.Collections.Generic;
using UnityEngine;

public class DoorRegistry : Singleton<DoorRegistry>
{
    private Dictionary<DoorID, DoorBase> registryList = new();

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("dang ki duoc khoi tao");
    }
    public void Register(DoorID id, DoorBase door)
    {
        if (registryList.ContainsKey(id))
        {
            Debug.Log($"Door {id} has existed, overwrite");
        }
        registryList[id] = door;
        door.OnChangeState += HandlerOnChangeState;
    }
    public void HandlerOnChangeState(DoorBase door)
    {
        List<Reaction> reactions = LevelController.Instance.LevelConfig.GetOnWrongReaction(door.GetData().id);
        ReactionBus.Instance.DispatchAll(reactions);
    }
    //public void HandlerOnChangeState(DoorBase door)
    //{
    //    if (EventManager.Instance.IsTriggerStart)
    //    {
    //        //Lay ra cac door trong onwrong reaction de phan hoi
    //        List<DoorData> data = LevelController.Instance.LevelConfig.GetReactDoor(
    //            LevelController.Instance.LevelConfig.GetOnWrongReaction(door.GetData().id));
    //        foreach(var value in data)
    //        {
    //            //Lay cac doorbase theo id de phan hoi
    //            if (registryList.TryGetValue(value.id, out DoorBase doorBase))
    //            {
                    
    //            }
    //        }
    //    }
    //}

    public DoorBase Get(DoorID id)
    {
        registryList.TryGetValue(id, out DoorBase value);
        return value;
    }

    public void Unregister(DoorID id) => registryList.Remove(id);
}
