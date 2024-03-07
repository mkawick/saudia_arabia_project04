using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnCombatantsAuthoring : MonoBehaviour
{
    public GameObject combatantPrefabs;
    public GameObject labelPrefab;
    public Material [] combatantMaterials;
    public int numSpawnedCombatantsPerTeam;

    private class Baker : Baker<SpawnCombatantsAuthoring>
    {
        public override void Bake(SpawnCombatantsAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            if (entity != null)
            {
                Entity spawnedEntity = GetEntity(authoring.combatantPrefabs, TransformUsageFlags.Dynamic);
                AddComponent(entity, new SpawnCombatantsConfig
                {
                    combatantPrefabEntity = spawnedEntity,
                    numSpawnedCombatantsPerTeam = authoring.numSpawnedCombatantsPerTeam,
                    //combatantMaterials = authoring.combatantMaterials
                });
            }
        }
    }
}

