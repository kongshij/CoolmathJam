using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Jetpack : MonoBehaviour
{
    private const float TOTAL_FUEL = 10.0f;
    private const float REFUEL_SPEED = 1.0f;
    private const float BURN_SPEED = 1.0f;
    private const float FLYING_FORCE = 125.0f;
    private const float GROUNDED_CHECK_RAY_LENGTH = 1.0f;
    private const float GRAVITY = 200f;
    
    private Rigidbody playerRb;
    private InputSystem_Actions actions;
    [SerializeField] private float fuel = TOTAL_FUEL;
    private bool isActive = false;
    private bool isGrounded = false;


    private void Awake()
    {
        actions = new InputSystem_Actions();
    }

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Jump.performed += UseJetpack;
    }

    private void OnDisable()
    {
        actions.Player.Disable();
        actions.Player.Jump.performed -= UseJetpack;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, GROUNDED_CHECK_RAY_LENGTH);

        if (isGrounded && !isActive)
        {
            if (fuel < TOTAL_FUEL)
                fuel += Time.deltaTime * REFUEL_SPEED;
        }
        else if (isActive)
        {
            if (fuel <= 0)
            {
                isActive = false;
                return;
            }
            fuel -= Time.deltaTime * BURN_SPEED;
        }
    }

    private void FixedUpdate()
    {
        if (isActive && fuel > 0)
        {
            playerRb.AddForce(Vector3.up * FLYING_FORCE);
        }
        else if (!isActive && !isGrounded)
        {
            playerRb.AddForce(Vector3.down * GRAVITY, ForceMode.Acceleration);
        }
    }

    private void UseJetpack(InputAction.CallbackContext context)
    {
        isActive = context.ReadValueAsButton();
    }
}
