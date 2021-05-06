using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EagleG : MonoBehaviour
{
    public AIPath aIPath;
  
    void Update()
    {
        if (aIPath.desiredVelocity.x >= 0.01)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aIPath.desiredVelocity.x <= -0.01)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
