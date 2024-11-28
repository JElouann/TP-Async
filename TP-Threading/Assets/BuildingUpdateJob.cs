using Unity.Collections;
using Unity.Jobs;

public struct BuildingUpdateJob : IJobParallelFor
{
    public NativeArray<Building.Data> BuildingDataArray;

    public void Execute(int index)
    {
        Building.Data buildingData = BuildingDataArray[index];
        buildingData.UpdatePowerUsage();
        BuildingDataArray[index] = buildingData;
    }
}
