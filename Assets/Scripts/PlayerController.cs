using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput controls;
    private Vector2 moveInput;
    private Vector3 PlayerMovementInput;

    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private Transform FeetTransform;

    [Header("PLAYER VALUES")]
    [SerializeField] private float Speed;
    [SerializeField] private float FallForce;

    [Header("SLOPE SETTINGS")]
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private float rayDist = 2.0f;
    [SerializeField] private float alignmentSpeed = 10f;

    private bool canDodge = true;

    private void Awake()
    {
        controls = new PlayerInput();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        if (canDodge)
            controls.Gameplay.Dodge.started += DodgeActive;

        controls.Gameplay.Dodge.canceled += DodgeReleased;
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();

    }

    private void Update()
    {
        moveInput = controls.Gameplay.Move.ReadValue<Vector2>();
        PlayerMovementInput = new Vector3(moveInput.x, 0, moveInput.y);

        Vector3 moveVector = transform.TransformDirection(PlayerMovementInput) * Speed;

        PlayerBody.velocity = new Vector3(moveVector.x, PlayerBody.velocity.y, moveVector.z);
    }

    private void FixedUpdate()
    {
        Vector3 gravityForce = Physics.gravity * FallForce;
        PlayerBody.AddForce(gravityForce, ForceMode.Acceleration);
        ///AlignWithFloor();
    }

    private void DodgeActive(InputAction.CallbackContext context)
    {
        Speed = Speed * 7.5f;
        Invoke(nameof(resetDodge), 0.076f);
        canDodge = false;
    }
    private void DodgeReleased(InputAction.CallbackContext context)
    {
        canDodge = true;
    }

    private void resetDodge()
    {
        Speed = Speed / 7.5f;
    }
    private void AlignWithFloor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayDist, floorMask))
        {
            Vector3 forwardProjection = Vector3.ProjectOnPlane(transform.forward, hit.normal);
            Quaternion targetRotation = Quaternion.LookRotation(forwardProjection, hit.normal);
            
            PlayerBody.MoveRotation(Quaternion.Slerp(PlayerBody.rotation, targetRotation, alignmentSpeed * Time.fixedDeltaTime));
        }
        else
        {
            Quaternion uprightRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, Vector3.up), Vector3.up);
            PlayerBody.MoveRotation(Quaternion.Slerp(PlayerBody.rotation, uprightRotation, alignmentSpeed * Time.fixedDeltaTime));
        }
    }
}
