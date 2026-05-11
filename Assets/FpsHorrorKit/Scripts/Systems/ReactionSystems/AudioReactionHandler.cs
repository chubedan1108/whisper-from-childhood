using UnityEngine;

public class AudioReactionHandler : MonoBehaviour, IReactionHandler
{
    public ReactionType type { get; set; } = ReactionType.Audio;

    void Start()
    {
        ReactionBus.Instance.Register(type,this);
    }
    public void Execute(Reaction reactionData)
    {
        Debug.Log("Phan hoi am thanh khi su kien sai xay ra");
    }

   
}
