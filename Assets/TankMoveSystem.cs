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
        float speed = 0.5f;
        float rotationalSpeed = 0.5f;
        float3 targetLocation = new float3(5, 0, 5);
        foreach (var transform in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<TankData>())
        {
            float3 heading = targetLocation - transform.ValueRO.Position;
            heading.y = 0;

            quaternion tragetDirection = quaternion.LookRotation(heading, math.up());
            transform.ValueRW.Rotation =
                math.slerp(transform.ValueRW.Rotation.value, tragetDirection.value, deltaTime * rotationalSpeed);
            
            transform.ValueRW.Position += 0.5f * deltaTime * math.forward(transform.ValueRW.Rotation.value);
        }
    }
}