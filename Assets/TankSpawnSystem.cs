using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = System.Random;

public partial struct TankSpawnSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }

    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;

        var config = SystemAPI.GetSingleton<Config>();
        var random = new Random();

        var ecb = new EntityCommandBuffer(Allocator.Temp);
        var tanks = new NativeArray<Entity>(config.numTanks, Allocator.Temp);
        ecb.Instantiate(config.tankPrefab, tanks);
        
        var q = Quaternion.Euler(new Vector3(0, 45, 0));

        foreach (var tank in tanks)
        {
            ecb.SetComponent(tank, LocalTransform.FromPositionRotation(
                new float3(
                    random.Next(-100, 100),
                    0,
                    random.Next(-100, 100)),
                new quaternion(q.x,q.y,q.z,q.w)
            ));
        }

        ecb.Playback(state.EntityManager);
    }
}