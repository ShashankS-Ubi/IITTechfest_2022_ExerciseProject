using UnityEngine;

public class StorageBuilding : Building
{
    private StorageBuildingUpgradeData? _currData;
    private bool _addedToInventory = false;

    public override void CreateBuilding(IBuildingUpgradeData upgradeData)
    {
        // write code to initialize values and cache data upon creation of building.

    }

    public override void UpgradeBuilding(IBuildingUpgradeData upgradeData)
    {
        // write code using the above data to modify the bulding values. bascially upgrade the datas of the building.
        // if the data has a mesh, change the mesh of the building too
    }

    public override void RepairBuilding()
    {
        // write code to replenish the health to current man=ximum health
    }

    public override void DestroyBuilding()
    {
        // write code to remove the total capacity and the available capacity when the building is destroyed.
        // and call base.destroy the building
        // reset any values that might need resetting
    }

    public override ResourceQuantity? GetUpgradeCost()
    {
        // return the current upgrade cost
        return null;
    }

    public override ResourceQuantity? GetRepairCost()
    {
        // return the current repair cost
        return null;
    }
}
