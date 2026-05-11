using UnityEngine;

public class TextRespond2 : MonoBehaviour
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
        if(doorBase.GetDoorState() == DoorState.Close || doorBase.GetDoorState() == DoorState.Lock)// Dang dong
        {
            StartCoroutine(doorBase.OpenDoor(-45f, 15f));
        }
        doorBase.ChangeState(DoorState.Open);
        Debug.Log("Handle Respond");
    }

    private void OnDisable()
    {
        doorBase.OnRespondToChainReact -= HandleOnRespondToChainReact;

    }
}
