using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using UnityEngine;

public class LabeledCapsule : MonoBehaviour
{
    [SerializeField]
    TMP_Text textOverHead;
    [SerializeField]
    Canvas canvas;
    Transform textOverTransform;
    public int health = 100;
   // private EntityManager entityManager;
   // private Entity healthEntity;
   // public HealthComponent hc;

    //public Entity HealthEntity { get => healthEntity; set => healthEntity = value; }

    void Awake()
    {
        textOverTransform = textOverHead.transform;
    }
    private void Start()
    {
        textOverHead = GetComponentInChildren<TMP_Text>();
    }
    /* private IEnumerator Start() // hack to grab entity manager
     {
         //entityManager.
         entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
         yield return new WaitForSeconds(0.2f);// wait for ECS to finish loading
         //HealthEntity = entityManager.GetComponentData<HealthComponent>(this.gameObject);
     }*/
    void LateUpdate()
    {
        //int health = 10;
        /*if (healthEntity != null)
        {
            var healthComponent = entityManager.GetComponentData<HealthComponent>(healthEntity);
            health = healthComponent.health;
        }*/
        var camera = Camera.main;
        Transform camPos = camera.transform;
        textOverTransform.LookAt(camPos.forward - camPos.position);
        textOverTransform.rotation = camPos.rotation;
        textOverHead.text = health.ToString();
    }
}
