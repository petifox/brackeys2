using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
    private KeyCode sprintKey = KeyCode.LeftShift;

    public float speed = 1;
    public float sprintSpeed = 1;
    
    private float lookSpeed = 5;

    public GameObject placeholderBuilding;

    public Transform placePnt;

    private AttackMode attackMode = AttackMode.Melee;

    private Rigidbody rb;

    private bool sprinting = false;

    private Camera mainCamera = null;

    public event Action<AttackMode> onAttackChanged;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Update() {
        MouseClicks();
        TestNewMode();
        RotatePlayer();
        MovePlayer();
        TestSprint();
    }

    private void LateUpdate()
    {
        UpdateCamera();
    }

    private void MovePlayer() {
        float zInput = Input.GetAxis("Vertical");
        float xInput = Input.GetAxis("Horizontal");

        Vector3 newVel = new Vector3(xInput, 0, zInput);
        if (sprinting) {
            rb.AddForce(newVel * sprintSpeed, ForceMode.Force);
        }
        else {
            rb.AddForce(newVel * speed, ForceMode.Force);
        }
    }

    private void RotatePlayer() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Construct a ray from the current mouse coordinates
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            var point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(point, transform.up);
        }
    }

    private void TestNewMode() {
        bool keyPressed = true;
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            attackMode = AttackMode.Melee;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            attackMode = AttackMode.Weapon;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            attackMode = AttackMode.Build;
        }
        else
        {
            keyPressed = false;
        }

        if (keyPressed)
        {
            onAttackChanged?.Invoke(attackMode);
        }
    }

    private void MouseClicks() {
        if(Input.GetMouseButtonDown(0))
        {
            switch (attackMode)
            {
                case AttackMode.Melee:
                    break;
                case AttackMode.Weapon:
                    break;
                case AttackMode.Build:
                    PlaceBuilding();
                    break;
            }
        }
    }

    private void PlaceBuilding() {
        Instantiate(placeholderBuilding, placePnt.position, Quaternion.identity);
    }

    private void Attack() {

    }

    private void TestSprint() {
        if (Input.GetKey(sprintKey)) {
            sprinting = true;
        }
        else {
            sprinting = false;
        }
    }

    private void UpdateCamera()
    {
        mainCamera.transform.position = new Vector3(this.transform.position.x, mainCamera.transform.position.y, this.transform.position.z);
    }
}

public enum AttackMode
{
    Melee,
    Weapon,
    Build
}
