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
    public float verticalInput;
    public float horizontalInput;
    private PlayerUI plrUI;
    public float Gold = 0f;
    private Vector3 moveDir;

    //Get Components
    [Header("OBJECT REQUIREMENTS")]
    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private Transform FeetTransform;
    [SerializeField] private GameObject attackOBJ;
    [SerializeField] private Transform cameraObj;

    [Header("PLAYER VALUES")]
    [SerializeField] private float Speed;
    [SerializeField] private float FallForce;
    [SerializeField] private float hitKnockback;

    [Header("SLOPE SETTINGS")]
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private float rayDist = 3.0f;
    [SerializeField] private float maxSlopeAngle = 50f;

    //Flags
    private bool damage = false;
    private string knockback_side = null;

    //Cooldowns
    private float dodge_debounce;
    private float attack_debounce;

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
        cameraObj = Camera.main.transform;
    }

    private void OnEnable()
    {
        //Enable Controls
        controls.Gameplay.Enable();

        controls.Gameplay.Move.performed += i => moveInput = i.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += i => moveInput = Vector2.zero;


        //Keybinds Enabled
        controls.Gameplay.Dodge.started += DodgeActive;
        controls.Gameplay.Attack.started += AttackActive;
    }

    private void OnDisable()
    {
        //Disable Controls
        controls.Gameplay.Disable();
    }

    private void Update()
    {
        if (dodge_debounce > 0)
            dodge_debounce -= 1f * Time.deltaTime;

        if (attack_debounce > 0)
            attack_debounce -= 1f * Time.deltaTime;

        //Manage Player Inputs
        HandleMovement();
        HandleRotation();

        //Use check ground function
        CheckGroundAndSlope();
        //newFloorAlign();

        HandleAllInputs();
    }

    private void HandleAllInputs()
    {
        verticalInput = -moveInput.y;
        horizontalInput = -moveInput.x;
    }

    private void FixedUpdate()
    {
        //Player Gravity

    }

    private void CheckGroundAndSlope()
    {
        //Check if we are on the ground
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, rayDist, floorMask))
        {
            isGrounded = true;

            //Check the angle below
            currentSlopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
            if (currentSlopeAngle > 0 && currentSlopeAngle <= maxSlopeAngle) //Check if angle is greater than 0
                isOnSlope = true;
            else //Otherwise, we are on normal ground
                isOnSlope = false;
        }
        //Otherwise, we are airborne
        else
        {
            isGrounded = false;
            isOnSlope = false;
            currentSlopeAngle = 0f;
        }

    }

    public void BlinkChar()
    {
       //InvokeRepeating("StartBlink", 0, 0.4f);
    }

    private void StartBlink()
    {
        print("BLINKING");
        Renderer targetRender = GetComponent<Renderer>();
        targetRender.enabled = !targetRender;
    }

    public void ResetBlink()
    {
        Renderer targetRender = GetComponent<Renderer>();
        targetRender.enabled = true;
    }

    public void Damaged(string hitSide)
    {
        //We are hit
        damage = true;
        print(hitSide);
        knockback_side = hitSide;
        Invoke(nameof(notDamaged), 0.15f);
    }

    private void notDamaged()
    {
        //Reset hit flag
        damage = false;
    }

    private void AttackActive(InputAction.CallbackContext context)
    {
        if (attack_debounce > 0) return;
        GameObject spawnedObject;

        spawnedObject = Instantiate(attackOBJ, transform.position + -transform.forward * 2f, transform.rotation);

        attack_debounce = 0.4f;
        Destroy(spawnedObject, 0.1f);
    }

    private void DodgeActive(InputAction.CallbackContext context)
    {
        if (dodge_debounce > 0) return;
        //We have dodged
        Speed = Speed * 7;
        dodge_debounce = 1f;
        Invoke(nameof(resetDodge), 0.05f);
    }

    private void resetDodge()
    {
        //Reset player dodge state
        Speed = Speed / 7;
    }

    private void HandleMovement()
    {
        moveDir = cameraObj.forward * verticalInput;
        moveDir = moveDir + cameraObj.right * horizontalInput;
        moveDir.Normalize();
        moveDir.y = 0;
        moveDir = moveDir * Speed;

        Vector3 movementVel = moveDir;
        PlayerBody.velocity = movementVel;
    }

    private void HandleRotation()
    {
        Vector3 targetDir = Vector3.zero;

        targetDir = cameraObj.forward * verticalInput;
        targetDir = targetDir + cameraObj.right * horizontalInput;
        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
            targetDir = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        Quaternion plrRotation = Quaternion.Slerp(transform.rotation, targetRotation, 15 * Time.deltaTime);

        transform.rotation = plrRotation;
    }

    private void newFloorAlign() //UNUSED
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDist, floorMask))
            transform.up = Vector3.Slerp(transform.up, hit.normal, 0.8f);
        else
            transform.up = Vector3.up;
    }
}
