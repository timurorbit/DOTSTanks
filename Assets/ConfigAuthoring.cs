using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ConfigAuthoring : MonoBehaviour
{
    public GameObject tankPrefab;
    public int numTanks = 500;

    class Baker : Baker<ConfigAuthoring>
    {
        public override void Bake(ConfigAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Config
            {
                tankPrefab = GetEntity(authoring.tankPrefab, TransformUsageFlags.Dynamic),
                numTanks = authoring.numTanks
            });
        }
    }
}

public struct Config : IComponentData
{
    public Entity tankPrefab;
    public int numTanks;
}