using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public GameObject instrument;
    private InstrumentManager im;

    public GameObject drumcam;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        im = instrument.GetComponent<InstrumentManager>();
        // let the gameObject fall down
        gameObject.transform.position = new Vector3(0, 5, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!im.showDrum)
        {
            drumcam.SetActive(false);
            if (controller.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection = moveDirection * speed;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }
            else
            {
                moveDirection.x = Input.GetAxis("Horizontal") * speed;
                moveDirection.z = Input.GetAxis("Vertical") * speed;
                moveDirection = transform.TransformDirection(moveDirection);
            }
        } else
        {
            drumcam.SetActive(true);
        }

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}

