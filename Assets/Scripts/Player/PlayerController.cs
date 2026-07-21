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
    private PlayerUI plrUI;

    //Get Components
    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private Transform FeetTransform;

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

        //Keybinds Enabled
        controls.Gameplay.Dodge.started += DodgeActive;
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
            if (damage) //If we have been hit, push the player away from the hitbox
            {
                if (knockback_side == "Left") //Move right
                    PlayerBody.AddForce(transform.right * hitKnockback, ForceMode.Impulse);
                else if (knockback_side == "Right") //Move Left
                    PlayerBody.AddForce(-transform.right * hitKnockback, ForceMode.Impulse);
                else if (knockback_side == "Back") // Move back
                    PlayerBody.AddForce(transform.forward * hitKnockback, ForceMode.Impulse);
                else if (knockback_side == "Front") // Move Foward
                    PlayerBody.AddForce(-transform.forward * hitKnockback, ForceMode.Impulse);
                else if (knockback_side == "Top") //Move Up (UNUSED)
                    PlayerBody.AddForce(transform.up * hitKnockback, ForceMode.Impulse);
                else if (knockback_side == "Bottom") // Move Down (UNUSED)
                    PlayerBody.AddForce(-transform.up * hitKnockback, ForceMode.Impulse);
            }
            else
            {
                //Normal Velocity
                PlayerBody.velocity = new Vector3(moveVector.x, PlayerBody.velocity.y, moveVector.z);
            }
        }


        if (dodge_debounce > 0)
            dodge_debounce -= 1f * Time.deltaTime;       
        //Use check ground function
        CheckGroundAndSlope();
        newFloorAlign();
    }

    private void FixedUpdate()
    {
        //Player Gravity
        Vector3 gravityForce = Physics.gravity * FallForce;
        PlayerBody.AddForce(gravityForce, ForceMode.Acceleration);
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
        Invoke(nameof(notDamaged), 0.1f);
    }

    private void notDamaged()
    {
        //Reset hit flag
        damage = false;
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
