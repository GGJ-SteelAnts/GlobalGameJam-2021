using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public float speed = 25;
    public float runSpeed = 50;
    public float mouseSensitive = 100;
    public float jump = 30;
    public bool onGround = false;
    private bool run = false;
    private Camera playerCamera;
    private Rigidbody rigidBody;
    public GameObject playerModel;

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
        Move();
        RunSwitch();
        Animation();
    }

    private void FixedUpdate()
    {
        Jump();
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

    void Animation()
    {
        float localSpeed = 5f;
        if (Input.GetAxis("Horizontal") > 0)
        {
            playerModel.transform.rotation = Quaternion.Lerp(
                playerModel.transform.rotation,
                Quaternion.Euler(playerModel.transform.eulerAngles.x, 0f, playerModel.transform.eulerAngles.z),
                localSpeed * Time.deltaTime
            );
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            playerModel.transform.rotation = Quaternion.Lerp(
                playerModel.transform.rotation,
                Quaternion.Euler(playerModel.transform.eulerAngles.x, -180f, playerModel.transform.eulerAngles.z),
                localSpeed * Time.deltaTime
            );
        }
    }

    void Move()
    {
        rigidBody.MovePosition(
            transform.position +
            (transform.right * (run ? runSpeed : speed) * Input.GetAxis("Horizontal") * Time.deltaTime)
        );
    }

    void Jump()
    {
        if (Input.GetAxisRaw("Jump") > 0) {
            if (rigidBody.velocity.y <= 1 && onGround)
            {
                rigidBody.AddForce(transform.up * jump * 10 * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground" || other.tag == "Objects")
        {
            rigidBody.MovePosition(transform.position);
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

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground" || other.tag == "Objects")
        {
            if (rigidBody.velocity.y <= 1 && !onGround)
            {
                rigidBody.MovePosition(transform.position);
                onGround = true;
            }
        }
    }
}
