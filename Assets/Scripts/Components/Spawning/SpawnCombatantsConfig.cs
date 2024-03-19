using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct SpawnCombatantsConfig : IComponentData
{
    public Entity entityTeam1, entityTeam2, entityTeam3, entityTeam4;
    public FixedList128Bytes<Color32> combatantColors;
    public FixedList64Bytes<Int16> numSpawnedCombatantsPerTeam;
    public int defaultHealth;
}

