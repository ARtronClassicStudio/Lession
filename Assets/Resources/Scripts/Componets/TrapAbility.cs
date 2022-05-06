using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAbility : CollisionAbility
{
    public int Damage = 10;

    public new void Execute()
    {
        foreach (Collider target in collision)
        {
            Player targetHealth = target?.gameObject?.GetComponent<Player>();
            if(targetHealth != null)
            {
                targetHealth.health -= Damage;
            }
        }
    } 
}

