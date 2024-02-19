using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EntityMoveAuthoring : MonoBehaviour
{
    public float moveSpeed;
    public float moveSpeedMultiplier;

    private class Baker : Baker<EntityMoveAuthoring>
    {
        public override void Bake(EntityMoveAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            if (entity != null)
            {
                AddComponent(entity, new EntityMove
                {
                    moveSpeed = authoring.moveSpeed,
                    moveSpeedMultiplier = authoring.moveSpeedMultiplier
                });
            }
        }
    }
}
