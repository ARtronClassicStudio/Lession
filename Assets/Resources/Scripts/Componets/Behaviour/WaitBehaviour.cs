using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitBehaviour : MonoBehaviour,IBehaviour
{

    public Player player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    public void Behave()
    {
      

        if((transform.position - player.transform.position).magnitude < 5)
        {
            agent.SetDestination(player.transform.position);
        }
        
    }

    public float Evaluate()
    {
        return 0.5f;
    }


}
