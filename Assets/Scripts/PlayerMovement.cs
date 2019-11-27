using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;    

    public float walkSpeed = 40f;
    public float runSpeed = 60f;
    public float maxSlideTime = 1f;

    float horizontalMove = 0f;
    float slideTimer = 0f;

    bool run = false;
    bool jump = false;
    bool crouch = false;
    bool slide = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButton("Run") && !slide)
        {
            run = true;
            animator.SetBool("IsRunning", true);
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }
        else
        {
            run = false;
            animator.SetBool("IsRunning", false);
            horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;
        }

        if (Input.GetButtonDown("Jump") && !slide && !crouch)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (Input.GetButtonDown("Slide") && !slide && run)
        {
            slideTimer = 0f;
            animator.SetBool("IsSliding", true);

            slide = true;
        }

        if (slide)
        {
            slideTimer += Time.deltaTime;

            if (slideTimer > maxSlideTime)
            {
                slide = false;
                animator.SetBool("IsSliding", false);
            }
        }

        if(Input.GetButtonDown("Horizontal"))
        {
            animator.SetBool("IsSliding", false);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
