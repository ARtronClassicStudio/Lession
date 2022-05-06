using System;
using System.Linq;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CollisionSystem : ComponentSystem
{
    private EntityQuery query;
    private Collider[] results = new Collider[50];
    

    protected override void OnCreate()
    {
        query = GetEntityQuery(
            ComponentType.ReadOnly<ActorColliderData>(),
            ComponentType.ReadOnly<Transform>()); 
    }

    protected override void OnUpdate()
    {
        World dstManager = World.DefaultGameObjectInjectionWorld;
        Entities.With(query).ForEach(
            (Entity entity, CollisionAbility collisionAbility, ref ActorColliderData colliderData) => 
            {
                GameObject gameObject = collisionAbility.gameObject;
                float3 position = gameObject.transform.position;
                Quaternion rotation = gameObject.transform.rotation;
                List<Collider> resultsList = new List<Collider>();
                collisionAbility.collision?.Clear();

                int size = 0;

                switch (colliderData.ColliderType)
                {
                    case ColliderType.Sphere:
                        size = Physics.OverlapSphereNonAlloc(colliderData.SphereCenter + position, colliderData.SphereRadius, results);
                        break;
                    case ColliderType.Capsule:
                        float3 center = ((colliderData.CapsuleStart + position) + (colliderData.CapsuleEnd + position)) / 2f;
                        float3 point1 = colliderData.CapsuleStart + position;
                        float3 point2 = colliderData.CapsuleEnd + position;
                        point1 = (float3)(rotation * (point1 - center)) + center;
                        point2 = (float3)(rotation * (point2 - center)) + center;
                        size = Physics.OverlapCapsuleNonAlloc(point1,point2,colliderData.CapsuleRadius,results);
                        break;
                    case ColliderType.Box:
                        size = Physics.OverlapBoxNonAlloc(colliderData.BoxCenter + position, colliderData.BoxHalfExtents, results, colliderData.BoxOrientation * rotation);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if(size > 0)
                {
                    foreach (Collider result in results)
                    {
                        collisionAbility?.collision?.Add(result);
                    }
                  
                    collisionAbility.Execute();
                }

            });
            
    }

}
