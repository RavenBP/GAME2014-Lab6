using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Joystick joystick;
    public float joystickHorizontalSensitivity;
    public float joystickVerticalSensitivity;
    public float horizontalForce;
    public float jumpForce;
    public float maximumVelocityX;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _Move();
    }

    private void _Move()
    {
        if (isGrounded == true)
        {
            // Running
            if (joystick.Horizontal > joystickHorizontalSensitivity)
            {
                // Move to right
                rigidbody2D.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
                spriteRenderer.flipX = false;
                animator.SetInteger("AnimState", (int)PlayerMovementState.RUN);
                Debug.Log("Right");
            }
            else if (joystick.Horizontal < -joystickHorizontalSensitivity)
            {
                // Move to left
                rigidbody2D.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
                spriteRenderer.flipX = true;
                animator.SetInteger("AnimState", (int)PlayerMovementState.RUN);
                Debug.Log("Left");
            }
            else if ((joystick.Vertical < -joystickVerticalSensitivity))
            {
                animator.SetInteger("AnimState", (int)PlayerMovementState.CROUCH);
            }
            else if (isGrounded)
            {
                animator.SetInteger("AnimState", (int)PlayerMovementState.IDLE);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // tag is platforms
        if (collision.gameObject.CompareTag("Platforms"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platforms"))
        {
            isGrounded = false;
            animator.SetInteger("AnimState", (int)PlayerMovementState.JUMP);
        }
    }

    public void OnJumpButtonPressed()
    {
        if (isGrounded == true)
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce);
        }
    }
}
