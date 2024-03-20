using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SpawnCombatantsAuthoring : MonoBehaviour
{
    public GameObject testTargetToFollow;
    public GameObject[] combatantPrefabVariants;
    //public GameObject labelPrefab;
    public Color32[] combatantColors;
    public short[] numSpawnedCombatantsPerTeam;

    private class Baker : Baker<SpawnCombatantsAuthoring>
    {
        public override void Bake(SpawnCombatantsAuthoring authoring)
        {
            var newEntity = GetEntity(TransformUsageFlags.None);
            if (newEntity != null)
            {
                FixedList64Bytes<Int16> combatantCount = new FixedList64Bytes<Int16>();
                for(int count = 0; count< authoring.numSpawnedCombatantsPerTeam.Length; count++)
                {
                    combatantCount.Add(authoring.numSpawnedCombatantsPerTeam[count]);
                }
                FixedList128Bytes<Color32> combatantColor = new FixedList128Bytes<Color32>();
                for (int count = 0; count < authoring.combatantColors.Length; count++)
                {
                    combatantColor.Add(authoring.combatantColors[count]);
                }
              /*  FixedList512Bytes<Entity> entityPrefabs = new FixedList512Bytes<Entity>();
                for (int count = 0; count < authoring.combatantPrefabVariants.Length; count++)
                {
                    Entity spawnedEntity = GetEntity(authoring.combatantPrefabVariants[count], TransformUsageFlags.Dynamic);
                    entityPrefabs.Add(spawnedEntity);
                }*/
                Entity entityTeam1 = GetEntity(authoring.combatantPrefabVariants[0], TransformUsageFlags.Dynamic);
                Entity entityTeam2 = GetEntity(authoring.combatantPrefabVariants[1], TransformUsageFlags.Dynamic);
                Entity entityTeam3 = GetEntity(authoring.combatantPrefabVariants[2], TransformUsageFlags.Dynamic);
                Entity entityTeam4 = GetEntity(authoring.combatantPrefabVariants[3], TransformUsageFlags.Dynamic);
                var targetEntity = GetEntity(authoring.testTargetToFollow, TransformUsageFlags.Dynamic);

                AddComponent(newEntity, new SpawnCombatantsConfig
                {
                    entityTeam1 = entityTeam1,
                    entityTeam2 = entityTeam2,
                    entityTeam3 = entityTeam3,
                    entityTeam4 = entityTeam4,
                    testTargetToFollow = targetEntity,
                    numSpawnedCombatantsPerTeam = combatantCount, 
                    combatantColors = combatantColor,
                    defaultHealth = 100,
                });


               /* SetComponent(newEntity, new EntityMoveTarget
                {
                    target = targetEntity
                });*/
            }
        }
    }
}

