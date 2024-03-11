using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private PolygonCollider2D myCollider;
    private Animator myAnimator;
    [SerializeField] private SpriteRenderer mySpriteRenderer;

    [Header("Slime ground checks")]
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private float castDistance;
    [SerializeField] private Vector2 wallCheckSize;
    [SerializeField] private float castDistanceWall;

    [Header("Slime movment")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float screenSpace;
    private float dirX = 0f;
    private bool fliped = true;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<PolygonCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        Movement((dirX * moveSpeed), myRigidbody.velocity.y);

       
        if( Input.GetButtonDown("Jump") && InGrownd() )
            Movement(myRigidbody.velocity.x, jumpForce);

        UpdateAnimation();
    }

    private void Movement( float speedX, float speedY )
    {
        Vector2 screenhedge = Camera.main.WorldToScreenPoint( transform.position );
        if( (screenhedge.x < screenSpace && speedX < 0) )
        {
            myRigidbody.velocity = new Vector2(0, speedY);
        }
        else
        {
            myRigidbody.velocity = new Vector2(speedX, speedY);
        }
    }

    private void UpdateAnimation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < transform.position.x && fliped)
            Flip();
        else if (mousePos.x > transform.position.x && !fliped)
            Flip();

        myAnimator.SetFloat("yVelocity", myRigidbody.velocity.y );
        myAnimator.SetFloat("xVelocity", myRigidbody.velocity.x );
        myAnimator.SetBool("onGround", InGrownd());
    }

    private void Flip()
    {
        fliped = !fliped;
        transform.Rotate(0, 180, 0);
    }

    private bool InGrownd()
    {
       return Physics2D.BoxCast(transform.position, groundCheckSize, 0, -transform.up, castDistance, jumpableGround);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(transform.position - transform.up * castDistance, groundCheckSize);
        //Gizmos.DrawWireCube(transform.position + transform.right * castDistanceWall, wallCheckSize);
    }

}
