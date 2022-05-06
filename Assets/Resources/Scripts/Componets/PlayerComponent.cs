using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class PlayerComponent : MonoBehaviour,IConvertGameObjectToEntity
{
    public bool EnableAnimation;
    public Animation anim;
        public float speed = 10;
        public Transform target;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new PlayerData());
        dstManager.AddComponentData(entity, new MoveData
        {
            Speed = speed / 1000,        

        });
    }
}


public struct PlayerData : IComponentData
{
    public float2 Move;
    public float2 MoveCamera;
    public float Sprint;
 


}

public struct MoveData : IComponentData
{
    public float Speed;
}