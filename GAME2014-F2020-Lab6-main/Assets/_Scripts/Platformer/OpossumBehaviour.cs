using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        isGroundAhead = false;
        direction = 1.0f;
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
        if (Physics2D.Linecast(transform.position, lookInFrontPoint.position, collisionWallLayer) == true)
        {
            _FlipX();
        }

        Debug.DrawLine(transform.position, lookInFrontPoint.position, Color.red);
    }

    private void _LookAhead()
    {
        isGroundAhead = (Physics2D.Linecast(transform.position, lookAheadPoint.position, collisionGroundLayer));

        Debug.DrawLine(transform.position, lookAheadPoint.position, Color.green);
    }

    private void _Move()
    {
        if (isGroundAhead == true)
        {
            rigidbody2D.AddForce(Vector2.left * runForce * Time.deltaTime * direction);
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
