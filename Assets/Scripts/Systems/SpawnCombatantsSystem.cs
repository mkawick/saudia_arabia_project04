using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using System;
using UnityEngine;
using Unity.Collections;

public partial class SpawnCombatantsSystem : SystemBase
{
    public Action<int, float3, int> OnCreateUnit; // id, pos, hps
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnCombatantsConfig>();
    }

    public void OnDestroy(ref SystemState state)
    {
        //throw new NotImplementedException();
    }

    protected override void OnUpdate()
    {
        Enabled = false;
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        SpawnCombatantsConfig spawnConfig = SystemAPI.GetSingleton<SpawnCombatantsConfig>();

        for(int i=0, id=100; i< spawnConfig.numSpawnedCombatantsPerTeam; i++, id+=1)
        {
            Entity newEntity = EntityManager.Instantiate(spawnConfig.combatantPrefabEntity);
            var pos = new float3(UnityEngine.Random.Range(-40, 40),
                    0,
                    UnityEngine.Random.Range(-40, 40));

            EntityManager.SetComponentData(newEntity, new LocalTransform{
                    Position = pos,
                    Scale = 1
                }
            );

            //int health = spawnConfig.defaultHealth;
            EntityManager.AddComponentData(newEntity, new URPMaterialPropertyBaseColor { Value = new float4(0, 0, 1, 1) });
            var healthComp = EntityManager.GetComponentData<HealthComponent>(newEntity);
            //new HealthComponent { health = health, maxHealth = health });
            EntityManager.AddComponentData(newEntity, new IdComponent { id = id });
            OnCreateUnit?.Invoke(id, pos, healthComp.health);
        }
    }
}
