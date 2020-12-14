using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 500f;
    public float jumpForce = 14f;
    public Transform groundCheckPoint;
    public LayerMask groundLayer;
    public int coins = 0;
    //public float froundCheckRadius;
    Collider2D isTouchingGound;
   
    private Rigidbody2D _body;
    private Animator _anim;
    private CircleCollider2D _circle;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        //or we can drag animator in unity and just type animator
        _circle = GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        //movement rght left
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;

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
      
        //animations
            _anim.SetBool("IsJumping", !grounded);
        _anim.SetFloat("speed", Mathf.Abs(deltaX));
        //_anim.SetBool("IsJumping", false);

        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }

    }
    //connecting moving platform with player(parenting)
   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "MovingPlatform")
        {
            transform.parent = collision.gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            coins++;
        }

    }

}
