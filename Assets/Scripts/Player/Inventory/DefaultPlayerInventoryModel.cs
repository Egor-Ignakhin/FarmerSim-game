using FarmerSim.Invnentory;

using System;
using System.Collections.Generic;

namespace FarmerSim.Player.Inventory
{
    public class DefaultPlayerInventoryModel : IPlayerInventoryModel
    {
        public event Action InventoryModelChanged;
        private readonly List<IInventoryItem> inventoryItems = new List<IInventoryItem>();
        private int moneyCount;

        public void PushItem(IInventoryItem item)
        {
            if (IsNotFilled())
            {
                inventoryItems.Add(item);

                InventoryModelChanged?.Invoke();
            }
        }

        public int GetItemsCount<T>()
        {
            int count = 0;
            foreach (var item in inventoryItems)
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

        public int GetMoneyCount()
        {
            return moneyCount;
        }

        public bool HaveItems<T>()
        {
            foreach (var item in inventoryItems)
            {
                if (item is T)
                    return true;
            }
            return false;
        }

        public void SellItem<T>(int moneyCount)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i] is T)
                {
                    inventoryItems.RemoveAt(i);
                    InventoryModelChanged?.Invoke();
                    this.moneyCount += moneyCount;
                    return;
                }
            }
        }
    }
}