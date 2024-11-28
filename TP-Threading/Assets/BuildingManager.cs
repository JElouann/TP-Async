using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

// tout recopi�
public class BuildingManager : MonoBehaviour
{
    [SerializeField] private List<Building> buildings;
    
    private NativeArray<Building.Data> _buildingDataArray;
    private BuildingUpdateJob _job;
    private JobHandle _jobHandle;

    private void Awake()
    {
        // cr�ation du tableau de b�timents partag�s par les threads
        _buildingDataArray = new(buildings.Count, Allocator.Persistent);

        // on le remplit avec les b�timents
        for(int i = 0; i < buildings.Count; i++)
        {
            _buildingDataArray[i] = new Building.Data(buildings[i]);
        }

        // on cr�e le job sans le lancer
        _job = new BuildingUpdateJob() { BuildingDataArray = _buildingDataArray };
    }

    // on lance le job � chaque frame et on attend sa compl�tion � chaque fin de frame
    private void Update()
    {
        // on lance le job
        _jobHandle = _job.Schedule(buildings.Count, 1);
    }

    private void LateUpdate()
    {
        // on s'assure que le job soit achev�
        _jobHandle.Complete();
    }

    private void OnDestroy()
    {
        // obligatoire car native array pas trait� par Garbace Collector
        _buildingDataArray.Dispose();
    }
}   
