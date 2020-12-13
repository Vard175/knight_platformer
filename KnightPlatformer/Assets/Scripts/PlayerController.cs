using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 500f;
    public float jumpForce = 14f;
    public Transform groundCheckPoint;
    public LayerMask groundLayer;
    //public float froundCheckRadius;
    private bool isTouchingGound;


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

        //float radius = _circle.radius; GroundCheckRadius
        isTouchingGound = Physics2D.OverlapCircle(groundCheckPoint.position, _circle.radius,groundLayer);
        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && isTouchingGound)
        {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
          //  _anim.SetBool("IsJumping", true);
        }

        //animations
        _anim.SetBool("IsJumping", !isTouchingGound);
        _anim.SetFloat("speed", Mathf.Abs(deltaX));
        //_anim.SetBool("IsJumping", false);

        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }
 
    }
    
}
