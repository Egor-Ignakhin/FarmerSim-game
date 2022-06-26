
using FarmerSim.Invnentory;

using System.Collections.Generic;

using UnityEngine;

namespace FarmerSim.Player.Inventory
{
    public class BackpackPlayerInventoryView : MonoBehaviour, IPlayerInventoryView
    {
        private IInventoryModel playerInventoryModel;

        [SerializeField] private List<GameObject> wheatPacks = new List<GameObject>();

        public void Initialize(IInventoryModel inventoryModel)
        {
            playerInventoryModel = inventoryModel;
            playerInventoryModel.InventoryModelChanged += OnInventoryModelChanged;
            OnInventoryModelChanged();
        }

        private void OnInventoryModelChanged()
        {
            var packsCount = playerInventoryModel.GetItemsCount<WheatPackItem>();
            for (int i = 0; i < wheatPacks.Count; i++)
            {
                wheatPacks[i].SetActive(packsCount > i);
            }
        }

        private void OnDestroy()
        {
            playerInventoryModel.InventoryModelChanged -= OnInventoryModelChanged;
        }
    }
}