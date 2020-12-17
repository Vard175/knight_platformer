using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour
{
    public virtual void ReceiveDamage()
    {
        Die();
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
