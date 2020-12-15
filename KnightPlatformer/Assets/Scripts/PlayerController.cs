using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum State {idle,running,jumping,hurt};
    private State currentState = State.idle;

    public float speed = 500f;
    public float jumpForce = 14f;
    public Transform groundCheckPoint;
    public LayerMask groundLayer;
    private float hurtForce = 10f;
    Collider2D isTouchingGound;
   
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
        //movement rght left
        if (currentState != State.hurt)
        {
            float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            Vector2 movement = new Vector2(deltaX, _body.velocity.y);
            _body.velocity = movement;
            _anim.SetFloat("speed", Mathf.Abs(deltaX));

            if (!Mathf.Approximately(deltaX, 0))
            {
                transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
            }
        }
        if (_body.velocity.x > 0.2)
            currentState = State.running;
    
        //jumping with a ground check
        //float radius = _circle.radius; GroundCheckRadius
        isTouchingGound = Physics2D.OverlapCircle(groundCheckPoint.position, _circle.radius, groundLayer);
        bool grounded = false;
        if (isTouchingGound != null)
        {
            grounded = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            currentState = State.jumping;
        }
      
        //animations
        _anim.SetBool("IsJumping", !grounded);
       /* _anim.SetFloat("speed", Mathf.Abs(deltaX));

        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }
       */

    }
    //connecting moving platform with player(parenting)
   private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag( "MovingPlatform"))
        {
            transform.parent = other.gameObject.transform;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (currentState == State.jumping)
            {
                Destroy(other.gameObject);
            }
            else
            {
                currentState = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x)
                    _body.velocity = new Vector2(-hurtForce, _body.velocity.y);
                else if(other.gameObject.transform.position.x < transform.position.x)
                        _body.velocity = new Vector2(hurtForce, _body.velocity.y);

            }


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent = null;
    }
    private void AnimationStates()
    {
        if (currentState == State.hurt)
        {
            
        }

    }




    //connecting player with moving platform

    /*  WHY DON'T YOU WORK??????
     *MovingPlatform platform=null;
        if (isTouchingGound!=null)
        {
            platform = isTouchingGound.GetComponent<MovingPlatform>();
        } 
        if (platform != null)
        {
            transform.parent = platform.transform;
            // Sets "newParent" as the new parent of the child GameObject.
            transform.SetParent(platform.transform);
        }
        else
        {
            transform.parent = null;
        }*/
}
