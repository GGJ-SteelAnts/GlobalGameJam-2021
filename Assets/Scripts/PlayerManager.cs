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

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground" || other.tag == "Objects")
        {
            jumpPower = Vector3.zero;
            onGround = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground" || other.tag == "Objects")
        {
            onGround = false;
        }
    }
}
