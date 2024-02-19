using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct MoveToTargetSystem : ISystem
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
        var transformData = SystemAPI.GetComponentLookup<LocalTransform>(isReadOnly: true);
        foreach (var data in SystemAPI.Query<RefRW<LocalTransform>, RefRO<EntityMove>, RefRO<EntityMoveTarget>>())
        {
            Entity targetEntity = data.Item3.ValueRO.target;
            if (transformData.TryGetComponent(targetEntity, out var targetTransform))
            {
                float3 targetPos = targetTransform.Position;

                var dist = math.distance(targetPos, data.Item1.ValueRW.Position);
                if (dist < data.Item2.ValueRO.moveSpeed)
                    return;

                var diff = targetPos - data.Item1.ValueRW.Position;
                var dir = math.normalize(diff);
                dir *= data.Item2.ValueRO.moveSpeed * SystemAPI.Time.DeltaTime;

                data.Item1.ValueRW.Position += dir;
            }
        }
    }
}
