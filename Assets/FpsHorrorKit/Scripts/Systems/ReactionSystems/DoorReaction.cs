using UnityEngine;

public class DoorReaction : MonoBehaviour, IReactionHandler
{
    public ReactionType type { get; set; }

    public void Execute(Reaction reactionData)
    {
        Debug.Log($"Door reaction {reactionData.doorID}");
    }
}

