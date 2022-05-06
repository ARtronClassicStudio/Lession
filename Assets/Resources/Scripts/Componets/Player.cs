using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Slider slider;
    public float health = 100;
    public AnimationClip deadAnimation;
    public AnimationClip fly;
    public Animation blood;
    public Animation playerAnimation;
    private Rigidbody rig;
    private bool act = true;
    private Vector3 c = Vector3.zero;
    private PlayerComponent component;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        component = GetComponent<PlayerComponent>();
    }

    private void OnCollisionExit(Collision collision)
    {
        if(component)
        component.EnableAnimation = false;
        playerAnimation.CrossFade(fly.name);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(component)
        component.EnableAnimation = true;
       
    }

    private void Update()
    {

        if (act)
        {

            

            if (rig.velocity.y < -50)
            {
                slider.value = 0;
                health = 0;

            }

            health = Mathf.Clamp(health, 0, 100);
            if (health < 1)
            {
                Destroy(GetComponent<PlayerComponent>());
              
                playerAnimation.CrossFade(deadAnimation.name);

                blood.Play();
                act = false;
                
            }
        }
        else
        {


            Quaternion rotation = Quaternion.Euler(c.x, c.y, 0);
            Vector3 position = rotation * new Vector3(0, 0, -5) + transform.position;
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, position, 0.1f);
            Camera.main.transform.rotation = rotation;
            c.x += Time.deltaTime;
            c.y -= Time.deltaTime;
        }

    }

}
