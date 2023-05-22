using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    private float xRotation;

    [Header("General")]
    [SerializeField] private Rigidbody rb3d;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float playerHeight;
    [SerializeField] private float airMoveDivider;
    [SerializeField] private float groundCheckHeight;
    [Space]
    [SerializeField] private LayerMask groundLayer;

    [Header("Camera")]
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;
    [Space]
    [SerializeField] private Camera cam;


    // Refrences
    private void Start()
    {
        // Locking cursor
        GameManager.instance.CursorVisiblity(false);

        // Call jump method when space is pressed
        GameManager.instance.inputManager.Player.Jump.performed += ctx => PlayerJump();
    }

    private void FixedUpdate()
    {
        // Methods
        PlayerMovement();
        CameraMovement();
    }


    // Player movement
    private void PlayerMovement()
    {
        Vector2 moveInputRaw = GameManager.instance.inputManager.Player.Movement.ReadValue<Vector2>().normalized * moveSpeed * Time.fixedDeltaTime; // Input value

        // Moving player
        if (GameManager.instance.inputManager.Player.Movement.ReadValue<Vector2>() != Vector2.zero)
        {
            if (isGrounded())
            {
                Vector2 moveInput = moveInputRaw;
                rb3d.velocity = transform.TransformDirection(new Vector3(moveInput.x, rb3d.velocity.y, moveInput.y));
            }
            else
            {
                Vector2 moveInput = moveInputRaw / airMoveDivider; // Reducing movement speed in air
                rb3d.velocity = transform.TransformDirection(new Vector3(moveInput.x, rb3d.velocity.y, moveInput.y));
            }
        }

        // Changing Drag
        if (isGrounded())
            rb3d.drag = groundDrag;
        else
            rb3d.drag = 0;
    }


    // Player jumping
    private void PlayerJump()
    {
        if (isGrounded())
        {
            rb3d.velocity = new Vector3(rb3d.velocity.x, 0, rb3d.velocity.z);
            rb3d.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }


    // Checking is grounded
    private bool isGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + groundCheckHeight, groundLayer))
            return true;
        else
            return false;
    }


    // Camera movement
    private void CameraMovement()
    {
        Vector2 mouseInput = GameManager.instance.inputManager.Player.Camera.ReadValue<Vector2>(); // Mouse Delta value

        // Rotate Camera
        xRotation -= (mouseInput.y * Time.fixedDeltaTime) * sensitivityY;
        xRotation = Mathf.Clamp(xRotation, -80, 80);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // Rotate Player
        transform.Rotate(Vector3.up * (mouseInput.x * Time.fixedDeltaTime) * sensitivityX);
    }
}