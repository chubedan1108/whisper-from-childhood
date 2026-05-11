namespace FpsHorrorKit
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerInteract : Singleton<PlayerInteract>
    {
       
        [Header("Raycast Settings")]
        public bool sendRaycast;
        public float interactRange = 2.0f; // Khoảng cách tương tác

        [Header("Highlight Settings")]
        public GameObject higlightObject;
        public TextMeshProUGUI interactTextUI;
        public Image interactImageUI;

        public bool showHiglight;

        private IInteractable currentInteractable;

        private GameObject defaultHighlightObj;
        private string defaultInteractText;
        [SerializeField] private bool canDragDoor;

        private void Start()
        {
            showHiglight = true;
            sendRaycast = true;

            defaultInteractText = "Nhấn [E] để tương tác";
            interactTextUI.text = defaultInteractText;

            defaultHighlightObj = higlightObject;
        }

        void Update()
        {
            if (currentInteractable != null)
            {
                // Nếu đang giữ chuột trái và có thể kéo cửa
                if (Input.GetMouseButton(0) && canDragDoor)
                {
                    higlightObject.SetActive(false); // Ẩn Highlight khi đang tương tác giữ
                    currentInteractable.HoldInteract();
                    sendRaycast = false; // Tạm dừng bắn Raycast để tập trung tương tác
                }
                // Khi thả chuột trái
                else if (Input.GetMouseButtonUp(0))
                {
                    UnHighlight();
                    currentInteractable.UnHighlight();

                    canDragDoor = false;
                    sendRaycast = true; // Kích hoạt lại Raycast
                    currentInteractable = null;
                }
            }

            if (sendRaycast)
            {
                showHiglight = true;
                SendRaycast();
            }
            else
            {
                showHiglight = false;
            }
        }

        // Phương thức bắn Raycast để kiểm tra vật thể trước mặt
        private void SendRaycast()
        {
            // Bắn một tia Ray từ tâm màn hình Camera
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit; // Lưu thông tin vật thể bị tia Ray bắn trúng

            // Kiểm tra xem tia Ray có va chạm với vật thể nào trong khoảng cách tương tác không
            if (Physics.Raycast(ray, out hit, interactRange))
            {
                // Thử lấy component IInteractable từ vật thể bị va chạm
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                // Nếu vật thể có sở hữu interface IInteractable
                if (interactable != null)
                {
                    currentInteractable = interactable;
                    canDragDoor = true;

                    Highlight(); // Gọi phương thức làm nổi bật vật thể

                    // Nếu người chơi nhấn phím tương tác (E) và vật thể đang được làm nổi bật
                    if (FpsAssetsInputs.Instance.interact && higlightObject.activeSelf)
                    {
                        currentInteractable.Interact(); // Thực hiện hành động tương tác
                        UnHighlight();
                        FpsAssetsInputs.Instance.interact = false;
                    }
                }
                // Nếu vật thể không có interface IInteractable
                else
                {
                    UnHighlight();
                }
            }
            // Nếu không bắn trúng vật thể nào
            else
            {
                UnHighlight();
            }
        }

        private void OnDrawGizmos()
        {
            // Vẽ vòng tròn biểu thị phạm vi tương tác trong cửa sổ Scene
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactRange);
        }

        // Phương thức hiển thị Highlight
        private void Highlight()
        {
            if (currentInteractable != null)
            {
                currentInteractable.Highlight();
            }
            higlightObject.SetActive(showHiglight);
        }

        // Phương thức tắt Highlight và reset về mặc định
        private void UnHighlight()
        {
            canDragDoor = false;

            higlightObject.SetActive(false);
            higlightObject = defaultHighlightObj;
            interactTextUI.text = defaultInteractText;
        }

        // Thay đổi nội dung văn bản tương tác
        public void ChangeInteractText(string interactText)
        {
            interactTextUI.text = interactText;
            higlightObject = interactTextUI.gameObject;
        }

        // Thay đổi hình ảnh icon tương tác
        public void ChangeInteractImage(Sprite interactImage)
        {
            interactImageUI.sprite = interactImage;
            higlightObject = interactImageUI.gameObject;
        }
    }
}