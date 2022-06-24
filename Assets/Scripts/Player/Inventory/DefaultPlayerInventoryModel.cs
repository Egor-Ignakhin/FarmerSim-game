using FarmerSim.Invnentory;

using System;
using System.Collections.Generic;

namespace FarmerSim.Player
{
    public class DefaultPlayerInventoryModel : IInventoryModel
    {

        public event Action InventoryModelChanged;
        private readonly List<IInventoryItem> inventoryItems = new List<IInventoryItem>();

        public void PushItem(IInventoryItem item)
        {
            if (IsNotFilled())
                AddItem(item);
        }

        private void AddItem(IInventoryItem item)
        {
            inventoryItems.Add(item);

            InventoryModelChanged?.Invoke();
        }

        public int GetItemsCountByType<T>()
        {
            int count = 0;
            foreach(var item in inventoryItems)
            {
                if (item is T)
                {
                    count++;
                }
            }

            return count;
        }

        public int GetMaxItemsCount()
        {
            return 40;
        }

        public bool IsNotFilled()
        {
            return inventoryItems.Count < GetMaxItemsCount();
        }
    }
}