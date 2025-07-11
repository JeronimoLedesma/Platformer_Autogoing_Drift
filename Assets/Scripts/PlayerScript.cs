using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float speed;
    [SerializeField] float jumForce;
    [SerializeField] Rigidbody rb;
    bool canJump;

    [Header("Dash")]
    [SerializeField] Transform cameraPlayer;
    [SerializeField] float rotationSpeed;
    [SerializeField] float dashForceStart;
    [SerializeField] float dashCharge;
    [SerializeField] float dashUpward;
    [SerializeField] float dashDuration;
    bool isDashing;
    bool isCharging;

    [Header("Camara")]
    Vector3 cameraForward;
    Vector3 cameraRight;
    Vector3 cameraUp;

    [Header("Ground")]
    [SerializeField] bool grounded;
    [SerializeField] LayerMask ground;
    [SerializeField] float height;
    [SerializeField] float groundDrag;

    [Header("WallClimb")]
    [SerializeField] float climbSpeed;
    float climbTimer;
    [SerializeField] float climbMaxTime;
    bool isClimbing;

    [Header("WallDetect")]
    [SerializeField] float detectionLength;
    [SerializeField] float sphereCastRadius;
    float wallAngle;
    [SerializeField] float wallAngleMax;
    public LayerMask wall;
    RaycastHit frontWall;
    public bool isWall;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;
        isDashing = false;
        isCharging = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraForward = cameraPlayer.transform.forward;
        cameraRight = cameraPlayer.transform.right;
        cameraUp = cameraPlayer.transform.up;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        grounded = Physics.Raycast(transform.position, Vector3.down, height * 0.5f + 0.2f, ground);

        if (grounded && !isDashing)
        {
            rb.linearDamping = groundDrag;
            climbTimer = climbMaxTime;
        }
        else
        {
            rb.linearDamping = 0f;
        }

        isWall = Physics.SphereCast(transform.position, sphereCastRadius, cameraForward, out frontWall, detectionLength, wall);
        wallAngle = Vector3.Angle(cameraForward, -frontWall.normal);

        //Implementar climb
        /*StateOfClimb();
        if(isClimbing) ClimbingMove();*/

        ChargeTime();
        if (!isCharging)
        {
            rb.AddForce(cameraForward * speed, ForceMode.Force);
        }
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canJump)
            {
                rb.AddForce(0f, jumForce, 0f, ForceMode.Impulse);
                canJump = false;
            }
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isCharging = true;
        }
        if (context.canceled)
        {
            isDashing = true;
            if (dashCharge < 2)
            {
                Vector3 forceToApply = cameraForward * dashForceStart + cameraUp * dashUpward;
                rb.AddForce(forceToApply, ForceMode.Impulse);
                speed = dashForceStart;
            }
            if (dashCharge >= 2)
            {
                Vector3 forceToApply = cameraForward * dashForceStart * 2 + cameraUp * dashUpward;
                rb.AddForce(forceToApply, ForceMode.Impulse);
                speed = dashForceStart*2;
            }
            isCharging = false;
        }
    }

    void ChargeTime()
    {
        if (isCharging)
        {
            dashCharge += Time.deltaTime;
        }
        else
        {
            dashCharge = 0f;
        }
    }
}
