using UnityEngine;

public class TextRespond : MonoBehaviour
{
    private DoorBase doorBase;

    private void Awake()
    {
        doorBase = GetComponent<DoorBase>();
    }
    private void Start()
    {
        doorBase.OnRespondToChainReact += HandleOnRespondToChainReact;
    }

    private void HandleOnRespondToChainReact()
    {
        Debug.Log($"Recieve react from {doorBase}");
    }

    private void OnDisable()
    {
        doorBase.OnRespondToChainReact -= HandleOnRespondToChainReact;

    }
}
