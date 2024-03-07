using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct SpawnCombatantsConfig : IComponentData
{
    public Entity combatantPrefabEntity;
    //public Material[] combatantMaterials;
    public int numSpawnedCombatantsPerTeam;
    public int defaultHealth;
}

