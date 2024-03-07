using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EntityHealthAuthoring : MonoBehaviour
{
    public int health;
    public int maxHealth;
   // public GameObject healthPrefab;

    private class Baker : Baker<EntityHealthAuthoring>
    {
        public override void Bake(EntityHealthAuthoring authoring)
        {
            HealthComponent hc;
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            //GameObject display = null;
           /* if (authoring.healthPrefab != null)
            {
               // display = Instantiate(authoring.healthPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                var labeled = display.GetComponent<LabeledCapsule>();
                labeled.health = 12;
            }*/
            if (entity != null)
            {
                AddComponent(entity, new MaxHealthComponent
                {
                    maxHealth = authoring.maxHealth
                });
                /*  AddComponentObject(entity, new HealthComponent
                  {
                      health = authoring.health,
                    //  textDisplay = display
                  });*/
                AddComponent(entity, new HealthComponent
                {
                    health = authoring.health,
                    maxHealth = authoring.maxHealth
                });
            }
            /*var label = GetComponent<LabeledCapsule>();
            if (label)
            {
                label.HealthEntity = entity;
                //var em = World.DefaultGameObjectInjectionWorld.EntityManager;
                //label.hc = em.GetComponentData <HealthComponent>(entity);
            }*/
        }
    }
}
