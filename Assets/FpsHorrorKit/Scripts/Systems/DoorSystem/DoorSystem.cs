using FpsHorrorKit;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class DoorSystem : DoorBase
{
    [Header("Highlight UI")]
    public DoorUIConfig uIConfig;

    public AudioSource doorAudioSource;
  
    public override void Interact()
    {
        if (!isFinished) return; //Cua dang duoc thao tac
        if (base.currentState == DoorState.Lock && !hasKey) return; 
        if (base.currentState == DoorState.Lock && hasKey) 
        {
            base.ChangeState(DoorState.Close);
            //Cua duoc mo khoa
            return;
        }
        if (base.currentState == DoorState.Open) //Dong cua
        {
            StartCoroutine(OpenDoor(base.angleConfig.endRotation, base.angleConfig.rotationSpeed));
            base.ChangeState(DoorState.Close);
        }
        else if (base.currentState == DoorState.Close) //Mo cua
        {
            StartCoroutine(OpenDoor(base.angleConfig.startRotation, base.angleConfig.rotationSpeed));
            base.ChangeState(DoorState.Open);
        }
    }

    public override void Highlight()
    {
        if (hasKey && currentState == DoorState.Lock)
        {
            PlayerInteract.Instance.ChangeInteractText(uIConfig.useKeyText);
        }
        else if (currentState == DoorState.Lock)
        {
            PlayerInteract.Instance.ChangeInteractText(uIConfig.doorLockedText);
        }
        else if (currentState == DoorState.Close && hasKey)
        {
            PlayerInteract.Instance.ChangeInteractText(uIConfig.interactText+"\n"+uIConfig.lockDoor);
        }
        else
        {
             PlayerInteract.Instance.ChangeInteractText(uIConfig.interactText);
        }
    }

    public override void HoldInteract() { }
    public override void UnHighlight() { }

    public override void StopInteract()
    {
        if (base.currentState == DoorState.Close && hasKey) //Khoa cua
        {
            base.ChangeState(DoorState.Lock);
        }
    }
}
