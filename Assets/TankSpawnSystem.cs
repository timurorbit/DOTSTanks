using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

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

        foreach (var tank in tanks)
        {
            ecb.SetComponent(tank, LocalTransform.FromPosition(
                random.Next(-100, 100),
                0,
                random.Next(-100, 100)
            ));
        }
        
        ecb.Playback(state.EntityManager);
    }
}