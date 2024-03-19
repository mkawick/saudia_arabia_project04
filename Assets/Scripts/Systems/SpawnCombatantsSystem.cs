using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using System;
using UnityEngine;
using Unity.Collections;
using System.Linq;
using Unity.Assertions;

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

        //QuickTest(ref spawnConfig);
        int unitId = 100;
        int numTeams = spawnConfig.numSpawnedCombatantsPerTeam.Length;
        for (int team = 0; team < numTeams; team++)
        {
            int numUnits = spawnConfig.numSpawnedCombatantsPerTeam[team];
            for (int i = 0; i < numUnits; i++, unitId  += 1)
            {
                Entity newEntity = EntityManager.Instantiate(GetPrefab(team, ref spawnConfig));
                var pos = new float3(UnityEngine.Random.Range(-40, 40),
                        0,
                        UnityEngine.Random.Range(-40, 40));

                EntityManager.SetComponentData(newEntity, new LocalTransform {
                    Position = pos,
                    Scale = 1
                });

                //EntityManager.AddComponentData(newEntity, new URPMaterialPropertyBaseColor { Value = new float4(color.r, color.g, color.b, color.a) });
                var healthComp = EntityManager.GetComponentData<HealthComponent>(newEntity);
                EntityManager.AddComponentData(newEntity, new IdComponent { id = unitId });
                OnCreateUnit?.Invoke(unitId, pos, healthComp.health);
            }
        }
    }

    Entity GetPrefab(int teamIndex, ref SpawnCombatantsConfig spawnConfig)
    {
        switch(teamIndex)
        {
            case 0: return spawnConfig.entityTeam1; break;
            case 1: return spawnConfig.entityTeam2; break;
            case 2: return spawnConfig.entityTeam3; break;
            case 3: return spawnConfig.entityTeam4; break;
        }
        return Entity.Null;
    }

   /* void QuickTest(ref SpawnCombatantsConfig spawnConfig)
    {
        Entity newEntity = EntityManager.Instantiate(spawnConfig.testBlue);
        var pos = new float3(UnityEngine.Random.Range(-40, 40),
                0,
                UnityEngine.Random.Range(-40, 40));

        EntityManager.SetComponentData(newEntity, new LocalTransform
        {
            Position = pos,
            Scale = 1
        });
        int unitId = 99;

        var healthComp = EntityManager.GetComponentData<HealthComponent>(newEntity);
        EntityManager.AddComponentData(newEntity, new IdComponent { id = unitId });
        OnCreateUnit?.Invoke(unitId, pos, healthComp.health);
    }*/
}
