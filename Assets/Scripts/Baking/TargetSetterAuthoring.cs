using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class TargetSetterAuthoring : MonoBehaviour
{

    private class Baker : Baker<TargetSetterAuthoring>
    {
        public override void Bake(TargetSetterAuthoring authoring)
        {
            var thisEntity = GetEntity(TransformUsageFlags.None);
            if (thisEntity != null)
            {
              /*  AddComponent(thisEntity, new TargetSetterSystem
                {
                });*/
            }
        }
    }
}
