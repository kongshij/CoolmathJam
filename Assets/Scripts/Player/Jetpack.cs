using UnityEngine;
using UnityEngine.InputSystem;

public class Jetpack : MonoBehaviour
{
    private const float TOTAL_FUEL = 10.0f;
    private const float REFUEL_SPEED = 1.0f;
    private const float BURN_SPEED = 1.0f;
    
    [SerializeField] private PlayerController controller;
    private InputSystem_Actions actions;
    [SerializeField] private float fuel = TOTAL_FUEL;
    private bool isActive = false;


    void Awake()
    {
        actions = new InputSystem_Actions();
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

    void Update()
    {
        if (controller.GetGrounded() && !isActive)
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
            controller.Fly();
        }
    }

    private void UseJetpack(InputAction.CallbackContext context)
    {
        isActive = context.ReadValueAsButton();
    }
}
