using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine.UIElements;

public partial struct SpawnCombatantsSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnCombatantsConfig>();
    }

    public void OnDestroy(ref SystemState state)
    {
        //throw new NotImplementedException();
    }

    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;

        SpawnCombatantsConfig spawnConfig = SystemAPI.GetSingleton<SpawnCombatantsConfig>();

        for(int i=0; i< spawnConfig.numSpawnedCombatantsPerTeam; i++)
        {
            Entity newEntity = state.EntityManager.Instantiate(spawnConfig.combatantPrefabEntity);

            state.EntityManager.SetComponentData(newEntity, new LocalTransform
            {
                Position = new float3(UnityEngine.Random.Range(-40, 40), 
                    0,
                    UnityEngine.Random.Range(-40, 40)),
                    Scale = 1
                }
            );
        }
       /* foreach (var entity in SystemAPI.Query<RefRW<LocalTransform>, RefRO<EntityMove>, RefRO<EntityMoveDestination>>())
        {
            var dist = math.distance(entity.Item3.ValueRO.destination, entity.Item1.ValueRW.Position);
            if (dist < entity.Item2.ValueRO.moveSpeed)
                return;

            var diff = entity.Item3.ValueRO.destination - entity.Item1.ValueRW.Position;
            var dir = math.normalize(diff);
            dir *= entity.Item2.ValueRO.moveSpeed * SystemAPI.Time.DeltaTime;

            entity.Item1.ValueRW.Position += dir;
        }*/
    }
}
