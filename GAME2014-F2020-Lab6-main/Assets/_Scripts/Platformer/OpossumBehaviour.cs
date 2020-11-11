using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public enum RampDirection
{
    NONE,
    UP,
    DOWN
}

public class OpossumBehaviour : MonoBehaviour
{
    public float runForce;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidbody2D;
    public Transform lookAheadPoint;
    public Transform lookInFrontPoint;
    public bool isGroundAhead;
    public LayerMask collisionGroundLayer;
    public LayerMask collisionWallLayer;
    public float direction;
    public bool onRamp;
    public RampDirection rampDirection;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        isGroundAhead = false;
        direction = 1.0f;
        rampDirection = RampDirection.NONE;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _LookInFront();
        _LookAhead();
        _Move();
    }

    private void _LookInFront()
    {
        var wallHit = Physics2D.Linecast(transform.position, lookInFrontPoint.position, collisionWallLayer);
        if (wallHit == true)
        {
            if (wallHit.collider.CompareTag("Walls"))
            {
                if (!onRamp)
                {
                    _FlipX();
                }

                rampDirection = RampDirection.DOWN;
            }
            else
            {
                rampDirection = RampDirection.UP;
            }
        }

        Debug.DrawLine(transform.position, lookInFrontPoint.position, Color.red);
    }

    private void _LookAhead()
    {
        var groundHit = Physics2D.Linecast(transform.position, lookAheadPoint.position, collisionGroundLayer);
        if (groundHit)
        {
            if (groundHit.collider.CompareTag("Ramps"))
            {
                Debug.Log("Ramps!");
                onRamp = true;
            }

            if (groundHit.collider.CompareTag("Platforms"))
            {
                Debug.Log("Platforms!");
                onRamp = false;
            }

            isGroundAhead = true;
        }
        else
        {
            isGroundAhead = false;
        }

        Debug.DrawLine(transform.position, lookAheadPoint.position, Color.green);
    }

    private void _Move()
    {
        if (isGroundAhead == true)
        {
            rigidbody2D.AddForce(Vector2.left * runForce * Time.deltaTime * direction);

            if (onRamp == true)
            {
                if (rampDirection == RampDirection.UP)
                {
                    //rigidbody2D.AddForce(Vector2.up * runForce * Time.deltaTime);
                }
                else
                {
                    //rigidbody2D.AddForce(Vector2.down * runForce * Time.deltaTime);
                }

                transform.rotation = Quaternion.Euler(0.0f, 0.0f, -26.0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
        }
        else
        {
            _FlipX();
        }

        rigidbody2D.velocity *= 0.9f;
    }

    private void _FlipX()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
        direction *= -1.0f;
    }
}
