using UnityEngine;

public interface IReactionHandler
{
    public ReactionType type { get; set; }
    public void Execute(Reaction reactionData);
}