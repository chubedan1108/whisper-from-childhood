using FpsHorrorKit;
using System;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DoorBase : MonoBehaviour, IInteractable
{
    public Action<DoorBase> OnChangeState;
    public Action OnRespondToChainReact;
    public Action OnAchieveTargetState;

    public bool hasKey;

    [SerializeField] protected DoorState currentState;
    [SerializeField] protected DoorAngle angleConfig;
    [SerializeField] protected DoorData data;
    protected bool isFinished = false; //trong qua trinh mo thi khong thao tac duoc
    

    protected virtual void Awake()
    {
        isFinished = true;
        transform.localEulerAngles = new Vector3(0, angleConfig.defaulStartRotation, 0);
    }
    public void ChangeState(DoorState state)
    {
        this.currentState = state;
        OnChangeState?.Invoke(this);
       
    }

    public virtual void Highlight()
    {
        Debug.Log($"{this.gameObject.name} + hight light");
    }

    public virtual void HoldInteract()
    {
        Debug.Log($"{this.gameObject.name} + hold interact");
    }

    public virtual void Interact()
    {
        Debug.Log($"{this.gameObject.name} + interact");
    }

    public virtual void UnHighlight()
    {
        Debug.Log($"{this.gameObject.name} + un hight light");
    }

    public virtual void StopInteract()
    {
        Debug.Log($"{this.gameObject.name} + stop interact");
    }

    public virtual IEnumerator OpenDoor(float targetRotation, float rotationSpeed)
    {
        isFinished = false;
        // if (doorAudioSource != null) doorAudioSource.Play();

        while (Mathf.Abs(Mathf.DeltaAngle(transform.localEulerAngles.y, targetRotation)) > 0.1f)
        {
            float step = rotationSpeed * Time.deltaTime;
            float newY = Mathf.MoveTowardsAngle(transform.localEulerAngles.y, targetRotation, step);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, newY, transform.localEulerAngles.z);
            yield return null;
        }
        //Dam bao goc quay cuoi cung chinh xac
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, targetRotation, transform.localEulerAngles.z);
        isFinished = true;
    }

    public bool HasKey() { return hasKey; }

    public void SetKey(bool key) { this.hasKey = key; }

    public DoorData GetData() { return this.data; }

    public DoorState GetDoorState() { return this.currentState; }
}
