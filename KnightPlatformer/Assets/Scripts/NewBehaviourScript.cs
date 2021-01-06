using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    Transform castPos;
    [SerializeField]
    float baseCastDist;
    const string left = "left";
    const string right = "right";
    string facingDirection;

    Rigidbody2D rb2d;
    float moveSpeed = 5f;

    Vector3 baseScale;

    void Start()
    {
        baseScale = transform.localScale;
        facingDirection = right;
        rb2d = GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        float vX = moveSpeed;
        if (facingDirection == left)
            vX = -moveSpeed;
        rb2d.velocity = new Vector2(vX, rb2d.velocity.y);
        if (isHittingWall()) //|| isNearEdge() ) 
        {
            if (facingDirection == left)
                ChangefacingDirection(right);
            else if (facingDirection == right)
                ChangefacingDirection(left);
        }
            
    }

    void ChangefacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;
        if (newDirection == left)
            newScale.x = -baseScale.x;
        else if (newDirection == right)
            newScale.x = baseScale.x;

        transform.localScale = newScale;
        facingDirection = newDirection;
    }
    bool isHittingWall()
    {
        bool value = false;

        float castDist = baseCastDist;
        if (facingDirection == left)
            castDist = -baseCastDist;
        else
            castDist = baseCastDist;

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;
        Debug.DrawLine(castPos.position, targetPos, Color.blue);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            value = true;
        }
        else value = false;
        return value;
    }
    bool isNearEdge()
    { 
          bool value = true;
         float castDist = baseCastDist;
        
        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;
        Debug.DrawLine(castPos.position, targetPos, Color.red);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            value = false;
        }
        else value = true;
        return value;
    }
}
