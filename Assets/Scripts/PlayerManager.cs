using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public float speed = 8;
    public float runSpeed = 20;
    public float rotateSpeed = 5;
    public float mouseSensitive = 100;
    public float jump = 5;
    private Vector3 jumpPower = new Vector3();
    public bool onGround = false;
    private bool run = false;
    private Camera playerCamera;
    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(0);
        }
        Jump();
        Move();
        RunSwitch();
    }

    void FixedUpdate()
    {
        Rotate();
    }

    void RunSwitch()
    {
        if (Input.GetAxisRaw("Run") > 0)
        {
            run = true;
        }
        else
        {
            run = false;
        }
    }

    void Move()
    {
        rigidBody.MovePosition(
            transform.position +
            (transform.forward * (run ? runSpeed : speed) * Input.GetAxis("Vertical") * Time.deltaTime) +
            (transform.right * (run ? runSpeed : speed) * Input.GetAxis("Horizontal") * Time.deltaTime) +
            jumpPower
        );
    }

    void Jump()
    {
        Debug.Log(rigidBody.velocity.y);
        if (Input.GetAxisRaw("Jump") > 0) {
            if (rigidBody.velocity.y <= 2 && onGround)
            {
                jumpPower = transform.up * jump * Time.deltaTime;
            }
        }
    }

    void Rotate()
    {
        rigidBody.freezeRotation = false;
        transform.Rotate(new Vector3(0, rotateSpeed * Input.GetAxis("Mouse X") * mouseSensitive * Time.deltaTime, 0));
        if (
            ((playerCamera.transform.localEulerAngles.x >= 270 && playerCamera.transform.localEulerAngles.x <= 360) &&
            playerCamera.transform.localEulerAngles.y == 0 &&
            playerCamera.transform.localEulerAngles.z == 0) ||
            ((playerCamera.transform.localEulerAngles.x <= 90 && playerCamera.transform.localEulerAngles.x >= 0) &&
            playerCamera.transform.localEulerAngles.y == 0 &&
            playerCamera.transform.localEulerAngles.z == 0)
        )
        {
            playerCamera.transform.Rotate(new Vector3(rotateSpeed * -Input.GetAxis("Mouse Y") * mouseSensitive * Time.deltaTime, 0, 0));
        }
        else if (
          ((playerCamera.transform.localEulerAngles.x >= 270 && playerCamera.transform.localEulerAngles.x <= 360) &&
          playerCamera.transform.localEulerAngles.y == 180 &&
          playerCamera.transform.localEulerAngles.z == 180)
      )
        {
            playerCamera.transform.Rotate(new Vector3(rotateSpeed * (-Input.GetAxis("Mouse Y") > 0 ? -Input.GetAxis("Mouse Y") : 0) * mouseSensitive * Time.deltaTime, 0, 0));
        }
        else if (
        ((playerCamera.transform.localEulerAngles.x <= 90 && playerCamera.transform.localEulerAngles.x >= 0) &&
        playerCamera.transform.localEulerAngles.y == 180 &&
        playerCamera.transform.localEulerAngles.z == 180)
      )
        {
            playerCamera.transform.Rotate(new Vector3(rotateSpeed * (-Input.GetAxis("Mouse Y") < 0 ? -Input.GetAxis("Mouse Y") : 0) * mouseSensitive * Time.deltaTime, 0, 0));
        }
        else
        {
            playerCamera.transform.Rotate(new Vector3(rotateSpeed * -Input.GetAxis("Mouse Y") * mouseSensitive * Time.deltaTime, 0, 0));
        }
        rigidBody.freezeRotation = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            jumpPower = Vector3.zero;
            onGround = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            onGround = false;
        }
    }
}
