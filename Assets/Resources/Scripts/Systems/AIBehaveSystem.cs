using UnityEngine;
using System.Collections.Generic;
using Unity.Entities;


public class AIBehaveSystem : ComponentSystem
{
    private EntityQuery behaveQuery;

    protected override void OnCreate()
    {
        behaveQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
    }

    protected override void OnUpdate()
    {
        Entities.With(behaveQuery).ForEach(
            (Entity entity, BehaviourManager manager) =>
            {
                if(manager.activebehaviour != null)
                {
                    manager.activebehaviour.Behave();
                }

            });
    }


}
