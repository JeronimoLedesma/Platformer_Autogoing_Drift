using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float speed;
    [SerializeField] float jumForce;
    [SerializeField] Rigidbody rb;
    private ConstantForce gravityForce;
    private float gravity;
    private Vector3 gravityDirection;
    float baseSpeed;
    bool canJump;

    [Header("Dash")]
    [SerializeField] Transform cameraPlayer;
    [SerializeField] float rotationSpeed;
    [SerializeField] float dashForceStart;
    [SerializeField] float dashCharge;
    [SerializeField] float dashUpward;
    [SerializeField] float dashDuration;
    [SerializeField] float dashGravityMultiplier;
    [SerializeField] bool isDashing;
    [SerializeField] bool isCharging;


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
        baseSpeed = speed;
        gravityForce = GetComponent<ConstantForce>();
        gravity = -9.8f;
        gravityDirection = new Vector3(0, gravity, 0);
        gravityForce.force = gravityDirection;
        
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
        if (grounded) { 
        canJump = true;
        }

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

        
        StateOfClimb();
        if(isClimbing) ClimbingMove();

        SpeedControl();

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
            rb.linearVelocity = Vector3.zero;
            gravityDirection = gravityDirection * dashGravityMultiplier;
        }
        if (context.canceled)
        {
            isDashing = true;
            if (dashCharge < 2)
            {
                Vector3 forceToApply = cameraForward * dashForceStart + cameraUp * dashUpward;
                rb.AddForce(forceToApply, ForceMode.Impulse);
                speed = dashForceStart;
                Invoke(nameof(ResetDash), dashDuration);
            }
            if (dashCharge >= 2)
            {
                Vector3 forceToApply = cameraForward * dashForceStart * 2 + cameraUp * dashUpward;
                rb.AddForce(forceToApply, ForceMode.Impulse);
                speed = dashForceStart*2;
                Invoke(nameof(ResetDash), dashDuration);
            }
            isCharging = false;
            gravityDirection = new Vector3(0, gravity, 0);
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

    private void ResetDash()
    {
        isDashing = false;
        speed = baseSpeed;
    }

    private void SpeedControl()
    {
        Vector3 flatSpeed = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatSpeed.magnitude > speed)
        {
            Vector3 limit = flatSpeed.normalized * speed;
            rb.linearVelocity = new Vector3(limit.x, rb.linearVelocity.y, limit.z);
        }
    }

    private void StateOfClimb()
    {
        if (wallAngle < wallAngleMax && isWall)
        {
            if (!isClimbing && climbTimer > 0) StartClimb();

            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            if (climbTimer < 0) StopClimb();
        }

        else
        {
            if (isClimbing) StopClimb();
        }
    }

    private void StartClimb()
    {
        isClimbing = true;
    }

    private void ClimbingMove()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, climbSpeed, rb.linearVelocity.z);
    }

    private void StopClimb()
    {
        isClimbing = false;
    }
}
