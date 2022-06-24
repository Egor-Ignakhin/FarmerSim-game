using System;

namespace FarmerSim.Invnentory
{
    public interface IInventoryModel
    {
        event Action InventoryModelChanged;

        void PushItem(IInventoryItem item);

        int GetItemsCountByType<IInventoryItem>();

        int GetMaxItemsCount();

        bool IsNotFilled();
    }
}