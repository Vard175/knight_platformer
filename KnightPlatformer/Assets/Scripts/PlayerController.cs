using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum State { idle, running, jumping, hurt };
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
        //running
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;
        _anim.SetFloat("Speed", Mathf.Abs(deltaX));
        if (_body.velocity.x > 0)
            currentState = State.running;

        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }

        //jumping with a groundcheck
        Collider2D[] groundTouch = Physics2D.OverlapCircleAll(groundCheckPoint.position,0.2f);
        isGrounded = false;
        if (groundTouch.Length>1)
            isGrounded = true;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            currentState = State.jumping;
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
       _anim.SetBool("IsJumping", !isGrounded);

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("MovingPlatform")) //connecting player with moving platform
            transform.parent = other.gameObject.transform;
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (currentState == State.jumping)
                Destroy(other.gameObject);
           else
            {
                if (other.gameObject.transform.position.x > transform.position.x)
                    _body.velocity = new Vector2(-hurtForce, _body.velocity.y);
                else if (other.gameObject.transform.position.x < transform.position.x)
                    _body.velocity = new Vector2(hurtForce, _body.velocity.y);
                currentState = State.hurt;

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent = null;
    }
}



/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed = 500f,
                 jumpForce = 14f,
                 hurtForce = 10f;

    public Transform groundCheckPoint;
    public LayerMask groundLayer;
    bool grounded;


    private Rigidbody2D _body;
    private Animator _anim;
    private CircleCollider2D _circle;
    //CurrentState = State.Idle;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _circle = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    void Update()
    {
        if (grounded)
            CurrentState = State.Idle;

        if (Input.GetButton("Horizontal"))
            Run();
        if (Input.GetButton("Jump") && grounded)
            Jump();
    }

    //connecting moving platform with player(parenting)
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("MovingPlatform"))
        {
            transform.parent = other.gameObject.transform;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent = null;
    }

    private void GroundCheck()
    {
        Collider2D isTouchingGound = Physics2D.OverlapCircle(groundCheckPoint.position, _circle.radius, groundLayer);
        grounded = false;
        if (isTouchingGound != null)
        {
            grounded = true;
        }
        if (!grounded)
            CurrentState = State.Jump;
    }
    private void Jump()
    {

        _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private void Run()
    {
        float deltaX = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
         Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;
       

        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }
       if (grounded)
            CurrentState = State.Run;
    } 

        private State CurrentState
        {
            get { return (State)_anim.GetInteger("State"); }
            set { _anim.SetInteger("State", (int)value); }
        }

}
public enum State{
    Idle,
    Jump,
    Run,
    Hurt
}*/

