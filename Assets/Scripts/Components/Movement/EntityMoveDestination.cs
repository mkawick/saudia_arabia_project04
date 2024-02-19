using Unity.Entities;
using Unity.Mathematics;

public struct EntityMoveDestination : IComponentData
{
    public float3 destination;
}
