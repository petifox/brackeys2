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

    private int attackMode = 1;

    private Rigidbody rb;

    private bool sprinting = false;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        MouseClicks();
        TestNewMode();
        RotatePlayer();
        MovePlayer();
        TestSprint();
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
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            attackMode = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            attackMode = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            attackMode = 3;
        }
    }

    private void MouseClicks() {
        if (attackMode == 2 && Input.GetMouseButtonDown(0)) {
            
        }
        if (attackMode == 3 && Input.GetMouseButtonDown(0)) {
            PlaceBuilding();
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
}
