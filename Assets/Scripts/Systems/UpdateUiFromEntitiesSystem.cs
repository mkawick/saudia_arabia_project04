using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using System;
using UnityEngine;
using Unity.Collections;
using static UnityEditor.PlayerSettings;

public partial class UpdateUiFromEntitiesSystem : SystemBase
{
    public Action<int, float3, int> OnUpdateUnit; // id, pos, hps

    protected override void OnUpdate()
    {
        foreach (var entity in SystemAPI.Query<RefRO<LocalTransform>, RefRO<IdComponent>, RefRO<HealthComponent>>())
        {
            OnUpdateUnit?.Invoke(entity.Item2.ValueRO.id, entity.Item1.ValueRO.Position, entity.Item3.ValueRO.health);
        }
    }
}
