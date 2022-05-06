using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyHealth : MonoBehaviour,IAbilityTarget
{
    public float addHealth = 10;
    public float returnHealth = 10;
    public List<GameObject> Targets { get; set; }

    public void Execute()
    {
        foreach (GameObject target in Targets)
        {
            Player health = target.GetComponent<Player>();

            if (health != null)
            {
                
                health.health += addHealth;
                health.slider.value = health.health;
                StartCoroutine(ReturnHealth());
                GetComponent<CollisionAbility>().enabled = false;
                GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    IEnumerator ReturnHealth()
    {
        yield return new WaitForSeconds(returnHealth);
        GetComponent<CollisionAbility>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }

}
