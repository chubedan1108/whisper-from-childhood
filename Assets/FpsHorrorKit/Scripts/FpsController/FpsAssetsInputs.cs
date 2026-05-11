namespace FpsHorrorKit
{
    using System.Linq;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class FpsAssetsInputs : Singleton<FpsAssetsInputs>
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Interaction Values")]
        public bool interact;
        public bool stopInteract;
        public bool useFlashlight;
        public bool useCamera;
        public bool fire;

        [Header("Item Usage Values")]
        public bool isPressed;
        public bool isSelectedItem;
        public int itemIndex;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        int currentItemIndex = -1;

        private FpsAssets _inputActions;

        protected override void Awake()
        {
            base.Awake();
            _inputActions = new FpsAssets();
            _inputActions.Player.Enable();
            _inputActions.Player.Jump.performed += context => jump = true;
            _inputActions.Player.Jump.canceled += context => jump = false;
            _inputActions.Player.Sprint.performed += context => sprint = context.ReadValueAsButton();
            _inputActions.Player.Sprint.canceled += context => sprint = context.ReadValueAsButton();
            _inputActions.Player.Fire.performed += context => fire = true;
            _inputActions.Player.Interact.performed += context => interact = true;
            _inputActions.Player.Interact.canceled += context => interact = false;
            _inputActions.Player.StopInteract.performed += context => stopInteract = true;
            _inputActions.Player.UseFlashlight.performed += context => useFlashlight = true;
            _inputActions.Player.UseCamera.performed += context => useCamera = true;
        }

        private void OnDisable()
        {
            _inputActions.Player.Disable();
        }

        public Vector2 GetInputMoveVector()
        {
            Vector2 move = _inputActions.Player.Move.ReadValue<Vector2>();
            //move = move.normalized;
            this.move = move;
            return move;
        }
        
        public Vector2 GetInputLookVector()
        {
            Vector2 look = _inputActions.Player.Look.ReadValue<Vector2>();
            this.look = look;
            return look;
        }

        //public void OnMove(InputValue value)
        //{
        //    MoveInput(value.Get<Vector2>());
          
        //}
        //public void OnLook(InputValue value)
        //{
        //    LookInput(value.Get<Vector2>());
        //}
        //public void OnJump(InputValue value)
        //{
        //    JumpInput(value.isPressed);
      
        //}
        //public void OnSprint(InputValue value)
        //{
        //    SprintInput(value.isPressed);
        //}

        //public void OnFire(InputValue value)
        //{
        //    FireInput(value.isPressed);
        //}

        //public void OnInteract(InputValue value)
        //{
        //    InteractInput(value.isPressed);
        //}

        //public void OnStopInteract(InputValue value)
        //{
        //    StopInteractInput(value.isPressed);
        //}

        //public void OnUseFlashlight(InputValue value)
        //{
        //    UseFlashlightInput(value.isPressed);
        //}

        //public void OnUseCamera(InputValue value)
        //{
        //    UseCameraInput(value.isPressed);
        //}
        public void OnKey1(InputValue value)
        {
            UseItem(1);
        }
        public void OnKey2(InputValue value)
        {
            UseItem(2);
        }
        public void OnKey3(InputValue value)
        {
            UseItem(3);
        }
        public void OnKey4(InputValue value)
        {
            UseItem(4);
        }

        

        public void UseItem(int newItemIndex)
        {
            isPressed = true;

            if (currentItemIndex != newItemIndex)
            {
                isSelectedItem = true;
                itemIndex = newItemIndex;
                currentItemIndex = newItemIndex;
            }
            else
            {
                currentItemIndex = -1;
                isSelectedItem = false;
            }
        }

        // Metotlar
        private void MoveInput(Vector2 moveInput) => move = moveInput;
        private void LookInput(Vector2 lookInput) => look = lookInput;
        private void JumpInput(bool jumpInput) => jump = jumpInput;
        private void SprintInput(bool sprintInput) => sprint = sprintInput;
        private void FireInput(bool fireInput) => fire = fireInput;
        private void InteractInput(bool interactInput) => interact = interactInput;
        private void StopInteractInput(bool stopInteractInput) => stopInteract = stopInteractInput;
        private void UseFlashlightInput(bool useFlashlightInput) => useFlashlight = useFlashlightInput;
        private void UseCameraInput(bool useCameraInput) => useCamera = useCameraInput;

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}