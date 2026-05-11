using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ReactionBus : Singleton<ReactionBus>
{
    private Dictionary<ReactionType, IReactionHandler> dic = new();

    public void Register(ReactionType type, IReactionHandler i)
    {
        if (dic.ContainsKey(type))
        {
            Debug.Log($"{type} has existed, overwrite");
        }
        dic[type] = i;
    }

    public void Dispatch(Reaction reaction)
    {
        if (dic.TryGetValue(reaction.type, out IReactionHandler value))
        {
            value.Execute(reaction);
            Debug.Log(value);
        }
        
    }

    public void DispatchAll(List<Reaction> reacions)
    {
        foreach(var react in reacions)
        {
            if (dic.TryGetValue(react.type, out IReactionHandler value))
            {
                value.Execute(react);
                Debug.Log(value);
            }
        }
    }
}
