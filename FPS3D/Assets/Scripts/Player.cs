using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Transform cam;

    public Transform nozzle;

    public GameObject bulletPrefab;

    public float bulletSpeed = 800f;

    GameObject bullet;

    float speed = 0;
    public float walkSpeed = 0.1f;
    public float rotSpeed = 5f;
    public float bulletExpTime = 2f;
    public float jumpForce = 300f;
    public float sprintSpeed = 0.2f;

    float curRotX = 0;
    Vector3 curCamRotation;

    public float maxRotAngle = 60f;

    bool canJump = true;

    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 moveRight = transform.right * h;
        Vector3 moveForward = transform.forward * v;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (v>=0) {
                if (v!=0) {
                    speed = sprintSpeed;
                }
                else
                {
                    speed = sprintSpeed / 1.5f;
                }
            }
        }
        else
        {
            speed = walkSpeed;
        }

        Vector3 movement = (moveRight + moveForward).normalized * speed * 100f;

        movement.y = rb.velocity.y;

        rb.velocity = movement;

        curRotX += (mouseY * rotSpeed);

        curRotX = Mathf.Clamp(curRotX, -maxRotAngle, maxRotAngle);

        transform.Rotate(Vector3.up * mouseX * rotSpeed);

        curCamRotation = cam.rotation.eulerAngles;

        curCamRotation.x = -curRotX;

        cam.rotation = Quaternion.Euler(curCamRotation);

        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            rb.AddForce(Vector3.up * jumpForce);
            canJump = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            bullet = Instantiate(bulletPrefab, nozzle.position, nozzle.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed);
            Destroy(bullet, bulletExpTime);
        }

    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Ground") {
            canJump = true;
        }
    }

}
