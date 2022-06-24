
using FarmerSim.Invnentory;

using System.Collections.Generic;

using UnityEngine;

namespace FarmerSim.Player
{
    public class BoxPlayerInventoryView : MonoBehaviour, IInventoryView
    {
        private IInventoryModel playerInventoryModel;
        [SerializeField] private List<GameObject> wheatPacks = new List<GameObject>();

        public void Initialize(IInventoryModel inventoryModel)
        {
            this.playerInventoryModel = inventoryModel;
            playerInventoryModel.InventoryModelChanged += OnInventoryModelChanged;
            OnInventoryModelChanged();
        }

        private void OnInventoryModelChanged()
        {
            var packsCount = playerInventoryModel.GetItemsCountByType<WheatPackItem>();
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