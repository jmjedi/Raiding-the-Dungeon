using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    //Get player Values
    private PlayerInput controls;
    private Vector2 moveInput;
    private Vector3 PlayerMovementInput;

    //Get Components
    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private Transform FeetTransform;

    [Header("PLAYER VALUES")]
    [SerializeField] private float Speed;
    [SerializeField] private float FallForce;

    [Header("SLOPE SETTINGS")]
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private float rayDist = 3.0f;
    [SerializeField] private float maxSlopeAngle = 50f;

    //Flags
    private bool canDodge = true;

    //SLOPE BASED
    public bool isGrounded;
    public bool isOnSlope;
    public float currentSlopeAngle;
    private Vector3 slopeMoveDir;

    private RaycastHit slopeHit;

    private void Awake()
    {
        //Get Controls
        controls = new PlayerInput();
    }

    private void OnEnable()
    {
        //Enable Controls
        controls.Gameplay.Enable();
        if (canDodge)
            controls.Gameplay.Dodge.started += DodgeActive;

        controls.Gameplay.Dodge.canceled += DodgeReleased;
    }

    private void OnDisable()
    {
        //Disable Controls
        controls.Gameplay.Disable();
    }

    private void Update()
    {
        //Get player input stick
        moveInput = controls.Gameplay.Move.ReadValue<Vector2>();
        PlayerMovementInput = new Vector3(moveInput.x, 0, moveInput.y);

        Vector3 moveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        //Check if we are on a slope to slow down player
        if (isOnSlope)
        {
            if (PlayerBody.velocity.y < 0) //Going up
                moveVector = Vector3.ProjectOnPlane(moveVector, slopeHit.normal).normalized * Speed;
            else //Going down
                moveVector = Vector3.ProjectOnPlane(moveVector, slopeHit.normal).normalized * Speed;
            PlayerBody.velocity = moveVector;
        }
        else
        {
            //Normal Velocity
            PlayerBody.velocity = new Vector3(moveVector.x, PlayerBody.velocity.y, moveVector.z);
        }

        CheckGroundAndSlope();
    }

    private void FixedUpdate()
    {
        Vector3 gravityForce = Physics.gravity * FallForce;
        PlayerBody.AddForce(gravityForce, ForceMode.Acceleration);
    }

    private void CheckGroundAndSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, rayDist, floorMask))
        {
            isGrounded = true;

            currentSlopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
            if (currentSlopeAngle > 0 && currentSlopeAngle <= maxSlopeAngle)
                isOnSlope = true;
            else
                isOnSlope = false;
        }
        else
        {
            isGrounded = false;
            isOnSlope = false;
            currentSlopeAngle = 0f;
        }

    }

    private void DodgeActive(InputAction.CallbackContext context)
    {
        Speed = Speed * 7;
        Invoke(nameof(resetDodge), 0.05f);
        canDodge = false;
    }
    private void DodgeReleased(InputAction.CallbackContext context)
    {
        canDodge = true;
    }

    private void resetDodge()
    {
        Speed = Speed / 7;
    }

    private void newFloorAlign()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDist, floorMask))
            transform.up = hit.normal;
        else
            transform.up = Vector3.up;
    }
}
