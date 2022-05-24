using Unity.Entities;
using UnityEngine;

public class PlayerMove : ComponentSystem
{
    private EntityQuery entityQuery;
    public float l;
    public Vector2 mouse;

    protected override void OnCreate()
    {
        entityQuery = GetEntityQuery(ComponentType.ReadOnly<PlayerData>(),
            ComponentType.ReadOnly<MoveData>(),
            ComponentType.ReadOnly<Transform>(),
            ComponentType.ReadOnly<PlayerComponent>());
    }

    protected override void OnUpdate()
    {
       Entities.With(entityQuery).ForEach(
           (Entity entity,PlayerComponent component,ref PlayerData player,ref MoveData move) => 
           {
               if (component)
               {
                   Quaternion o = Quaternion.Euler(0, mouse.y, 0);
                   Vector3 p = o * new Vector3(0, 0, 5) + component.transform.position;
                   Quaternion r = Quaternion.LookRotation(p - component.transform.position);
                   component.transform.rotation = Quaternion.Slerp(component.transform.rotation, r, Time.DeltaTime * 5);


                   Quaternion rotation = Quaternion.Euler(mouse.x, mouse.y, 0);
                   Vector3 position = rotation * new Vector3(0, 0, -5) + component.target.position;
                   Camera.main.transform.rotation = rotation;
                   Camera.main.transform.position = position;

                   if (player.enabledFX)
                   {
                       component.RunFX();
                   }

                   mouse.y -= player.MoveCamera.x;
                   mouse.x -= player.MoveCamera.y;
                   mouse.x = Mathf.Clamp(mouse.x, -10, 70);

                   component.skybox.SetFloat("_Position",component.blend);

                   if (player.Move.x != 0)
                   {
                       if (component.EnableAnimation)
                       {
                           component.anim.CrossFade("Walk");
                       }


                   }
                   else
                   {
                       if (component.EnableAnimation)
                       {
                           component.anim.CrossFade("Idle");
                       }

                   }

                   if (player.Move.y != 0)
                   {
                       if (component.EnableAnimation)
                       {
                           component.anim.CrossFade("Walk");
                       }
                   }

                   if (player.Move.x < -0.5f)
                   {
                       component.transform.position -= component.transform.right * component.speed * Time.DeltaTime;

                   }
                   else if (player.Move.x > 0.5f)
                   {
                       component.transform.position += component.transform.right * component.speed * Time.DeltaTime;

                   }
                   if (player.Move.y < -0.05f)
                   {
                       component.transform.position -= component.transform.forward * component.speed * Time.DeltaTime;

                   }
                   else if (player.Move.y > 0.05f)
                   {
                       component.transform.position += component.transform.forward * component.speed * Time.DeltaTime;

                   }
               }
           });
    }



}
