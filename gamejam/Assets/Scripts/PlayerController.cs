using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public static PlayerController instance;

    [Header("Input Settings")]
    [SerializeField] private string horizontalInputName = "Horizontal";
    [SerializeField] private string verticalInputName = "Vertical";
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode useKey = KeyCode.E;
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] private KeyCode meleeAttack = KeyCode.Alpha1;
    [SerializeField] private KeyCode buildAttack = KeyCode.Alpha2;
    private float movementSpeed;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 15f;
    [SerializeField] private float runSpeed = 30f;
    [SerializeField] private float runBuildUpSpeed = 15f;
    private bool isRunning;

    [SerializeField] private float slopeForce = 50f;
    [SerializeField] private float slopeForceRayLength = 10f;

    private CharacterController controller;

    [Header("Jump Settings")]
    [SerializeField] private AnimationCurve jumpFalloff = null;
    [SerializeField] private float jumpMultiplier = 4.5f;

    bool isJumping;
    bool isPaused;

    [Header("Controller Settings")]
    [SerializeField] private float gravity = 20.0f;
    public GameObject placeholderBuilding;
    public Transform placePosition;
    private AttackMode attackMode = AttackMode.Melee;
    private Vector3 playerVelocity;
    private Camera mainCamera = null;
    public event Action<AttackMode> onAttackChanged;

    private GameObject weapon;
    [SerializeField] private float attackDuration = 0.5f;
    private float timePassed;
    private bool isAttacking = false;

    public int cost;
    public bool isStoped;


    private void Awake()
    {
        instance = this;
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        weapon = GetComponentInChildren<sword>().gameObject;
        weapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStoped)
            return;

        AdjustGravity();
        HandlePause();
        if (isPaused)
        {
            return;
        }
        PlayerMovement();
        RotatePlayer();
        HandleAttackMode();
        HandleAttack();
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void LateUpdate()
    {
        UpdateCamera();
    }

    void PlayerMovement()
    {
        JumpInput2();
        float verticalInput = Input.GetAxis(verticalInputName);
        float horizontalInput = Input.GetAxis(horizontalInputName);

        Vector3 forwardMovement = Vector3.forward * verticalInput;
        Vector3 rightMovement = Vector3.right * horizontalInput;

        // Apply gravity
        playerVelocity.y -= gravity * Time.deltaTime;

        controller.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);
        if ((verticalInput != 0 || horizontalInput != 0) && OnSlope())
        {
            playerVelocity += Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime;
        }

        Vector3 adjustedMoveVector = (Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed + playerVelocity) * Time.deltaTime;
        controller.Move(adjustedMoveVector);

        SetMovementSpeed();

        //Prevent Jitter
        if (isJumping && !controller.isGrounded)
        {
            controller.slopeLimit = 90f;
        }
        else
        {
            isJumping = false;
            controller.slopeLimit = 45f;
        }
    }

    private void AdjustGravity()
    {
        if (controller.isGrounded && playerVelocity.y <= 0f)
        {
            playerVelocity.y = -2.0f;
        }
    }
    
    private void RotatePlayer()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Construct a ray from the current mouse coordinates
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(point, transform.up);
        }
    }

    void SetMovementSpeed()
    {
        if (Input.GetKey(sprintKey))
        {
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * runBuildUpSpeed);
        }
        else
        {
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, Time.deltaTime * runBuildUpSpeed);

        }
    }

    bool OnSlope()
    {
        if (isJumping)
        {
            return false;
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2 * slopeForceRayLength))
        {
            if (hit.normal != Vector3.up)
            {
                return true;
            }

        }

        return false;
    }

    void JumpInput2()
    {
        if (controller.isGrounded && Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            playerVelocity.y = Mathf.Sqrt(2f * jumpMultiplier * gravity);
        }
    }

    private void HandleAttackMode()
    {
        bool keyPressed = true;
        if (Input.GetKeyDown(meleeAttack))
        {
            attackMode = AttackMode.Build;
        }
        else if (Input.GetKeyDown(buildAttack))
        {
            attackMode = AttackMode.Build;
        }
        else
        {
            attackMode = AttackMode.Build;
            keyPressed = false;
        }

        if (keyPressed)
        {
            onAttackChanged?.Invoke(attackMode);
        }

        timePassed += Time.deltaTime;
        if (timePassed  > attackDuration)
        {
            isAttacking = false;
            weapon.SetActive(false);
        }
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (attackMode)
            {
                case AttackMode.Melee:
                    Attack();
                    break;
                case AttackMode.Weapon:
                    break;
                case AttackMode.Build:
                    PlaceBuilding();
                    break;
            }
        }
    }

    private void PlaceBuilding()
    {
        if(ResourceCounter.instance.CheckAmount() >= cost)
        {
            ResourceCounter.instance.Remove(cost);
            Instantiate(placeholderBuilding, placePosition.position, Quaternion.identity);
        }
        else
        {
            fade.instance.Notify();
        }
    }

    private void HandlePause()
    {
    }

    private void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            weapon.SetActive(true);
            timePassed = 0;
        }
    }

    #region Camera Methods
    private void UpdateCamera()
    {
        mainCamera.transform.position = new Vector3(this.transform.position.x, mainCamera.transform.position.y, this.transform.position.z);
    }
    #endregion
}

public enum AttackMode
{
    Melee,
    Weapon,
    Build
}
