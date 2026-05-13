using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quan ly tuong tac va he thong phan hoi cua cac canh cua trong game
/// </summary>
public class DoorController : MonoBehaviour
{
   
    [Serializable]
    public struct DoorGroup
    {
        public DoorID id;
        public DoorBase doorBase;
        public List<DoorBase> chainReactList;
    }

    [SerializeField] private List<DoorGroup> doorGroups = new();
    private Dictionary<DoorID, List<DoorBase>> dic = new();
     
    private void Start()
    {
        Initialize();
    }
     

    private void Initialize()
    {
        foreach(DoorGroup doorGroup in doorGroups)
        {
            if (!dic.ContainsKey(doorGroup.id))
            {
                dic.Add(doorGroup.id, doorGroup.chainReactList);
            }
            
            doorGroup.doorBase.OnChangeState += HandleOnChangeState;
        }
    } 

    
    private void HandleOnChangeState(DoorBase door)
    {
        if (EventManager.Instance.IsTriggerStart)
        {
            dic.TryGetValue(door.GetData().id, out List<DoorBase> value);
            foreach (DoorBase reactDoor in value)
            {
                reactDoor.OnRespondToChainReact?.Invoke();
            }
        }
        
    }

    private void OnDisable()
    {
        foreach(DoorGroup dg in doorGroups)
        {
            foreach(DoorBase db in dg.chainReactList)
            {
                db.OnChangeState -= HandleOnChangeState;
            }
        }
    }
}
