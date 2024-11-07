using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public class MoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = Entities
               .WithName("MoveSystem")
               .ForEach((ref Translation position) =>
               {
                   position.Value.z += 0.05f;
               })
               .Schedule(inputDeps);

        return jobHandle;
    }

}
