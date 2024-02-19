using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EntityMoveDestAuthoring : MonoBehaviour
{
    public Vector3 destination;

    private class Baker : Baker<EntityMoveDestAuthoring>
    {
        public override void Bake(EntityMoveDestAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            if (entity != null)
            {
                AddComponent(entity, new EntityMoveDestination
                {
                    destination = authoring.destination
                });
            }
        }
    }
}
