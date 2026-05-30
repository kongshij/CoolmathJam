using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 5.0f;
    private Rigidbody rigidBody;
    private InputAction moveAction;
    private Vector3 movementDir;
    private bool isGrounded;
    private InputAction lookAction;
    [SerializeField] private float lookSpeed = 5f;
    [SerializeField] private float rotationSmoothTime = 0.05f;
    private float targetYaw;
    private float currentYaw;
    private float yawVelocity;
    [SerializeField] Animator animator;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rigidBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        lookAction = InputSystem.actions.FindAction("Look");
        currentYaw = transform.eulerAngles.y;
        targetYaw = currentYaw;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.25f);

        Vector2 inputDir = Vector2.zero;
        if (moveAction != null)
            inputDir = moveAction.ReadValue<Vector2>();

        movementDir = (transform.right * inputDir.x + transform.forward * inputDir.y).normalized;

        if (lookAction != null)
        {
            Vector2 lookDelta = lookAction.ReadValue<Vector2>();
            if (lookDelta.sqrMagnitude > 0.0001f)
            {
                targetYaw += lookDelta.x * lookSpeed * Time.deltaTime;
            }
        }

        currentYaw = Mathf.SmoothDampAngle(currentYaw, targetYaw, ref yawVelocity, rotationSmoothTime); // Thanks mathf for smoothdampangle
        transform.rotation = Quaternion.Euler(0f, currentYaw, 0f);
    }

    private void FixedUpdate()
    {
        /* Forgot move position is for kinematic bodies
         * feel like it'd be cool if unity seperated different types of bodies in different components instead of just rigidbody
         * but anywayss im just going to set velocity directly
        */
        rigidBody.linearVelocity = movementDir * playerSpeed;
        animator.SetBool("isWalking", movementDir.magnitude >= 0.1f);
    }

    public void Fly() => rigidBody.AddForce(Vector3.up * 10);

    public bool GetGrounded() => isGrounded;
}