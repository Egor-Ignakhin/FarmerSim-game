using System;

namespace FarmerSim.Invnentory
{
    public interface IInventoryModel
    {
        event Action InventoryModelChanged;

        int GetItemsCount<IInventoryItem>();

        int GetMaxItemsCount();

        int GetMoneyCount();

        bool IsNotFilled();

        bool HaveItems<T>();

        void SellItem<T>(int moneyCount);

        void PushItem(IInventoryItem item);
    }
}