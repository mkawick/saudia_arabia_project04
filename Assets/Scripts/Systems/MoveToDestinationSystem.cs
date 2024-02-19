using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct MoveEntitySystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        //throw new NotImplementedException();
    }

    public void OnDestroy(ref SystemState state)
    {
        //throw new NotImplementedException();
    }

    public void OnUpdate(ref SystemState state)
    {
        foreach (var entity in SystemAPI.Query<RefRW<LocalTransform>, RefRO<EntityMove>, RefRO<EntityMoveDestination>>()) 
        {
            var dist = math.distance(entity.Item3.ValueRO.destination, entity.Item1.ValueRW.Position);
            if (dist < entity.Item2.ValueRO.moveSpeed)
                return;

            var diff = entity.Item3.ValueRO.destination - entity.Item1.ValueRW.Position;
            var dir = math.normalize(diff);
            dir *= entity.Item2.ValueRO.moveSpeed * SystemAPI.Time.DeltaTime;

            entity.Item1.ValueRW.Position += dir;
        }
    }
}
