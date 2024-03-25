using Unity.Mathematics;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UIElements;

public partial struct TargetSetterSystem : ISystem //SystemBase
{
    public void OnCreate(ref SystemState state)
    //public void OnCreate()
    {
        state.RequireForUpdate<EntityMoveTarget>();
    }

    public void OnDestroy(ref SystemState state)
    {
        //throw new NotImplementedException();
    }

    // https://www.youtube.com/watch?v=t11uB7Gl6m8

    public void OnUpdate(ref SystemState state)
    {
        var teamData = SystemAPI.GetComponentLookup<TeamUnitComponent>(isReadOnly: true);
        var targetData = SystemAPI.GetComponentLookup<EntityMoveTarget>(isReadOnly: true);


        EntityQueryBuilder queryBuilder = new EntityQueryBuilder(Allocator.Temp)
            .WithAll<TeamUnitComponent, EntityMoveTarget>();
        EntityQuery entityQuery = state.EntityManager.CreateEntityQuery(queryBuilder);
        NativeArray<Entity> listOfCombatants = entityQuery.ToEntityArray(Allocator.Temp);

        int numCombatants = listOfCombatants.Length;
        foreach (var combatant in listOfCombatants)
        {
            if (targetData.TryGetComponent(combatant, out var target))
            {
                if(target.target == Entity.Null)
                {
                    if (teamData.TryGetComponent(combatant, out var myTeam))
                    {
                        int attemptIndex = -1;
                        bool found = false;
                        do
                        {
                            attemptIndex = UnityEngine.Random.Range(0, numCombatants);
                            // todo, deal with no remaining opponents
                            var attemptedOpponent = listOfCombatants[attemptIndex];
                            if (teamData.TryGetComponent(attemptedOpponent, out var targetTeam))
                            {
                                if (targetTeam.teamId != myTeam.teamId)
                                {
                                    target.target = attemptedOpponent;
                                    state.EntityManager.SetComponentData<EntityMoveTarget>(combatant, target);
                                    //targetData.SetComponent(target);
                                    found = true;
                                }
                            }

                        } while (found == false);
                    }
                }
            }
        }

        listOfCombatants.Dispose();
        queryBuilder.Dispose();
        entityQuery.Dispose();
    }
}
