using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum State { idle, running, jumping,falling, hurt };
    private State currentState = State.idle;
 
    public float speed = 500f;
    public float jumpForce = 14f;
    public Transform groundCheckPoint;
    public LayerMask groundLayer;
    bool isGrounded;
    private float hurtForce = 5f;
  
    private Rigidbody2D _body;
    private Animator _anim;
    private CircleCollider2D _circle;


    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _circle = GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        if (currentState!=State.hurt)
             Running();
   
    Jumping();
        AnimationStates();
        _anim.SetInteger("state", (int)currentState);

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("MovingPlatform")) //connecting player with moving platform
            transform.parent = other.gameObject.transform;
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (currentState == State.falling)
                Destroy(other.gameObject);
           else
            {
                currentState = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x)
                    _body.velocity = new Vector2(-hurtForce, _body.velocity.y);
                else if (other.gameObject.transform.position.x < transform.position.x)
                    _body.velocity = new Vector2(hurtForce, _body.velocity.y);
                currentState = State.hurt;

            }
        }
    }
    private void Running()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;
      
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }
    }
    private void Jumping()
    {
        Collider2D[] groundTouch = Physics2D.OverlapCircleAll(groundCheckPoint.position, 0.2f);
        isGrounded = false;
        if (groundTouch.Length > 1)
            isGrounded = true;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            currentState = State.jumping;
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
      private void AnimationStates()
      {


        if (currentState == State.jumping)
        {
           if (_body.velocity.y < 0.1f)
            {
                currentState = State.falling;
            }
        }
        else if (currentState == State.falling)
         {
            if (isGrounded)
            {
                currentState = State.idle;
            }
        }
         else if (currentState == State.hurt)
        {
            if (Mathf.Abs(_body.velocity.x) < 0.2f)
            {
                currentState = State.idle;
            }
        }
        else
        if (Mathf.Abs(_body.velocity.x) > 0.2f)
        {
            currentState = State.running;
        }
        else
        {
            currentState = State.idle;
        }
      }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent = null;
    }

}





