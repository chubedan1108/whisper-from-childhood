using FpsHorrorKit;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public CharacterController characterController;
    public FpsAssetsInputs inputTest;
    public float speed = 5f;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Start()
    {
       
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(inputTest.GetInputMoveVector().x, 0, inputTest.GetInputMoveVector().y).normalized;
        Vector3 finalMove = move * speed;

        characterController.Move(finalMove * Time.fixedDeltaTime);
        
    }
}
