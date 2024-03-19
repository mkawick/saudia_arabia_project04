using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class WorldSpaceHealthDisplaySystem : MonoBehaviour
{
    public GameObject healthDisplayUnit;
    private GameObject unitCreated;
    Dictionary<int, LabeledCapsule> capsules;
    // Start is called before the first frame update
    void Start()
    {
        capsules = new Dictionary<int, LabeledCapsule>();
        unitCreated = Instantiate(healthDisplayUnit, Vector3.zero, Quaternion.identity);
        //unitCreated.GetComponent<LabeledCapsule>().c
    }

    // Update is called once per frame
   /* void Update()
    {
        
    }*/

    private void OnEnable()
    {
        var createEntitiesSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SpawnCombatantsSystem>();
        createEntitiesSystem.OnCreateUnit += CreateUnit;

        var updateEntitiesSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<UpdateUiFromEntitiesSystem>();
        updateEntitiesSystem.OnUpdateUnit += UpdateUnit;
        
    }

    private void OnDisable()
    {
        if (World.DefaultGameObjectInjectionWorld == null) return;
        var createEntitiesSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SpawnCombatantsSystem>();
        createEntitiesSystem.OnCreateUnit -= CreateUnit;

        var updateEntitiesSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<UpdateUiFromEntitiesSystem>();
        updateEntitiesSystem.OnUpdateUnit -= UpdateUnit;
    }
    private void CreateUnit(int unitId, float3 startPosition, int health)
    {
        if (capsules.ContainsKey(unitId))
            return;

        GameObject go = Instantiate(healthDisplayUnit, Vector3.zero, Quaternion.identity);
        go.transform.position = startPosition;
        var capsule = go.GetComponent<LabeledCapsule>();
        capsule.health = health;
        capsules.Add(unitId, capsule);
    }
    private void UpdateUnit(int unitId, float3 position, int health)
    {
        if (capsules.ContainsKey(unitId) == false)
        {
            CreateUnit(unitId, position, health);
            return;
        }

        capsules[unitId].transform.position = position;
        capsules[unitId].health = health;
    }
    private void DeleteUnit(int unitId)
    {

    }
}
