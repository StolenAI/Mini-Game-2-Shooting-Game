using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 PlayerVelocity;
    public float speed = 5f;

    private bool IsGrounded;
    public float gravity = -9.8f;

    public float JumpHeight = 0.5f;

    bool Crouching = false;
    float CrouchTimer = 1;
    bool lerpCrouch = false;
    bool Sprinting = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            CrouchTimer += Time.deltaTime;
            float p = CrouchTimer / 1;
            p *= p;
            if (Crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                CrouchTimer = 0f;
            }
        }
    }

    //receive the inputs for our InputManger.cs and apply them to our character controller
    public void ProcessMove (Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;  
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        PlayerVelocity.y += gravity * Time.deltaTime;

        if (IsGrounded && PlayerVelocity.y < 0)
            PlayerVelocity.y = -2f;
        controller.Move (PlayerVelocity * Time.deltaTime);         
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            PlayerVelocity.y = Mathf.Sqrt(JumpHeight * -3.0f * gravity);
        }
    }

    public void Sprint()
    {
        Sprinting = !Sprinting;
        if (Sprinting)
            speed = 8;
        else
            speed = 4;
    }

    public void Crouch()
    {
        Crouching = !Crouching;
        CrouchTimer = 0;
        lerpCrouch = true;
    }
}
