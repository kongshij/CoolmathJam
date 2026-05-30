using UnityEngine;
using UnityEngine.InputSystem;

// Might wanna rename to Interactor
public class Collector : MonoBehaviour
{
    InputSystem_Actions actions;
    IInteractable currentInteractable = null;

    void Awake()
    {
        actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Interact.performed += StartInteract;
        actions.Player.Interact.canceled += StartInteract;
    }

    private void OnDisable()
    {
        actions.Player.Disable();
        actions.Player.Interact.performed -= StartInteract;
    }

    private void StartInteract(InputAction.CallbackContext context)
    {
        if (context.performed) currentInteractable?.Interact();
    }


    private void OnTriggerEnter(Collider other)
    {
        // Collision with cheese - ICheese supports different types of cheese
        other.GetComponent<ICheese>()?.Collect();

        // Collision with an interactable object (i.e., Bank, shop, etc.)
        currentInteractable = other.GetComponent<IInteractable>();
    }

    private void OnTriggerExit(Collider other)
    {
        currentInteractable = null;

        if (other.GetComponent<Bank>()) EventManager.closeBankUI?.Invoke();
    }
}
