using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;



public class CollisionAbility : MonoBehaviour,  IConvertGameObjectToEntity,IAbility
{

    public Collider col;
    public List<MonoBehaviour> collisionAction = new List<MonoBehaviour>();
    [HideInInspector]
    public List<Collider> collision;

    public List<IAbilityTarget> collisionActionAbilities  = new List<IAbilityTarget>();

    private void Start()
    {
        foreach (MonoBehaviour action in collisionAction)
        {
            if(action is IAbilityTarget ability)
            {
                collisionActionAbilities.Add(ability);
            }
            else
            {
                Debug.LogError("Collision action must derive from IAbility.");
            }
        }
    }

    public void Execute()
    {
        foreach (IAbilityTarget action in collisionActionAbilities)
        {
            action.Targets = new List<GameObject>();
            collision.ForEach(c => 
            {
               if(c != null) action.Targets.Add(c.gameObject);
            });
            action.Execute();
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = gameObject.transform.position;
        switch (col)
        {
            case SphereCollider sphere:
                sphere.ToWorldSpaceSphere(out float3 sphereCenter, out float sphereRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Sphere,
                    SphereCenter = sphereCenter - position,
                    SphereRadius = sphereRadius,
                    initialTakeoff = true
                });
                break;
            case CapsuleCollider capsule:
                capsule.ToWorldSpaceCapsule(out float3 capsuleStart, out float3 capsuleEnd, out float capsuleRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Capsule,
                    CapsuleStart = capsuleStart - position,
                    CapsuleEnd = capsuleEnd - position,
                    CapsuleRadius = capsuleRadius,
                    initialTakeoff = true
                });
                break;
            case BoxCollider box:
                box.ToWorldSpaceBox(out float3 boxCenter, out float3 boxHalfExtents, out quaternion boxOrientation);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Box,
                    BoxCenter = boxCenter - position,
                    BoxHalfExtents = boxHalfExtents,
                    BoxOrientation = boxOrientation,
                    initialTakeoff = true

                });
                break;
        }
        col.enabled = false;
    }

}

public struct ActorColliderData : IComponentData
{
    public ColliderType ColliderType;
    public float3 SphereCenter;
    public float SphereRadius;
    public float3 CapsuleStart;
    public float3 CapsuleEnd;
    public float CapsuleRadius;
    public float3 BoxCenter;
    public float3 BoxHalfExtents;
    public quaternion BoxOrientation;
    public bool initialTakeoff;



}

public enum ColliderType
{
    Sphere = 0,
    Capsule = 1,
    Box = 2,
}


