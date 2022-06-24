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

        int GetMoneyCount();

        bool HaveItems<T>();

        void SellItem<T>(int moneyCount);
    }
}