using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
        public Transform pos1, pos2;
        public float speed = 2.5f;
        public Transform startPos;
        Vector3 nextPos;
    private void Start()
    {
        nextPos = startPos.position;
    }
    private void FixedUpdate()
    {
        if (transform.position == pos1.position)
            nextPos = pos2.position;
        if (transform.position == pos2.position)
            nextPos = pos1.position;

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}

/*  public Vector3 finishPos=new Vector3(95f,5.49f,0f); 
  public float speed = 0.5f;
  private Vector3 startPos;
  private float trackPercent = 0;
  private int direction = 1; 
void Start()
{
  startPos = transform.position;
}
void Update()
{
  trackPercent += direction * speed * Time.deltaTime;
  float x = (finishPos.x - startPos.x) * trackPercent + startPos.x;
  float y = (finishPos.y - startPos.y) * trackPercent + startPos.y;
  transform.position = new Vector3(x, y, startPos.z);
  if ((direction == 1 && trackPercent > .9f) || (direction == -1 && trackPercent < .1f))
  {
      direction *= -1;
  }
}*/
