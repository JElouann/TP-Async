using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

// tout recopié
public class BuildingManager : MonoBehaviour
{
    [SerializeField] private List<Building> buildings;
    
    private NativeArray<Building.Data> _buildingDataArray;
    private BuildingUpdateJob _job;
    private JobHandle _jobHandle;

    private void Awake()
    {
        // création du tableau de bâtiments partagés par les threads
        _buildingDataArray = new(buildings.Count, Allocator.Persistent);

        // on le remplit avec les bâtiments
        for(int i = 0; i < buildings.Count; i++)
        {
            _buildingDataArray[i] = new Building.Data(buildings[i]);
        }

        // on crée le job sans le lancer
        _job = new BuildingUpdateJob() { BuildingDataArray = _buildingDataArray };
    }

    // on lance le job à chaque frame et on attend sa complétion à chaque fin de frame
    private void Update()
    {
        // on lance le job
        _jobHandle = _job.Schedule(buildings.Count, 1);
    }

    private void LateUpdate()
    {
        // on s'assure que le job soit achevé
        _jobHandle.Complete();
    }

    private void OnDestroy()
    {
        // obligatoire car native array pas traité par Garbace Collector
        _buildingDataArray.Dispose();
    }
}   
