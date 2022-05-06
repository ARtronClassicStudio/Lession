using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ApplyDamage : MonoBehaviour,IAbilityTarget
{
    
   public float damage = 10;
   public List<GameObject> Targets { get; set; }

    public void Execute()
    {
        foreach (GameObject target in Targets)
        {
            Player health = target.GetComponent<Player>();

            if (health != null)
            {
                health.health -= damage * Time.deltaTime;
                health.slider.value = health.health;


            }
        }
    }
}
