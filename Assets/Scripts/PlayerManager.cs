using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed = 8;
    public float runSpeed = 20;
    public float mouseSensitive = 100;
    public float jump = 5;
    public float health = 100;
    public float actualHealth;
    private float[] actualPowerTimes = new float[] { 0f, 0f, 0f };
    public bool onGround = true;
    private bool run = false;
    private Animator playerAnimator;
    private Rigidbody rigidBody;
    private PowerCubeManager powerCubeManager;

    private bool startEating = false;

    private float saveSpeed;
    private float saveRunSpeed;
    private float saveJump;
    private Vector3 saveSize;
    private Vector3 savePosition;
    private Vector3 saveCameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        actualHealth = health;
        playerAnimator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody>();

        saveSpeed = speed;
        saveRunSpeed = runSpeed;
        saveJump = jump;
        saveSize = playerAnimator.transform.localScale;
        savePosition = playerAnimator.transform.localPosition;
        saveCameraPosition = Camera.main.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(0);
        }
        if (actualHealth <= 0)
        {
            playerAnimator.Play("Die");
        }
        DeactivePowerCube();
        Move();
        RunSwitch();
        Animation();

        if (startEating)
        {
            float stepSmaller = 0.8f * Time.deltaTime;
            powerCubeManager.transform.localScale = Vector3.Slerp(powerCubeManager.transform.localScale, new Vector3(0.01f, 0.01f, 0.01f), stepSmaller);
        }
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

    public void Die()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void Animation()
    {
        float localSpeed = 5f;
        if (Input.GetAxis("Horizontal") > 0 && onGround)
        {
            playerAnimator.SetBool("Walk", true);
            playerAnimator.transform.rotation = Quaternion.Lerp(
                playerAnimator.transform.rotation,
                Quaternion.Euler(playerAnimator.transform.eulerAngles.x, -90f, playerAnimator.transform.eulerAngles.z),
                localSpeed * Time.deltaTime
            );
        }
        else if (Input.GetAxis("Horizontal") < 0 && onGround)
        {
            playerAnimator.SetBool("Walk", true);
            playerAnimator.transform.rotation = Quaternion.Lerp(
                playerAnimator.transform.rotation,
                Quaternion.Euler(playerAnimator.transform.eulerAngles.x, 90f, playerAnimator.transform.eulerAngles.z),
                localSpeed * Time.deltaTime
            );
        }
        else
        {
            playerAnimator.SetBool("Walk", false);
        }
    }

    void Move()
    {
        if (onGround) {
            rigidBody.MovePosition(
                transform.position +
                (transform.right * (run ? runSpeed : speed) * Input.GetAxis("Horizontal") * Time.deltaTime)
            );
        }
    }

    void Jump()
    {
        if (Input.GetAxisRaw("Jump") > 0)
        {
            if (rigidBody.velocity.y <= 1 && onGround)
            {
                rigidBody.AddForce(
                    (transform.right * (run ? runSpeed : speed) * 5 * Input.GetAxis("Horizontal") * Time.deltaTime) + 
                    (transform.up * jump * 10 * Time.deltaTime), 
                    ForceMode.VelocityChange
                );
            }
        }
    }

    public void ActivePowerCube(float power, float powerTime, PowerCubeManager.PowerType powerType)
    {
        if (actualPowerTimes.Length <= (powerType.GetHashCode()) || actualPowerTimes[powerType.GetHashCode() - 1] < Time.time) {
            actualPowerTimes[powerType.GetHashCode() - 1] = Time.time + powerTime;
            if (powerType == PowerCubeManager.PowerType.Bigger)
            {
                saveSize = playerAnimator.transform.localScale;
                savePosition = playerAnimator.transform.localPosition;
                saveCameraPosition = Camera.main.transform.localPosition;

                playerAnimator.transform.localScale = new Vector3(
                    playerAnimator.transform.localScale.x + power,
                    playerAnimator.transform.localScale.y + power,
                    playerAnimator.transform.localScale.z + power
                );
                playerAnimator.transform.localPosition = new Vector3(
                    playerAnimator.transform.localPosition.x,
                    playerAnimator.transform.localPosition.y + power * 2,
                    playerAnimator.transform.localPosition.z
                );
                Camera.main.transform.localPosition = new Vector3(
                    Camera.main.transform.localPosition.x,
                    Camera.main.transform.localPosition.y,
                    Camera.main.transform.localPosition.z - power * 2
                );

            }
            else if (powerType == PowerCubeManager.PowerType.Faster)
            {
                saveSpeed = speed;
                saveRunSpeed = runSpeed;

                speed += power;
                runSpeed += power;
            }
            else if (powerType == PowerCubeManager.PowerType.Jumper)
            {
                saveJump = jump;

                jump += power;
            }
        }
    }

    private void DeactivePowerCube()
    {
        if (actualPowerTimes[0] != 0f && actualPowerTimes[0] < Time.time)
        {
            playerAnimator.transform.localScale = saveSize;
            playerAnimator.transform.localPosition = savePosition;
            Camera.main.transform.localPosition = saveCameraPosition;
            actualPowerTimes[0] = 0f;
        }
        if (actualPowerTimes[1] != 0f && actualPowerTimes[1] < Time.time)
        {
            speed = saveSpeed;
            runSpeed = saveRunSpeed;
            actualPowerTimes[1] = 0f;
        }
        if (actualPowerTimes[2] != 0f && actualPowerTimes[2] < Time.time)
        {
            jump = saveJump;
            actualPowerTimes[2] = 0f;
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
            if (rigidBody.velocity.y <= 1 && !onGround) {
                rigidBody.MovePosition(transform.position);
                onGround = true;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Objects")
        {
            Vector3 hit = collision.contacts[0].normal;
            if (hit.x != 0) {
                powerCubeManager = collision.gameObject.GetComponent<PowerCubeManager>();
                playerAnimator.SetTrigger("Eat");
            }
        }
    }

    public void damage(float damage, bool instaKill)
    {
        if (instaKill)
        {
            actualHealth = 0;
        } 
        else
        {
            actualHealth -= damage;
        }
    }

    public void StartEatPowerCube()
    {
        startEating = true;
    }

    public void EndEatPowerCube()
    {
        startEating = false;
        ActivePowerCube(powerCubeManager.powerUnit, powerCubeManager.powerTime, powerCubeManager.powerType);
        Destroy(powerCubeManager.gameObject);
    }
}
