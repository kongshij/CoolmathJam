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
    [SerializeField] private float lookSpeed = 10f;
    [SerializeField] private float rotationSmoothTime = 0.05f;
    private float targetYaw;
    private float currentYaw;
    private float yawVelocity;

    // Might remove depending on if we have animations
    public Transform[] legs; // Assign BackLeg.L, BackLeg.R, FrontLeg_01.L, FrontLeg_01.R
    private Vector3[] defaultLegPositions;
    [SerializeField] private float legStepDistance = 0.2f;
    [SerializeField] private float legStepSpeed = 5.0f;
    private Vector3[] defaultLegLocalPositions;
    private float stepTimer = 0f;
    private bool stepPhase = false; // false = group A, true = group B
    [SerializeField] private float stepInterval = 0.2f;


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

    private void Awake()
    {
        defaultLegLocalPositions = new Vector3[legs.Length];
        for (int i = 0; i < legs.Length; i++)
        {
            defaultLegLocalPositions[i] = transform.InverseTransformPoint(legs[i].position);
        }
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

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

        currentYaw = Mathf.SmoothDampAngle(currentYaw, targetYaw, ref yawVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0f, currentYaw, 0f);
    }

    private void FixedUpdate()
    {
        Vector3 newPos = rigidBody.position + movementDir * playerSpeed * Time.fixedDeltaTime;
        rigidBody.MovePosition(newPos);
    }


    private void LateUpdate()
    {
        if (movementDir.sqrMagnitude > 0.01f)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer > stepInterval)
            {
                stepTimer = 0f;
                stepPhase = !stepPhase;
            }

            for (int i = 0; i < legs.Length; i++)
            {
                // Only move legs in the current group
                bool isGroupA = (i == 0 || i == 3);
                if ((stepPhase && isGroupA) || (!stepPhase && !isGroupA))
                {
                    Vector3 forwardStep = transform.InverseTransformDirection(movementDir.normalized) * legStepDistance;
                    Vector3 targetLocalPos = defaultLegLocalPositions[i] + forwardStep;
                    targetLocalPos.y = defaultLegLocalPositions[i].y;
                    Vector3 targetWorldPos = transform.TransformPoint(targetLocalPos);
                    legs[i].position = Vector3.Lerp(legs[i].position, targetWorldPos, Time.deltaTime * legStepSpeed);
                }
                else
                {
                    // Hold/rest position for legs not in this phase
                    Vector3 restLocalPos = defaultLegLocalPositions[i];
                    restLocalPos.y = transform.InverseTransformPoint(legs[i].position).y;
                    Vector3 restWorldPos = transform.TransformPoint(restLocalPos);
                    legs[i].position = Vector3.Lerp(legs[i].position, restWorldPos, Time.deltaTime * legStepSpeed);
                }
            }
        }
        else
        {
            for (int i = 0; i < legs.Length; i++)
            {
                Vector3 restLocalPos = defaultLegLocalPositions[i];
                restLocalPos.y = transform.InverseTransformPoint(legs[i].position).y;
                Vector3 restWorldPos = transform.TransformPoint(restLocalPos);
                legs[i].position = Vector3.Lerp(legs[i].position, restWorldPos, Time.deltaTime * legStepSpeed);
            }
        }
    }
}