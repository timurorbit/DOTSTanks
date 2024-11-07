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
        float3 targetLocation = new float3(5, 0, 5);
        foreach (var transform in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<TankData>())
        {
            float3 heading = targetLocation - transform.ValueRO.Position;
            heading.y = 0;

            
            transform.ValueRW.Rotation.value = quaternion.LookRotation(heading, math.up()).value;
            
            transform.ValueRW.Position += 0.5f * deltaTime * math.forward(transform.ValueRW.Rotation.value);
        }
    }
}