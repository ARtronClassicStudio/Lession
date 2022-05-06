using UnityEngine;
using System.Collections.Generic;
using Unity.Entities;


public class AIEvaluateSystem : ComponentSystem
{
    private EntityQuery evaluateQuery;

    protected override void OnCreate()
    {
        evaluateQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
    }

    protected override void OnUpdate()
    {
        Entities.With(evaluateQuery).ForEach(
            (Entity entity,BehaviourManager manager) =>
            {
                float highScore = float.MinValue;

                manager.activebehaviour = null;

                foreach(MonoBehaviour behaviour in manager.behaviours)
                {
                    if(behaviour is IBehaviour ai)
                    {
                        float currentScore = ai.Evaluate();
                        if(currentScore > highScore)
                        {
                            highScore = currentScore;
                            manager.activebehaviour = ai;
                        }
                    }

                }
            });
    }


}
