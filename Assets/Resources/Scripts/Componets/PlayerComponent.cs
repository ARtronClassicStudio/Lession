using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class PlayerComponent : MonoBehaviour,IConvertGameObjectToEntity
{
    public bool EnableAnimation;
    public float speed;
    public Animation anim;
    public Transform target;
    public Animation animFX;
    public Material skybox;
    public ParticleSystem particle;
    [Range(-3,0)]
    public float blend = -3;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new PlayerData());
        dstManager.AddComponentData(entity, new MoveData());
    }

    public void RunFX()
    {
        particle.Play();
        animFX.CrossFade("FX");
    }

    public void StopParticle()
    {
        particle.Stop();
        animFX.Stop();
    }

}


public struct PlayerData : IComponentData
{
    public float2 Move;
    public float2 MoveCamera;
    public bool enabledFX;
}

public struct MoveData : IComponentData
{
    public float Speed;
}