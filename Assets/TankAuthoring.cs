using Unity.Entities;
using UnityEngine;


public class TankAuthoring : MonoBehaviour
{
    class Baker : Baker<TankAuthoring>
    {
        public override void Bake(TankAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new TankData()
            {
            });
        }
    }
}

public struct TankData : IComponentData
{
}