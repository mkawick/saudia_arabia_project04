using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EntityMoveTargetAuthoring : MonoBehaviour
{
    public GameObject destination;

    private class Baker : Baker<EntityMoveTargetAuthoring>
    {
        public override void Bake(EntityMoveTargetAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var targetEntity = GetEntity(authoring.destination, TransformUsageFlags.Dynamic);
            if (entity != null && targetEntity != null)
            {
                AddComponent(entity, new EntityMoveTarget
                {
                    target = targetEntity
                });
            }
        }
    }
}
