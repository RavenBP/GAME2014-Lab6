using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Joystick joystick;
    public float joystickSensitivity;
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
    void Update()
    {
        _Move();
    }

    private void _Move()
    {
        if (isGrounded == true)
        {
            if (joystick.Horizontal > joystickSensitivity)
            {
                // Move to right
                rigidbody2D.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
                spriteRenderer.flipX = false;
                animator.SetInteger("AnimState", 1);
                Debug.Log("Right");
            }
            else if (joystick.Horizontal < -joystickSensitivity)
            {
                // Move to left
                rigidbody2D.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
                spriteRenderer.flipX = true;
                animator.SetInteger("AnimState", 1);
                Debug.Log("Left");
            }
            else if (joystick.Vertical > joystickSensitivity)
            {
                // Jump
                rigidbody2D.AddForce(Vector2.up * jumpForce * Time.deltaTime);
            }
            else
            {
                animator.SetInteger("AnimState", 0);
            }
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
