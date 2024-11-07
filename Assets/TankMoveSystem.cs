using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public partial struct TankMoveSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        
    }

    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = Time.deltaTime;
        float3 targetLocation = new float3(5,5,5);
        foreach (var transform in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<TankData>())
        {
            transform.ValueRW.Position += 0.05f * deltaTime * (targetLocation - transform.ValueRO.Position);
        }
    }

}
