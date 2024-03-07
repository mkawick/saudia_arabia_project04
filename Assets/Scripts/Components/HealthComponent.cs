using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct HealthComponent : IComponentData
{
    public int health;
    public int maxHealth;
}

public struct IdComponent : IComponentData
{
    public int id;
}
