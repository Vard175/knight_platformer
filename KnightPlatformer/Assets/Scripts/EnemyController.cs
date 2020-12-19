
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float distance;

    private bool movingRight = true;
    public LayerMask layer;

    public Transform groundDetection;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance,layer);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}


    /*
    public float speed = 9f;
   private bool groundDetected,
         wallDetected;
    public Transform groundCheck,
                     wallCheck;
    public LayerMask whatIsGround;
   private float groundCheckDistance=0.5f,
          wallCheckDistance=0.1f;
    Vector2 movement;

    private int facingDirection;
   private GameObject swordMan;
    private SpriteRenderer sprite;
    private Animator _anim;
    private Rigidbody2D _body;

    private void Start()
    {
        facingDirection = 1;
        _anim = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        swordMan = GameObject.Find("Skelet");
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {

         groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
         wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        Debug.Log(groundDetected);


        if (!groundDetected || wallDetected)
        {
            facingDirection *= -1;
            swordMan.transform.Rotate(0f, 180f, 0f);
        }
        else
        {
            movement.Set(speed * facingDirection, _body.velocity.y);
            _body.velocity = movement;
        }
    } */
    /* private void Flip()
     {
         facingDirection *= -1;
         swordMan.transform.Rotate(0f, 180f, 0f);
         Debug.Log("flip");
     }
     private void Move()    
     {

         movement.Set(speed * facingDirection, _body.velocity.y);
         _body.velocity = movement;
     }*/

