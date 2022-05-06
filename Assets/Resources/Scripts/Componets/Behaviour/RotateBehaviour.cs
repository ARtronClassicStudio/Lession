using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehaviour : MonoBehaviour, IBehaviour
{

    public Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void Behave()
    {
        transform.Rotate(Vector3.up, 10);
    }

    public float Evaluate()
    {
        if (player == null) return 0;
        return 1 / (gameObject.transform.position - player.transform.position).magnitude;
    }
}
