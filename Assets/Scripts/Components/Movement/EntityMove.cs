using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct EntityMove : IComponentData
{
    public float moveSpeed;
    public float moveSpeedMultiplier;
    public float3 destination; 
}
