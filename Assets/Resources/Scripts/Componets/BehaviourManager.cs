using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class BehaviourManager : MonoBehaviour,IConvertGameObjectToEntity
{
    public List<MonoBehaviour> behaviours;
    public IBehaviour activebehaviour;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<AIAgent>(entity);
    }
}

public struct AIAgent : IComponentData
{

}
